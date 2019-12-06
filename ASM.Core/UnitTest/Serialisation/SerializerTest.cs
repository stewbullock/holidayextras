#if DEBUG
using ASM.Core.Serialisation;
using NUnit.Framework;

namespace ASM.Core.UnitTest.Serialisation
{
    class SerializerTest
    {
        [SetUp]
        public void Setup()
        { }

        [Test]
        public void SerializeToXmlExpectIntAsString()
        {
            // Arrange           
            var fileReader = new FileReader();
            var serialiser = new Serializer(fileReader);

            // Act
            var result = serialiser.SerializeToXml(10, new System.Xml.XmlWriterSettings { OmitXmlDeclaration = true });

            // Assert
            Assert.AreEqual("<int>10</int>", result);
        }
    }
}
#endif