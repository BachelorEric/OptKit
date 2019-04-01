using OptKit.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace OptKit.UnitTest.PropertyRegister
{
    public class RefEntity : Entity<double>
    {
        #region 引用属性 Ref
        /// <summary>
        /// 引用属性Id
        /// </summary>
        [Caption("引用属性")]
        [Column]
        public static readonly IRefIdProperty<double> RefIdProperty =
            P<RefEntity>.RegisterRefId(e => e.RefId, ReferenceType.Normal);

        /// <summary>
        /// 引用属性Id
        /// </summary>
        public double RefId
        {
            get { return Get(RefIdProperty); }
            set { Set(value, RefIdProperty); }
        }

        /// <summary>
        /// 引用属性
        /// </summary>
        public static readonly IProperty<Ref> RefProperty =
            P<RefEntity>.RegisterRef(e => e.Ref, RefIdProperty);

        /// <summary>
        /// 引用属性
        /// </summary>
        public Ref Ref
        {
            get { return Get(RefProperty); }
            set { Set(value, RefProperty); }
        }
        #endregion

        #region 自引用 Self
        /// <summary>
        /// 自引用Id
        /// </summary>
        [Caption("自引用")]
        [Column]
        public static readonly IRefIdProperty<double?> SelfIdProperty = P<RefEntity>.RegisterRefId(e => e.SelfId, ReferenceType.Normal);

        /// <summary>
        /// 自引用Id
        /// </summary>
        public double? SelfId
        {
            get { return Get(SelfIdProperty); }
            set { Set(value, SelfIdProperty); }
        }

        /// <summary>
        /// 自引用
        /// </summary>
        public static readonly IRefEntityProperty<RefEntity> SelfProperty = P<RefEntity>.RegisterRef(e => e.Self, SelfIdProperty);

        /// <summary>
        /// 自引用
        /// </summary>
        public RefEntity Self
        {
            get { return Get(SelfProperty); }
            set { Set(value, SelfProperty); }
        }
        #endregion

    }

    public class Ref : Entity<double>
    {

    }
}
