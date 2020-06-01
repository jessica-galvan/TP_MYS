using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float health = 10f;
    private float fireRate;
    private float nextFire;
    public GameObject EnemyBullet;
    


    void Start()
    {
        fireRate = 2f;
        nextFire = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0, 0.05f * Mathf.Sin(1f * Time.time));
        CheckFire();
    }

    void CheckFire()
    {
        if (Time.time > nextFire)
        {
            Instantiate(EnemyBullet, transform.position, Quaternion.identity);
            nextFire = Time.time + fireRate;
        }

    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health < 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
        Debug.Log("im destroyed");
    }
}
