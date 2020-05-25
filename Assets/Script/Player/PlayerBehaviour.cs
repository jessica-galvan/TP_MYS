using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
//using Vector3 = UnityEngine.Vector3;
using Vector2 = UnityEngine.Vector3;
public class PlayerBehaviour : MonoBehaviour
{
	private float moveSpeed = 5f;
	private Vector2 movement;
	[SerializeField]     //Permite que se vea en Unity aunque sea privado, asi le arrastramos el objeto
	private Rigidbody2D rb;
	[SerializeField]
	private GameObject proyectil;
	[SerializeField]
	private Animator animator;

	//private Vector3 actualMousePosition;


	void Update()
    {
		//Capta movimientos Horizontales y Verticales. 
		movement.x = Input.GetAxisRaw("Horizontal");
		movement.y = Input.GetAxisRaw("Vertical");

		//Dentro del animator, set las vairables especificadas con los valores obtenidos de con los GetAxisRaw.
		animator.SetFloat("Horizontal", movement.x);
		animator.SetFloat("Vertical", movement.y);
		animator.SetFloat("Speed", movement.sqrMagnitude);

		//Hacer que salga un proyectil al apretar el boton izquierdo del mouse.
		if (Input.GetKeyDown(KeyCode.Mouse0))
		{
			Instantiate(proyectil, transform.position, transform.rotation);
			//animator.SetTrigger("OnAttack");
		}
	}

	private void FixedUpdate()
	{
		rb.MovePosition((Vector2)rb.position + (movement * moveSpeed * Time.fixedDeltaTime));
	}
}
