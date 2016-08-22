using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using XCode.DataAccessLayer;

namespace XTool.BLL
{
    public class OrderBLL
    {
        public static void DatabaseInit(string dbFilePath)
        {
            DAL.AddConnStr("ConnName", string.Format("Data Source={0};", dbFilePath), null, "Sqlite");

            new OrderBatchStatus() { StatusID = 1, StatusName = "草稿" }.Save();
            new OrderBatchStatus() { StatusID = 2, StatusName = "部分放行" }.Save();
            new OrderBatchStatus() { StatusID = 3, StatusName = "完成" }.Save();
        }

        public static void AddOrderItem(Dictionary<string, List<OrderItem>> dicItem, Dictionary<string, string> dicAttach)
        {
            foreach (var item in dicItem)
            {
                OrderBatch orderBatch;
                List<OrderBatch> lstOrderBatch = OrderBatch.FindAllByHawbCode(item.Key);
                if (lstOrderBatch.Count < 1)
                {
                    OrderBatch batch = new OrderBatch();
                    batch.CreateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    batch.HawbCode = item.Key;
                    batch.StatusID = 1;
                    batch.Save();

                    OrderBatchLog log = new OrderBatchLog();
                    log.BatchID = batch.BatchID;
                    log.CreateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    log.LogConent = "新建批次";
                    log.Save();

                    orderBatch = batch;
                }
                else
                {
                    orderBatch = lstOrderBatch[0];
                }
                item.Value.ForEach(p =>
                {
                    p.HawbCode = p.HawbCode.ToUpper();
                    List<OrderItem> lstOrderItem = FindOrderItem(p.HawbCode);

                    OrderItem orderItem = null;
                    if (lstOrderItem.Count > 0)
                    {
                        foreach (var itemOrderItem in lstOrderItem)
                        {
                            if (itemOrderItem.BatchID == orderBatch.BatchID)
                            {
                                orderItem = itemOrderItem;
                                continue;
                            }
                        }
                    }

                    OrderItemAttach attach = new OrderItemAttach();
                    attach.Content = dicAttach[p.HawbCode];
                    if (orderItem != null)
                    {
                        if (!string.Equals(orderItem.OrderStatus, p.OrderStatus))
                        {
                            lstOrderItem[0].OrderStatus = p.OrderStatus;
                            AddOrderItemLog(orderItem, string.Format("状态由{0}修改为{1}", lstOrderItem[0].OrderStatus, p.OrderStatus));
                            lstOrderItem[0].ScanningTime = null;
                            lstOrderItem[0].Save();
                        }

                        attach.OrderID = orderItem.ID;
                    }
                    else
                    {
                        p.BatchID = orderBatch.BatchID;
                        p.ScanningTime = "";
                        p.Save();
                        attach.OrderID = p.ID;
                    }
                    attach.Save();
                });
            }
        }

        public static List<OrderItem> FindOrderItem(string hawbCode)
        {
            return OrderItem.FindAllByHawbCode(hawbCode);
        }

        public static string ScannedOrderItem(OrderItem item)
        {
            item.ScanningTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            item.Save();
            AddOrderItemLog(item, "分运单号扫描");
            return UpdateOrderBatchStatus(item.BatchID);
        }

        public static void AddOrderItemLog(OrderItem item, string logContent)
        {
            OrderItemLog log = new OrderItemLog();
            log.OrderID = item.ID;
            log.CreateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            log.LogConent = logContent;
            log.Save();
        }

