using OptKit.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace OptKit.XUnit.ComponentModel
{
    class TrackableClass : INotifyPropertyChanged, ITrackable
    {
        bool _suppressNotifyChanged;

        string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                if (!object.Equals(_name, value))
                {
                    var oldValue = _name;
                    _name = value;
                    OnPropertyChanged(nameof(Name));
                    OnValueChanged(nameof(Name), value, oldValue);
                }
            }
        }

        double _qty;
        public double Qty
        {
            get { return _qty; }
            set
            {
                if (!object.Equals(_qty, value))
                {
                    var oldValue = _qty;
                    _qty = value;
                    OnPropertyChanged(nameof(Qty));
                    OnValueChanged(nameof(Qty), value, oldValue);
                }
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        public event EventHandler<ValueChangedEventArgs> ValueChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (!_suppressNotifyChanged)
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void OnValueChanged(string propertyName, object newValue, object oldValue)
        {
            if (!_suppressNotifyChanged)
                ValueChanged?.Invoke(this, new ValueChangedEventArgs(propertyName, newValue, oldValue));
        }

        public void RaisePropertyChanged(string propertyName)
        {
            OnPropertyChanged(propertyName);
        }

        public void RaiseValueChanged(string propertyName, object newValue, object oldValue)
        {
            OnValueChanged(propertyName, newValue, oldValue);
        }

        public void ResumeNotifyChanged()
        {
            _suppressNotifyChanged = false;
        }

        public void SuppressNotifyChanged()
        {
            _suppressNotifyChanged = true;
        }
    }
}
