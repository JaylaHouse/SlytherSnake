using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class DirectionTests
    {
        GameObject bodyPrefab = Resources.Load<GameObject>("SnakeController");

        // A Test behaves as an ordinary method
        [Test]
        public void CatchingErrors()
        {
            GameObject gameobject = new GameObject("Test");
            Assert.Throws<MissingComponentException>(
                () => gameobject.GetComponent<Rigidbody>().velocity = Vector3.one);

        }

        [Test]
        public void TestSnakeVelocity()
        {

            GameObject bodyPrefab = new GameObject();
            bodyPrefab.AddComponent<Rigidbody>();

            Vector3 PositionsHistory = Vector3.zero;
            Quaternion SpeedHistory = Quaternion.identity;
            GameObject snake = Object.Instantiate(bodyPrefab, PositionsHistory, SpeedHistory);

            Assert.That(snake.GetComponent<Rigidbody>().velocity, Is.EqualTo(Vector3.zero));
        }
    }
}

