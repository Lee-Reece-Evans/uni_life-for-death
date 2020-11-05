using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerOverlap : MonoBehaviour {

    public bool isOverlapping;

    private void Start()
    {
        isOverlapping = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            isOverlapping = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
            isOverlapping = false;
    }
}