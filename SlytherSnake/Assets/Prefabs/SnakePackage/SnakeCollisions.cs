using System.Collections;
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
		tm.speed = 5f;       //changed from .1f to .25f becayse leos area is bigger so needs to go faster (Change made by Jayla)
	}

	void OnTriggerEnter(Collider other)
	{
		//Debug.Log("Triggredd");
		//collide with wall or rock
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
			tm.ChangeColor();
		}

		if (other.gameObject.CompareTag("ColorRed"))
		{
			//change color to red

			Destroy(other.gameObject);
			tm.addBodyPart();
			tm.ChangeColorRed();

		}

		if (other.gameObject.CompareTag("pickUp"))
		{

			Destroy(other.gameObject);
			tm.addBodyPart();
		}

	}
}
