using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Door : MonoBehaviour {
    // user input variable values
    public int soulCost;
    public bool greenKey;
    public bool blueKey;
    public bool orangeKey;
    public bool purpleKey;

    // UI
    public TextMeshPro doorText;

    // logic variables
    private bool triggerActive;
    private bool doorUnlocked;
    private bool hasDoorRequirements;

    // list of enemies inside trigger
    List<GameObject> objInTrig = new List<GameObject>();

    // script to retrieve player collectables
    PlayerCollectables pc;

    // door
    public GameObject doorObject;
    private Collider doorCol;
    public AudioSource playSound;

    // disolve
    Dissolve dissolveDoor;

    // array of spawners
    public GameObject[] spawners;

    // Use this for initialization
    private void Start ()
    {
        playSound = GetComponent<AudioSource>();

        pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCollectables>();

        doorCol = doorObject.GetComponent<Collider>();
        dissolveDoor = doorObject.GetComponent<Dissolve>();

        triggerActive = false;
        doorUnlocked = false;
        hasDoorRequirements = false;
        doorText.gameObject.SetActive(false);
    }

    // variabels won't change if they update while in trigger. should use on trigger stay?
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" || other.tag == "Zombie")
        {
            objInTrig.Add(other.gameObject);
        }

        if (!doorUnlocked)
        {
            if (other.tag == "Player")
            {
                // trigger is active
                triggerActive = true;
                doorText.gameObject.SetActive(true);

                if (greenKey)
                {
                    if (pc.hasGreenKey)
                    {
                        hasDoorRequirements = true;
                    }
                    else
                    {
                        hasDoorRequirements = false;
                    }
                }
                else if (blueKey)
                {
                    if (pc.hasBlueKey)
                    {
                        hasDoorRequirements = true;
                    }
                    else
                    {
                        hasDoorRequirements = false;
                    }
                }
                else if (orangeKey)
                {
                    if (pc.hasOrangeKey)
                    {
                        hasDoorRequirements = true;
                    }
                    else
                    {
                        hasDoorRequirements = false;
                    }
                }
                else if (purpleKey)
                {
                    if (pc.hasPurpleKey)
                    {
                        hasDoorRequirements = true;
                    }
                    else
                    {
                        hasDoorRequirements = false;
                    }
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" || other.tag == "Zombie")
        {
            objInTrig.Remove(other.gameObject);
        }

        if (!doorUnlocked)
        {
            if (other.tag == "Player")
            {
                triggerActive = false;
                doorText.gameObject.SetActive(false);
            }
        }
    }

    private void Update ()
    {
        // remove destroyed game objects that died in trigger. LINQ query - lamba expression
        objInTrig.RemoveAll(GameObject => GameObject == null);

        if (doorUnlocked)
        {
            if (objInTrig.Count == 0)
            {
                triggerActive = false;
            }
            else
            {
                triggerActive = true;
            }
        }

        if (!doorUnlocked && triggerActive && Input.GetKeyDown(KeyCode.F) && hasDoorRequirements && pc.souls >= soulCost)
        {
            playSound.Play();
            pc.SubtractSouls(soulCost);
            doorUnlocked = true;
            doorText.gameObject.SetActive(false);
            doorCol.isTrigger = true;

            if (spawners.Length > 0) // only do if door has spawners attached
            {
                foreach (GameObject spawner in spawners)
                {
                    spawner.SetActive(true);
                }

                GameManager.Instance.UpdateActiveSpawners();
            }
        }

        if (doorUnlocked && triggerActive)
        {
            dissolveDoor.DissolveOut();
        }
        else if (doorUnlocked && !triggerActive)
        {
            dissolveDoor.DissolveIn();
        }
	}
}
