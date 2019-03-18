using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptKit.ComponentModel
{
    /// <summary>
    /// Value of property changed event args.
    /// </summary>
    public class ValueChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Create instance of ValueChangedEventArgs.
        /// </summary>
        public ValueChangedEventArgs() { }

        /// <summary>
        /// Create instance of ValueChangedEventArgs.
        /// </summary>
        /// <param name="propertyName">Name of the changed property</param>
        /// <param name="newValue">New value</param>
        /// <param name="oldValue">Old value</param>
        public ValueChangedEventArgs(string propertyName, object newValue, object oldValue)
        {
            PropertyName = propertyName;
            NewValue = newValue;
            OldValue = oldValue;
        }

        /// <summary>
        /// Name of the changed property.
        /// </summary>
        public string PropertyName { get; set; }

        /// <summary>
        /// Old value of the property.
        /// </summary>
        public object OldValue { get; set; }

        /// <summary>
        /// New value of the property.
        /// </summary>
        public object NewValue { get; set; }
    }
}
