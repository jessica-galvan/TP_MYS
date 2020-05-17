using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class CubeIzquierda : MonoBehaviour
{
    public float speed;
    private Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(6, 1);
    }

    // Update is called once per frame
    void Update()
    {

        transform.position += -transform.right;
        transform.position += direction * (speed * Time.deltaTime);
       
        

    }
}
