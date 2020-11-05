using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : MonoBehaviour {

    PlayerHealth ph;

    private void Start()
    {
       ph = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            ph.RestoreHealth(15f); // restore health to player
            GameManager.Instance.HPspawned--; // tell game manager it was picked up
            Destroy(gameObject);
        }
    }
}
