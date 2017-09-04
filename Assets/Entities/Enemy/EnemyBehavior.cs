using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour {
    public GameObject projectile;
    public float projectileSpeed = 10;
    public float shotsPerSecond = 0.5f;
    public float health = 150;
    public int scoreValue = 150;
    public AudioClip fireSound;
    public AudioClip deathSound;
    private ScoreKeeper scoreKeeper;

    private void Start()
    {
       scoreKeeper = GameObject.Find("Score").GetComponent<ScoreKeeper>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision);
        Projectile missile = collision.gameObject.GetComponent<Projectile>();
        if (missile)
        {
            health -= missile.GetDamage();
            missile.Hit();
            if(health <= 0)
            {
                Die();
            }
            Debug.Log("Hit by a projectile!");
        }
    }

    private void Die()
    {
        AudioSource.PlayClipAtPoint(deathSound, transform.position);
        Destroy(gameObject);
        scoreKeeper.updateScore(scoreValue);
    }

    private void Update()
    {
        float probability = shotsPerSecond * Time.deltaTime;
        if (Random.value < probability)
        {
            Fire();
        }
        
    }

    private void Fire()
    {
        Vector3 startPosition = transform.position + new Vector3(0, -0.8f, 0);
        GameObject missile = Instantiate(projectile, startPosition, Quaternion.identity) as GameObject;
        missile.GetComponent<Rigidbody2D>().velocity = new Vector3(0, -projectileSpeed, 0);
        AudioSource.PlayClipAtPoint(fireSound, transform.position);
    }
}
