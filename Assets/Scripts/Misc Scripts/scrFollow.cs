using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrFollow : MonoBehaviour
{
    public GameObject drone;
    public GameObject hookshotSpot;

    // Update is called once per frame
    void FixedUpdate()
    {
        hookshotSpot.transform.position = new Vector3(drone.transform.position.x, drone.transform.position.y, drone.transform.position.z);
    }
}
