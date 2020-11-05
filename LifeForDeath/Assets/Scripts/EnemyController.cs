using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {

    // player
    public Transform player;
    PlayerHealth ph;

    // AI components
    NavMeshAgent agent;
    Animator anim;
    Dissolve[] dissolveZombie; // array of scripts for dissolving zombie materials
    private Collider m_collider;
    private Rigidbody[] AllRBodies; // array of rigidbody components

    // audio
    public AudioClip[] spawnSoundsArray; // array of sound clips
    public AudioClip[] runSoundsArray;
    public AudioClip[] attackSoundsArray;
    public AudioClip deathSound;
    public AudioSource playSpawnSound;
    public AudioSource playSound;

    // logic global variables
    public bool isDead; // used to control update function call
    public float damage = 5; // damage to player
    public float runsoundTime;

    // Use this for initialization
    private void Start ()
    {
        // find objects / components
        player = GameObject.FindGameObjectWithTag("Player").transform; // players transform
        ph = player.GetComponent<PlayerHealth>(); // player health script
        agent = GetComponent<NavMeshAgent>(); // AI navmeshagent
        dissolveZombie = GetComponentsInChildren<Dissolve>(); // dissolve material script
        anim = GetComponent<Animator>(); // AI animator
        AllRBodies = GetComponentsInChildren<Rigidbody>(); // AI Ragdoll rigid bodies
        m_collider = GetComponent<Collider>(); // AI parent collider

        // spawning sounds
        playSpawnSound.clip = spawnSoundsArray[Random.Range(0, spawnSoundsArray.Length)]; // choose random clip
        playSpawnSound.Play(); // play clip

        runsoundTime = Random.Range(2, 6);

        // booleans
        isDead = false; // is zombie dead
    }
	
	// Update is called once per frame
	private void Update ()
    {
        if (anim.GetBool("isSpawning") == true)
        {
            foreach (Dissolve mat in dissolveZombie) // iterate through all materials used by zombie
            {
                mat.DissolveIn();
            }
        }

        if(anim.GetBool("isSpawning") == false && isDead == false)
        {
            float distance = Vector3.Distance(player.position, transform.position); // finding distance between player and AI

            if (distance > 1.8f)
            {
                agent.isStopped = false; // zombie can move
                anim.SetBool("isAttacking", false);
                agent.SetDestination(player.position); // chase player

                if (GameManager.Instance.wave < 3) // walking zombies
                {
                    anim.SetBool("isWalking", true);
                }
                else // zombies begin running
                {
                    anim.SetBool("isWalking", false);
                    anim.SetBool("isRunning", true);
                    agent.speed = 3.5f;
                }

                if (runsoundTime < 0f) // if timer has been reached
                {
                    if (!playSound.isPlaying) // if not already playing a sound
                    {
                        // chasing player sounds
                        playSound.clip = runSoundsArray[Random.Range(0, attackSoundsArray.Length)]; // choose random clip
                        playSound.Play(); // play clip

                        runsoundTime = Random.Range(3, 12); // set a new time before next sound plays
                    }
                }
                else // if timer has not been reached
                {
                    runsoundTime -= Time.deltaTime; // count down
                }
            }
            else
            {
                agent.isStopped = true; // zombie can not move
                anim.SetBool("isWalking", false);
                anim.SetBool("isRunning", false);
                anim.SetBool("isAttacking", true);

                // keep enemy facing player (source: KU ai steering presentation slides)
                Vector3 fixedPos = new Vector3(player.position.x, transform.position.y, player.position.z);
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(fixedPos - transform.position), Time.deltaTime * 6f);
            }
        }

        if (isDead)
        {
            if (anim.GetBool("isSpawning") == true)
            {
                anim.SetBool("isSpawning", false); // stop the dissolve in behaviour if killed before spawning sequence
            }

            foreach (Dissolve mat in dissolveZombie) // iterate through all materials used by zombie
            {
                mat.DissolveOut();
            }
        }
	}

    public void PlayDeath(bool dead)
    {
        playSpawnSound.clip = deathSound;
        playSpawnSound.Play();

        anim.enabled = !dead; // disable animations
        isDead = dead; // stop onUpdate calls
        agent.isStopped = dead; // stop agent moving

        m_collider.enabled = !dead; // disable collider used for player collision only. bone colliders stay for ragdoll

        foreach (Rigidbody rig in AllRBodies) // enable ragdoll
        {
           rig.isKinematic = !dead;
           rig.useGravity = dead;
        }
    }

    public void DealDamage() // used by attack animation event
    {
        // attack sounds
        playSound.clip = attackSoundsArray[Random.Range(0, attackSoundsArray.Length)]; // choose random clip
        playSound.Play(); // play clip

        ph.TakeDamage(damage);
    }

    public void SpawnFinished() // used by spawn animation event
    {
        anim.SetBool("isSpawning", false);
    }
}
