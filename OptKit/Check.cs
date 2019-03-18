using OptKit.Properties;
using System;
using System.Collections.Generic;

namespace OptKit
{
    /// <summary>
    /// 参数验证
    /// </summary>
    public class Check
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="parameterName"></param>
        /// <returns></returns>
        public static T NotNull<T>(T value, string parameterName)
        {
            if (value == null)
            {
                throw new ArgumentNullException(parameterName);
            }

            return value;
        }

        public static string NotNullOrEmpty(string value, string parameterName)
        {
            if (value.IsNullOrEmpty())
            {
                throw new ArgumentException(Resources.ShouldNotBeNullOrEmpty.FormatArgs(parameterName), parameterName);
            }

            return value;
        }

        public static string NotNullOrWhiteSpace(string value, string parameterName)
        {
            if (value.IsNullOrWhiteSpace())
            {
                throw new ArgumentException(Resources.ShouldNotBeNullOrWhiteSpace.FormatArgs(parameterName), parameterName);
            }

            return value;
        }

        public static ICollection<T> NotNullOrEmpty<T>(ICollection<T> value, string parameterName)
        {
            if (value.IsNullOrEmpty())
            {
                throw new ArgumentException(Resources.ShouldNotBeNullOrEmpty.FormatArgs(parameterName), parameterName);
            }

            return value;
        }
    }
}
