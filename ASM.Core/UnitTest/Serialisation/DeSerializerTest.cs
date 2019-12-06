#if DEBUG
using ASM.Core.Serialisation;
using NUnit.Framework;

namespace ASM.Core.UnitTest.Serialisation
{
    class DeSerializerTest
    {
        [SetUp]
        public void Setup()
        { }

        [Test]
        public void DeSerializeFromXmlExpectStringAsInt()
        {
            // Arrange           
            var deSerialiser = new DeSerializer();

            // Act
            var result = deSerialiser.DeSerializeFromXml<int>("<int>10</int >");

            // Assert
            Assert.AreEqual(10, result);
        }
    }
}
#endif