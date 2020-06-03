using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;
using Vector2 = UnityEngine.Vector3;
using System.Threading;
using UnityEditor.Experimental.Rendering;
using UnityEngine.SocialPlatforms.Impl;
using UnityEditor.Experimental.GraphView;
using UnityEditor;
using UnityEngine.UIElements;
//using Quaternion = UnityEngine.Quaternion;

	[RequireComponent(typeof(LineRenderer))]

public class PlayerBehaviour : MonoBehaviour
{
	[Header("Variables")]
	[SerializeField]
	private int health = 3;
	[SerializeField]
	private float moveSpeed = 5f;
	[SerializeField]
	private float cooldownAttack = 5f;
	[SerializeField]
	private bool canDie;
	[SerializeField]
	private int coins = 0;
	//Raycast
	[SerializeField]
	private float rayLenght = 5f;
	[SerializeField]
	private int reflections = 5;
	[SerializeField]
	private int alphaMultiplier =2;

	[Header("Objetos")]
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
	[SerializeField]
	private GameObject HUD;

	//Raycast
	private Vector3 actualPositionMouse;
	private RaycastHit2D hit2D;
	private LineRenderer laser;
	[SerializeField]
	private LayerMask layersToHit;

	//Otros
	private Vector2 movement;
	private Vector2 movementDirection;
	private Vector2 direction;
	private bool llave;

	//Timer cooldown
	private bool canMove = true;
	private bool canAttack = true;
	private bool canCount = false;
	private float timer;

	//Timer Animation
	private bool canCount2 = false;
	private bool canAnimateAttack = false;
	private float timerAnimation;

	//franco intento raycast y linerenderer
	public int Reflections;
	public float maxLenght;
	private LineRenderer myLineRenderer;
	private Ray myRay;
	private RaycastHit myHit;
	private Vector3 myDirection;
	public Transform player;

	private void Start()
	{
		llave = false;
		timer = cooldownAttack;
		canDie = true;
		//laser = GetComponent<LineRenderer>();
		myLineRenderer = GetComponent<LineRenderer>();
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

		//Mouse Position
		actualPositionMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);

		//Hacer que salga un proyectil al apretar el boton izquierdo del mouse, e inicia la animacion del ataque
		if (Input.GetButtonDown("Fire1") && canAttack)
		{
			
			//canMove es para que no se pueda mover mientras este apretando el mouse.
			canMove = false;
			animator.SetFloat("OnAttack", 1f);

			//ACA IRIA EL RAYCAST Y LINERENDERER SI FUNCIONARA (cosa de que se active solo cuando el jugador esta apuntando)
		}

		//RAYCAST & LINERENDERER
		//CastLaser(transform.position, actualPositionMouse, directionProyectil);

		//Cuando el jugador deja de apretar el boton, se termina la animación. 
		if (Input.GetButtonUp("Fire1") && canAttack)
		{
			soundManagerScript.PlaySound("Shoot");
			//Instantiate(proyectil, transform.position + directionProyectil, transform.rotation);
			
			//Con esto, le agregamos un offset del pivot del player al tiro. El alpha multiplier es para separarlo más porque con solo la normal quizas no alcanza.
			direction = (Vector3)actualPositionMouse - (Vector3)transform.position;
			direction.Normalize();
			var dn = transform.position +  direction * alphaMultiplier;
			Instantiate(proyectil, dn, transform.rotation);
			
			canMove = true;
			//Start cooldown attack
			canCount = true;
			canAttack = false;
			timer = cooldownAttack;
			//un segundo timer que extiende un segundo la animación del brazo, me parece que queda mejor. 
			timerAnimation = 0.1f;
			canAnimateAttack = true;
			canCount2 = true;
		}
		
		//El timer del cooldown para el ataque
		if (timer>= 0.0f && canCount)
		{
			timer -= Time.deltaTime;
		}
		else if (timer <= 0.0f && !canAttack)
		{
			canCount = false;
			timer = 0.0f;
			canAttack = true;
		}

		//Timer de la animacion, dejenlo ser, ese segundo extra queda mejor.
		if (timerAnimation >= 0.0f && canCount2)
		{
			timerAnimation -= Time.deltaTime;
		}
		else if (timerAnimation <= 0.0f && canAnimateAttack)
		{
			canCount2 = false;
			timerAnimation = 0.0f;
			canAnimateAttack = false;
			animator.SetFloat("OnAttack", 0);
		}


		//RAYCAST INTENTO DOS - FRANCO
		/*myRay = new Ray(transform.position, actualPositionMouse);
		myLineRenderer.positionCount = 1;
		myLineRenderer.SetPosition(0, player.transform.position);
		float remainingLenght = maxLenght;*/

