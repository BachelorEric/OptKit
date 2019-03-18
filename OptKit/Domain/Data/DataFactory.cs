using OptKit.Properties;
using System;
using System.Collections.Generic;
using System.Text;

namespace OptKit.Domain.Data
{
    public abstract class DataFactory
    {
        static DataFactory _instance;
        public static DataFactory Instance
        {
            get
            {
                if (_instance == null)
                    throw new AppException(Resources.TypeNotInitialized.FormatArgs(nameof(DataFactory)));
                return _instance;
            }
        }

        public static void SetFactory(DataFactory factory)
        {
            _instance = factory;
        }

        public abstract IFieldData Create(Type type);
    }
}
