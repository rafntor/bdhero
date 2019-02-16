using System;
using System.Linq;
using System.Reflection;

namespace DotNetUtils.Extensions
{
    /// <summary>
    ///     Extension methods for <see cref="MemberInfo"/> objects.
    /// </summary>
    public static class MemberInfoExtensions
    {
        /// <summary>
        ///     Retrieves a list of all properties defined by this member which implement an interface.
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        public static PropertyInfo[] GetAllInterfaceProperties(this MemberInfo member)
        {
            var type = member.DeclaringType;

            if (type == null)
                return new PropertyInfo[0];

            var interfaces = type.GetInterfaces();
            var interfaceProperties =
                interfaces.SelectMany(@interface => @interface.GetProperties().Where(info => Matches(member, info)))
                          .ToArray();

            return interfaceProperties;
        }

        private static bool Matches(MemberInfo member, PropertyInfo interfaceProperty)
        {
            // This is weak: among other things, an implementation 
            // may be deliberately hiding an interface member
            return interfaceProperty.Name == member.Name &&
                   interfaceProperty.MemberType == member.MemberType;
        }
    }
}
