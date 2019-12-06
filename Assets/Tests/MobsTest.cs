using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
namespace Tests
{
    public class MobsTest : MonoBehaviour
    {
        PlayerControl Player;
        PlaterStats PlayerStats;
        [UnitySetUp]
        public IEnumerator SceneLoad()
        {
            SceneManager.LoadScene("TestScene1");
            yield return new WaitForSeconds(2f);
            Player = FindObjectOfType<PlayerControl>();
            PlayerStats = FindObjectOfType<PlaterStats>();
        }
        [UnityTest]
        public IEnumerator DogStaysWall()
        {
            float a1 = FindObjectOfType<DogScript>().transform.position.x;
            yield return new WaitForSeconds(0.1f);
            float a2 = FindObjectOfType<DogScript>().transform.position.x;
            bool isFacingRight1;
            if (a2 < a1) { isFacingRight1 = false; }
            else { isFacingRight1 = true; }
            yield return new WaitForSeconds(4f);
            a1 = FindObjectOfType<DogScript>().transform.position.x;
            yield return new WaitForSeconds(0.1f);
            a2 = FindObjectOfType<DogScript>().transform.position.x;
            bool isFacingRight2;
            if (a2 > a1) { isFacingRight2 = true; }
            else { isFacingRight2 = false; }
            bool correct;

            if (!isFacingRight1 && isFacingRight2) correct = true;
            else correct = false;


            Assert.AreEqual(true, correct);
        }
        [UnityTest]
        public IEnumerator CoinPickUP()
        {
            Coin[] coins = FindObjectsOfType<Coin>();
            int lenght = coins.Length;
            GameObject.FindGameObjectWithTag("Player").transform.position = new Vector3(361, -5, 0);
            yield return new WaitForSeconds(1f);
            Coin[] coins2 = FindObjectsOfType<Coin>();
            int lenght2 = coins2.Length;
            Assert.AreEqual(--lenght, lenght2);
        }

        [UnityTest]
        public IEnumerator TakeHeal()
        {
            GameObject.FindGameObjectWithTag("Player").transform.position = new Vector3(400, -5, 0);
            PlayerStats.TakeDamage(5);
            GameObject.FindGameObjectWithTag("Player").transform.position = new Vector3(450, -5, 0);
            yield return new WaitForSeconds(1f);
            Assert.AreEqual(7, PlayerStats.get_health());
        }
        [UnityTest]
        public IEnumerator DogDies()
        {
            GameObject.FindGameObjectWithTag("Player").transform.position = new Vector3(20, -135, 0);
            GameObject.FindGameObjectWithTag("Dog").transform.position = new Vector3(139, -135, 0);
            Player.Shoot();
            yield return new WaitForSeconds(0.1f);
            Player.Shoot();
            yield return new WaitForSeconds(0.1f);
            Player.Shoot();
            yield return new WaitForSeconds(4f);
            bool IsDogDies = false;
            if (GameObject.FindGameObjectWithTag("Dog") == null)
            {
                IsDogDies = true;
            }
            Assert.AreEqual(true, IsDogDies);

        }
        [UnityTest]
        public IEnumerator LaserCanDamage()
        {
            GameObject.FindGameObjectWithTag("Player").transform.position = new Vector3(450, -100, 0);
            int HP1 = PlayerStats.get_health();
            yield return new WaitForSeconds(4f);
            GameObject.FindGameObjectWithTag("Player").transform.position = new Vector3(400, -100, 0);
            int HP2 = PlayerStats.get_health();

            Assert.AreEqual(HP1 - 4, HP2);

        }
    }
}