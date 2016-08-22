﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using XCode;
using XCode.Configuration;
using XCode.DataAccessLayer;

namespace XTool.BLL
{
    /// <summary>OrderBatchStatus</summary>
    /// <remarks></remarks>
    [Serializable]
    [DataObject]
    [Description("")]
    [BindIndex("sqlite_master_PK_OrderBatchStatus", true, "StatusID")]
    [BindTable("OrderBatchStatus", Description = "", ConnName = "ConnName", DbType = DatabaseType.SQLite)]
    public partial class OrderBatchStatus : IOrderBatchStatus
    {
        #region 属性
        private Int64 _StatusID;
        /// <summary></summary>
        [DisplayName("StatusID")]
        [Description("")]
        [DataObjectField(true, false, false, 8)]
        [BindColumn(1, "StatusID", "", "1", "integer", 19, 0, false)]
        public virtual Int64 StatusID
        {
            get { return _StatusID; }
            set { if (OnPropertyChanging(__.StatusID, value)) { _StatusID = value; OnPropertyChanged(__.StatusID); } }
        }

        private String _StatusName;
        /// <summary></summary>
        [DisplayName("StatusName")]
        [Description("")]
        [DataObjectField(false, false, false, 64)]
        [BindColumn(2, "StatusName", "", null, "text(64)", 0, 0, false)]
        public virtual String StatusName
        {
            get { return _StatusName; }
            set { if (OnPropertyChanging(__.StatusName, value)) { _StatusName = value; OnPropertyChanged(__.StatusName); } }
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
                    case __.StatusID : return _StatusID;
                    case __.StatusName : return _StatusName;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case __.StatusID : _StatusID = Convert.ToInt64(value); break;
                    case __.StatusName : _StatusName = Convert.ToString(value); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得OrderBatchStatus字段信息的快捷方式</summary>
        public partial class _
        {
            ///<summary></summary>
            public static readonly Field StatusID = FindByName(__.StatusID);

            ///<summary></summary>
            public static readonly Field StatusName = FindByName(__.StatusName);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
        }

        /// <summary>取得OrderBatchStatus字段名称的快捷方式</summary>
        partial class __
        {
            ///<summary></summary>
            public const String StatusID = "StatusID";

            ///<summary></summary>
            public const String StatusName = "StatusName";

        }
        #endregion
    }

    /// <summary>OrderBatchStatus接口</summary>
    /// <remarks></remarks>
    public partial interface IOrderBatchStatus
    {
        #region 属性
        /// <summary></summary>
        Int64 StatusID { get; set; }

        /// <summary></summary>
        String StatusName { get; set; }
        #endregion

        #region 获取/设置 字段值
        /// <summary>获取/设置 字段值。</summary>
        /// <param name="name">字段名</param>
        /// <returns></returns>
        Object this[String name] { get; set; }
        #endregion
    }
}