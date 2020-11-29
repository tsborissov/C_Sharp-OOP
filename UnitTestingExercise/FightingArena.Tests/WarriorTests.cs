using System;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class WarriorTests
    {
        private Warrior warrior;
        private Warrior enemyWarior;
        private const string WARRIOR_NAME = "WarriorName";
        private const string ENEMY_NAME = "EnemyName";
        private const int WARRIOR_DAMAGE = 15;
        private const int WARRIOR_HP = 50;
        private const int MIN_ATTACK_HP = 30;

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestWarriorConstructorWithValidData()
        {
            warrior = new Warrior(WARRIOR_NAME, WARRIOR_DAMAGE, WARRIOR_HP);

            string expectedName = WARRIOR_NAME;
            int expectedDamage = WARRIOR_DAMAGE;
            int expectedHp = WARRIOR_HP;

            string actualName = this.warrior.Name;
            int actualDamage = this.warrior.Damage;
            int actualHp = this.warrior.HP;
            
            Assert.AreEqual(actualName, expectedName);
            Assert.AreEqual(actualDamage, expectedDamage);
            Assert.AreEqual(actualHp, expectedHp);
        }

        [Test]
        [TestCase(null)]
        [TestCase(" ")]
        public void NullOrWhiteSpaceNameShouldThrowException(string name)
        {
            Assert.Throws<ArgumentException>(() => new Warrior(name, WARRIOR_DAMAGE, WARRIOR_HP));
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        public void NegativeOrZeroDamageShouldThrowException(int damage)
        {
            Assert.Throws<ArgumentException>(() => new Warrior(WARRIOR_NAME, damage, WARRIOR_HP));
        }

        [Test]
        [TestCase(-1)]
        public void NegativeHPShouldThrowException(int hp)
        {
            Assert.Throws<ArgumentException>(() => new Warrior(WARRIOR_NAME, WARRIOR_DAMAGE, hp));
        }

        [Test]
        [TestCase(MIN_ATTACK_HP)]
        [TestCase(MIN_ATTACK_HP - 1)]
        public void AttackWithLowHpShouldThrowException(int hp)
        {
            this.warrior = new Warrior(WARRIOR_NAME, WARRIOR_DAMAGE, hp);
            this.enemyWarior = new Warrior(ENEMY_NAME, WARRIOR_DAMAGE, WARRIOR_HP);

            Assert.Throws<InvalidOperationException>(() => this.warrior.Attack(enemyWarior));
        }

        [Test]
        [TestCase(MIN_ATTACK_HP)]
        [TestCase(MIN_ATTACK_HP - 1)]
        public void AttackEnemyWithLowHpShouldThrowException(int hp)
        {
            this.warrior = new Warrior(WARRIOR_NAME, WARRIOR_DAMAGE, WARRIOR_HP);
            this.enemyWarior = new Warrior(ENEMY_NAME, WARRIOR_DAMAGE, hp);

            Assert.Throws<InvalidOperationException>(() => this.warrior.Attack(enemyWarior));
        }

        [Test]
        [TestCase(WARRIOR_HP + 1)]
        [TestCase(WARRIOR_HP + 2)]
        public void AttackStrongerEnemyShouldThrowException(int damage)
        {
            this.warrior = new Warrior(WARRIOR_NAME, WARRIOR_DAMAGE, WARRIOR_HP);
            this.enemyWarior = new Warrior(ENEMY_NAME, damage, WARRIOR_HP);

            Assert.Throws<InvalidOperationException>(() => this.warrior.Attack(enemyWarior));
        }

        [Test]
        public void SuccessAttackShouldDecreaseWarriorHp()
        {
            this.warrior = new Warrior(WARRIOR_NAME, WARRIOR_DAMAGE, WARRIOR_HP);
            this.enemyWarior = new Warrior(ENEMY_NAME, WARRIOR_DAMAGE, WARRIOR_HP);

            this.warrior.Attack(enemyWarior);

            int expectedWarriorHp = WARRIOR_HP - WARRIOR_DAMAGE;
            int actualWarriorHp = warrior.HP;

            Assert.AreEqual(actualWarriorHp, expectedWarriorHp);
        }

        [Test]
        [TestCase(WARRIOR_HP)]
        [TestCase(WARRIOR_HP + 1)]
        [TestCase(WARRIOR_HP + 2)]
        [TestCase(WARRIOR_HP + 3)]
        public void GreaterWarriorDamageThanEnemyHpAfterAttackShouldZeroEnemyHp(int damage)
        {
            this.warrior = new Warrior(WARRIOR_NAME, damage, WARRIOR_HP);
            this.enemyWarior = new Warrior(ENEMY_NAME, WARRIOR_DAMAGE, WARRIOR_HP);

            this.warrior.Attack(enemyWarior);

            int expectedEnemyHp = 0;
            int actualEnemyHp = enemyWarior.HP;

            Assert.AreEqual(actualEnemyHp, expectedEnemyHp);
        }

        [Test]
        public void EqualOrLowerWarriorDamagetThanEnemyHpAfterAttackShouldDecreaseEnemyHp()
        {
            this.warrior = new Warrior(WARRIOR_NAME, WARRIOR_DAMAGE, WARRIOR_HP);
            this.enemyWarior = new Warrior(ENEMY_NAME, WARRIOR_DAMAGE, WARRIOR_HP);

            this.warrior.Attack(enemyWarior);

            int expectedEnemyHp = WARRIOR_HP - WARRIOR_DAMAGE;
            int actualEnemyHp = enemyWarior.HP;

            Assert.AreEqual(actualEnemyHp, expectedEnemyHp);
        }

    }
}