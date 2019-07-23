using System;
using System.Linq;

namespace Budgetor.Helpers
{
    public static class EnumExtensions
    {
        public static TAttribute GetAttribute<TAttribute>(this Enum value)
            where TAttribute : Attribute
        {
            var type = value.GetType();
            var name = Enum.GetName(type, value);

            var t = type.GetField(name) // I prefer to get attributes this way
                .GetCustomAttributes(false)
                .OfType<TAttribute>();
            var t2 = t.FirstOrDefault();
            return type.GetField(name) // I prefer to get attributes this way
                .GetCustomAttributes(false)
                .OfType<TAttribute>()
                .SingleOrDefault();
        }
    }
}
