using System.Data;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.Collections.Generic;
using NPOI.XSSF.UserModel;
using System;
using NPOI.SS.Util;
using System.Text;
using System.Windows.Forms;
using XTool.Constant;

namespace XTool.File
{
    public partial class ExcelHelper
    {
        #region 对外提供的方法
        /// <summary>
        /// 获取Excel工作表
        /// </summary>
        /// <param name="strPath"></param>
        /// <returns></returns>
        public static DataTable GetExcelSheetStructure(string strPath)
        {
            ExcelIdeType objExcelIdeType = GetExtFileType(strPath);
            DataTable dtResult = new DataTable();
            if (objExcelIdeType.Equals(ExcelIdeType.Excel2003))
            {
                dtResult = GetExcelSheetStructureForXLS(strPath);
            }
            else if (objExcelIdeType.Equals(ExcelIdeType.Excel2007))
            {
                dtResult = GetExcelSheetStructureForXLSX(strPath);
            }
            return dtResult;
        }

        /// <summary>
        /// 获取Excel的列文件
        /// </summary>
        /// <param name="strPath">Excel文件所在目录</param>
        /// <param name="iSheetIndex">表序列号</param>
        /// <returns></returns>
        public static DataTable GetExcelColumnsName(string strPath, int iSheetIndex)
        {
            ExcelIdeType objExcelIdeType = GetExtFileType(strPath);
            DataTable dtResult = new DataTable();
            if (objExcelIdeType.Equals(ExcelIdeType.Excel2003))
            {
                dtResult = GetExcelColumnsNameForXLS(strPath, iSheetIndex);
            }
            else if (objExcelIdeType.Equals(ExcelIdeType.Excel2007))
            {
                dtResult = GetExcelColumnsNameForXLSX(strPath, iSheetIndex);
            }
            else if (objExcelIdeType.Equals(ExcelIdeType.ExcelCSV))
            {
                dtResult = GetExcelColumnsNameForCSV(strPath, iSheetIndex);
            }
            return dtResult;
        }

        /// <summary>
        /// 将Excel文件中的数据读出到DataTable中(xls)
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static DataTable ExcelToTable(string file, int iIndex)
        {
            ExcelIdeType objExcelIdeType = GetExtFileType(file);
            DataTable dtResult = new DataTable();
            if (objExcelIdeType.Equals(ExcelIdeType.Excel2003))
            {
                dtResult = ExcelToTableForXLS(file, iIndex);
            }
            else if (objExcelIdeType.Equals(ExcelIdeType.Excel2007))
            {
                dtResult = ExcelToTableForXLSX(file, iIndex);
            }
            else if (objExcelIdeType.Equals(ExcelIdeType.ExcelCSV))
            {
                dtResult = ExcelToTableForCSV(file, iIndex);
            }
            return dtResult;
        }
        #endregion

        #region Excel2003

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strPath"></param>
        /// <returns></returns>
        private static DataTable GetExcelSheetStructureForXLS(string strPath)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Index", typeof(int));
            dt.Columns.Add("TableName", typeof(string));

            HSSFWorkbook workbook;
            using (FileStream file = new FileStream(strPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                workbook = new HSSFWorkbook(file);
            }
            int iSheetCount = workbook.NumberOfSheets;
            for (int i = 0; i < iSheetCount; i++)
            {
                dt.NewRow();
                dt.Rows.Add(i, workbook.GetSheetName(i));
            }
            return dt;
        }

        /// <summary>
        /// 获取Excel的列文件
        /// </summary>
        /// <param name="strPath">Excel文件所在目录</param>
        /// <param name="iSheetIndex">表序列号</param>
        /// <returns></returns>
        private static DataTable GetExcelColumnsNameForXLS(string strPath, int iSheetIndex)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Index", typeof(int));
            dt.Columns.Add("Column", typeof(string));
            dt.Columns.Add("中文", typeof(string));
            dt.Columns.Add("英文", typeof(string));

