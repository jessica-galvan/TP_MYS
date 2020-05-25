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
		movement.x = Input.GetAxisRaw("Horizontal");
		movement.y = Input.GetAxisRaw("Vertical");

		animator.SetFloat("Horizontal", movement.x);
		animator.SetFloat("Vertical", movement.y);
		animator.SetFloat("Speed", movement.sqrMagnitude);

		//Obtener la posicion actual del mouse dentor del juego. Y convierto el Vector3 en 2.	
		//actualMousePosition = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
		//Hacer que salga un proyectil al apretar el boton izquierdo del mouse.
		if (Input.GetKeyDown(KeyCode.Mouse0))
		{
			Instantiate(proyectil, transform.position, transform.rotation);
			//animatorPlayer.SetTrigger("OnAttack");
		}
				
	}

	private void FixedUpdate()
	{
		rb.MovePosition((Vector2)rb.position + (movement * moveSpeed * Time.fixedDeltaTime));
	}
}
