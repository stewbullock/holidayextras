namespace ASM.Core.Serialisation
{
    public interface IDeSerializer
    {
        T DeSerializeFromXml<T>(string xml);
    }
}