using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour
{
    //Settings
    public float BodySpeed = 1f;
    public int Gap = 10;

    // References
    public GameObject BodyPrefab;

    // Lists
    private List<GameObject> BodyParts = new List<GameObject>();
    private List<Vector3> PositionsHistory = new List<Vector3>();

    void Start()
    {
        //making snake parts spawn at different times 
        Invoke("GrowSnake", 1);
        Invoke("GrowSnake", 2);
        
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

            float speed = 0.1f;
            if (dir.magnitude > speed)
            {
                dir.Normalize();
                transform.Translate(dir * speed);
            }

            Vector3 spherePos = transform.position;
            spherePos.y = 0;
        }
    }
    private void GrowSnake()
    {
        // Instantiate body instance and
        // add it to the list
        GameObject body = Instantiate(BodyPrefab);
        BodyParts.Add(body);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("pickUp"))
        {
            Destroy(other.gameObject);
            GrowSnake();
        }
    }
}
