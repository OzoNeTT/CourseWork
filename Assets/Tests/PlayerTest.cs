using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
namespace Tests
{

    public class PlayerTest : MonoBehaviour
    {
        PlayerControl Player;
        PlaterStats PlayerStats;
        Enemy1 enem1;
        Enemy2 enem2;
        [Test]
        public void EditorTest()
        {
            var gameObject = new GameObject();

            var newGameObjectName = "My game object";
            gameObject.name = newGameObjectName;

            Assert.AreEqual(newGameObjectName, gameObject.name);
        }
        [UnitySetUp]
        public IEnumerator SceneLoad()
        {
            SceneManager.LoadScene("TestScene1");
            yield return new WaitForSeconds(2f);
            Player = FindObjectOfType<PlayerControl>();
            PlayerStats = FindObjectOfType<PlaterStats>();
        }
        [Test]
        public void FacingDirectionTest()
        {
           
            bool facing = Player.isFacingRight;
            Assert.AreEqual(facing, false);
           
        }
        [Test]
        public void HealthAfterDamage()
        {
            int fullHP = PlayerStats.get_health();
            int damage = 5;
            PlayerStats.TakeDamage(damage);
            Assert.AreEqual(PlayerStats.get_health(), fullHP - damage);
        }
        [Test]
        public void CanShoot()
        {
            Player.Shoot();
            Assert.True(FindObjectOfType<BulletController>());
        }
        [UnityTest]
        public IEnumerator CanKill()
        {
            bool isEnemy1OnMap = FindObjectOfType<Enemy1>();
            Player.Shoot();
            yield return new WaitForSeconds(4);
            bool isEnemy1OnMapAfter = FindObjectOfType<Enemy1>();
            Assert.AreEqual(isEnemy1OnMap, !isEnemy1OnMapAfter);
        }
    }
}
