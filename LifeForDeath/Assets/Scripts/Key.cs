using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Key : MonoBehaviour {

    // user input variable values
    public bool isGreenKey;
    public bool isBlueKey;
    public bool isOrangeKey;
    public bool isRedKey;
    public bool isPurpleKey;

    // UI
    public TextMeshPro keyText;

    // gameobject
    public GameObject skull;

    //referenced scripts
    PlayerCollectables pc;
    Dissolve dissolveKey;

    // logic variables
    private bool triggerActive;
    private bool isPickedUp;

    // Use this for initialization
    private void Start ()
    {
        dissolveKey = skull.GetComponent<Dissolve>();
        pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCollectables>();

        isPickedUp = false;
        triggerActive = false;
        keyText.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isPickedUp) // only can use trigger when item is not picked up
        {
            if (other.tag == "Player")
            {
                triggerActive = true;
                keyText.gameObject.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!isPickedUp) // only can use trigger when item is not picked up
        {
            if (other.tag == "Player")
            {
                triggerActive = false;
                keyText.gameObject.SetActive(false);
            }
        }
    }

    private void Update ()
    {
		if (triggerActive && !isPickedUp && Input.GetKeyDown(KeyCode.F))
        {
            // change boolean in playerCollecatbles depending on which was picked up
            if (isGreenKey)
                pc.GreenKeyCollected();
            if (isBlueKey)
                pc.BlueKeyCollected();
            if (isRedKey)
                pc.RedKeyCollected();
            if (isPurpleKey)
                pc.PurpleKeyCollected();
            if (isOrangeKey)
                pc.OrangeKeyCollected();

            isPickedUp = true; // trigger can not be used again, commence dissolving

            keyText.gameObject.SetActive(false);
        }

        if (isPickedUp) // item is picked up
        {
            dissolveKey.DissolveOut();

            if (dissolveKey.dissolvetime == 1) // completed dissolving
            {
                Destroy(skull); // after dissolved; trigger is a child also gets destroyed
            }
        }
    }
}
