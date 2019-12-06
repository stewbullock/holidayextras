#if DEBUG
using ASM.Core;
using ASM.Core.Serialisation;
using ASM.HolidayExtra.Core.Domain;
using ASM.HolidayExtra.Core.Repository;
using ASM.HolidayExtra.Service.Model;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ASM.HolidayExtra.Core.UnitTest.Domain
{
    class UserResolverTest
    {
        [SetUp]
        public void Setup()
        { }

        [Test]
        public void RetrieveAllReturnServiceUserMappedFromDomain()
        {
            // Arrange
            var mapper = new UserMapper();
            var deSerializer = new DeSerializer();
            var domainUsers = new List<Model.User>
            {
                new Model.User
                {
                    Id = 1,
                    Forename = "Jack",
                    Surname = "Jones",
                    Email = "jack.jones@test.co.uk",
                    Created = DateTime.Parse("1 Dec 2019 10:00"),
                    CreatedBy = "Test User"
                }
            };
            var repository = new UserRepository(domainUsers);
            var resolver = new UserResolver(repository, mapper, deSerializer);

            // Act
            var result = resolver.RetrieveAll();
            var serviceUsers = result.ResponseObject;

            // Assert
            Assert.IsTrue(ResponseState.Success == result.ResponseState);
            Assert.IsNotNull(serviceUsers[0]);
            Assert.AreEqual(1, serviceUsers.Count);
            Assert.AreEqual(1, serviceUsers[0].Id);
            Assert.AreEqual("Jack", serviceUsers[0].Forename);
            Assert.AreEqual("Jones", serviceUsers[0].Surname);
            Assert.AreEqual("jack.jones@test.co.uk", serviceUsers[0].Email);
            Assert.AreEqual(DateTime.Parse("1 Dec 2019 10:00"), serviceUsers[0].Created);
            Assert.AreEqual("Test User", serviceUsers[0].CreatedBy);
            Assert.IsNull(serviceUsers[0].Changed);
            Assert.IsNull(serviceUsers[0].ChangedBy);
        }

        [Test]
        public void CreateReturnServiceUsersExtendedWithTrackedUser()
        {
            // Arrange
            var mapper = new UserMapper();
            var domainUsers = new List<Model.User>
            {
                new Model.User
                {
                    Id = 1,
                    Forename = "Jack",
                    Surname = "Jones",
                    Email = "jack.jones@test.co.uk",
                    Created = DateTime.Parse("1 Dec 2019 10:00"),
                    CreatedBy = "Test User"
                }
            };

            var trackUser = new TrackUser
            {
                Forename = "Joe",
                Surname = "Bloggs",
                Email = "joe.bloggs@test.co.uk",
                CreatedBy = "Test User 1"
            };
            var repository = new UserRepository(domainUsers);
            var deSerializer = new DeSerializer();
            var resolver = new UserResolver(repository, mapper, deSerializer);

            // Act
            var result = resolver.Create(trackUser);
            var serviceUsers = result.ResponseObject;

            // Assert
            Assert.IsTrue(ResponseState.Success == result.ResponseState);
            Assert.AreEqual(2, serviceUsers.Count);
            Assert.AreEqual(1, serviceUsers[0].Id);
            Assert.AreEqual("Jack", serviceUsers[0].Forename);
            Assert.AreEqual("Jones", serviceUsers[0].Surname);
            Assert.AreEqual("jack.jones@test.co.uk", serviceUsers[0].Email);
            Assert.AreEqual(DateTime.Parse("1 Dec 2019 10:00"), serviceUsers[0].Created);
            Assert.AreEqual("Test User", serviceUsers[0].CreatedBy);
            Assert.IsNull(serviceUsers[0].Changed);
            Assert.IsNull(serviceUsers[0].ChangedBy);
            Assert.AreEqual(2, serviceUsers[1].Id);
            Assert.AreEqual("Joe", serviceUsers[1].Forename);
            Assert.AreEqual("Bloggs", serviceUsers[1].Surname);
            Assert.AreEqual("joe.bloggs@test.co.uk", serviceUsers[1].Email);
            Assert.AreEqual("Test User 1", serviceUsers[1].CreatedBy);
            Assert.IsNull(serviceUsers[1].Changed);
            Assert.IsNull(serviceUsers[1].ChangedBy);
        }

        [Test]
        public void CreateReturnServiceUsersExtendedWithTrackedUserFromXml()
        {
            // Arrange
            var mapper = new UserMapper();
            var domainUsers = new List<Model.User>
            {
                new Model.User
                {
                    Id = 1,
                    Forename = "Jack",
                    Surname = "Jones",
                    Email = "jack.jones@test.co.uk",
                    Created = DateTime.Parse("1 Dec 2019 10:00"),
                    CreatedBy = "Test User"
                }
            };

            var trackUser = "<?xml version=\"1.0\" encoding=\"utf - 8\"?><TrackUser><Forename>Joe</Forename><Surname>Bloggs</Surname><Email>joe.bloggs@test.co.uk</Email><CreatedBy>Test User 1</CreatedBy></TrackUser>";

            var repository = new UserRepository(domainUsers);
            var deSerializer = new DeSerializer();
            var resolver = new UserResolver(repository, mapper, deSerializer);

            // Act
            var result = resolver.Create(trackUser);
            var serviceUsers = result.ResponseObject;

            // Assert
            Assert.IsTrue(ResponseState.Success == result.ResponseState);
            Assert.AreEqual(2, serviceUsers.Count);
            Assert.AreEqual(1, serviceUsers[0].Id);
            Assert.AreEqual("Jack", serviceUsers[0].Forename);
            Assert.AreEqual("Jones", serviceUsers[0].Surname);
            Assert.AreEqual("jack.jones@test.co.uk", serviceUsers[0].Email);
            Assert.AreEqual(DateTime.Parse("1 Dec 2019 10:00"), serviceUsers[0].Created);
            Assert.AreEqual("Test User", serviceUsers[0].CreatedBy);
            Assert.IsNull(serviceUsers[0].Changed);
            Assert.IsNull(serviceUsers[0].ChangedBy);
            Assert.AreEqual(2, serviceUsers[1].Id);
            Assert.AreEqual("Joe", serviceUsers[1].Forename);
            Assert.AreEqual("Bloggs", serviceUsers[1].Surname);
            Assert.AreEqual("joe.bloggs@test.co.uk", serviceUsers[1].Email);
            Assert.AreEqual("Test User 1", serviceUsers[1].CreatedBy);
            Assert.IsNull(serviceUsers[1].Changed);
            Assert.IsNull(serviceUsers[1].ChangedBy);
        }

        [Test]
        public void CreateReturnFailedResponseWhenTrackedUserMalformedXml()
        {
            // Arrange
            var mapper = new UserMapper();
            var domainUsers = new List<Model.User>
            {
                new Model.User
                {
                    Id = 1,
                    Forename = "Jack",
                    Surname = "Jones",
                    Email = "jack.jones@test.co.uk",
                    Created = DateTime.Parse("1 Dec 2019 10:00"),
                    CreatedBy = "Test User"
                }
            };

            var trackUser = "<?xml version=\"1.0\" encoding=\"utf - 8\"?><ChangeUser><Forename>Joe</Forename><Surname>Bloggs</Surname><Email>joe.bloggs@test.co.uk</Email><CreatedBy>Test User 1</CreatedBy></ChangeUser>";

            var repository = new UserRepository(domainUsers);
            var deSerializer = new DeSerializer();
            var resolver = new UserResolver(repository, mapper, deSerializer);

            // Act
            var result = resolver.Create(trackUser);

            // Assert
            Assert.IsTrue(ResponseState.Failed == result.ResponseState);
        }

        [Test]
        public void ChangeReturnServiceUserChangedAndMappedFromDomain()
        {
            // Arrange
            var mapper = new UserMapper();
            var domainUsers = new List<Model.User>
            {
                new Model.User
                {
                    Id = 1,
                    Forename = "Jack",
                    Surname = "Jones",
                    Email = "jack.jones@test.co.uk",
                    Created = DateTime.Parse("1 Dec 2019 10:00"),
                    CreatedBy = "Test User"
                }
            };
            var repository = new UserRepository(domainUsers);
            var deSerializer = new DeSerializer();
            var resolver = new UserResolver(repository, mapper, deSerializer);

            var changeUser = new ChangeUser
            {
                Id = 1,
                Forename = "Joe Joe",
                Surname = "Bloggs",
                Email = "joe.bloggs@test.co.uk",
                ChangedBy = "Test User 1"
            };

            // Act
            var result = resolver.Change(changeUser);
            var serviceUsers = result.ResponseObject;

            // Assert
            Assert.IsTrue(ResponseState.Success == result.ResponseState);
            Assert.AreEqual(1, serviceUsers.Count);
            Assert.AreEqual(1, serviceUsers[0].Id);
            Assert.AreEqual("Joe Joe", serviceUsers[0].Forename);
            Assert.AreEqual("Bloggs", serviceUsers[0].Surname);
            Assert.AreEqual("joe.bloggs@test.co.uk", serviceUsers[0].Email);
            Assert.AreEqual(DateTime.Parse("1 Dec 2019 10:00"), serviceUsers[0].Created);
            Assert.AreEqual("Test User", serviceUsers[0].CreatedBy);
            Assert.AreEqual("Test User 1", serviceUsers[0].ChangedBy);
            Assert.IsNotNull(serviceUsers[0].Changed);
        }

        [Test]
        public void ChangeReturnFailedResponseWithMismatchInChangeIdentifier()
        {
            // Arrange
            var mapper = new UserMapper();
            var domainUsers = new List<Model.User>
            {
                new Model.User
                {
                    Id = 1,
                    Forename = "Jack",
                    Surname = "Jones",
                    Email = "jack.jones@test.co.uk",
                    Created = DateTime.Parse("1 Dec 2019 10:00"),
                    CreatedBy = "Test User"
                }
            };
            var repository = new UserRepository(domainUsers);
            var deSerializer = new DeSerializer();
            var resolver = new UserResolver(repository, mapper, deSerializer);

            var changeUser = new ChangeUser
            {
                Id = 2,
                Forename = "Joe Joe",
                Surname = "Bloggs",
                Email = "joe.bloggs@test.co.uk",
                ChangedBy = "Test User 1"
            };

            // Act
            var result = resolver.Change(changeUser);

            // Assert
            Assert.IsTrue(ResponseState.Failed == result.ResponseState);
            Assert.IsNull(result.ResponseObject);
            Assert.AreEqual("User with Id:2 cannot be found for change", result.Failures[0]);
        }

        [Test]
        public void DeleteReturnServiceUsersExcludingDeletedFromDomain()
        {
            // Arrange
            var mapper = new UserMapper();
            var domainUsers = new List<Model.User>
            {
                new Model.User
                {
                    Id = 1,
                    Forename = "Jack",
                    Surname = "Jones",
                    Email = "jack.jones@test.co.uk",
                    Created = DateTime.Parse("1 Dec 2019 10:00"),
                    CreatedBy = "Test User"
                },
                 new Model.User
                {
                    Id = 2,
                    Forename = "Jim",
                    Surname = "Smith",
                    Email = "jim.smith@test.co.uk"
                }
            };
            var repository = new UserRepository(domainUsers);
            var deSerializer = new DeSerializer();
            var resolver = new UserResolver(repository, mapper, deSerializer);

            var deleteUser = new DeleteUser
            {
                Id = 1
            };

            // Act
            var result = resolver.Delete(deleteUser);
            var serviceUsers = result.ResponseObject;

            // Assert
            Assert.IsTrue(ResponseState.Success == result.ResponseState);
            Assert.AreEqual(1, serviceUsers.Count);
            Assert.AreEqual(2, serviceUsers[0].Id);
        }

        [Test]
        public void DeleteReturnFailedResponseWithMismatchInDeleteIdentifier()
        {
            // Arrange
            var mapper = new UserMapper();
            var domainUsers = new List<Model.User>
            {
                new Model.User
                {
                    Id = 1,
                    Forename = "Jack",
                    Surname = "Jones",
                    Email = "jack.jones@test.co.uk",
                    Created = DateTime.Parse("1 Dec 2019 10:00"),
                    CreatedBy = "Test User"
                }
            };
            var repository = new UserRepository(domainUsers);
            var deSerializer = new DeSerializer();
            var resolver = new UserResolver(repository, mapper, deSerializer);

            var deleteUser = new DeleteUser
            {
                Id = 3
            };

            // Act
            var result = resolver.Delete(deleteUser);

            // Assert
            Assert.IsTrue(ResponseState.Failed == result.ResponseState);
            Assert.IsNull(result.ResponseObject);
            Assert.AreEqual("User with Id:3 cannot be found for deletion", result.Failures[0]);
        }

        [Test]
        public void SearchReturnUserThatMatchOnIdentifier()
        {
            // Arrange
            var mapper = new UserMapper();
            var domainUsers = new List<Model.User>
            {
                new Model.User
                {
                    Id = 1,
                    Forename = "Jack",
                    Surname = "Jones",
                    Email = "jack.jones@test.co.uk",
                    Created = DateTime.Parse("1 Dec 2019 10:00"),
                    CreatedBy = "Test User"
                },
                 new Model.User
                {
                    Id = 2,
                    Forename = "Jim",
                    Surname = "Smith",
                    Email = "jim.smith@test.co.uk"
                },
                 new Model.User
                {
                    Id = 3,
                    Forename = "Jim",
                    Surname = "Loner",
                    Email = "jim.loner@test.co.uk"
                }
                 ,
                 new Model.User
                {
                    Id = 4,
                    Forename = "Tom",
                    Surname = "Jones",
                    Email = "tom.jones@test.co.uk"
                }
            };
            var repository = new UserRepository(domainUsers);
            var deSerializer = new DeSerializer();
            var resolver = new UserResolver(repository, mapper, deSerializer);

            var searchUser = new SearchUser
            {
                Id = 1,
                Forename = ""
            };

            // Act
            var result = resolver.Search(searchUser);
            var serviceUsers = result.ResponseObject;

            // Assert
            Assert.IsTrue(ResponseState.Success == result.ResponseState);
            Assert.AreEqual(1, serviceUsers.Count);
            Assert.AreEqual("Jack", serviceUsers[0].Forename);
            Assert.AreEqual("Jones", serviceUsers[0].Surname);
        }

        [Test]
        public void SearchReturnUserThatMatchOnIdentifierOrForename()
        {
            // Arrange
            var mapper = new UserMapper();
            var domainUsers = new List<Model.User>
            {
                new Model.User
                {
                    Id = 1,
                    Forename = "Jack",
                    Surname = "Jones",
                    Email = "jack.jones@test.co.uk",
                    Created = DateTime.Parse("1 Dec 2019 10:00"),
                    CreatedBy = "Test User"
                },
                 new Model.User
                {
                    Id = 2,
                    Forename = "Jim",
                    Surname = "Smith",
                    Email = "jim.smith@test.co.uk"
                },
                 new Model.User
                {
                    Id = 3,
                    Forename = "Jim",
                    Surname = "Loner",
                    Email = "jim.loner@test.co.uk"
                }
                 ,
                 new Model.User
                {
                    Id = 4,
                    Forename = "Tom",
                    Surname = "Jones",
                    Email = "tom.jones@test.co.uk"
                }
            };
            var repository = new UserRepository(domainUsers);
            var deSerializer = new DeSerializer();
            var resolver = new UserResolver(repository, mapper, deSerializer);

            var searchUser = new SearchUser
            {
                Id = 1,
                Forename = "Tom"
            };

            // Act
            var result = resolver.Search(searchUser);
            var serviceUsers = result.ResponseObject;

            // Assert
            Assert.IsTrue(ResponseState.Success == result.ResponseState);
            Assert.AreEqual(2, serviceUsers.Count);
            Assert.IsTrue(serviceUsers.Any(user => user.Id == 1));
            Assert.IsTrue(serviceUsers.Any(user => user.Id == 4));
        }

        [Test]
        public void SearchReturnUserThatMatchOnForenameOrSurname()
        {
            // Arrange
            var mapper = new UserMapper();
            var domainUsers = new List<Model.User>
            {
                new Model.User
                {
                    Id = 1,
                    Forename = "Jack",
                    Surname = "Jones",
                    Email = "jack.jones@test.co.uk",
                    Created = DateTime.Parse("1 Dec 2019 10:00"),
                    CreatedBy = "Test User"
                },
                 new Model.User
                {
                    Id = 2,
                    Forename = "Jim",
                    Surname = "Smith",
                    Email = "jim.smith@test.co.uk"
                },
                 new Model.User
                {
                    Id = 3,
                    Forename = "Jim",
                    Surname = "Loner",
                    Email = "jim.loner@test.co.uk"
                }
                 ,
                 new Model.User
                {
                    Id = 4,
                    Forename = "Tom",
                    Surname = "Jones",
                    Email = "tom.jones@test.co.uk"
                }
            };
            var repository = new UserRepository(domainUsers);
            var deSerializer = new DeSerializer();
            var resolver = new UserResolver(repository, mapper, deSerializer);

            var searchUser = new SearchUser
            {
                Forename = "Jim",
                Surname = "one"
            };

            // Act
            var result = resolver.Search(searchUser);
            var serviceUsers = result.ResponseObject;

            // Assert
            Assert.IsTrue(ResponseState.Success == result.ResponseState);
            Assert.AreEqual(4, serviceUsers.Count);
            Assert.IsTrue(serviceUsers.Any(user => user.Id == 1));
            Assert.IsTrue(serviceUsers.Any(user => user.Id == 2));
            Assert.IsTrue(serviceUsers.Any(user => user.Id == 3));
            Assert.IsTrue(serviceUsers.Any(user => user.Id == 4));
        }

        [Test]
        public void SearchReturnEmptyUserList()
        {
            // Arrange
            var mapper = new UserMapper();
            var domainUsers = new List<Model.User>
            {
                new Model.User
                {
                    Id = 1,
                    Forename = "Jack",
                    Surname = "Jones",
                    Email = "jack.jones@test.co.uk",
                    Created = DateTime.Parse("1 Dec 2019 10:00"),
                    CreatedBy = "Test User"
                },
                 new Model.User
                {
                    Id = 2,
                    Forename = "Jim",
                    Surname = "Smith",
                    Email = "jim.smith@test.co.uk"
                },
                 new Model.User
                {
                    Id = 3,
                    Forename = "Jim",
                    Surname = "Loner",
                    Email = "jim.loner@test.co.uk"
                }
                 ,
                 new Model.User
                {
                    Id = 4,
                    Forename = "Tom",
                    Surname = "Jones",
                    Email = "tom.jones@test.co.uk"
                }
            };
            var repository = new UserRepository(domainUsers);
            var deSerializer = new DeSerializer();
            var resolver = new UserResolver(repository, mapper, deSerializer);

            var searchUser = new SearchUser
            {
                Id = 99
            };

            // Act
            var result = resolver.Search(searchUser);
            var serviceUsers = result.ResponseObject;

            // Assert
            Assert.IsTrue(ResponseState.Success == result.ResponseState);
            Assert.AreEqual(0, serviceUsers.Count);
        }
    }
}
#endif