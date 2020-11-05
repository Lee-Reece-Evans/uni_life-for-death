using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelCollect : MonoBehaviour
{
    PlayerCollectables pc;

    public AudioSource playSound;

    // Start is called before the first frame update
    private void Start()
    {
        pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCollectables>();
        playSound = GameObject.FindGameObjectWithTag("FuelSpawners").GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playSound.Play();

            pc.AddFuel(Random.Range(20, 40)); // add between 20 and 40 units of fuel

            GameManager.Instance.fuelSpawned--;

            Destroy(gameObject);
        }
    }
}
