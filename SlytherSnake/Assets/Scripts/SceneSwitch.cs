using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneSwitch : MonoBehaviour
{
    public string sceneName;

    void OnTriggerEnter(Collider other){
        if (other.gameObject.CompareTag("Head"))
        {
            SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
            Debug.Log("Collided");
        }
        

    }

}
