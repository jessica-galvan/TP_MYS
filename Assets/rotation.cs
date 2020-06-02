using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotation : MonoBehaviour
{
    private Vector3 actualPositionMouse;

    // Start is called before the first frame update
    void Start()
    {
        actualPositionMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Instantiate(proyectil, transform.position + transform.right, transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
