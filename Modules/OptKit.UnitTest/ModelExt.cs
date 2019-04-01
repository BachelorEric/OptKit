using OptKit.ComponentModel;
using OptKit.Domain;
using System;
using System.Collections.Generic;
using System.Text;
[assembly: OptKit.PropertyDeclare(typeof(OptKit.UnitTest.ModelAExt))]
[assembly: OptKit.PropertyDeclare(typeof(OptKit.UnitTest.ModelBExt))]

namespace OptKit.UnitTest
{
    class ModelAExt
    {
        /// <summary>
        /// 计数
        /// </summary>
        [Caption("计数A")]
        public static readonly IProperty NameProperty = P<ModelA>.Register("CountA", typeof(int), typeof(ModelAExt));
    }

    class ModelBExt
    {
        /// <summary>
        /// 计数
        /// </summary>
        [Caption("计数B")]
        public static readonly IProperty NameProperty = P<ModelB>.Register("CountB", typeof(int), typeof(ModelBExt));
    }
}
