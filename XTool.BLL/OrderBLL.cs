using System;
using System.Collections.Generic;
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
        }

        public static void AddOrderItem(List<OrderItem> lstItem)
        {
            OrderBatch batch = new OrderBatch();
            batch.create_time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            batch.order_count = lstItem.Count;
            batch.scanned_count = 0;
            int order_id = batch.Save();

            lstItem.ForEach(p =>
            {
                p.hawb_code = p.hawb_code.ToUpper();
                p.order_id = batch.id;
                p.Save();
            });
        }

        public static List<OrderItem> FindOrderItem(string hawb_code)
        {
            return OrderItem.FindAllByhawb_code(hawb_code);
        }

        public static string ScannedOrderItem(OrderItem item)
        {
            OrderBatch batch = OrderBatch.FindByid((int)item.order_id);
            if (string.IsNullOrWhiteSpace(item.scanning_time))
            {
                item.scanning_time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                item.Save();
                batch.scanned_count++;
                batch.Save();
            }
            return string.Format("{0}/{1}", batch.scanned_count, batch.order_count);
        }

        public static List<OrderBatch> GetOrderBatchList()
        {
            return OrderBatch.FindAll();
        }

        public static List<OrderItem> GetOrderItemList(int order_id)
        {
            return OrderItem.FindAll("order_id=" + order_id, "id", null, 0, 0);
        }
        public static void DeleteOrderBatch(int order_id)
        {
            OrderItem.Delete("order_id=" + order_id);
            OrderBatch batch = OrderBatch.FindByid(order_id);
            batch.Delete();
        }

        public static void DeleteOrderItem(int id)
        {
            OrderItem item = OrderItem.FindByid(id);
            OrderBatch batch = OrderBatch.FindByid((int)item.order_id);
            batch.order_count--;
            if (!string.IsNullOrWhiteSpace(item.scanning_time))
            {
                batch.scanned_count--;
            }
            batch.Save();
            item.Delete();
        }
    }
}
