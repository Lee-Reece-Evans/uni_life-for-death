using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipComponentItem : MonoBehaviour
{
    PlayerCollectables pc;

    private void Start()
    {
        pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCollectables>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            pc.ShipBatteryCollected();
            Destroy(gameObject);
        }
    }
}
