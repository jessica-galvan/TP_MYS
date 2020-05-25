using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public Vector3 initialRotation;
    public float speedRotation;
    public Transform target;


    // Start is called before the first frame update
    void Start()
    {
       transform.rotation = Quaternion.Euler(initialRotation);
    }

    // Update is called once per frame
    void Update()
    {
       transform.Rotate(new Vector3(0, 0, speedRotation));
    }
}
