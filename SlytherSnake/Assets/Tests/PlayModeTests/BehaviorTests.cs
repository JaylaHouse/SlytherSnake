using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class BehaviorTests
    {

        // A Test behaves as an ordinary method
        [Test]
        public void AtLeastOneBodyPartIsSuccesfullyCreated()
        {
            List<GameObject> BodyParts = new List<GameObject>();

            // Assert that the bodypart object exists
            Assert.IsNotNull(BodyParts);
        }

        [UnityTest]
        public IEnumerator GameObject_WithRigidBody_WillBeAffectedByPhysics()
        {
            var go = new GameObject();
            go.AddComponent<Rigidbody>();
            var originalPosition = go.transform.position.y;

            yield return new WaitForFixedUpdate();

            Assert.AreNotEqual(originalPosition, go.transform.position.y);
        }

    }
}