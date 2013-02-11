using System;
using System.Reflection;

namespace PropertyChangeLogger
{
    internal static class ExtensionMethods
    {
        internal static bool HasCustomAttributeOfType(this PropertyInfo obj, Type attributeType)
        {
            return obj.GetCustomAttributes(attributeType, true).Length > 0;
        }
    }
}
