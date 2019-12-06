﻿using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
namespace Tests
{
    public class EnemyCheksTest : MonoBehaviour
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
        public IEnumerator CheckGhostMoving()
        {
            Transform OldGhostPosition = GameObject.FindGameObjectWithTag("Ghost").transform;
            Debug.Log(OldGhostPosition.position.x);
            yield return new WaitForSeconds(2f);
            Transform NewGhostPosition = GameObject.FindGameObjectWithTag("Ghost").transform;
            Assert.AreNotSame(OldGhostPosition.position, NewGhostPosition.position);
        }

        [UnityTest]
        public IEnumerator DieWithSaw()
        {
            int HP = PlayerStats.get_health();
            GameObject.FindGameObjectWithTag("Player").transform.position = new Vector3(-50, -18, 0);
            yield return new WaitForSeconds(2f);
            Assert.AreEqual(HP--, PlayerStats.get_health());
        }
        [Test]
        public void DogMove()
        {
            DogScript Dogie = FindObjectOfType<DogScript>();
            Assert.AreEqual(Dogie.moveRight, false);
        }
        [Test]
        public void DogMoveSpeed()
        {
            DogScript Dogie = FindObjectOfType<DogScript>();
            Assert.AreEqual(Dogie.moveSpeed, 5);
        }
        [UnityTest]
        public IEnumerator DogWalking()
        {
            Transform OldDogPosition = GameObject.FindGameObjectWithTag("Dog").transform;
            yield return new WaitForSeconds(2f);
            Transform NewDogPosition = GameObject.FindGameObjectWithTag("Dog").transform;
            Assert.AreNotSame(OldDogPosition.position, NewDogPosition.position);
        }
    }
}
