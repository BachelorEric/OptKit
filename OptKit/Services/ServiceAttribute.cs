using System;
using System.Collections.Generic;
using System.Text;

namespace OptKit.Services
{
    /// <summary>
    /// Specifies that the interface is a service that is accessible via <c>RT.Services</c>.
    /// </summary>
    /// <remarks>
    /// This attribute is mostly intended as documentation, so that it is easily possible to see
    /// if a given service is globally available in SharpDevelop.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, Inherited = false)]
    public class ServiceAttribute : Attribute
    {
        /// <summary>
        /// Creates a new ServiceAttribute instance.
        /// </summary>
        public ServiceAttribute() { }
        /// <summary>
        /// Creates a new ServiceAttribute instance.
        /// </summary>
        /// <param name="fallbackType"></param>
        public ServiceAttribute(Type fallbackType) { FallbackType = fallbackType; }

        /// <summary>
        /// The class that implements the interface and serves as a fallback service
        /// in case no real implementation is registered.
        /// </summary>
        /// <remarks>
        /// This property is also useful for unit tests, as there usually is no real service instance when testing.
        /// Fallback services must not maintain any state, as that would be preserved between runs.
        /// </remarks>
        public Type FallbackType { get; set; }
    }
}