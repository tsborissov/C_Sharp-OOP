using System;
using NUnit.Framework;
using System.Collections.Generic;

namespace Tests
{
    [TestFixture]
    public class ArenaTests
    {
        private Warrior warrior;
        private Warrior enemyWarrior;
        private Arena arena;
        private const string WARRIOR_NAME = "WarriorName";
        private const string ENEMY_NAME = "EnemyName";
        private const int WARRIOR_DAMAGE = 15;
        private const int WARRIOR_HP = 50;
        private const int MIN_ATTACK_HP = 30;

        private List<Warrior> warriors;

        [SetUp]
        public void Setup()
        {
            this.warrior = new Warrior(WARRIOR_NAME, WARRIOR_DAMAGE, WARRIOR_HP);
            this.enemyWarrior = new Warrior(ENEMY_NAME, WARRIOR_DAMAGE, WARRIOR_HP);
            this.arena = new Arena();
            this.warriors = new List<Warrior>();

            this.arena.Enroll(this.warrior);
            this.arena.Enroll(this.enemyWarrior);
        }

        [Test]
        public void ArenaConstructorTest()
        {
            this.arena = new Arena();

            int expectedWarriorsCount = 0;
            int actualWarriorsCount = arena.Count;

            Assert.AreEqual(actualWarriorsCount, expectedWarriorsCount);
        }

        [Test]
        public void SuccessfullEnrolmentShouldIncreaseArenaCount()
        {
            this.warrior = new Warrior(WARRIOR_NAME, WARRIOR_DAMAGE, WARRIOR_HP);
            this.arena = new Arena();

            this.arena.Enroll(this.warrior);

            int expectedCount = 1;
            int actualCount = arena.Count;

            Assert.AreEqual(actualCount, expectedCount);
        }

        [Test]
        public void EnrollingWarriorWithExistingNameShouldThrowException()
        {
            Assert.Throws<InvalidOperationException>(() => this.arena.Enroll(this.warrior));
        }

        [Test]
        public void TestWarriorsCollection()
        {
            this.warriors.Add(warrior);
            this.warriors.Add(enemyWarrior);

            CollectionAssert.AreEqual(this.arena.Warriors, this.warriors);
        }

        [Test]
        [TestCase("NameNotInList")]
        public void NonExistentAttackerShouldThrowException(string name)
        {
            Assert.Throws<InvalidOperationException>(() => this.arena.Fight(name, ENEMY_NAME));
        }

        [Test]
        [TestCase("NameNotInList")]
        public void NonExistentDefenderShouldThrowException(string name)
        {
            Assert.Throws<InvalidOperationException>(() => this.arena.Fight(WARRIOR_NAME, name));
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
        public void FightWithLowHpShouldThrowException(int hp)
        {
            this.warrior = new Warrior(WARRIOR_NAME, WARRIOR_DAMAGE, hp);
            this.enemyWarrior = new Warrior(ENEMY_NAME, WARRIOR_DAMAGE, WARRIOR_HP);
            this.arena = new Arena();
            this.arena.Enroll(warrior);
            this.arena.Enroll(enemyWarrior);

            Assert.Throws<InvalidOperationException>(() => this.arena.Fight(WARRIOR_NAME, ENEMY_NAME));
        }

        [Test]
        [TestCase(MIN_ATTACK_HP)]
        [TestCase(MIN_ATTACK_HP - 1)]
        public void FightEnemyWithLowHpShouldThrowException(int hp)
        {
            this.warrior = new Warrior(WARRIOR_NAME, WARRIOR_DAMAGE, WARRIOR_HP);
            this.enemyWarrior = new Warrior(ENEMY_NAME, WARRIOR_DAMAGE, hp);
            this.arena = new Arena();
            this.arena.Enroll(warrior);
            this.arena.Enroll(enemyWarrior);

            Assert.Throws<InvalidOperationException>(() => this.arena.Fight(WARRIOR_NAME, ENEMY_NAME));
        }

        [Test]
        [TestCase(WARRIOR_HP + 1)]
        [TestCase(WARRIOR_HP + 2)]
        public void FightStrongerEnemyShouldThrowException(int damage)
        {
            this.warrior = new Warrior(WARRIOR_NAME, WARRIOR_DAMAGE, WARRIOR_HP);
            this.enemyWarrior = new Warrior(ENEMY_NAME, damage, WARRIOR_HP);
            this.arena = new Arena();
            this.arena.Enroll(warrior);
            this.arena.Enroll(enemyWarrior);

            Assert.Throws<InvalidOperationException>(() => this.arena.Fight(WARRIOR_NAME, ENEMY_NAME));
        }

        [Test]
        public void SuccessFightShouldDecreaseWarriorHp()
        {
            this.warrior = new Warrior(WARRIOR_NAME, WARRIOR_DAMAGE, WARRIOR_HP);
            this.enemyWarrior = new Warrior(ENEMY_NAME, WARRIOR_DAMAGE, WARRIOR_HP);
            this.arena = new Arena();
            this.arena.Enroll(warrior);
            this.arena.Enroll(enemyWarrior);

            this.arena.Fight(WARRIOR_NAME, ENEMY_NAME);

            int expectedWarriorHp = WARRIOR_HP - WARRIOR_DAMAGE;
            int actualWarriorHp = warrior.HP;

            Assert.AreEqual(actualWarriorHp, expectedWarriorHp);
        }

        [Test]
        [TestCase(WARRIOR_HP)]
        [TestCase(WARRIOR_HP + 1)]
        [TestCase(WARRIOR_HP + 2)]
        [TestCase(WARRIOR_HP + 3)]
        public void GreaterWarriorDamageThanEnemyHpAfterFightShouldZeroEnemyHp(int damage)
        {
            this.warrior = new Warrior(WARRIOR_NAME, damage, WARRIOR_HP);
            this.enemyWarrior = new Warrior(ENEMY_NAME, WARRIOR_DAMAGE, WARRIOR_HP);
            this.arena = new Arena();
            this.arena.Enroll(warrior);
            this.arena.Enroll(enemyWarrior);

            this.arena.Fight(WARRIOR_NAME, ENEMY_NAME);

            int expectedEnemyHp = 0;
            int actualEnemyHp = enemyWarrior.HP;

            Assert.AreEqual(actualEnemyHp, expectedEnemyHp);
        }

        [Test]
        public void EqualOrLowerWarriorDamagetThanEnemyHpAfterFightShouldDecreaseEnemyHp()
        {
            this.warrior = new Warrior(WARRIOR_NAME, WARRIOR_DAMAGE, WARRIOR_HP);
            this.enemyWarrior = new Warrior(ENEMY_NAME, WARRIOR_DAMAGE, WARRIOR_HP);
            this.arena = new Arena();
            this.arena.Enroll(warrior);
            this.arena.Enroll(enemyWarrior);

            this.arena.Fight(WARRIOR_NAME, ENEMY_NAME);

            int expectedEnemyHp = WARRIOR_HP - WARRIOR_DAMAGE;
            int actualEnemyHp = enemyWarrior.HP;

            Assert.AreEqual(actualEnemyHp, expectedEnemyHp);
        }
    }
}
