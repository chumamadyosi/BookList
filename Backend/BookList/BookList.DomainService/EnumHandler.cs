using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookList.DomainService
{
    public static class EnumHandler
    {
        public static string GetEnumDescription(this Enum enumValue)
        {
            var field = enumValue.GetType().GetField(enumValue.ToString());
            if (field != null && Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
            {
                return attribute.Description;
            }
            return enumValue.ToString();
        }

        public static T GetValueFromDescription<T>(string description) where T : Enum
        {
            foreach (var enumValue in (T[])Enum.GetValues(typeof(T)))
            {
                if (enumValue.GetEnumDescription() == description)
                {
                    return enumValue;
                }
            }

            throw new ArgumentException("Could not find enum value for description", nameof(description));
        }
    }
}
