using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrentEnemy : MonoBehaviour
{
    public int health = 10;
    float fireRate;
    float nextFire;
    public GameObject enemyTrentBullet;
    public Transform enemyTrentFirePoint;
    public Animator trentAnimator;

    // Start is called before the first frame update
    void Start()
    {
        //var oldPosition = transform.position += new Vector3(0.04f * Mathf.Sin(1f * Time.time), 0);
        fireRate = 2f;
        nextFire = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        

        if (!PauseMenuBehaviour.GameIsPause)
        {
            transform.position += new Vector3(0.04f * Mathf.Sin(1f * Time.time), 0);
            trentAnimator.SetBool("IsWalking", true);
        }

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

    public void TakeSecondDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            soundManagerScript.PlaySound("EnemyDie");
            TrentDie();
        }
    }
    void TrentDie()
    {
        trentAnimator.SetBool("Die", true);
        Destroy(gameObject, 1f);
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
