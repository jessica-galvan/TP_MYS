using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Numerics;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class ProyectilPlayerBehaviour : MonoBehaviour
{
	//Variables internas
	private Vector2 actualPositionMouse; 
	private Vector2 direction;
	private DateTime birthObject;
	private double timeOfLife = 3; //porque el AddSeconds lo pide como doble. 
	
	//Objetos que hay que asignar
	[SerializeField]
	private Rigidbody2D rb;
	[SerializeField]
	public int damage = 1;
	[SerializeField]
	private float speed = 6f;

	//Objetos que busca internamente
	private GameObject player;
	

	void Awake()
	{
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
		//NO FUNCIONA LUEGO DE QUE EL PROYETIL SE HIZO TRIGGER
		if(collision.gameObject.CompareTag("Wall"))
        {
			direction = Vector2.Reflect(direction, collision.contacts[0].normal);
		}
		if(collision.gameObject.CompareTag("Player"))
		{
			Destroy(gameObject);
		}
    }

	public void OnTriggerEnter2D(Collider2D collision)
	{
		//NO FUNCIONA EL REBOTE, NO SE SABE PORQUE TODAVIA, problema de haberlo pasado a Trigger
		ContactPoint2D[] contacts = new ContactPoint2D[2];
		//si chocas contra una pared, rebota
		if (collision.gameObject.CompareTag("Wall"))
		{
			Vector3 normal = contacts[0].normal;
			direction = Vector2.Reflect(direction, normal);
		}

		//ESTO SI FUNCIONA
		//antes de hacer el get component, hagamos que chequee con que collisiona. Dependiendo del tag que tenga, hace una cosa o la otra.
		if (collision.gameObject.tag == "Mole")
		{
			Enemy mole = collision.GetComponent<Enemy>();
			mole.TakeDamage(damage);
			Destroy(gameObject);
		}
		else if (collision.gameObject.tag == "Trent")
		{
			TrentEnemy trent = collision.GetComponent<TrentEnemy>();
			trent.TakeSecondDamage(damage);
			Destroy(gameObject);
		}


		//FRANCO hizo esto.
		/*Enemy enemy = collision.GetComponent<Enemy>();
		{
			if (enemy)
			{
				enemy.TakeDamage(damage);
				Debug.Log("im an enemy!");
			}
			Destroy(gameObject);

		}
		TrentEnemy trent = collision.GetComponent<TrentEnemy>();
		if (trent)
		{
			trent.TakeSecondDamage(damage);
			Debug.Log("Im the second enemy");
		}
		Destroy(gameObject);*/
	}	
}
