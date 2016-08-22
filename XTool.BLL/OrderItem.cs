﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using XCode;
using XCode.Configuration;
using XCode.DataAccessLayer;

namespace XTool.BLL
{
    /// <summary>OrderItem</summary>
    /// <remarks></remarks>
    [Serializable]
    [DataObject]
    [Description("")]
    [BindIndex("sqlite_master_PK_OrderItem", true, "ID")]
    [BindIndex("Index_OrderItem_HawbCode", false, "HawbCode")]
    [BindIndex("Index_OrderItem_BatchID", false, "BatchID")]
    [BindTable("OrderItem", Description = "", ConnName = "ConnName", DbType = DatabaseType.SQLite)]
    public partial class OrderItem : IOrderItem
    {
        #region 属性
        private Int32 _ID;
        /// <summary></summary>
        [DisplayName("ID")]
        [Description("")]
        [DataObjectField(true, true, false, 8)]
        [BindColumn(1, "ID", "", null, "integer", 19, 0, false)]
        public virtual Int32 ID
        {
            get { return _ID; }
            set { if (OnPropertyChanging(__.ID, value)) { _ID = value; OnPropertyChanged(__.ID); } }
        }

        private Int64 _BatchID;
        /// <summary></summary>
        [DisplayName("BatchID")]
        [Description("")]
        [DataObjectField(false, false, false, 8)]
        [BindColumn(2, "BatchID", "", null, "integer", 19, 0, false)]
        public virtual Int64 BatchID
        {
            get { return _BatchID; }
            set { if (OnPropertyChanging(__.BatchID, value)) { _BatchID = value; OnPropertyChanged(__.BatchID); } }
        }

        private String _HawbCode;
        /// <summary></summary>
        [DisplayName("HawbCode")]
        [Description("")]
        [DataObjectField(false, false, false, 64)]
        [BindColumn(3, "HawbCode", "", null, "text(64)", 0, 0, false)]
        public virtual String HawbCode
        {
            get { return _HawbCode; }
            set { if (OnPropertyChanging(__.HawbCode, value)) { _HawbCode = value; OnPropertyChanged(__.HawbCode); } }
        }

        private String _ScanningTime;
        /// <summary></summary>
        [DisplayName("ScanningTime")]
        [Description("")]
        [DataObjectField(false, false, true, 32)]
        [BindColumn(4, "ScanningTime", "", null, "text(32)", 0, 0, false)]
        public virtual String ScanningTime
        {
            get { return _ScanningTime; }
            set { if (OnPropertyChanging(__.ScanningTime, value)) { _ScanningTime = value; OnPropertyChanged(__.ScanningTime); } }
        }

        private String _OrderStatus;
        /// <summary></summary>
        [DisplayName("OrderStatus")]
        [Description("")]
        [DataObjectField(false, false, false, 128)]
        [BindColumn(5, "OrderStatus", "", null, "text(128)", 0, 0, false)]
        public virtual String OrderStatus
        {
            get { return _OrderStatus; }
            set { if (OnPropertyChanging(__.OrderStatus, value)) { _OrderStatus = value; OnPropertyChanged(__.OrderStatus); } }
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
                    case __.ID : return _ID;
                    case __.BatchID : return _BatchID;
                    case __.HawbCode : return _HawbCode;
                    case __.ScanningTime : return _ScanningTime;
                    case __.OrderStatus : return _OrderStatus;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case __.ID : _ID = Convert.ToInt32(value); break;
                    case __.BatchID : _BatchID = Convert.ToInt64(value); break;
                    case __.HawbCode : _HawbCode = Convert.ToString(value); break;
                    case __.ScanningTime : _ScanningTime = Convert.ToString(value); break;
                    case __.OrderStatus : _OrderStatus = Convert.ToString(value); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得OrderItem字段信息的快捷方式</summary>
        public partial class _
        {
            ///<summary></summary>
            public static readonly Field ID = FindByName(__.ID);

            ///<summary></summary>
            public static readonly Field BatchID = FindByName(__.BatchID);

            ///<summary></summary>
            public static readonly Field HawbCode = FindByName(__.HawbCode);

            ///<summary></summary>
            public static readonly Field ScanningTime = FindByName(__.ScanningTime);

            ///<summary></summary>
            public static readonly Field OrderStatus = FindByName(__.OrderStatus);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
        }

        /// <summary>取得OrderItem字段名称的快捷方式</summary>
        partial class __
        {
            ///<summary></summary>
            public const String ID = "ID";

            ///<summary></summary>
            public const String BatchID = "BatchID";

            ///<summary></summary>
            public const String HawbCode = "HawbCode";

            ///<summary></summary>
            public const String ScanningTime = "ScanningTime";

            ///<summary></summary>
            public const String OrderStatus = "OrderStatus";

        }
        #endregion
    }

    /// <summary>OrderItem接口</summary>
    /// <remarks></remarks>
    public partial interface IOrderItem
    {
        #region 属性
        /// <summary></summary>
        Int32 ID { get; set; }

        /// <summary></summary>
        Int64 BatchID { get; set; }

        /// <summary></summary>
        String HawbCode { get; set; }

        /// <summary></summary>
        String ScanningTime { get; set; }

        /// <summary></summary>
        String OrderStatus { get; set; }
        #endregion

        #region 获取/设置 字段值
        /// <summary>获取/设置 字段值。</summary>
        /// <param name="name">字段名</param>
        /// <returns></returns>
        Object this[String name] { get; set; }
        #endregion
    }
}