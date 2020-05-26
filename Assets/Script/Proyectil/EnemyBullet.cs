using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed = 7f;
    public Rigidbody2D rb2;
    PlayerBehaviour target;
    new Vector2 movement;


    // Start is called before the first frame update
    void Start()
    {
        rb2.GetComponent<Rigidbody2D>();
        target = GameObject.FindObjectOfType<PlayerBehaviour>();
        movement = (target.transform.position - transform.position).normalized * speed;
        rb2.velocity = new Vector2(movement.x, movement.y);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerBehaviour playerdetection = collision.GetComponent<PlayerBehaviour>();
        if (playerdetection != null)
        {
            playerdetection.TakeEnemyDamage(1);
            Destroy(gameObject);
        }
    }

}
