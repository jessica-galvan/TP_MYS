using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ProyectilPlayerBehaviour : MonoBehaviour
{ 
	private Vector2 actualPositionMouse; 
	private Vector2 direction;
	private float speed = 6f;
	private DateTime birthObject;
	private double timeOfLife = 3; //porque el AddSeconds lo pide como doble. 
	[SerializeField]
	private GameObject player;

	void Awake()
	{
		actualPositionMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		birthObject = DateTime.Now;
		player = GameObject.Find("Player");
		Debug.Log(player);
	}

	void Update()
	{
		direction = (Vector3)actualPositionMouse - player.transform.position;
		direction.Normalize();
		transform.position += (Vector3)direction * speed * Time.deltaTime;
		Debug.Log(actualPositionMouse);
		//Se lo transforma a 3D para que funcione. 

		//Para darle un tiempo de vida al proyectil y que luego se destruya de la escena EL OBJETO.
		if (DateTime.Now > birthObject.AddSeconds(timeOfLife))
		{
			Destroy(gameObject);
		}
	}

}
