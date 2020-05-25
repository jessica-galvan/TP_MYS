using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class NewScriptMove : MonoBehaviour
{
    public Transform target;
    public float speed;
    public Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        /* mira a un target
        Vector3 difference = target.position - transform.position;
        float angulo = Mathf.Atan2(difference.x, difference.y) * Mathf.Rad2Deg;
        
        transform.rotation = Quaternion.Euler(0, 0, angulo);
        */

        Vector3 posicionMouseMundo = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(posicionMouseMundo.x, posicionMouseMundo.y, 0);

        /*Vector3 diferencia = posicionMouseMundo - this.transform.position;
        float angulo = Mathf.Atan2(diferencia.y, diferencia.x) * Mathf.Rad2Deg;
        
        transform.rotation = Quaternion.Euler(0, 0, angulo);

        //Debug.DrawRay(transform.position, diferencia.normalized * 10f, Color.blue);

        //Movimiento con Get Key
        /* if (Input.GetKey(KeyCode.W))
             transform.position += Vector3.up * speed * Time.deltaTime;
             */

        //Movimiento con Axis
        var direction = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
             transform.position += direction * speed * Time.deltaTime;

    }
}
