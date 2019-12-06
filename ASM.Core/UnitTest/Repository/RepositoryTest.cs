#if DEBUG
using ASM.Core.Repository;
using NUnit.Framework;
using System.Collections.Generic;

namespace ASM.Core.UnitTest.Repository
{
    class RepositoryTest
    {
        [SetUp]
        public void Setup()
        { }

        [Test]
        public void GetAllExpectAllTestIdentitiesReturned()
        {
            // Arrange
            var testIdentity1 = new TestIdentity { Id = 1 };
            var testIdentity2 = new TestIdentity { Id = 2 };

            var testIdentities = new List<TestIdentity>
            {
                testIdentity1,
                testIdentity2
            };

            var repository = new Repository<TestIdentity>(testIdentities);

            // Act
            var result = repository.GetAll();

            // Assert
            CollectionAssert.Contains(result, testIdentity1);
            CollectionAssert.Contains(result, testIdentity2);
        }

        [Test]
        public void GetByIdExpectOnlyTestIdentityOneReturned()
        {
            // Arrange
            var testIdentity1 = new TestIdentity { Id = 1 };
            var testIdentity2 = new TestIdentity { Id = 2 };

            var testIdentities = new List<TestIdentity>
            {
                testIdentity1,
                testIdentity2
            };

            var repository = new Repository<TestIdentity>(testIdentities);

            // Act
            var result = repository.GetById(1);

            // Assert
            Assert.AreSame(testIdentity1, result);
        }

        [Test]
        public void GetByIdExpectOnlyTestIdentitiesOneAndTwoReturned()
        {
            // Arrange
            var testIdentity1 = new TestIdentity { Id = 1 };
            var testIdentity2 = new TestIdentity { Id = 2 };
            var testIdentity3 = new TestIdentity { Id = 3 };
            var testIdentity4 = new TestIdentity { Id = 4 };

            var testIdentities = new List<TestIdentity>
            {
                testIdentity1,
                testIdentity2,
                testIdentity3,
                testIdentity4
            };

            var repository = new Repository<TestIdentity>(testIdentities);

            // Act
            var result = repository.GetById(new List<int> { 1, 2 });

            // Assert
            CollectionAssert.Contains(result, testIdentity1);
            CollectionAssert.Contains(result, testIdentity2);
            CollectionAssert.DoesNotContain(result, testIdentity3);
            CollectionAssert.DoesNotContain(result, testIdentity4);
        }

        [Test]
        public void FIndExpectOnlyTestIdentitiesThreeAndFourReturned()
        {
            // Arrange
            var testIdentity1 = new TestIdentity { Id = 1 };
            var testIdentity2 = new TestIdentity { Id = 2 };
            var testIdentity3 = new TestIdentity { Id = 3 };
            var testIdentity4 = new TestIdentity { Id = 4 };

            var testIdentities = new List<TestIdentity>
            {
                testIdentity1,
                testIdentity2,
                testIdentity3,
                testIdentity4
            };

            var repository = new Repository<TestIdentity>(testIdentities);

            // Act
            var result = repository.Find(testIdentity => (testIdentity.Id == 3 || testIdentity.Id == 4));

            // Assert                        
            CollectionAssert.DoesNotContain(result, testIdentity1);
            CollectionAssert.DoesNotContain(result, testIdentity2);
            CollectionAssert.Contains(result, testIdentity3);
            CollectionAssert.Contains(result, testIdentity4);
        }

        [Test]
        public void AddExpectOnlyTestIdentityOneToBeAdded()
        {
            // Arrange
            var testIdentities = new List<TestIdentity>();
            var repository = new Repository<TestIdentity>(testIdentities);
            var testIdentity1 = new TestIdentity { Id = 1 };

            // Act
            repository.Add(testIdentity1);

            // Assert                        
            CollectionAssert.Contains(testIdentities, testIdentity1);
        }

        [Test]
        public void AddExpectOnlyTestIdentitiesOneAndTwoToBeAdded()
        {
            // Arrange
            var testIdentities = new List<TestIdentity>();
            var repository = new Repository<TestIdentity>(testIdentities);
            var testIdentity1 = new TestIdentity { Id = 1 };
            var testIdentity2 = new TestIdentity { Id = 2 };

            // Act
            repository.Add(new List<TestIdentity> { testIdentity1, testIdentity2 });

            // Assert                        
            CollectionAssert.Contains(testIdentities, testIdentity1);
            CollectionAssert.Contains(testIdentities, testIdentity2);
        }

        [Test]
        public void RemoveExpectOnlyIdentityOneToBeRemoved()
        {
            // Arrange
            var testIdentity1 = new TestIdentity { Id = 1 };
            var testIdentity2 = new TestIdentity { Id = 2 };
            var testIdentity3 = new TestIdentity { Id = 3 };
            var testIdentity4 = new TestIdentity { Id = 4 };

            var testIdentities = new List<TestIdentity>
            {
                testIdentity1,
                testIdentity2,
                testIdentity3,
                testIdentity4
            };

            var repository = new Repository<TestIdentity>(testIdentities);

            // Act
            repository.Remove(testIdentity1);

            // Assert                        
            CollectionAssert.DoesNotContain(testIdentities, testIdentity1);
            CollectionAssert.Contains(testIdentities, testIdentity2);
            CollectionAssert.Contains(testIdentities, testIdentity3);
            CollectionAssert.Contains(testIdentities, testIdentity4);
        }

        [Test]
        public void RemoveExpectOnlyTestIdentitiesOneAndTwoToBeRemoved()
        {
            // Arrange
            var testIdentity1 = new TestIdentity { Id = 1 };
            var testIdentity2 = new TestIdentity { Id = 2 };
            var testIdentity3 = new TestIdentity { Id = 3 };
            var testIdentity4 = new TestIdentity { Id = 4 };

            var testIdentities = new List<TestIdentity>
            {
                testIdentity1,
                testIdentity2,
                testIdentity3,
                testIdentity4
            };

            var repository = new Repository<TestIdentity>(testIdentities);

            // Act
            repository.Remove(new List<TestIdentity> { testIdentity1, testIdentity2 });

            // Assert                        
            CollectionAssert.DoesNotContain(testIdentities, testIdentity1);
            CollectionAssert.DoesNotContain(testIdentities, testIdentity2);
            CollectionAssert.Contains(testIdentities, testIdentity3);
            CollectionAssert.Contains(testIdentities, testIdentity4);
        }
    }
}
#endif