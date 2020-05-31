using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrentEnemy : MonoBehaviour
{
    public float health = 10f;
    float fireRate;
    float nextFire;
    public GameObject enemyTrentBullet;
    public Transform enemyTrentFirePoint;

    // Start is called before the first frame update
    void Start()
    {
        fireRate = 2f;
        nextFire = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0.04f * Mathf.Sin(1f * Time.time), 0);
        CheckTrentFire();
        
    }
    void CheckTrentFire()
    {
        if (Time.time > nextFire)
        {
            Instantiate(enemyTrentBullet, enemyTrentFirePoint.position, Quaternion.identity);
            nextFire = Time.time + fireRate;
        }


    }    

    public void TakeSecondDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            TrentDie();
        }
    }
    void TrentDie()
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerBehaviour player = collision.GetComponent<PlayerBehaviour>();
        if (player != null)
        {
            player.TakeEnemyDamage(1);
        }
    }
}
