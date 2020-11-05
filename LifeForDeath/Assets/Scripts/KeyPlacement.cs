using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KeyPlacement : MonoBehaviour {

    // user input variable values
    public bool isGreenStand;
    public bool isBlueStand;
    public bool isOrangeStand;
    public bool isRedStand;
    public bool isPurpleStand;

    // UI
    public TextMeshPro keyText;

    // gameobject
    public GameObject skull;
    public GameObject blockade;
    public GameObject skullLight;

    //scrips
    PlayerCollectables pc;
    Dissolve dissolveKey;

    // logic variables
    private bool triggerActive;
    private bool isPlaced;

    // Use this for initialization
    private void Start () {
        dissolveKey = skull.GetComponent<Dissolve>();
        skullLight = skull.transform.Find("Light").gameObject;
        pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCollectables>();

        isPlaced = false;
        triggerActive = false;
        keyText.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isPlaced)
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
        if (!isPlaced)
        {
            if (other.tag == "Player")
            {
                triggerActive = false;
                keyText.gameObject.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    private void Update () {
        if (triggerActive && !isPlaced && Input.GetKeyDown(KeyCode.F))
        {
            if (isGreenStand && pc.hasGreenKey)
                isPlaced = true;
            if (isBlueStand && pc.hasBlueKey)
                isPlaced = true;
            if (isOrangeStand && pc.hasOrangeKey)
                isPlaced = true;
            if (isRedStand && pc.hasRedKey)
                isPlaced = true;
            if (isPurpleStand && pc.hasPurpleKey)
                isPlaced = true;
        }

        if (isPlaced)
        {
            Destroy(blockade); // destroy the blockage associated with the key

            keyText.gameObject.SetActive(false);

            if (skullLight != null) // if a light exists
                skullLight.SetActive(true);

            dissolveKey.DissolveIn();

            if (dissolveKey.dissolvetime == 0) // when dissolve has finished
            {
                // delete trigger only, key stays
                Destroy(this.gameObject);
            }
        }
    }
}
