using NUnit.Framework;
using System;

namespace TestingDemo.Tests
{

    [TestFixture]
    public class PersonTest
    {
        
        [Test]
        [TestCase("Gosho")]
        [TestCase(null)]
        [TestCase(" ")]
        [TestCase("1")]
        public void DoesPersonNameWorksProperly(string name)
        {
            Person person = new Person(name);

            string expectedResult = $"My name is {name}";
            string actualResult = person.PersonName();

            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
