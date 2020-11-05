using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WinGame : MonoBehaviour
{
    // UI
    public TextMeshPro shipText;

    // player
    public GameObject player;
    public GameObject crosshair;

    // script to retrieve player collectables
    PlayerCollectables pc;

    // wingame animator
    public Animator anim;

    // wingame camera
    public GameObject wgCam;

    // logic variables
    private bool triggerActive;

    public AudioSource playSound;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        pc = player.GetComponent<PlayerCollectables>();

        triggerActive = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            triggerActive = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            triggerActive = false;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (triggerActive && pc.fuel == 100 && pc.hasShipBattery && Input.GetKeyDown(KeyCode.F))
        {
            shipText.gameObject.SetActive(false);
            crosshair.SetActive(false);

            GameManager.Instance.gameWon = true;
            KillZombies();

            player.SetActive(false); // disable the fps camera and player controls
            wgCam.SetActive(true); // activate win game camera

            anim.SetTrigger("WinGame"); // play win game animation for the ship
            playSound.Play();

            EndGame();
        }
    }

    private void KillZombies()
    {
        GameObject[] zombies = GameObject.FindGameObjectsWithTag("MyZombie");

        foreach (GameObject zombie in zombies) // destroy all zombies in scene to stop them bugging out
        {
            Destroy(zombie);
        }
    }

    private void EndGame()
    {
        StartCoroutine("EndGameFade");
    }

    IEnumerator EndGameFade()
    {
        yield return new WaitForSeconds(5.0f); // wait for ship to finish flying away
        GameManager.Instance.GameOver();
    }
}
