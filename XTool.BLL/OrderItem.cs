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
    [BindIndex("sqlite_master_PK_OrderItem", true, "id")]
    [BindIndex("index_orderitem_hawb_code", false, "hawb_code")]
    [BindTable("OrderItem", Description = "", ConnName = "ConnName", DbType = DatabaseType.SQLite)]
    public partial class OrderItem : IOrderItem
    {
        #region 属性
        private Int32 _id;
        /// <summary></summary>
        [DisplayName("id")]
        [Description("")]
        [DataObjectField(true, true, false, 8)]
        [BindColumn(1, "id", "", null, "integer", 19, 0, false)]
        public virtual Int32 id
        {
            get { return _id; }
            set { if (OnPropertyChanging(__.id, value)) { _id = value; OnPropertyChanged(__.id); } }
        }

        private Int64 _order_id;
        /// <summary></summary>
        [DisplayName("order_id")]
        [Description("")]
        [DataObjectField(false, false, true, 8)]
        [BindColumn(2, "order_id", "", null, "integer", 19, 0, false)]
        public virtual Int64 order_id
        {
            get { return _order_id; }
            set { if (OnPropertyChanging(__.order_id, value)) { _order_id = value; OnPropertyChanged(__.order_id); } }
        }

        private String _hawb_code;
        /// <summary></summary>
        [DisplayName("hawb_code")]
        [Description("")]
        [DataObjectField(false, false, true, 64)]
        [BindColumn(3, "hawb_code", "", null, "text(64)", 0, 0, false)]
        public virtual String hawb_code
        {
            get { return _hawb_code; }
            set { if (OnPropertyChanging(__.hawb_code, value)) { _hawb_code = value; OnPropertyChanged(__.hawb_code); } }
        }

        private String _order_status;
        /// <summary></summary>
        [DisplayName("order_status")]
        [Description("")]
        [DataObjectField(false, false, true, 256)]
        [BindColumn(4, "order_status", "", null, "text(256)", 0, 0, false)]
        public virtual String order_status
        {
            get { return _order_status; }
            set { if (OnPropertyChanging(__.order_status, value)) { _order_status = value; OnPropertyChanged(__.order_status); } }
        }

        private String _scanning_time;
        /// <summary></summary>
        [DisplayName("scanning_time")]
        [Description("")]
        [DataObjectField(false, false, true, 32)]
        [BindColumn(5, "scanning_time", "", null, "text(32)", 0, 0, false)]
        public virtual String scanning_time
        {
            get { return _scanning_time; }
            set { if (OnPropertyChanging(__.scanning_time, value)) { _scanning_time = value; OnPropertyChanged(__.scanning_time); } }
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
                    case __.id : return _id;
                    case __.order_id : return _order_id;
                    case __.hawb_code : return _hawb_code;
                    case __.order_status : return _order_status;
                    case __.scanning_time : return _scanning_time;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case __.id : _id = Convert.ToInt32(value); break;
                    case __.order_id : _order_id = Convert.ToInt64(value); break;
                    case __.hawb_code : _hawb_code = Convert.ToString(value); break;
                    case __.order_status : _order_status = Convert.ToString(value); break;
                    case __.scanning_time : _scanning_time = Convert.ToString(value); break;
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
            public static readonly Field id = FindByName(__.id);

            ///<summary></summary>
            public static readonly Field order_id = FindByName(__.order_id);

            ///<summary></summary>
            public static readonly Field hawb_code = FindByName(__.hawb_code);

            ///<summary></summary>
            public static readonly Field order_status = FindByName(__.order_status);

            ///<summary></summary>
            public static readonly Field scanning_time = FindByName(__.scanning_time);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
        }

        /// <summary>取得OrderItem字段名称的快捷方式</summary>
        partial class __
        {
            ///<summary></summary>
            public const String id = "id";

            ///<summary></summary>
            public const String order_id = "order_id";

            ///<summary></summary>
            public const String hawb_code = "hawb_code";

            ///<summary></summary>
            public const String order_status = "order_status";

            ///<summary></summary>
            public const String scanning_time = "scanning_time";

        }
        #endregion
    }

    /// <summary>OrderItem接口</summary>
    /// <remarks></remarks>
    public partial interface IOrderItem
    {
        #region 属性
        /// <summary></summary>
        Int32 id { get; set; }

        /// <summary></summary>
        Int64 order_id { get; set; }

        /// <summary></summary>
        String hawb_code { get; set; }

        /// <summary></summary>
        String order_status { get; set; }

        /// <summary></summary>
        String scanning_time { get; set; }
        #endregion

        #region 获取/设置 字段值
        /// <summary>获取/设置 字段值。</summary>
        /// <param name="name">字段名</param>
        /// <returns></returns>
        Object this[String name] { get; set; }
        #endregion
    }
}