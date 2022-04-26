using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class TestMovement : MonoBehaviour {

	public List<Transform> bodyParts = new List<Transform>();
	public float minDistance = 0.015f;
	public float speed = 3f;
	public float rotationSpeed = 50.0f;
	public GameObject bodyPartPrefab;
	public int startBodySize = 1;
	public string sceneName;
	Color newColor;
	Color newColorR;
	Renderer rend;


	//movement variables
	private float distance;
	private Transform currentBodyPart;
	private Transform prevBodyPart;
	///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	//rotation variables
	private Quaternion startingRotation;
	private float angel = 90;
	private float fTurnRate = 90.0f;
	//swipe variables with mouse
	private Vector2 firstPressPos;
	private Vector2 secondPressPos;
	private Vector2 currentSwipe;

	//start function
	void Start(){
		///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		//get starting rotation
		if(bodyParts.Count != 0)
		startingRotation = bodyParts[0].rotation;
		///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		//adding body parts in the begining
		for(int i=0; i<startBodySize - 1; i++){
			addBodyPart();
		}
		newColor = Color.blue;
		newColorR = Color.red;
		rend = GetComponent<Renderer>();

	}
     
     void Update () {
     	move();

     	//TODO Add bodyparts
     	if(Input.GetKeyUp(KeyCode.Q)) addBodyPart();
     }

     //move function
     public void move(){
     	float currentSpeed = speed;

     	if(Input.GetKey(KeyCode.UpArrow)) currentSpeed *= 2;

     	//move the head
     	if(bodyParts.Count != 0)
     	bodyParts[0].Translate(bodyParts[0].forward * currentSpeed * Time.smoothDeltaTime, Space.World);

		///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		//rotate the head with directions
		//go to 90 degrees with right arrow
		if( Input.GetKeyDown( KeyCode.RightArrow ) && bodyParts.Count != 0){
	        StopAllCoroutines();
	        StartCoroutine(Rotate(angel));
	        angel+=90;
			bodyParts[0].Rotate (-Vector3.forward * fTurnRate * Time.deltaTime);
	    }

	    //go to -90 degrees with left arrow
		if( Input.GetKeyDown( KeyCode.LeftArrow )  && bodyParts.Count != 0){
	        StopAllCoroutines();
	        StartCoroutine(Rotate(angel-180));
	        angel-=90;
			bodyParts[0].Rotate (Vector3.forward * fTurnRate * Time.deltaTime);
	    }

	    //move the head
		if(bodyParts.Count != 0)
		bodyParts[0].position = bodyParts[0].position + bodyParts[0].forward * 2.0f * Time.deltaTime;
		///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

     	for(int i = 1; i<bodyParts.Count; i++){
     		currentBodyPart = bodyParts[i];
     		prevBodyPart = bodyParts[i-1];

     		//calc distance between current and prev bodyPart
     		distance = Vector3.Distance(prevBodyPart.position, currentBodyPart.position);

     		//previous part position
     		Vector3 newPosition = prevBodyPart.position;

     		//lock the y to prevent bodypart from moving up
     		newPosition.y = bodyParts[0].position.y;

			//calc time delta
			float dTime = Time.deltaTime * (distance/minDistance) * currentSpeed;

			//keep time delta small to prevent slow movement (frame drop)
			if(dTime > 0.5f) dTime = 0.5f;

			//move between current position and new position
			currentBodyPart.position = Vector3.Slerp(currentBodyPart.position, newPosition, dTime);

			//rotate current body part
			currentBodyPart.rotation = Quaternion.Slerp(currentBodyPart.rotation, prevBodyPart.rotation, dTime);
     	}
     }

     public void addBodyPart(){
		if(bodyParts.Count != 0){
			//instantiate after last bodypart in the list
			Transform newPart = (Instantiate(bodyPartPrefab, bodyParts[bodyParts.Count -1].position, bodyParts[bodyParts.Count -1].rotation) as GameObject).transform;
			//set parent to be this transform
			newPart.SetParent(transform);
			//add the new part to the list
			bodyParts.Add(newPart);
		}
     }

     //slow rotation function
	 IEnumerator Rotate(float rotationAmount){
	    Quaternion finalRotation = Quaternion.Euler( 0, rotationAmount, 0 ) * startingRotation;

	    while(bodyParts[0].rotation != finalRotation){
			bodyParts[0].rotation = Quaternion.Lerp(bodyParts[0].rotation, finalRotation, Time.deltaTime*rotationSpeed);
	        yield return null;
	    }
	}

	//trigger wall collision
	
	//collision 
	void OnCollisionEnter(Collision collision){
		Debug.Log("Collision");
	}

	public void ChangeColor() {

		foreach (Transform body in bodyParts)
		{
			body.GetComponent<Renderer>().material.color = newColor;

		}
	}

	public void ChangeColorRed()
    {
		foreach (Transform body in bodyParts)
		{
			body.GetComponent<Renderer>().material.color = newColorR;

		}

	}

}
