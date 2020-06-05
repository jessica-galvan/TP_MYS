using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed = 7f;
    public Rigidbody2D rb2;
    PlayerBehaviour target;
    private Vector2 movement;
    public int enemyDamage = 1;

    // Start is called before the first frame update
    void Start()
    {
        rb2.GetComponent<Rigidbody2D>();
        target = GameObject.FindObjectOfType<PlayerBehaviour>();
        movement = (target.transform.position - transform.position).normalized * speed;
        rb2.velocity = new Vector2(movement.x, movement.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerBehaviour playerdetection = collision.GetComponent<PlayerBehaviour>();
        if (playerdetection != null)
        {
            soundManagerScript.PlaySound("PlayerLooseLife");
            playerdetection.TakeEnemyDamage(enemyDamage);
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }

    

}
