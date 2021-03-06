﻿using UnityEngine;
using System;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class ProyectilPlayerBehaviour : MonoBehaviour
{
	//Variables internas
	private Vector2 actualPositionMouse; 
	private Vector2 direction;
	private DateTime birthObject;
	private double timeOfLife = 3; //porque el AddSeconds lo pide como double. 
	private GameObject player;

	//Objetos que hay que asignar
	[SerializeField]
	private Rigidbody2D rb;
	[SerializeField]
	public int damage = 1;
	[SerializeField]
	private float speed = 6f;

	void Awake()
	{
		//Ignoro la colision del proyectil con el jugador.
		Physics2D.IgnoreLayerCollision(8, 11);
		//Obtener la posicion actual del mouse dentor del juego.	
		actualPositionMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		
		//Guardame el tiempo en el que fue creado
		birthObject = DateTime.Now;

		//Busca al player
		player = GameObject.Find("Player");
		
		//Calculame el vector de donde sale a donde va.
		direction = (Vector3)actualPositionMouse - player.transform.position;
		direction.Normalize();

		//Faltaria hacerlo rotar (si el tiro deja de ser una cosa rendonda que demuestre que no rota)
		//quizas usaria algo como transform.Rotate(0, 0, Mathf(direction.y, direction.x) * Mathf.Rad2Deg);
	}

	void Update()
	{	
		//Se lo transforma a 3D para que funcione. 
		transform.position += (Vector3)direction * speed * Time.deltaTime;

		//Para darle un tiempo de vida al proyectil y que luego se destruya de la escena EL OBJETO.
		if (DateTime.Now > birthObject.AddSeconds(timeOfLife))
		{
			Destroy(gameObject);
		}
	}
	//hacemos que cuando entra en collision con una pared, refleje. 
	private void OnCollisionEnter2D(Collision2D collision)
    {
		if (collision.gameObject.CompareTag("Wall"))
        {
			direction = Vector2.Reflect(direction, collision.contacts[0].normal);
		}

		//if (collision.gameObject.CompareTag("Player"))
		//{
		//	Debug.Log("Choco");
		//	Destroy(gameObject);
		//}
	}

	public void OnTriggerEnter2D(Collider2D collision)
	{
		//antes de hacer el get component, hagamos que chequee con que collisiona. Dependiendo del tag que tenga, hace una cosa o la otra.
		if (collision.gameObject.tag == "Mole")
		{
			soundManagerScript.PlaySound("HitEnemy");
			Enemy mole = collision.GetComponent<Enemy>();
			mole.TakeDamage(damage);
			Destroy(gameObject);
		}
		else if (collision.gameObject.tag == "Trent")
		{
			soundManagerScript.PlaySound("HitEnemy");
			TrentEnemy trent = collision.GetComponent<TrentEnemy>();
			trent.TakeSecondDamage(damage);
			Destroy(gameObject);
		}
	}

    public void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Wall")
        {
			Debug.Log("DESTROY");
        }
    }
}
