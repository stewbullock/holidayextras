#if DEBUG
using ASM.HolidayExtra.Core.Domain;
using ASM.HolidayExtra.Service.Model;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace ASM.HolidayExtra.Core.UnitTest.Domain
{
    class UserMapperTest
    {
        [SetUp]
        public void Setup()
        { }

        [Test]
        public void MapReturnServiceUsersFromDomain()
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

            // Act
            var result = mapper.Map(domainUsers);

            // Assert
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(1, result[0].Id);
            Assert.AreEqual("Jack", result[0].Forename);
            Assert.AreEqual("Jones", result[0].Surname);
            Assert.AreEqual("jack.jones@test.co.uk", result[0].Email);
            Assert.AreEqual(DateTime.Parse("1 Dec 2019 10:00"), result[0].Created);
            Assert.AreEqual("Test User", result[0].CreatedBy);
            Assert.IsNull(result[0].Changed);
            Assert.IsNull(result[0].ChangedBy);
        }

        [Test]
        public void MapReturnNoServiceUsersFromDomainWhenEmpty()
        {
            // Arrange
            var mapper = new UserMapper();
            var domainUsers = new List<Model.User> { };

            // Act
            var result = mapper.Map(domainUsers);

            // Assert
            Assert.AreEqual(0, result.Count);
        }

        [Test]
        public void MapReturnNoServiceUsersFromDomainWhenNotDefined()
        {
            // Arrange
            var mapper = new UserMapper();

            // Act
            var result = mapper.Map(null);

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public void MapExpectChangeToDomain()
        {
            // Arrange
            var mapper = new UserMapper();
            var domainUser = new Model.User
            {
                Id = 1,
                Forename = "Jack",
                Surname = "Jones",
                Email = "jack.jones@test.co.uk",
                Created = DateTime.Parse("1 Dec 2019 10:00"),
                CreatedBy = "Test User"
            };

            var change = new ChangeUser
            {
                Id = 1,
                Forename = "Jerry",
                Surname = "Jones(Cowboys)",
                Email = "jerry.jonesofcowboys@test.co.uk",
                ChangedBy = "Test User 2"
            };

            // Act
            mapper.Map(change, domainUser);

            // Assert
            Assert.AreEqual(1, domainUser.Id);
            Assert.AreEqual("Jerry", domainUser.Forename);
            Assert.AreEqual("Jones(Cowboys)", domainUser.Surname);
            Assert.AreEqual("jerry.jonesofcowboys@test.co.uk", domainUser.Email);
            Assert.AreEqual(DateTime.Parse("1 Dec 2019 10:00"), domainUser.Created);
            Assert.AreEqual("Test User", domainUser.CreatedBy);
            Assert.IsNotNull(domainUser.Changed);
            Assert.AreEqual("Test User 2", domainUser.ChangedBy);
        }

        [Test]
        public void MapExpectNoChangeToDomainWhenMismatchInIdentifier()
        {
            // Arrange
            var mapper = new UserMapper();
            var domainUser = new Model.User
            {
                Id = 1,
                Forename = "Jack",
                Surname = "Jones",
                Email = "jack.jones@test.co.uk",
                Created = DateTime.Parse("1 Dec 2019 10:00"),
                CreatedBy = "Test User"
            };

            var change = new ChangeUser
            {
                Id = 2,
                Forename = "Jerry",
                Surname = "Jones(Cowboys)",
                Email = "jerry.jonesofcowboys@test.co.uk",
                ChangedBy = "Test User 2"
            };

            // Act
            mapper.Map(change, domainUser);

            // Assert
            Assert.AreEqual(1, domainUser.Id);
            Assert.AreEqual("Jack", domainUser.Forename);
            Assert.AreEqual("Jones", domainUser.Surname);
            Assert.AreEqual("jack.jones@test.co.uk", domainUser.Email);
            Assert.AreEqual(DateTime.Parse("1 Dec 2019 10:00"), domainUser.Created);
            Assert.AreEqual("Test User", domainUser.CreatedBy);
            Assert.IsNull(domainUser.Changed);
            Assert.IsNull(domainUser.ChangedBy);
        }

        [Test]
        public void MapExpectNoChangeToDomainWhenChangeUserUndefined()
        {
            // Arrange
            var mapper = new UserMapper();
            var domainUser = new Model.User
            {
                Id = 1,
                Forename = "Jack",
                Surname = "Jones",
                Email = "jack.jones@test.co.uk",
                Created = DateTime.Parse("1 Dec 2019 10:00"),
                CreatedBy = "Test User"
            };

            // Act
            mapper.Map(null, domainUser);

            // Assert
            Assert.AreEqual(1, domainUser.Id);
            Assert.AreEqual("Jack", domainUser.Forename);
            Assert.AreEqual("Jones", domainUser.Surname);
            Assert.AreEqual("jack.jones@test.co.uk", domainUser.Email);
            Assert.AreEqual(DateTime.Parse("1 Dec 2019 10:00"), domainUser.Created);
            Assert.AreEqual("Test User", domainUser.CreatedBy);
            Assert.IsNull(domainUser.Changed);
            Assert.IsNull(domainUser.ChangedBy);
        }

        [Test]
        public void MapExpectDomainUserFromServiceTrackUser()
        {
            // Arrange
            var mapper = new UserMapper();
            var trackUser = new TrackUser
            {
                Forename = "Jack",
                Surname = "Jones",
                Email = "jack.jones@test.co.uk",
                CreatedBy = "Test User"
            };

            // Act
            var domainUser = mapper.Map(trackUser, 100);

            // Assert
            Assert.AreEqual(100, domainUser.Id);
            Assert.AreEqual("Jack", domainUser.Forename);
            Assert.AreEqual("Jones", domainUser.Surname);
            Assert.AreEqual("jack.jones@test.co.uk", domainUser.Email);
            Assert.IsNotNull(domainUser.Created);
            Assert.AreEqual("Test User", domainUser.CreatedBy);
            Assert.IsNull(domainUser.Changed);
            Assert.IsNull(domainUser.ChangedBy);
        }

        [Test]
        public void MapExpectUndefinedDomainUserFromUndefinedServiceTrackUser()
        {
            // Arrange
            var mapper = new UserMapper();

            // Act
            var domainUser = mapper.Map(null, 100);

            // Assert
            Assert.IsNull(domainUser);
        }
    }
}
#endif