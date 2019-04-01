using System;
using System.Collections.Generic;
using System.Text;

namespace OptKit.UnitTest
{
    public class ModelA : Entity<double>
    {
        #region 名称 Name
        /// <summary>
        /// 名称
        /// </summary>
        [Caption("名称")]
        [Column]
        public static readonly IProperty<string> NameProperty = P<ModelA>.Register(e => e.Name);

        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            get { return Get(NameProperty); }
            set { Set(value, NameProperty); }
        }
        #endregion



        [Column]
        public virtual int IntProperty { get; set; }

        public virtual string A { get; set; }

        public virtual string B { get; set; }

        [Property(Expression = "A + B")]
        public virtual string AB { get; set; }
    }
}
