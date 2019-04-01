using OptKit.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace OptKit.UnitTest.PropertyRegister
{
    [Table]
    [Caption("父实体")]
    public class Parent : Entity<int>
    {
        #region 名称 Name
        /// <summary>
        /// 名称
        /// </summary>
        [Caption("名称")]
        [Column]
        public static readonly IProperty<string> NameProperty = P<Parent>.Register(e => e.Name);

        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            get { return Get(NameProperty); }
            set { Set(value, NameProperty); }
        }
        #endregion

        #region 子列表 ChildList
        /// <summary>
        /// 子列表
        /// </summary>
        [Caption("子列表")]
        public static readonly IListProperty<DomainList<Child>> ChildListProperty = P<Parent>.RegisterList(e => e.ChildList);

        /// <summary>
        /// 子列表
        /// </summary>
        public DomainList<Child> ChildList
        {
            get { return Get(ChildListProperty); }
        }
        #endregion
    }

    [Domain(Category = DomainCategory.Child)]
    [Table]
    [Caption("子实体")]
    public class Child : Entity<int>
    {
        #region 名称 Name
        /// <summary>
        /// 名称
        /// </summary>
        [Caption("名称")]
        [Column]
        public static readonly IProperty<string> NameProperty = P<Child>.Register(e => e.Name);

        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            get { return Get(NameProperty); }
            set { Set(value, NameProperty); }
        }
        #endregion

        #region 父 Parent
        /// <summary>
        /// 父Id
        /// </summary>
        [Caption("父")]
        [Column]
        public static readonly IRefIdProperty<double> ParentIdProperty = P<Child>.RegisterRefId(e => e.ParentId, ReferenceType.Parent);

        /// <summary>
        /// 父Id
        /// </summary>
        public double ParentId
        {
            get { return Get(ParentIdProperty); }
            set { Set(value, ParentIdProperty); }
        }

        /// <summary>
        /// 父
        /// </summary>
        public static readonly IRefEntityProperty<Parent> ParentProperty = P<Child>.RegisterRef(e => e.Parent, ParentIdProperty);

        /// <summary>
        /// 父
        /// </summary>
        public Parent Parent
        {
            get { return Get(ParentProperty); }
            set { Set(value, ParentProperty); }
        }
        #endregion
    }
}
