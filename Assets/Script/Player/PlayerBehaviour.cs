using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;
using Vector2 = UnityEngine.Vector3;
using System.Threading;
//using Quaternion = UnityEngine.Quaternion;

public class PlayerBehaviour : MonoBehaviour
{
	public Transform firePoint;
	private float moveSpeed = 5f;
	private Vector2 movement;
	private Vector2 movementDirection;
	[SerializeField]     //Permite que se vea en Unity aunque sea privado, asi le arrastramos el objeto
	private Rigidbody2D rb;
	[SerializeField]
	private GameObject proyectil;
	[SerializeField]
	private Animator animator;
	[SerializeField]
	public float health = 1f;
	private float timeOfAnimationAttack = 2f;

	//Raycast
	private Vector3 actualPositionMouse;
	private LineRenderer myLineRenderer;
	private RaycastHit2D hit2D;
	[SerializeField]
	private LayerMask layersToHit;
	[SerializeField]
	private float rayLenght = 5f;
	private int currentImpact;
	[SerializeField]
	private int maxImpact;


	private void Start()
	{
		myLineRenderer = GetComponent<LineRenderer>();
	}


	void Update()
	{

		//Capta movimientos Horizontales y Verticales. 
		movement.x = Input.GetAxisRaw("Horizontal");
		movement.y = Input.GetAxisRaw("Vertical");
		movementDirection = new Vector2(movement.x, movement.y);

		//Animator.
		if (movementDirection != Vector2.zero)
		{
			animator.SetFloat("Horizontal", movement.x);
			animator.SetFloat("Vertical", movement.y);
		}
		animator.SetFloat("Speed", movement.sqrMagnitude);

		//MOUSE POSITION
		actualPositionMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);

		//Hacer que salga un proyectil al apretar el boton izquierdo del mouse.
		if (Input.GetKeyDown(KeyCode.Mouse0))
		{
			GameObject shootObject = Instantiate(proyectil, firePoint.transform.position, transform.rotation);
			animator.SetFloat("OnAttack", 1f);
		}
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
			animator.SetFloat("OnAttack", 0);
		}

		//RAYCAST
		/*hit2D = Physics2D.Raycast(transform.position, actualPositionMouse, rayLenght);
		if (hit2D)
		{
			Debug.DrawLine(transform.position, actualPositionMouse, Color.red);
			if(hit2D.)
			Debug.Log("hola");
		}*/
	}

	private void FixedUpdate()
	{
		rb.MovePosition((Vector2)rb.position + (movement * moveSpeed * Time.fixedDeltaTime));

		//Para obtener la direccion a cual mirar segun donde este el mouse. 
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
		//Destroy(gameObject);
	}
}
