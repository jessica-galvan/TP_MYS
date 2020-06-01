using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;
using Vector2 = UnityEngine.Vector3;
using System.Threading;
using UnityEditor.Experimental.Rendering;
//using Quaternion = UnityEngine.Quaternion;

public class PlayerBehaviour : MonoBehaviour
{
	[Header("Variables")]
	[SerializeField]
	private float health = 1f;
	[SerializeField]
	private float moveSpeed = 5f;
	[SerializeField]
	private float timeLeftAttack = 2f;
	//Raycast
	[SerializeField]
	private float rayLenght = 5f;
	[SerializeField]
	private int reflections;

	[Header("Objetos")]
	[SerializeField]
	private Transform firePoint;
	[SerializeField]
	private Rigidbody2D rb;
	[SerializeField]
	private GameObject proyectil;
	[SerializeField]
	private Animator animator;
	[SerializeField]
	private GameObject DeathScreen;
	[SerializeField]
	private GameObject VictoryScreen;

	//Raycast
	private Vector3 actualPositionMouse;
	private LineRenderer laser;
	private RaycastHit2D hit2D;
	//[SerializeField]
	//private LayerMask layersToHit;


	//Otros
	private Vector2 movement;
	private Vector2 movementDirection;
	private bool llave;

	private void Start()
	{
		laser = GetComponent<LineRenderer>();
		llave = false;
	}


	void Update()
	{

		//Capta movimientos Horizontales y Verticales. 
		movement.x = Input.GetAxisRaw("Horizontal");
		movement.y = Input.GetAxisRaw("Vertical");
		movementDirection = new Vector2(movement.x, movement.y);

		//Animator. Si el jugador deja de moverse, se queda mirando en el sentido en que estaba yendo
		if (movementDirection != Vector2.zero)
		{
			animator.SetFloat("Horizontal", movement.x);
			animator.SetFloat("Vertical", movement.y);
		}
		animator.SetFloat("Speed", movement.sqrMagnitude);

		//MOUSE POSITION
		actualPositionMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);

		//Hacer que salga un proyectil al apretar el boton izquierdo del mouse, e inicia la animacion del ataque
		if (Input.GetButtonDown("Fire1"))
		{
			//Instantiate(proyectil, firePoint.transform.position, transform.rotation);
			animator.SetFloat("OnAttack", 1f);
			
			//TIMER para que se cambie la animacion del ataque //NO FUNCIONA
			timeLeftAttack -= Time.deltaTime;
			if (timeLeftAttack <= 0f)
			{
				Debug.Log("Chau");
				animator.SetFloat("OnAttack", 0f);
				timeLeftAttack = 2f;
			}
		}
		
		//Cuando el jugador deja de apretar el boton, se termina la animación. HAY QUE CAMBIARLO por un timer esto. 
		if (Input.GetButtonUp("Fire1"))
		{
			Instantiate(proyectil, firePoint.transform.position, transform.rotation);
			animator.SetFloat("OnAttack", 0);
		}

		//RAYCAST
		hit2D = Physics2D.Raycast(transform.position, actualPositionMouse, rayLenght);
			float remainingLenght = rayLenght;
			var ray = new Ray(transform.position, transform.right);
		    Debug.DrawRay(transform.position, transform.right *100, Color.red);//ver que onda acá

			if (hit2D)
			{
				laser.positionCount = 1;
				laser.SetPosition(0, transform.position);
				//laser.sortingOrder = 4;
				//laser.sortingLayerName = "UI";

				for (int i = 0; i < reflections; i++)
				{

					laser.positionCount += 1;
					laser.SetPosition(laser.positionCount - 1, hit2D.point);
					remainingLenght -= Vector2.Distance(ray.origin, hit2D.point);
					ray = new Ray(hit2D.point, Vector2.Reflect(ray.direction, hit2D.normal));
					//Debug.Log("Hola " + i);
					//if (laser){ Debug.Log("Chau" + i + laser.positionCount); }
				}
			} /*else
			{
				laser.positionCount += 1;
				laser.SetPosition(laser.positionCount - 1, ray.origin + ray.direction * remainingLenght);
			}*/
	}
	public void FixedUpdate()
	{
		rb.MovePosition((Vector2)rb.position + (movement * moveSpeed * Time.fixedDeltaTime));

		//Para obtener la direccion a cual mirar segun donde este el mouse. NO BORRAR
		/*Vector2 lookDir = (Vector2)actualPositionMouse - (Vector2)rb.position;
		float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
		rb.rotation = angle;*/
	}

	public void TakeEnemyDamage(float enemyDamage)
	{
		health -= enemyDamage;
		if (health <= 0)
		{
			PlayerDie();
		}
	}
	void PlayerDie()
	{
		DeathScreen.SetActive(true);
		Time.timeScale = 0f;
		//Destroy(gameObject);
	}

	public void OnTriggerEnter2D(Collider2D col)
	{
		//si la colision tiene tag Key
		if (col.gameObject.CompareTag("Key"))
		{
			Debug.Log("Obtuviste una llave");
			//Cambiame el bool a true
			llave = true;
			//y borrame el objeto llave de la escena
			var objeto = col.gameObject;
			Destroy(objeto);
		}

		//si la colision tiene tag Door
		if (col.gameObject.CompareTag("Door"))
		{
			//y tiene el bool llave en true
			if (llave)
			{
				VictoryScreen.SetActive(true);
				Time.timeScale = 0f;
			}
			else
			{
				//Acá pasaria algo quizas? Un cartel que diga que falta la llave, o bueno, nada.
				Debug.Log("No tenes la llave");
			}
		}
	}
}
