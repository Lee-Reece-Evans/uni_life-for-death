using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Powerups : MonoBehaviour {

    // user input variable values
    public int soulCost;
    public bool inc_firerate;
    public bool inc_damage;
    public bool inc_health;

    // UI
    public TextMeshPro powerupText;

    // particle effect
    public GameObject playEffect;

    // scripts needed
    PlayerCollectables pc;
    PlayerHealth ph;
    Gun gun;

    // logic variables
    private bool triggerActive;

    // Use this for initialization
    private void Start () {
		ph = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        gun = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Gun>();
        pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCollectables>(); ;

        triggerActive = false;
        powerupText.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            triggerActive = true;
            powerupText.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            triggerActive = false;
            powerupText.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    private void Update () {

        if (triggerActive && Input.GetKeyDown(KeyCode.F) && pc.souls >= soulCost)
        {
            pc.SubtractSouls(soulCost);

            if (inc_firerate)
            {
                gun.fireRate = 10f;
            }
            if (inc_damage)
            {
                gun.normDamage += 10f;
                gun.headDamage += 10f;
            }
            if (inc_health)
            {
                ph.maxHealth = 125f;
                ph.RestoreHealth(125f);
            }

            playEffect.SetActive(true);
            powerupText.gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}
