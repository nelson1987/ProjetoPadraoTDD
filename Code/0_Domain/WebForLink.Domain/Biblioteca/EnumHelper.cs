using System;
using System.Collections.Generic;
using System.Linq;
using WebForDocs.ExtensaoControllers;

namespace WebForDocs.Biblioteca
{
    public class EnumHelper
    {
        public static List<string> GetDescriptions<T>() where T : struct
        {
            Type type = typeof(T);

            return !type.IsEnum ? null : System.Enum.GetValues(type).Cast<System.Enum>().Select(x => x.GetDescription()).ToList();
        }
    }
}