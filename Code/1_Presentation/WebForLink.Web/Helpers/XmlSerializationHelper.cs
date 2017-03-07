using System.IO;
using System.Xml.Serialization;

namespace WebForLink.Web.Helpers
{
    public static class XmlSerializationHelper
    {
        public static string Serialize<T>(this T value)
        {
            var xmlSerializer = new XmlSerializer(typeof (T));
            var writer = new StringWriter();
            xmlSerializer.Serialize(writer, value);
            return writer.ToString();
        }

        public static T Deserialize<T>(this string rawvalue)
        {
            var xmlSerializer = new XmlSerializer(typeof (T));
            var reader = new StreamReader(rawvalue);
            var value = (T) xmlSerializer.Deserialize(reader);
            return value;
        }
    }
}