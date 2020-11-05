using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public GameObject healthpotion;

    public GameObject player;
    PlayerHealth ph;
    PlayerCollectables pc;

    EnemyController ec;

    public float health = 100f;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        ph = player.GetComponent<PlayerHealth>();
        pc = player.GetComponent<PlayerCollectables>();
        ec = GetComponent<EnemyController>();

        health += GameManager.Instance.zomHealthIncrease;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0f && ec.isDead == false)
        {
            DropHealth();
            pc.AddSouls(1);
            GameManager.Instance.totalSouls += 1;
            Die();
        }
    }

    private void DropHealth()
    {
        int randNum = Random.Range(1, 4); // random number between 1 and 4

        if (ph.health < 100 && randNum == 3 && GameManager.Instance.HPspawned < 3) // 3 is the lucky number !
        {
            Vector3 pos = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z); // spawn above ground
            Instantiate(healthpotion, pos, Quaternion.identity);
            GameManager.Instance.HPspawned++;
        }
    }

    private void Die()
    {
        ec.PlayDeath(true); // play death animation through enemy controller script
        GameManager.Instance.DecreaseZombieCount();
        Destroy(gameObject, 3.75f); // destroy enemy body
    }
}
