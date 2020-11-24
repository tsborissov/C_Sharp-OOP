using NUnit.Framework;
using System;

[TestFixture]
public class AxeTests
{
    [Test]
    public void WeaponLosesDurabilityAfterEachAttack()
    {
        // Arrange
        Dummy target = new Dummy(100, 100);
        Axe axe = new Axe(10, 10);

        // Act
        axe.Attack(target);

        // Assert
        var expectedResult = 9;
        var actualResult = axe.DurabilityPoints;

        Assert.That(expectedResult, Is.EqualTo(actualResult), "Axe Durability doesn't change after attack.");
    }

    [Test]
    [TestCase(100, 100, 10, 0)]
    [TestCase(100, 100, 10, -1)]
    public void AttackingWithBrokenWeapon(int health, int experience, int attack, int durability)
    {
        // Arrange
        Dummy target = new Dummy(health, experience);
        Axe axe = new Axe(attack, durability);

        // Act - Assert
        Assert.Throws<InvalidOperationException>(() => axe.Attack(target));
    }
}