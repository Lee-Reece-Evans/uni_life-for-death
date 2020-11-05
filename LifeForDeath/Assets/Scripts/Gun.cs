using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour {

    // ui crosshair/hitmarker
    public Image crosshair;
    public Image hitmarker;

    public GameObject player;
    PlayerHealth ph;
    public GameObject zombieImpactEffect;

    public float normDamage = 10f;
    public float headDamage = 15f;
    public float fireRate = 7f;
    public float ammoCost = 1f;
    public float nextFireTime = 0f;

    public Camera fpscam;
    public ParticleSystem muzzleFlash;
    public Animator anim;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        ph = player.GetComponent<PlayerHealth>();
        anim = GetComponent<Animator>();
        hitmarker.enabled = false;
    }

    private void Update ()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + 1f / fireRate;
            Shoot();
        }
        if (Input.GetButtonUp("Fire1") && anim.GetBool("Fire") == true)
        {
            anim.SetBool("Fire", false);
        }
	}

    // credit to KU University Presentation on raycasting
    private void Shoot()
    {
        GameManager.Instance.totalShotsfired += 1f;

        muzzleFlash.Play();
        GetComponent<AudioSource>().Play();
        anim.SetBool("Fire", true);

        RaycastHit hit;

        // bit shift index of layer 11 and layer 2 to get bit mask.
        int layerMask = 1 << 11 | 1 << 2; // check layers 11 and 2
        layerMask = ~layerMask; // inverse to ignore layers 11 and 2, and check everything else

        if (Physics.Raycast(fpscam.transform.position, fpscam.transform.forward, out hit, Mathf.Infinity, layerMask))
        {
            EnemyHealth enemy = hit.transform.GetComponentInParent<EnemyHealth>(); // multiple colliders for each body part
            if (enemy != null)
            {
                Hitmarker();

                if (hit.collider.name == "head") // headshot
                {
                    enemy.TakeDamage(headDamage);
                    GameManager.Instance.totalHeadshots += 1f;
                }
                else
                {
                    enemy.TakeDamage(normDamage); // bodyshot
                    GameManager.Instance.totalBodyshots += 1f;
                }

                GameObject zomImpact = Instantiate(zombieImpactEffect, hit.point, Quaternion.LookRotation(hit.normal)); // use surface normal to point effect out. source: (brackeys - youtube).
                Destroy(zomImpact, 2f);
            }
            else
            {
                ph.MissedShot(ammoCost); // take some player health as punishment for missing
            }
        }
    }

    void Hitmarker()
    {
        StopCoroutine("ShowHitMarker"); // stop coroutine before starting again
        hitmarker.enabled = true;
        crosshair.enabled = false;
        StartCoroutine("ShowHitMarker");
    }

    IEnumerator ShowHitMarker()
    {
        yield return new WaitForSeconds(0.1f);
        crosshair.enabled = true;
        hitmarker.enabled = false;
    }
}
