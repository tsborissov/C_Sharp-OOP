using NUnit.Framework;

namespace Bank.Tests
{
    [TestFixture]
    public class BankAccountTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        [TestCase(2000)]
        [TestCase(0.02)]

        public void AccountInitializeWithPositiveValue(decimal amount)
        {
            BankAccount account = new BankAccount(amount);
            
            Assert.That(account.Amount, Is.EqualTo(amount));
        }
    }
}