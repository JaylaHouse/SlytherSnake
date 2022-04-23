﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeCollisions : MonoBehaviour
{
	private float speedBoostDuration;
	public TestMovement tm;
    // Start is called before the first frame update
    void Start()
    {
		speedBoostDuration = 3.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void ActivateSpeedBoost()
	{
		StartCoroutine(SpeedBoostCooldown());
	}

	IEnumerator SpeedBoostCooldown()
	{
		tm.speed = tm.speed * 2;
		yield return new WaitForSeconds(speedBoostDuration);
		tm.speed = 4.5f;       //changed from .1f to .25f becayse leos area is bigger so needs to go faster (Change made by Jayla)
	}

	void OnTriggerEnter(Collider other)
	{
		//Debug.Log("Triggredd");
		//collide with wall or rock
		Debug.Log("regular Trigger");
		if (other.gameObject.CompareTag("Speedboost"))
		{
			//ActivateSpeedBoost();
			ActivateSpeedBoost();
			Destroy(other.gameObject);
			tm.addBodyPart();
		}

		if (other.gameObject.CompareTag("ColorChanger"))
		{
			//change all of the element colors

			Destroy(other.gameObject);
			tm.addBodyPart();

			/*tm.rend.material.color = tm.newColor;

			for (int i = 1; i < bodyParts.Count; i++)
			{
				currentBodyPart = tm.bodyParts[i];
				prevBodyPart = tm.bodyParts[i - 1];
				currentBodyPart.GetComponent<Renderer>().material.color = tm.newColor;
				prevBodyPart.GetComponent<Renderer>().material.color = tm.newColor;
			}


			tm.addBodyPart();*/
		}

		if (other.gameObject.CompareTag("ColorRed"))
		{
			//change color to red

			Destroy(other.gameObject);
			tm.addBodyPart();

			/*rend.material.color = newColorR;
			foreach (GameObject body in BodyParts)
			{
				body.GetComponent<Renderer>().material.color = newColorR;

			}*/

		}

		if (other.gameObject.CompareTag("pickUp"))
		{

			Destroy(other.gameObject);
			tm.addBodyPart();

		}
	}

}
