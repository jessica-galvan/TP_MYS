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
using System.Diagnostics.Tracing;
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
	private int reflections = 2;
	[SerializeField]
	private float alphaMultiplier =2;
	[SerializeField]
	private LayerMask layersToHit;

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
	private Vector2 dn;

	//Raycast
	private Vector3 actualPositionMouse;
	private RaycastHit2D hit2D;
	private LineRenderer laser;
	private Transform laserHit;

	//Otros
	private bool llave;
	private Vector2 movement;
	private Vector2 movementDirection;
	private Vector2 direction;
	private Vector2 facingDirection;
	private Vector2 checkDirection;

	//Timer cooldown
	private bool canMove = true;
	private bool canAttack = true;
	private bool canCount = false;
	private float timer;

	//Timer Animation
	private bool canCount2 = false;
	private bool canAnimateAttack = false;
	private float timerAnimation;


	private void Start()
	{
		llave = false;
		timer = cooldownAttack;
		canDie = true;
		laser = GetComponent<LineRenderer>();
		laser.useWorldSpace = true;
		checkDirection = new Vector2(0, 0);
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
			facingDirection = new Vector2(movement.x, movement.y);
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

		//Cuando el jugador deja de apretar el boton, se termina la animación. 
		if (Input.GetButtonUp("Fire1") && canAttack)
		{
			soundManagerScript.PlaySound("Shoot");
		
			//Con esto, le agregamos un offset del pivot del player al tiro. El alpha multiplier es para separarlo más porque con solo la normal quizas no alcanza.
			direction = (Vector3)actualPositionMouse - (Vector3)transform.position;
			direction.Normalize();

			dn = transform.position +  direction * alphaMultiplier;
			Debug.Log("dn: " + dn + "origen: " + transform.position);
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

		//RAYCAST & LINERENDERER
		CastLaser();

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

	void CastLaser() 
	{
		int laserContador = 0;
		int contador = 1;
		Vector2 position = transform.position;
		Vector2 direction = actualPositionMouse - position;
		direction.Normalize();
		hit2D = Physics2D.Raycast(position, direction, rayLenght, layersToHit);
		Debug.DrawLine(position, hit2D.point, Color.red);

		while (contador < reflections)
        {
			laser.positionCount = reflections;
			hit2D = Physics2D.Raycast(position, direction, rayLenght, layersToHit);

			if (hit2D)
			{
				//Line Renderer StartingPoint
				//laser.SetPosition(laser.positionCount-1, position);
				laser.SetPosition(laserContador, position);

				//Change position to hit2.point as a new starting point
				direction = (Vector3)hit2D.point - position;
				direction = Vector2.Reflect(direction, hit2D.normal);
				position = hit2D.point;
				contador++;
				laserContador++;

				//Line Renderer Ending Point changed after hit2d.point
				laser.SetPosition(laserContador, position);
			}
			else
			{
				//Debug.DrawLine(position, direction * rayLenght, Color.blue);
				//laser.positionCount = 1;
				//laser.SetPosition(0, position + direction);
				//laser.SetPosition(1, position + direction * 5f);
				break;
			}
		}
	}

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

	/*private void checkAlpha() 
	{
		if(facingDirection.x >  checkDirection.x)
        {
			Debug.Log("Right");
			
			//mouse is up/down alpha at least 3.5 or 4
			//mouse is left alpha 2
			//mouse is right alpha 1
			  
        }
		else if(facingDirection.x < checkDirection.x)
        {
			Debug.Log("Left");
			
			//mouse is up/down alpha at least 3.5 or 4
			//mouse is left alpha 2
			//mouse is right alpha 1	
		}
		else if(facingDirection.y < checkDirection.y)
        {
			Debug.Log("Down");
		}
		else if (facingDirection.y > checkDirection.y)
        {
			Debug.Log("Up");
		}
	}*/

}
