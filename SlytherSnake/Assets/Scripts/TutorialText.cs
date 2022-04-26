using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialText : MonoBehaviour
{
    public Text timeText;

    // Start is called before the first frame update
    void Start()
    {
        timeText.text = "Move around using the mouse. Reach the end of the Level before the Timer runs out.";
    }



    private void OnTriggerEnter(Collider other)
    {

        // Make the body grow after getting the pickups
        //BodySpeed = BodySpeed + 1;
        if (other.gameObject.CompareTag("SpeedTu"))
        {
            timeText.text = "This pick up will give you a speed boost.";
        }

        if (other.gameObject.CompareTag("ColorChangeTut"))
        {
            timeText.text = "This pick up will change the color of your player.";
        }


        if (other.gameObject.CompareTag("pickUpTut"))
        {
            timeText.text = "This and all pick ups will grow your player.";
        }
        
        if (other.gameObject.CompareTag("transitionCube"))
        {
            timeText.text = "Touch this cube to go to the next level.";
        }

        if (other.gameObject.CompareTag("FindEndTut"))
        {
            timeText.text = "Find the transition cube.";
        }

        if (other.gameObject.CompareTag("dialog1")) {
            timeText.text = "Welcome to the snow biome!";
        }

    }


}