        public static DataTable GetOrderBatchList()
        {
            DAL dal = DAL.Create("ConnName");
            DataSet ds = dal.Session.Query(@"
SELECT
	a.BatchID,
	a.StatusID,
	a.HawbCode,
	a.CreateTime,
	b.StatusName,
	(
		SELECT
			count(1)
		FROM
			OrderItem t1
		WHERE
			t1.BatchID = a.BatchID
		AND length(t1.ScanningTime) > 0
	) AS ScanningCount,
	(
		SELECT
			count(1)
		FROM
			OrderItem t1
		WHERE
			t1.BatchID = a.BatchID
	) AS OrderCount
FROM
	OrderBatch a
LEFT JOIN OrderBatchStatus b ON a.StatusID = b.StatusID
ORDER BY
	b.StatusID ASC,
	a.CreateTime DESC");
            if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
            {
                return ds.Tables[0];
            }
            return null;
        }

        public static DataTable GetOrderItemList(int batchID)
        {
            DAL dal = DAL.Create("ConnName");
            DataSet ds = dal.Session.Query(string.Format(@"
SELECT
	a.ID,
	a.BatchID,
	a.HawbCode,
	a.ScanningTime,
	a.OrderStatus,
	b.Content
FROM
	OrderItem a
LEFT JOIN OrderItemAttach b ON a.ID = b.OrderID
WHERE BatchID={0}
ORDER BY
	a.ID ASC", batchID));

            if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
            {
                return ds.Tables[0];
            }
            return null;
        }
        public static void DeleteOrderBatch(int batchID)
        {
            List<OrderItem> lstItem = OrderItem.FindAllByBatchID(batchID);
            lstItem.ForEach(p =>
            {
                OrderItemLog.Delete("OrderID=" + p.ID);
                OrderItemAttach.Delete("OrderID=" + p.ID);
            });
            OrderItem.Delete("BatchID=" + batchID);
            OrderBatch.Delete("BatchID=" + batchID);
            OrderBatchLog.Delete("BatchID=" + batchID);
        }

        public static void DeleteOrderItem(int id)
        {
            OrderItem item = OrderItem.FindByID(id);
            long BatchID = item.BatchID;
            OrderBatchLog log = new OrderBatchLog();
            log.BatchID = BatchID;
            log.CreateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            log.LogConent = "删除单号:" + item.HawbCode;
            log.Save();
            item.Delete();

            OrderItemLog.Delete("OrderID=" + id);
            OrderItemAttach.Delete("OrderID=" + id);

            UpdateOrderBatchStatus(BatchID);
        }

        private static string UpdateOrderBatchStatus(long batchID)
        {
            List<OrderItem> lstItem = OrderItem.FindAllByBatchID(batchID);
            List<long> lstStatus = new List<long>();
            lstItem.ForEach(p =>
            {
                if (string.IsNullOrWhiteSpace(p.ScanningTime))
                {
                    lstStatus.Add(1);
                }
                else
                {
                    lstStatus.Add(3);
                }
            });
            string result = string.Format("{0}/{1}", lstStatus.Where(p => p == 1).Count(), lstStatus.Count());
            long statusID = 1;
            if (lstStatus.Count > 0)
            {
                if (lstStatus.Distinct().Count() > 1)
                {
                    statusID = 2;
                }
                else
                {
                    statusID = lstStatus[0];
                }
            }
            OrderBatch orderBatch = OrderBatch.FindByBatchID((int)batchID);
            if (!string.Equals(orderBatch.StatusID, statusID))
            {
                string oldStatusName = GetStatusName(orderBatch.StatusID);
                orderBatch.StatusID = statusID;
                orderBatch.Save();

                OrderBatchLog log = new OrderBatchLog();
                log.BatchID = batchID;
                log.CreateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                log.LogConent = string.Format("状态由{0}修改为{1}", oldStatusName, GetStatusName(statusID));
                log.Save();
            }
            return result;
        }

        private static string GetStatusName(long statusID)
        {
            OrderBatchStatus orderBatchStatuss = OrderBatchStatus.FindByStatusID(statusID);
            if (orderBatchStatuss != null)
            {
                return orderBatchStatuss.StatusName;
            }
            return string.Empty;
        }


        public static List<OrderBatchLog> GetOrderBatchLogList(long statusID)
        {
            return OrderBatchLog.FindAllByBatchID(statusID);
        }

        public static List<OrderItemLog> GetOrderItemLogList(long orderID)
        {
            return OrderItemLog.FindAllByOrderID(orderID);
        }
    }
}
