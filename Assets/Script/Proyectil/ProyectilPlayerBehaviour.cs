using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Numerics;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class ProyectilPlayerBehaviour : MonoBehaviour
{
	//public Transform firePoint;
	//public GameObject bullet;
	public float Damage = 1f;
	private Vector2 actualPositionMouse; 
	private Vector2 direction;
	private float speed = 6f;
	private DateTime birthObject;
	private double timeOfLife = 3; //porque el AddSeconds lo pide como doble. 
	private GameObject player;
	[SerializeField]
	private Rigidbody2D rb;
	//[SerializeField]
	//private float force;

	void Awake()
	{
		//Obtener la posicion actual del mouse dentor del juego.	
		actualPositionMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		birthObject = DateTime.Now;
		player = GameObject.Find("Player");
		direction = (Vector3)actualPositionMouse - player.transform.position;
		direction.Normalize();
	}

	void Update()
	{
		//Se lo transforma a 3D para que funcione. 
		transform.position += (Vector3)direction * speed * Time.deltaTime;

		//Instantiate(bullet, firePoint.transform.position, firePoint.rotation);

		//Para darle un tiempo de vida al proyectil y que luego se destruya de la escena EL OBJETO.
		if (DateTime.Now > birthObject.AddSeconds(timeOfLife))
		{
			Destroy(gameObject);
		}
	}
	//hacemos que cuando entra en collision con una pared, refleje. 
    private void OnCollisionEnter2D(Collision2D collision)
    {
		if(collision.gameObject.CompareTag("Wall"))
        {
			direction = Vector2.Reflect(direction, collision.contacts[0].normal);
			Debug.Log("Choco");
		}
		if(collision.gameObject.CompareTag("Player"))
		{
			Destroy(gameObject);
		}
    }

	public void OnTriggerEnter2D(Collider2D collision)
	{
		Enemy enemy = collision.GetComponent<Enemy>();
		{
			if (enemy != null)
			{
				enemy.TakeDamage(1);
			}

			Destroy(gameObject);

		}
	}
}
