using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SnakeController : MonoBehaviour
{
    //Settings
    /*public float boostTimer;
    public bool boosting;
    public bool moving;*/
    private float speedBoostDuration;

    public float BodySpeed;
    public int Gap;
    public int count;
    public float speed;
    Color newColor;
    Color newColorR;
    Renderer rend;
    public string sceneName;
    //Renderer Brend;



    // References
    public GameObject BodyPrefab;
    //[SerializeField] private Material myMaterial;


    // Lists
    private List<GameObject> BodyParts = new List<GameObject>();
    private List<Vector3> PositionsHistory = new List<Vector3>();

    void Start()
    {
        //making snake parts spawn at different times 
        Invoke("GrowSnake", 1);
        Invoke("GrowSnake", 2);
        BodySpeed = 1f;
        Gap = 10;
        count = 1;
        speed = 0.2f;
        /*boostTimer = 0;
        boosting = false;
        moving = false;*/
        speedBoostDuration = 3;
        rend = GetComponent<Renderer>();
        //Brend = GameObject.FindGameObjectWithTag("BodyPart").GetComponent<Renderer>();
        newColor = Color.blue;
        newColorR = Color.red;
    }

    void Update()
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // Store position history
        PositionsHistory.Insert(0, transform.position);

        // Move body parts
        int index = 0;
        foreach (var body in BodyParts)
        {
            Vector3 point = PositionsHistory[Mathf.Clamp(index * Gap, 0, PositionsHistory.Count - 1)];

            // Move body towards the point along the snakes path
            Vector3 moveDirection = point - body.transform.position;

            //beginning of change 
            body.transform.Translate(moveDirection * BodySpeed);
            //end of change 

            // Rotate body towards the point along the snakes path
            body.transform.LookAt(point);

            index++;
        }



        if (Physics.Raycast(ray, out hit))
        {
            Vector3 mousePos = hit.point;

            Vector3 dir = hit.point - transform.position;
            dir.y = 0;

            //float speed = 0.1f;
            if (dir.magnitude > speed)
            {
                dir.Normalize();
                transform.Translate(dir * speed);
            }

            Vector3 spherePos = transform.position;
            spherePos.y = 0;
        }

        if(count > 3)
        {
            GameOver();
        }

       

        //transform.Translate(Vector3.forward * Time.deltaTime * BodySpeed);
    }

    public void ActivateSpeedBoost()
    {
        StartCoroutine(SpeedBoostCooldown());
    }

    IEnumerator SpeedBoostCooldown()
    {
        speed = speed * 2;
        yield return new WaitForSeconds(speedBoostDuration);
        speed = 0.2f;       //changed from .1f to .25f becayse leos area is bigger so needs to go faster (Change made by Jayla)
    }
    private void GrowSnake()
    {
        // Instantiate body instance and
        // add it to the list
        GameObject body = Instantiate(BodyPrefab);
        //rend.material.color = newColor;
        BodyParts.Add(body);
    }

    private void GameOver()
    {
        SceneManager.LoadScene(sceneName);
        //Application.Quit();
    }



    private void OnTriggerEnter(Collider other)
    {

        // Make the body grow after getting the pickups
        //BodySpeed = BodySpeed + 1;
        if (other.gameObject.CompareTag("Speedboost"))
        {
            ActivateSpeedBoost();
            Destroy(other.gameObject);
            /*boosting = true;
            speed = 0.3f;
            Destroy(other.gameObject);
            GrowSnake();*/
        }

        if (other.gameObject.CompareTag("ColorChanger"))
        {
            count++;

            Destroy(other.gameObject);

            GrowSnake();

            rend.material.color = newColor;
            foreach (GameObject body in BodyParts)
            {
                body.GetComponent<Renderer>().material.color = newColor;

            }

        }

        if (other.gameObject.CompareTag("ColorRed"))
        {
            count++;
            
            Destroy(other.gameObject);
            
            GrowSnake();

            rend.material.color = newColorR;
            foreach (GameObject body in BodyParts)
            {
                body.GetComponent<Renderer>().material.color = newColorR;

            }

        }


        if (other.gameObject.CompareTag("pickUp"))
        {

            Destroy(other.gameObject);
            GrowSnake();
        }

    }


}
