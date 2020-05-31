﻿using System.Collections;
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
	private LineRenderer laser;
	private RaycastHit2D hit2D;
	[SerializeField]
	private LayerMask layersToHit;
	[SerializeField]
	private float rayLenght = 5f;
	[SerializeField]
	private int reflections;

	//Condicion de ganar
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
		if (Input.GetKeyDown(KeyCode.Mouse0))
		{
			Instantiate(proyectil, firePoint.transform.position, transform.rotation);
			animator.SetFloat("OnAttack", 1f);
		}
		//Cuando el jugador deja de apretar el boton, se termina la animación. HAY QUE CAMBIARLO por un timer esto. 
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
			animator.SetFloat("OnAttack", 0);
		}

		//RAYCAST
		hit2D = Physics2D.Raycast(transform.position, actualPositionMouse, rayLenght);
		float remainingLenght = rayLenght;
		var ray = new Ray(transform.position, transform.right); //ver que onda acá
		
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

	private void FixedUpdate()
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
		//Comente esto porque me pudri de perder cuando necesito probar cosas.
		//Destroy(gameObject);
	}

    private void OnTriggerEnter2D(Collider2D col)
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
				//Acá iria a la pantalla de victoria o que aparezca visualemente en el canvas
				Debug.Log("Ganaste");
            } else
            {
				//Acá pasaria algo quizas? Un cartel que diga que falta la llave, o bueno, nada.
				Debug.Log("No tenes la llave");
            }
		}
	}
}