            HSSFWorkbook workbook;
            using (FileStream file = new FileStream(strPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                workbook = new HSSFWorkbook(file);
            }
            ISheet sheet = workbook.GetSheetAt(iSheetIndex);
            var rows = sheet.GetRowEnumerator();
            rows.MoveNext();
            IRow row = (HSSFRow)rows.Current;
            for (int i = 0; i < row.LastCellNum; i++)
            {
                ICell cell = row.GetCell(i);
                dt.NewRow();
                string strCellValue = cell.StringCellValue;
                string strCN = strCellValue;
                string strEN = strCellValue;
                if (strCellValue.Contains("/"))
                {
                    string[] astrValue = strCellValue.Split('/');
                    if (astrValue != null && astrValue.Length > 0)
                    {
                        strCN = astrValue[0];
                        strEN = astrValue[1];
                    }
                }
                dt.Rows.Add(i.ToString(), strCellValue, strCN, strEN);
            }
            return dt;
        }

        /// <summary>
        /// 将Excel文件中的数据读出到DataTable中(xls)
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        private static DataTable ExcelToTableForXLS(string file, int iIndex)
        {
            DataTable dt = new DataTable();
            using (FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                HSSFWorkbook hssfworkbook = new HSSFWorkbook(fs);
                ISheet sheet = hssfworkbook.GetSheetAt(iIndex);
                dt.TableName = hssfworkbook.GetSheetName(iIndex);
                //表头
                IRow header = sheet.GetRow(sheet.FirstRowNum);
                List<int> columns = new List<int>();
                string strColumnName = string.Empty;
                for (int i = 0; i < header.LastCellNum; i++)
                {
                    object obj = GetValueTypeForXLS(header.GetCell(i) as HSSFCell);
                    if (obj == null || obj.ToString() == string.Empty)
                    {
                        strColumnName = "列" + i.ToString();
                    }
                    else
                    {
                        strColumnName = obj.ToString().Trim();
                    }
                    if (dt.Columns.Contains(strColumnName))
                    {
                        throw new Exception(string.Format("表格中列名【{0}】重复", strColumnName));
                    }
                    dt.Columns.Add(new DataColumn(strColumnName));
                    columns.Add(i);
                }

                int iStart = sheet.FirstRowNum;
                int iLast = sheet.LastRowNum;
                iStart += 1;
                //数据
                for (int i = iStart; i <= iLast; i++)
                {
                    DataRow dr = dt.NewRow();
                    bool hasValue = false;
                    foreach (int j in columns)
                    {
                        if (sheet.GetRow(i) == null)
                        {
                            continue;
                        }
                        dr[j] = GetValueTypeForXLS(sheet.GetRow(i).GetCell(j) as HSSFCell);
                        if (dr[j] != null && dr[j].ToString() != string.Empty)
                        {
                            hasValue = true;
                        }
                    }
                    if (hasValue)
                    {
                        dt.Rows.Add(dr);
                    }
                }
            }
            return dt;
        }

        /// <summary>
        /// 将DataTable数据导出到Excel文件中(xls)
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="file"></param>
        public static void TableToExcelForXLS(DataTable dt, string file)
        {
            HSSFWorkbook hssfworkbook = new HSSFWorkbook();
            ISheet sheet = hssfworkbook.CreateSheet("Sheet1");

            //表头
            IRow row = sheet.CreateRow(0);
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                ICell cell = row.CreateCell(i);
                cell.SetCellValue(dt.Columns[i].ColumnName);
            }

