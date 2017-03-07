using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace WebForLink.Web.Controllers.Extensoes
{
    public static class EnumExtensions
    {
        public static string GetDescription(this System.Enum element)
        {
            Type type = element.GetType();

            MemberInfo[] memberInfo = type.GetMember(element.ToString());

            if (memberInfo != null && memberInfo.Count() > 0)
            {
                object[] attributes = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attributes != null && attributes.Length > 0)
                    return ((DescriptionAttribute)attributes[0]).Description;
            }

            return element.ToString();
        }

        public static T ParseEnum<T>(string value)
        {
            return (T)System.Enum.Parse(typeof(T), value, true);
        }

        public static T ToEnum<T>(this string value)
        {
            return (T)System.Enum.Parse(typeof(T), value, true);
        }
    }
}