using FakeAxeAndDummy;
using FakeAxeAndDummy.Contracts;
using FakeAxeAndDummy.Tests.Fakes;
using Moq;
using NUnit.Framework;

[TestFixture]
public class HeroTests
{
    [Test]
    public void GivenHeroShouldReceiveExperienceWhenTargetDies()
    {
        //IWeapon fakeWeapon = new FakeWeapon();
        //ITarget fakeTarget = new FakeTarget();
        //Hero hero = new Hero("TestHero", fakeWeapon);

        Mock<IWeapon> mockWeapon = new Mock<IWeapon>();
        //mockWeapon.Setup(w => w.AttackPoints).Returns(10);
        //mockWeapon.Setup(w => w.DurabilityPoints).Returns(10);

        Mock<ITarget> mockTarget = new Mock<ITarget>();
        mockTarget.Setup(t => t.GiveExperience()).Returns(25);
        mockTarget.Setup(t => t.IsDead()).Returns(true);

        Hero hero = new Hero("TestHero", mockWeapon.Object);

        hero.Attack(mockTarget.Object);

        int expectedExperience = 25;
        int actualExperience = hero.Experience;

        Assert.AreEqual(actualExperience, expectedExperience);
    }
}