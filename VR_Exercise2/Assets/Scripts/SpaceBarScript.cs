using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceBarScript : MonoBehaviour
{
    public Vector3 worldPosition;
    public GameObject capsule;
    public Rigidbody capsuleRB;
    public bool move;
    Plane plane = new Plane(Vector3.up, 0);

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Game Started");
        move = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            Debug.Log("space key was pressed");
        }
        
        if (move)
        {
            float distance;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (plane.Raycast(ray, out distance))
            {
                worldPosition = ray.GetPoint(distance);
                Debug.Log(worldPosition);
            }

            Vector3 distanceToRay = worldPosition - capsule.transform.position;
            Vector3 velocityCapsule = Vector3.Normalize(distanceToRay);

            capsuleRB.velocity = 3*velocityCapsule;

            capsule.transform.LookAt(worldPosition);

        }

    }
}
