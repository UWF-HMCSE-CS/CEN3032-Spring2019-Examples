using System;

using Domain;
using Services;
using RepositoryInterfaces;
using Moq;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ServiceTests
{
    [TestClass]
    public class AccountServiceTests
    {
        private Mock<IAccountRepository> mockRepository;
        private AccountService sut;

        [TestInitialize]
        public void Setup()
        {
            this.mockRepository = new Mock<IAccountRepository>();
            this.sut = new AccountService(mockRepository.Object);

        }

        [TestMethod]
        public void AddingTransactionToAccountDelegatesToAccountInstance()
        {
            //Arrange
            Account account = new Account();
            this.mockRepository.Setup(r => r.GetByName("Trading Account")).Returns(account);

            //Act
            sut.AddTransactionToAccount("Trading Account", 200m);

            //Assert
            Assert.AreEqual(200m, account.Balance);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CannotCreateAccountServiceWithNullRepository()
        {
            //Act
            new AccountService(null);
        }

    }
}
