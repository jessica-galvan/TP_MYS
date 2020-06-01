using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float health = 10f;
    private float fireRate;
    private float nextFire;
    public GameObject EnemyBullet;
    public Animator animatorMole;


    void Start()
    {
        fireRate = 2f;
        nextFire = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.position += new Vector3(0, 0.05f * Mathf.Sin(1f * Time.time));
        animatorMole.SetBool("Walking", true);
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
        animatorMole.SetBool("Die", true);
        Destroy(gameObject, 1f);
    }
    

}
