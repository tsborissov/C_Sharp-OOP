using System;
using NUnit.Framework;

[TestFixture]
public class DummyTests
{
    [Test]
    public void DummyLosesHealthIfAttacked()
    {
        Dummy dummy = new Dummy(100, 100);

        dummy.TakeAttack(10);

        int expectedResult = 90;
        int actualResult = dummy.Health;

        Assert.That(expectedResult, Is.EqualTo(actualResult));
    }

    [Test]
    public void DeadDummyThrowsExceptionIfAttacked()
    {
        Dummy dummy = new Dummy(0, 100);

        Assert.Throws<InvalidOperationException>(() => dummy.TakeAttack(10));
    }

    [Test]
    public void DeadDummyCanGiveExperience()
    {
        Dummy dummy = new Dummy(0, 10);

        int expectedResult = 10;
        int actualResult = dummy.GiveExperience();

        Assert.That(actualResult, Is.EqualTo(expectedResult));
    }

    [Test]
    public void AliveDummyCanNotGiveExperience()
    {
        Dummy dummy = new Dummy(100, 100);

        Assert.Throws<InvalidOperationException>(() => dummy.GiveExperience());
    }
}
