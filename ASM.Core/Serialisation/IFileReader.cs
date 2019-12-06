using System.IO;

namespace ASM.Core.Serialisation
{
    public interface IFileReader
    {
        string Read(string fullyQualifiedFileName);
        string Read(Stream stream);
    }
}