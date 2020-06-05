using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

public class TrentEnemyBullet : MonoBehaviour
{
    public float speed = 5f;
    public Rigidbody2D rbTrent;
    [SerializeField]
    private int damage = 1;
    

    // Start is called before the first frame update
    void Start()
    {
        var movement = new Vector2(1, 0);
        movement = transform.position += transform.right * speed * Time.deltaTime;
        rbTrent.velocity = new Vector2(-movement.x, 0);

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerBehaviour player = collision.GetComponent<PlayerBehaviour>();
        if (player != null)
        {
            soundManagerScript.PlaySound("PlayerLooseLife");
            player.TakeEnemyDamage(damage);
        }
        Destroy(gameObject);
    }
}
