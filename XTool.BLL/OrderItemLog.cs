﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using XCode;
using XCode.Configuration;
using XCode.DataAccessLayer;

namespace XTool.BLL
{
    /// <summary>OrderItemLog</summary>
    /// <remarks></remarks>
    [Serializable]
    [DataObject]
    [Description("")]
    [BindIndex("sqlite_master_PK_OrderItemLog", true, "LogID")]
    [BindIndex("Index_OrderItemLog_OrderID", false, "OrderID")]
    [BindTable("OrderItemLog", Description = "", ConnName = "ConnName", DbType = DatabaseType.SQLite)]
    public partial class OrderItemLog : IOrderItemLog
    {
        #region 属性
        private Int32 _LogID;
        /// <summary></summary>
        [DisplayName("LogID")]
        [Description("")]
        [DataObjectField(true, true, false, 8)]
        [BindColumn(1, "LogID", "", null, "integer", 19, 0, false)]
        public virtual Int32 LogID
        {
            get { return _LogID; }
            set { if (OnPropertyChanging(__.LogID, value)) { _LogID = value; OnPropertyChanged(__.LogID); } }
        }

        private Int64 _OrderID;
        /// <summary></summary>
        [DisplayName("OrderID")]
        [Description("")]
        [DataObjectField(false, false, false, 8)]
        [BindColumn(2, "OrderID", "", null, "integer", 19, 0, false)]
        public virtual Int64 OrderID
        {
            get { return _OrderID; }
            set { if (OnPropertyChanging(__.OrderID, value)) { _OrderID = value; OnPropertyChanged(__.OrderID); } }
        }

        private String _LogConent;
        /// <summary></summary>
        [DisplayName("LogConent")]
        [Description("")]
        [DataObjectField(false, false, false, 512)]
        [BindColumn(3, "LogConent", "", null, "text(512)", 0, 0, false)]
        public virtual String LogConent
        {
            get { return _LogConent; }
            set { if (OnPropertyChanging(__.LogConent, value)) { _LogConent = value; OnPropertyChanged(__.LogConent); } }
        }

        private String _CreateTime;
        /// <summary></summary>
        [DisplayName("CreateTime")]
        [Description("")]
        [DataObjectField(false, false, false, 32)]
        [BindColumn(4, "CreateTime", "", null, "text(32)", 0, 0, false)]
        public virtual String CreateTime
        {
            get { return _CreateTime; }
            set { if (OnPropertyChanging(__.CreateTime, value)) { _CreateTime = value; OnPropertyChanged(__.CreateTime); } }
        }
        #endregion

        #region 获取/设置 字段值
        /// <summary>
        /// 获取/设置 字段值。
        /// 一个索引，基类使用反射实现。
        /// 派生实体类可重写该索引，以避免反射带来的性能损耗
        /// </summary>
        /// <param name="name">字段名</param>
        /// <returns></returns>
        public override Object this[String name]
        {
            get
            {
                switch (name)
                {
                    case __.LogID : return _LogID;
                    case __.OrderID : return _OrderID;
                    case __.LogConent : return _LogConent;
                    case __.CreateTime : return _CreateTime;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case __.LogID : _LogID = Convert.ToInt32(value); break;
                    case __.OrderID : _OrderID = Convert.ToInt64(value); break;
                    case __.LogConent : _LogConent = Convert.ToString(value); break;
                    case __.CreateTime : _CreateTime = Convert.ToString(value); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得OrderItemLog字段信息的快捷方式</summary>
        public partial class _
        {
            ///<summary></summary>
            public static readonly Field LogID = FindByName(__.LogID);

            ///<summary></summary>
            public static readonly Field OrderID = FindByName(__.OrderID);

            ///<summary></summary>
            public static readonly Field LogConent = FindByName(__.LogConent);

            ///<summary></summary>
            public static readonly Field CreateTime = FindByName(__.CreateTime);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
        }

        /// <summary>取得OrderItemLog字段名称的快捷方式</summary>
        partial class __
        {
            ///<summary></summary>
            public const String LogID = "LogID";

            ///<summary></summary>
            public const String OrderID = "OrderID";

            ///<summary></summary>
            public const String LogConent = "LogConent";

            ///<summary></summary>
            public const String CreateTime = "CreateTime";

        }
        #endregion
    }

    /// <summary>OrderItemLog接口</summary>
    /// <remarks></remarks>
    public partial interface IOrderItemLog
    {
        #region 属性
        /// <summary></summary>
        Int32 LogID { get; set; }

        /// <summary></summary>
        Int64 OrderID { get; set; }

        /// <summary></summary>
        String LogConent { get; set; }

        /// <summary></summary>
        String CreateTime { get; set; }
        #endregion

        #region 获取/设置 字段值
        /// <summary>获取/设置 字段值。</summary>
        /// <param name="name">字段名</param>
        /// <returns></returns>
        Object this[String name] { get; set; }
        #endregion
    }
}