using UnityEngine;
using System.Collections;

public class HeadCollision : MonoBehaviour {

	// Use this for initialization
	void Start () {
		//get level manager

	}

	void OnTriggerEnter(Collider other){
		Debug.Log("Head Trigger");

		//collide with bodyPart
		if(other.gameObject.CompareTag("BodyPart")){
			Debug.Log("Triggred With BodyPart");
			//goto lose screen
		}
	}

}