            //数据
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                IRow row1 = sheet.CreateRow(i + 1);
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    ICell cell = row1.CreateCell(j);
                    cell.SetCellValue(dt.Rows[i][j].ToString());
                }
            }

            //转为字节数组
            MemoryStream stream = new MemoryStream();
            hssfworkbook.Write(stream);
            var buf = stream.ToArray();

            //保存为Excel文件
            using (FileStream fs = new FileStream(file, FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
            {
                fs.Write(buf, 0, buf.Length);
                fs.Flush();
            }
        }

        public static void TableToExcelForXLS(DataTable dt, string file, Dictionary<int, DictionaryData> listDictionary)
        {
            HSSFWorkbook hssfworkbook = new HSSFWorkbook();
            ISheet sheet = hssfworkbook.CreateSheet("Sheet1");

            //表头
            IRow row = sheet.CreateRow(0);
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                ICell cell = row.CreateCell(i);
                cell.SetCellValue(dt.Columns[i].ColumnName);

                try
                {
                    if (listDictionary != null && listDictionary.Count > 0 && listDictionary.ContainsKey(i) && !string.IsNullOrWhiteSpace(listDictionary[i].SheetName))
                    {
                        ISheet sheet2 = hssfworkbook.CreateSheet(listDictionary[i].SheetName);
                        if (listDictionary[i].ListDictionaryModel != null && listDictionary[i].ListDictionaryModel.Count > 0)
                        {
                            IRow row0 = sheet2.CreateRow(0);
                            row0.CreateCell(0).SetCellValue("代码");
                            row0.CreateCell(1).SetCellValue("名称");
                            for (int index = 0; index < listDictionary[i].ListDictionaryModel.Count; index++)
                            {
                                IRow row1 = sheet2.CreateRow(index + 1);
                                row1.CreateCell(0).SetCellValue(listDictionary[i].ListDictionaryModel[index].Code);
                                row1.CreateCell(1).SetCellValue(listDictionary[i].ListDictionaryModel[index].Name);
                            }

                            HSSFName range = (HSSFName)hssfworkbook.CreateName();
                            range.RefersToFormula = listDictionary[i].SheetName + "!$A$2:$A$" + listDictionary[i].ListDictionaryModel.Count + 1;
                            range.NameName = listDictionary[i].SheetName;

                            CellRangeAddressList regions = new CellRangeAddressList(1, 65535, i, i);
                            DVConstraint constraint = DVConstraint.CreateFormulaListConstraint(listDictionary[i].SheetName);
                            HSSFDataValidation dataValidate = new HSSFDataValidation(regions, constraint);
                            sheet.AddValidationData(dataValidate);
                        }
                    }
                }
                catch { }
            }

            //数据
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                IRow row1 = sheet.CreateRow(i + 1);
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    ICell cell = row1.CreateCell(j);
                    cell.SetCellValue(dt.Rows[i][j].ToString());
                }
            }

            //转为字节数组
            MemoryStream stream = new MemoryStream();
            hssfworkbook.Write(stream);
            var buf = stream.ToArray();

            //保存为Excel文件
            using (FileStream fs = new FileStream(file, FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
            {
                fs.Write(buf, 0, buf.Length);
                fs.Flush();
            }
        }

        /// <summary>
        /// 获取单元格类型(xls)
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        private static object GetValueTypeForXLS(HSSFCell cell)
        {
            if (cell == null)
                return null;
            switch (cell.CellType)
            {
                case CellType.BLANK: //BLANK:
                    return null;
                case CellType.BOOLEAN: //BOOLEAN:
                    return cell.BooleanCellValue;
                case CellType.NUMERIC: //NUMERIC:
                    if (NPOI.SS.UserModel.DateUtil.IsCellDateFormatted(cell))
                    {
                        return cell.DateCellValue;
                    }
                    else
                    {
                        return cell.NumericCellValue;
                    }
                case CellType.STRING: //STRING:
                    return cell.StringCellValue;
                case CellType.ERROR: //ERROR:
                    return cell.ErrorCellValue;
                case CellType.FORMULA: //FORMULA:
                    return cell.NumericCellValue;
                default:
                    return "=" + cell.CellFormula;
            }
        }
        #endregion

        #region Excel2007
        /// <summary>
        /// 将Excel文件中的数据读出到DataTable中(xlsx)
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        private static DataTable ExcelToTableForXLSX(string file, int iIndex)
        {
            DataTable dt = new DataTable();
            using (FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                XSSFWorkbook xssfworkbook = new XSSFWorkbook(fs);
                ISheet sheet = xssfworkbook.GetSheetAt(iIndex);
                dt.TableName = xssfworkbook.GetSheetName(iIndex);

                //表头
                IRow header = sheet.GetRow(sheet.FirstRowNum);
                List<int> columns = new List<int>();
                string strColumnName = string.Empty;

                for (int i = 0; i < header.LastCellNum; i++)
                {
                    object obj = GetValueTypeForXLSX(header.GetCell(i) as XSSFCell);
                    if (obj == null || obj.ToString() == string.Empty)
                    {
                        strColumnName = "列" + i.ToString();
                    }
                    else
                    {
                        strColumnName = obj.ToString().Trim();
                    }
                    if (dt.Columns.Contains(strColumnName))
                    {
                        throw new Exception(string.Format("表格中列名【{0}】重复", strColumnName));
                    }
                    dt.Columns.Add(new DataColumn(strColumnName));
                    columns.Add(i);
                }

                int iStart = sheet.FirstRowNum;
                int iLast = sheet.LastRowNum;
                iStart += 1;
                //数据
                for (int i = iStart; i <= iLast; i++)
                {
                    DataRow dr = dt.NewRow();
                    bool hasValue = false;
                    foreach (int j in columns)
                    {
                        dr[j] = GetValueTypeForXLSX(sheet.GetRow(i).GetCell(j) as XSSFCell);
                        if (dr[j] != null && dr[j].ToString() != string.Empty)
                        {
                            hasValue = true;
                        }
                    }
                    if (hasValue)
                    {
                        dt.Rows.Add(dr);
                    }
                }
            }
            return dt;
        }

        /// <summary>
        /// 将DataTable数据导出到Excel文件中(xlsx)
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="file"></param>
        private static void TableToExcelForXLSX(DataTable dt, string file)
        {
            XSSFWorkbook xssfworkbook = new XSSFWorkbook();
            ISheet sheet = xssfworkbook.CreateSheet("Sheet1");

            //表头
            IRow row = sheet.CreateRow(0);
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                ICell cell = row.CreateCell(i);
                cell.SetCellValue(dt.Columns[i].ColumnName);
            }

            //数据
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                IRow row1 = sheet.CreateRow(i + 1);
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    ICell cell = row1.CreateCell(j);
                    cell.SetCellValue(dt.Rows[i][j].ToString());
                }
            }

            //转为字节数组
            MemoryStream stream = new MemoryStream();
            xssfworkbook.Write(stream);
            var buf = stream.ToArray();

            //保存为Excel文件
            using (FileStream fs = new FileStream(file, FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
            {
                fs.Write(buf, 0, buf.Length);
                fs.Flush();
            }
        }

        /// <summary>
        /// 获取单元格类型(xlsx)
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        private static object GetValueTypeForXLSX(XSSFCell cell)
        {
            if (cell == null)
                return null;
            switch (cell.CellType)
            {
                case CellType.BLANK: //BLANK:
                    return null;
                case CellType.BOOLEAN: //BOOLEAN:
                    return cell.BooleanCellValue;
                case CellType.NUMERIC: //NUMERIC:
                    return cell.NumericCellValue;
                case CellType.STRING: //STRING:
                    return cell.StringCellValue;
                case CellType.ERROR: //ERROR:
                    return cell.ErrorCellValue;
                case CellType.FORMULA: //FORMULA:
                    switch (cell.CachedFormulaResultType)
                    {
                        case CellType.BOOLEAN:
                            return cell.BooleanCellValue;
                        case CellType.ERROR:
                            return cell.ErrorCellValue;
                        case CellType.NUMERIC:
                            return cell.NumericCellValue;
                        case CellType.STRING:
                            return cell.StringCellValue;
                        default:
                            return string.Empty;
                    }
                default:
                    return string.Empty;
            }
        }


        /// <summary>
        /// exact excel data into DataTable
        /// </summary>
        /// <param name="excel">excel file name</param>
        /// <param name="index">sheet index </param>
        /// <param name="header"> the first row in excel whether belongs the columns</param>
        /// <returns>DataTable</returns>
        private static DataTable ExcelToDataTableForXLSX(string excel, int index, bool header)
        {
            DataTable dt = new DataTable();
            XSSFWorkbook workbook;
            using (FileStream file = new FileStream(excel, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                workbook = new XSSFWorkbook(file);
            }
            ISheet sheet = workbook.GetSheetAt(index);
            var rows = sheet.GetRowEnumerator();
            rows.MoveNext();
            IRow row = (XSSFRow)rows.Current;
            for (int i = 0; i < row.LastCellNum; i++)
            {
                ICell cell = row.GetCell(i);
                if (cell == null)
                {
                    continue;
                }
                string columnName = header ? cell.StringCellValue : i.ToString();
                dt.Columns.Add(columnName, typeof(string));
            }
            if (!header)
            {
                DataRow first = dt.NewRow();
                for (int i = 0; i < row.LastCellNum; i++)
                {
                    ICell cell = row.GetCell(i);
                    first[i] = cell.StringCellValue;
                }
                dt.Rows.Add(first);
            }
            while (rows.MoveNext())
            {
                row = (XSSFRow)rows.Current;
                DataRow dataRow = dt.NewRow();
                for (int i = 0; i < row.LastCellNum; i++)
                {
                    ICell cell = row.GetCell(i);
                    if (cell == null)
                    {
                        continue;
                    }
                    dataRow[i] = cell.StringCellValue.Trim();
                }
                dt.Rows.Add(dataRow);
            }
            return dt;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strPath"></param>
        /// <returns></returns>
        private static DataTable GetExcelSheetStructureForXLSX(string strPath)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Index", typeof(int));
            dt.Columns.Add("TableName", typeof(string));

            XSSFWorkbook workbook;
            using (FileStream file = new FileStream(strPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                workbook = new XSSFWorkbook(file);
            }
            int iSheetCount = workbook.NumberOfSheets;
            for (int i = 0; i < iSheetCount; i++)
            {
                dt.NewRow();
                dt.Rows.Add(i, workbook.GetSheetName(i));
            }
            return dt;
        }

        /// <summary>
        /// 获取Excel的列文件
        /// </summary>
        /// <param name="strPath">Excel文件所在目录</param>
        /// <param name="iSheetIndex">表序列号</param>
        /// <returns></returns>
        private static DataTable GetExcelColumnsNameForXLSX(string strPath, int iSheetIndex)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Index", typeof(int));
            dt.Columns.Add("Column", typeof(string));
            dt.Columns.Add("中文", typeof(string));
            dt.Columns.Add("英文", typeof(string));

            XSSFWorkbook workbook;
            using (FileStream file = new FileStream(strPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                workbook = new XSSFWorkbook(file);
            }
            ISheet sheet = workbook.GetSheetAt(iSheetIndex);
            var rows = sheet.GetRowEnumerator();
            rows.MoveNext();
            IRow row = (XSSFRow)rows.Current;
            for (int i = 0; i < row.LastCellNum; i++)
            {
                ICell cell = row.GetCell(i);
                dt.NewRow();
                string strCellValue = cell.StringCellValue;
                string strCN = strCellValue;
                string strEN = strCellValue;
                if (strCellValue.Contains("/"))
                {
                    string[] astrValue = strCellValue.Split('/');
                    if (astrValue != null && astrValue.Length > 0)
                    {
                        strCN = astrValue[0];
                        strEN = astrValue[1];
                    }
                }
                dt.Rows.Add(i.ToString(), strCellValue, strCN, strEN);
            }
            return dt;
        }

        #endregion

        #region ExcelCSV
        /// <summary>
        /// 更新用户模板
        /// </summary>
        /// <param name="strPath">路径</param>
        /// <param name="strFileName">文件名</param>
        private static string CopyCustomSheetFileForCSV(string strPath, string strFileName)
        {
            System.IO.File.Copy(strPath, strFileName, true);
            return Path.Combine(Application.StartupPath, strFileName);
        }

        /// <summary>
        /// 获取CSV的列文件
        /// </summary>
        /// <param name="strPath">CSV文件所在目录</param>
        /// <param name="iSheetIndex">表序列号</param>
        /// <returns></returns>
        private static DataTable GetExcelColumnsNameForCSV(string strPath, int iSheetIndex)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Index", typeof(int));
            dt.Columns.Add("Column", typeof(string));
            dt.Columns.Add("中文", typeof(string));
            dt.Columns.Add("英文", typeof(string));

            using (FileStream file = new FileStream(strPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (StreamReader streamReader = new StreamReader(file, Encoding.UTF8))
                {
                    string strLine = string.Empty;
                    while (!string.IsNullOrWhiteSpace((strLine = streamReader.ReadLine())))
                    {
                        string[] tableHead = strLine.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                        //创建列
                        for (int i = 0; i < tableHead.Length; i++)
                        {
                            string strCellValue = tableHead[i].ToString();
                            string strCN = strCellValue;
                            string strEN = strCellValue;
                            if (strCellValue.Contains("/"))
                            {
                                string[] astrValue = strCellValue.Split('/');
                                if (astrValue != null && astrValue.Length > 0)
                                {
                                    strCN = astrValue[0];
                                    strEN = astrValue[1];
                                }
                            }
                            dt.NewRow();
                            dt.Rows.Add(i.ToString(), strCellValue, strCN, strEN);
                        }
                        break;
                    }
                }
            }
            return dt;
        }

        /// <summary>
        /// 将CSV文件中的数据读出到DataTable中(csv)
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        private static DataTable ExcelToTableForCSV(string file, int iIndex)
        {
            DataTable dt = new DataTable();
            bool isFirst = true;
            using (FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (StreamReader streamReader = new StreamReader(file, Encoding.UTF8))
                {
                    string strLine = string.Empty;
                    int columnCount = 0;
                    while (!string.IsNullOrWhiteSpace((strLine = streamReader.ReadLine())))
                    {
                        if (isFirst == true)
                        {
                            string[] tableHead = strLine.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                            isFirst = false;
                            columnCount = tableHead.Length;
                            //创建列
                            for (int i = 0; i < columnCount; i++)
                            {
                                if (dt.Columns.Contains(tableHead[i].Trim()))
                                {
                                    throw new Exception(string.Format("表格中列名【{0}】重复", tableHead[i].Trim()));
                                }
                                DataColumn dc = new DataColumn(tableHead[i].Trim(), typeof(string));
                                dt.Columns.Add(dc);
                            }
                        }
                        else
                        {
                            string[] aryLine = strLine.Split(',');
                            DataRow dr = dt.NewRow();
                            for (int j = 0; j < columnCount; j++)
                            {
                                dr[j] = aryLine[j];
                            }
                            dt.Rows.Add(dr);
                        }
                    }
                }
            }
            return dt;
        }

        #endregion

        #region 对内公共类库
        /// <summary>
        /// 获取文件的版本类型
        /// </summary>
        /// <param name="strFileName">文件路径</param>
        /// <returns>ExcelIdeType</returns>
        private static ExcelIdeType GetExtFileType(string strFileName)
        {
            ExcelIdeType objExcelIdeType = ExcelIdeType.Excel2003;
            switch (GetExtFileTypeName(strFileName))
            {
                case ".xls":
                    objExcelIdeType = ExcelIdeType.Excel2003;
                    break;
                case ".xlsx":
                    objExcelIdeType = ExcelIdeType.Excel2007;
                    break;
                case ".csv":
                    objExcelIdeType = ExcelIdeType.ExcelCSV;
                    break;
                default:
                    throw new Exception("操作失败：这不是一個有效的Excel文件。");
            }
            return objExcelIdeType;
        }


        /// <summary>
        /// 取文件的扩展名
        /// </summary>
        /// <param name="strFileName">文件路径</param>
        /// <returns>string</returns>
        private static string GetExtFileTypeName(string strFileName)
        {
            string sFile = strFileName;
            sFile = sFile.Substring(sFile.LastIndexOf("\\") + 1);
            sFile = sFile.Substring(sFile.LastIndexOf(".")).ToLower();
            return sFile;
        }
        #endregion
    }

    public partial class ExcelHelper
    {
        /// <summary>
        /// 根据模版导出
        /// </summary>
        /// <param name="listExportData"></param>
        /// <param name="file"></param>
        /// <param name="template"></param>
        public static void TableToExcel(List<ExportData> listExportData, string file, string template)
        {
            #region 获取workbook
            IWorkbook workbook;
            if (!string.IsNullOrWhiteSpace(template) && System.IO.File.Exists(template))
            {
                string strTemplateFileExtension = Path.GetExtension(template);
                using (FileStream templateFile = new FileStream(template, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    if (strTemplateFileExtension.ToLower() == FileSuffixConstant.FILESUFFIX_DEFALTEXT_XLS)
                    {
                        workbook = new HSSFWorkbook(templateFile);
                    }
                    else
                    {
                        workbook = new XSSFWorkbook(templateFile);
                    }
                }
            }
            else
            {
                string strFileExtension = Path.GetExtension(file);
                if (strFileExtension.ToLower() == FileSuffixConstant.FILESUFFIX_DEFALTEXT_XLS)
                {
                    workbook = new HSSFWorkbook();
                }
                else
                {
                    workbook = new XSSFWorkbook();
                }
            }
            #endregion
            Dictionary<int, List<ExportData>> dicExportData = ExportDataGroup(listExportData);
            foreach (int sheetIndex in dicExportData.Keys)
            {
                #region 获取Sheet页
                ISheet sheet = null;
                try
                {
                    sheet = workbook.GetSheetAt(sheetIndex - 1);
                }
                catch (System.Exception)
                {
                }
                if (sheet == null)
                {
                    sheet = workbook.CreateSheet(DateTime.Now.ToString("yyyyMMddHHmmss"));
                }
                #endregion
                int indexCurrentRow = 0;
                foreach (ExportData item in dicExportData[sheetIndex])
                {
                    #region 首列生成序号
                    if (item.ExportTable == null)
                    {
                        continue;
                    }
                    if (item.InsertRowNoToFirstColumn)
                    {
                        item.ExportTable = InsertRowNoToDataTableFirstColumn(item.ExportTable);
                    }
                    #endregion

                    indexCurrentRow = indexCurrentRow + item.StartRow - 1;
                    int indexCell = item.StartColumn - 1;

                    #region 复制行
                    if (item.HorizontalSign)
                    {
                        CopySheetRow(sheet, indexCurrentRow, item.ExportTable.Rows.Count);
                    }
                    else
                    {
                        CopySheetRow(sheet, indexCurrentRow, item.ExportTable.Columns.Count);
                    }
                    #endregion

                    #region 数据显示方向标志，H表示水平，V表示竖直
                    if (item.HorizontalSign)
                    {
                        #region 写标题
                        if (item.TitleSign)
                        {
                            IRow row = sheet.GetRow(indexCurrentRow);
                            if (row == null)
                            {
                                row = sheet.CreateRow(indexCurrentRow);
                            }
                            for (int i = 0; i < item.ExportTable.Columns.Count; i++)
                            {
                                ICell cell = row.GetCell(indexCell + i);
                                if (cell == null)
                                {
                                    cell = row.CreateCell(indexCell + i, CellType.STRING);
                                }
                                cell.SetCellValue(item.ExportTable.Columns[i].ColumnName);
                            }
                            indexCurrentRow++;
                        }
                        #endregion
                        #region 写数据
                        if (item.ExportTable.Rows.Count > 0)
                        {
                            for (int i = 0; i < item.ExportTable.Rows.Count; i++)
                            {
                                IRow row = sheet.GetRow(indexCurrentRow);
                                if (row == null)
                                {
                                    row = sheet.CreateRow(indexCurrentRow);
                                }
                                for (int j = 0; j < item.ExportTable.Columns.Count; j++)
                                {
                                    ICell cell = row.GetCell(indexCell + j);
                                    if (cell == null)
                                    {
                                        cell = row.CreateCell(indexCell + j, CellType.STRING);
                                    }
                                    cell.SetCellValue(item.ExportTable.Rows[i][j].ToString());
                                }
                                indexCurrentRow++;
                            }
                        }
                        #endregion
                    }
                    #endregion
                    else
                    {
                        for (int i = 0; i < item.ExportTable.Columns.Count; i++)
                        {
                            IRow row = sheet.GetRow(indexCurrentRow);
                            if (row == null)
                            {
                                row = sheet.CreateRow(indexCurrentRow);
                            }
                            if (item.TitleSign)
                            {
                                ICell cellTitle = row.GetCell(indexCell);
                                if (cellTitle == null)
                                {
                                    cellTitle = row.CreateCell(indexCell, CellType.STRING);
                                }
                                cellTitle.SetCellValue(item.ExportTable.Columns[i].ColumnName);
                            }
                            if (item.ExportTable.Rows.Count > 0)
                            {
                                for (int j = 0; j < item.ExportTable.Rows.Count; j++)
                                {
                                    int cellIndex = item.TitleSign ? (indexCell + 1 + j) : (indexCell + j);
                                    ICell cellValue = row.GetCell(cellIndex);
                                    if (cellValue == null)
                                    {
                                        cellValue = row.CreateCell(cellIndex, CellType.STRING);
                                    }
                                    cellValue.SetCellValue(item.ExportTable.Rows[j][i].ToString());
                                }
                            }
                            indexCurrentRow++;
                        }
                    }
                }
            }
            #region 将数据写入文件

            //转为字节数组
            MemoryStream stream = new MemoryStream();
            workbook.Write(stream);
            var buf = stream.ToArray();

            //保存为Excel文件
            using (FileStream fs = new FileStream(file, FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
            {
                fs.Write(buf, 0, buf.Length);
                fs.Flush();
            }

            #endregion
        }

        /// <summary>
        /// 将需要导出的数据按SheetIndex分组
        /// </summary>
        /// <param name="listExportData"></param>
        /// <returns></returns>
        private static Dictionary<int, List<ExportData>> ExportDataGroup(List<ExportData> listExportData)
        {
            Dictionary<int, List<ExportData>> dicExportData = new Dictionary<int, List<ExportData>>();
            foreach (ExportData item in listExportData)
            {
                if (!dicExportData.ContainsKey(item.SheetIndex))
                {
                    dicExportData.Add(item.SheetIndex, new List<ExportData>());
                }
                dicExportData[item.SheetIndex].Add(item);
            }

            return dicExportData;
        }

        /// <summary>
        /// 在DataTable的首行添加一个序号列
        /// </summary>
        /// <param name="dtData"></param>
        /// <returns></returns>
        private static DataTable InsertRowNoToDataTableFirstColumn(DataTable dtData)
        {
            if (dtData == null || dtData.Rows.Count < 1)
            {
                return dtData;
            }
            #region 添加序号列头
            string rowNoColulmnName = "No";
            if (dtData.Columns.Contains(rowNoColulmnName))
            {
                rowNoColulmnName = "No.";
            }
            if (dtData.Columns.Contains(rowNoColulmnName))
            {
                rowNoColulmnName = "序号";
            }
            if (dtData.Columns.Contains(rowNoColulmnName))
            {
                rowNoColulmnName = DateTime.Now.ToString("yyyyMMddHHmmss");
            }
            DataTable dtResult = new DataTable();
            dtResult.Columns.Add(rowNoColulmnName, typeof(int));
            #endregion
            #region 添加列头
            foreach (DataColumn column in dtData.Columns)
            {
                dtResult.Columns.Add(column.ColumnName, column.DataType, column.Expression);
            }
            #endregion
            #region 数据转换
            for (int i = 0; i < dtData.Rows.Count; i++)
            {
                DataRow row = dtResult.NewRow();
                row[rowNoColulmnName] = i + 1;
                foreach (DataColumn column in dtData.Columns)
                {
                    row[column.ColumnName] = dtData.Rows[i][column.ColumnName];
                }
                dtResult.Rows.Add(row);
            }
            #endregion
            return dtResult;
        }

        /// <summary>
        /// 复制行
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="sourceIndex"></param>
        /// <param name="copyCount"></param>
        private static void CopySheetRow(ISheet sheet, int sourceIndex, int copyCount)
        {
            IRow sourceRow = sheet.GetRow(sourceIndex);
            if (sheet == null || sourceRow == null)
            {
                return;
            }

            for (int i = 1; i < copyCount; i++)
            {
                try
                {
                    IRow row = sourceRow.CopyRowTo(sourceIndex + i);
                    row.Height = sourceRow.Height;
                }
                catch (System.Exception)
                {
                    break;
                }
            }
        }
    }

    /// <summary>
    /// Excel类型
    /// </summary>
    public enum ExcelIdeType
    {
        /// <summary>
        /// Excel2003
        /// </summary>
        Excel2003,
        /// <summary>
        /// Excel2007
        /// </summary>
        Excel2007,
        /// <summary>
        /// CSV
        /// </summary>
        ExcelCSV
    }

    public class ExportData
    {
        public int StartRow { get; set; }
        public int StartColumn { get; set; }
        public bool HorizontalSign { get; set; }
        public bool TitleSign { get; set; }
        public bool InsertRowNoToFirstColumn { get; set; }
        public int SheetIndex { get; set; }

        public DataTable ExportTable { get; set; }
    }

    public class DictionaryData
    {
        public string SheetName { get; set; }
        public List<DictionaryModel> ListDictionaryModel { get; set; }
    }

    public class DictionaryModel
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
