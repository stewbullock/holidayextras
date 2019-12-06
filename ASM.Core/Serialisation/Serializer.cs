using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace ASM.Core.Serialisation
{
    public class Serializer : ISerializer
    {
        #region Fields 

        private readonly IFileReader _fileReader;
        private readonly XmlSerializerNamespaces _serializerNamespaces;

        #endregion

        #region Methods 

        #region Constructor 

        public Serializer(IFileReader fileReader)
        {
            _fileReader = fileReader;
            _serializerNamespaces = new XmlSerializerNamespaces();
            _serializerNamespaces.Add(string.Empty, string.Empty);
        }

        #endregion

        public string SerializeToXml<T>(T toSerialize)
        {
            return SerializeToXml(toSerialize, new XmlWriterSettings());
        }

        public string SerializeToXml<T>(T toSerialize, XmlWriterSettings writerSettings)
        {
            string content;

            using (var stream = new MemoryStream())
            {
                var serializer = new XmlSerializer(typeof(T));
                var writer = XmlWriter.Create(stream, writerSettings);

                serializer.Serialize(writer, toSerialize, _serializerNamespaces);

                stream.Position = 0;
                content = _fileReader.Read(stream);

                stream.Close();
            }

            return content;
        }

        #endregion
    }
}