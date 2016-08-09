using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XTool.Constant
{
    public sealed class FileSuffixConstant
    {
        #region Excel文件格式 .xls
        /// <summary>
        /// Excel文件后缀 
        /// </summary>
        public static string FILESUFFIX_DEFALTEXT_XLS = ".xls";


        /// <summary>
        /// 获取或设置Excel文件名筛选器字符串 
        /// </summary>
        public static string FILESUFFIX_FILTER_XLS = "Excel(*.xls)|*.xls";

        /// <summary>
        /// Excel2007文件后缀 
        /// </summary>
        public static string FILESUFFIX_DEFALTEXT_XLSX = ".xlsx";


        /// <summary>
        /// 获取或设置Excel2007文件名筛选器字符串 
        /// </summary>
        public static string FILESUFFIX_FILTER_XLSX = "Excel(*.xlsx)|*.xlsx";

        /// <summary>
        /// 获取或设置Excel文件名筛选器字符串 
        /// </summary>
        public static string FILESUFFIX_FILTER_EXCEL = "文件(*.xls;*.xlsx)|*.xls;*.xlsx";

        /// <summary>
        /// 获取或设置Excel文件和CSV文件筛选器字符串 
        /// </summary>
        public static string FILESUFFIX_FILTER_EXCELCSV = "文件(*.xls;*.xlsx;*.csv)|*.xls;*.xlsx;*.csv";

        #endregion
    }
}
