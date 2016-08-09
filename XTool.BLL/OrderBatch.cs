﻿using System;
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
    [BindIndex("sqlite_master_PK_OrderBatch", true, "id")]
    [BindTable("OrderBatch", Description = "", ConnName = "ConnName", DbType = DatabaseType.SQLite)]
    public partial class OrderBatch : IOrderBatch
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

        private String _create_time;
        /// <summary></summary>
        [DisplayName("create_time")]
        [Description("")]
        [DataObjectField(false, false, true, 32)]
        [BindColumn(2, "create_time", "", null, "text(32)", 0, 0, false)]
        public virtual String create_time
        {
            get { return _create_time; }
            set { if (OnPropertyChanging(__.create_time, value)) { _create_time = value; OnPropertyChanged(__.create_time); } }
        }

        private Int64 _order_count;
        /// <summary></summary>
        [DisplayName("order_count")]
        [Description("")]
        [DataObjectField(false, false, true, 8)]
        [BindColumn(3, "order_count", "", null, "integer", 19, 0, false)]
        public virtual Int64 order_count
        {
            get { return _order_count; }
            set { if (OnPropertyChanging(__.order_count, value)) { _order_count = value; OnPropertyChanged(__.order_count); } }
        }

        private Int64 _scanned_count;
        /// <summary></summary>
        [DisplayName("scanned_count")]
        [Description("")]
        [DataObjectField(false, false, true, 8)]
        [BindColumn(4, "scanned_count", "", null, "integer", 19, 0, false)]
        public virtual Int64 scanned_count
        {
            get { return _scanned_count; }
            set { if (OnPropertyChanging(__.scanned_count, value)) { _scanned_count = value; OnPropertyChanged(__.scanned_count); } }
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
                    case __.create_time : return _create_time;
                    case __.order_count : return _order_count;
                    case __.scanned_count : return _scanned_count;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case __.id : _id = Convert.ToInt32(value); break;
                    case __.create_time : _create_time = Convert.ToString(value); break;
                    case __.order_count : _order_count = Convert.ToInt64(value); break;
                    case __.scanned_count : _scanned_count = Convert.ToInt64(value); break;
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
            public static readonly Field id = FindByName(__.id);

            ///<summary></summary>
            public static readonly Field create_time = FindByName(__.create_time);

            ///<summary></summary>
            public static readonly Field order_count = FindByName(__.order_count);

            ///<summary></summary>
            public static readonly Field scanned_count = FindByName(__.scanned_count);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
        }

        /// <summary>取得OrderBatch字段名称的快捷方式</summary>
        partial class __
        {
            ///<summary></summary>
            public const String id = "id";

            ///<summary></summary>
            public const String create_time = "create_time";

            ///<summary></summary>
            public const String order_count = "order_count";

            ///<summary></summary>
            public const String scanned_count = "scanned_count";

        }
        #endregion
    }

    /// <summary>OrderBatch接口</summary>
    /// <remarks></remarks>
    public partial interface IOrderBatch
    {
        #region 属性
        /// <summary></summary>
        Int32 id { get; set; }

        /// <summary></summary>
        String create_time { get; set; }

        /// <summary></summary>
        Int64 order_count { get; set; }

        /// <summary></summary>
        Int64 scanned_count { get; set; }
        #endregion

        #region 获取/设置 字段值
        /// <summary>获取/设置 字段值。</summary>
        /// <param name="name">字段名</param>
        /// <returns></returns>
        Object this[String name] { get; set; }
        #endregion
    }
}