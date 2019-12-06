using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace ASM.Core.Serialisation
{
    public class DeSerializer : IDeSerializer
    {
        #region Methods 

        public T DeSerializeFromXml<T>(string xml)
        {
            T item;
            var xmlSerializer = new XmlSerializer(typeof(T));

            using (var reader = XmlReader.Create(new StringReader(xml)))
            {
                item = (T)xmlSerializer.Deserialize(reader);
            }

            return item;
        }

        #endregion
    }
}