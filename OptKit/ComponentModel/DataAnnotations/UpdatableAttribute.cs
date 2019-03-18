using System;
using System.Collections.Generic;
using System.Text;

namespace OptKit.ComponentModel.DataAnnotations
{
    public class UpdatableAttribute : Attribute
    {
        public bool Updatable { get; set; }

        public UpdatableAttribute() { }

        public UpdatableAttribute(bool updatable)
        {
            Updatable = updatable;
        }
    }
}
