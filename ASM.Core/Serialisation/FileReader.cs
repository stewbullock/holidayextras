using System.IO;

namespace ASM.Core.Serialisation
{
    public class FileReader : IFileReader
    {
        #region Methods 

        public string Read(string fullyQualifiedFileName)
        {
            using (var reader = new StreamReader(fullyQualifiedFileName))
            {
                return ReadToEndAndClose(reader);
            }
        }

        public string Read(Stream stream)
        {
            using (var reader = new StreamReader(stream))
            {
                return ReadToEndAndClose(reader);
            }
        }

        private string ReadToEndAndClose(TextReader reader)
        {
            var fileContent = reader.ReadToEnd();
            reader.Close();

            return fileContent;
        }

        #endregion
    }
}