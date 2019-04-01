using System;
using System.Collections.Generic;
using System.Text;

namespace OptKit.UnitTest.PropertyType
{
    public class PropertyRegisterModel : Entity<double>
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
    }
}
