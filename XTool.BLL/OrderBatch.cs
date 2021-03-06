﻿﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using XCode;
using XCode.Configuration;
using XCode.DataAccessLayer;

namespace XTool.BLL
{
    /// <summary>OrderBatch</summary>
    /// <remarks></remarks>
    [Serializable]
    [DataObject]
    [Description("")]
    [BindIndex("sqlite_master_PK_OrderBatch", true, "BatchID")]
    [BindIndex("Index_OrderBatch_HawbCode", false, "HawbCode")]
    [BindTable("OrderBatch", Description = "", ConnName = "ConnName", DbType = DatabaseType.SQLite)]
    public partial class OrderBatch : IOrderBatch
    {
        #region 属性
        private Int32 _BatchID;
        /// <summary></summary>
        [DisplayName("BatchID")]
        [Description("")]
        [DataObjectField(true, true, false, 8)]
        [BindColumn(1, "BatchID", "", null, "integer", 19, 0, false)]
        public virtual Int32 BatchID
        {
            get { return _BatchID; }
            set { if (OnPropertyChanging(__.BatchID, value)) { _BatchID = value; OnPropertyChanged(__.BatchID); } }
        }

        private Int64 _StatusID;
        /// <summary></summary>
        [DisplayName("StatusID")]
        [Description("")]
        [DataObjectField(false, false, false, 8)]
        [BindColumn(2, "StatusID", "", "1", "integer", 19, 0, false)]
        public virtual Int64 StatusID
        {
            get { return _StatusID; }
            set { if (OnPropertyChanging(__.StatusID, value)) { _StatusID = value; OnPropertyChanged(__.StatusID); } }
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
                    case __.BatchID : return _BatchID;
                    case __.StatusID : return _StatusID;
                    case __.HawbCode : return _HawbCode;
                    case __.CreateTime : return _CreateTime;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case __.BatchID : _BatchID = Convert.ToInt32(value); break;
                    case __.StatusID : _StatusID = Convert.ToInt64(value); break;
                    case __.HawbCode : _HawbCode = Convert.ToString(value); break;
                    case __.CreateTime : _CreateTime = Convert.ToString(value); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得OrderBatch字段信息的快捷方式</summary>
        public partial class _
        {
            ///<summary></summary>
            public static readonly Field BatchID = FindByName(__.BatchID);

            ///<summary></summary>
            public static readonly Field StatusID = FindByName(__.StatusID);

            ///<summary></summary>
            public static readonly Field HawbCode = FindByName(__.HawbCode);

            ///<summary></summary>
            public static readonly Field CreateTime = FindByName(__.CreateTime);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
        }

        /// <summary>取得OrderBatch字段名称的快捷方式</summary>
        partial class __
        {
            ///<summary></summary>
            public const String BatchID = "BatchID";

            ///<summary></summary>
            public const String StatusID = "StatusID";

            ///<summary></summary>
            public const String HawbCode = "HawbCode";

            ///<summary></summary>
            public const String CreateTime = "CreateTime";

        }
        #endregion
    }

    /// <summary>OrderBatch接口</summary>
    /// <remarks></remarks>
    public partial interface IOrderBatch
    {
        #region 属性
        /// <summary></summary>
        Int32 BatchID { get; set; }

        /// <summary></summary>
        Int64 StatusID { get; set; }

        /// <summary></summary>
        String HawbCode { get; set; }

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