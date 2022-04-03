using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SceneSwitch : MonoBehaviour
{

    void OnTriggerEnter(Collider other){
        
        Application.LoadLevel(1);
        Debug.Log("Collided");

    }

}
