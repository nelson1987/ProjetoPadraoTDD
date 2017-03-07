using System;
using System.Collections.Generic;

namespace WebForLink.Web.Infrastructure
{
    public class EnumHelper
    {
        public static List<string> GetDescriptions<T>() where T : struct
        {
            Type type = typeof(T);

            //return !type.IsEnum ? null : Enum.GetValues(type).Cast<Enum>().Select(x =>x.GetDescription()).ToList();
            return null;
        }
        /*
        public static string GetEnumDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute),
                false);

            if (attributes != null &&
                attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }
        */
    }
}