		/*for (int i = 0; i < Reflections; i++)
		{
			if (Physics.Raycast(myRay.origin, myRay.direction, out myHit, remainingLenght))
			{
				myLineRenderer.positionCount += 1;
				myLineRenderer.SetPosition(myLineRenderer.positionCount - 1, myHit.point);
				remainingLenght -= Vector3.Distance(myRay.origin, myHit.point);
				myRay = new Ray(myHit.point, Vector3.Reflect(myRay.direction, myHit.normal));
				if (myHit.collider.tag != "Mole")
					break;
			}
			else
			{
				myLineRenderer.positionCount += 1;
				myLineRenderer.SetPosition(myLineRenderer.positionCount - 1, myRay.origin + myRay.direction * remainingLenght);
			}
		}*/


		/*for (int i = 0; i < Reflections; i++)
		{
			if (Physics.Raycast(myRay.origin, myRay.direction, out myHit, remainingLenght))
			{
				myLineRenderer.positionCount += 1;
				myLineRenderer.SetPosition(myLineRenderer.positionCount - 1, myHit.point);
				remainingLenght -= Vector3.Distance(myRay.origin, myHit.point);
				myRay = new Ray(myHit.point, Vector3.Reflect(myRay.direction, myHit.normal));
				if (myHit.collider.tag != "Mole")
					break;
			}
			else
			{
				myLineRenderer.positionCount += 1;
				myLineRenderer.SetPosition(myLineRenderer.positionCount - 1, myRay.origin + myRay.direction * remainingLenght);
			}
		}*/
	}
	public void FixedUpdate()
	{
		if(canMove)
        {
			rb.MovePosition((Vector2)rb.position + (movement * moveSpeed * Time.fixedDeltaTime));
		}

		//Para obtener la direccion a cual mirar segun donde este el mouse. NO BORRAR
		/*Vector2 lookDir = (Vector2)actualPositionMouse - (Vector2)rb.position;
		float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
		rb.rotation = angle;*/
	}

	public void TakeEnemyDamage(int enemyDamage)
	{
		health -= enemyDamage;
		scoreScript.health = health;
		if (health <= 0)
		{
            if (canDie)
            {
				PlayerDie();
			}
		}
	}
	void PlayerDie()
	{
		DeathScreen.SetActive(true);
		HUD.SetActive(false);
		Time.timeScale = 0f;
	}

	/*void CastLaser(Vector2 position, Vector2 direction, Vector2 offset) 
	{
		for (int i = 0; i < reflections; i++)
		{
			Debug.Log("Hola " + i);
			//Ray ray = new Ray (position, direction);
			hit2D = Physics2D.Raycast(position + offset, direction, rayLenght, layersToHit);
			//laser.positionCount += 1;
			//laser.SetPosition(laser.positionCount - 1, hit2D.point);

			//EHHH... 
			//var remainingLenght -= Vector2.Distance(ray.origin, hit2D.point);
			
			if (hit2D)
			{
				//Debug.DrawLine(position, hit2D.point, Color.red);
				//Debug.DrawRay(position, direction * rayLenght, Color.red);
				position = hit2D.point;
				direction = hit2D.normal;
			} 
			else
            {
				Debug.DrawRay(position, direction * rayLenght, Color.blue);
				//laser.positionCount += 1;
				//laser.SetPosition(laser.positionCount - 1, position + offset + direction);
				//laser.SetPosition(laser.positionCount - 1, position + offset + direction * remainingLenght);
				break;
			}
		}
	}*/

	public void OnTriggerEnter2D(Collider2D col)
	{
		//si la colision tiene tag Key
		if (col.gameObject.CompareTag("Key"))
		{
			llave = true;
			scoreScript.llave= true;
			Destroy(col.gameObject);
		}

		//si la colision tiene tag Door
		if (col.gameObject.CompareTag("Door"))
		{
			//y tiene el bool llave en true
			if (llave)
			{
				VictoryScreen.SetActive(true);
				HUD.SetActive(false);
				Time.timeScale = 0f;
			}
			else
			{
				//Acá pasaria algo quizas? Un cartel que diga que falta la llave, o bueno, nada.
				Debug.Log("No tenes la llave");
			}
		}

		//Colecciona monedas
		if (col.gameObject.CompareTag("collectable"))
		{
			soundManagerScript.PlaySound("PickKey");
			scoreScript.coins += 1;
			Destroy(col.gameObject);
		}
		
	}
}
