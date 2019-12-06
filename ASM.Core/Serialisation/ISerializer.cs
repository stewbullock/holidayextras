using System.Xml;

namespace ASM.Core.Serialisation
{
    public interface ISerializer
    {
        string SerializeToXml<T>(T toSerialize);
        string SerializeToXml<T>(T toSerialize, XmlWriterSettings writerSettings);
    }
}