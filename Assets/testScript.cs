using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testScript : MonoBehaviour
{
    public Transform firePoint;

    private void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            Debug.Log("ROTATION");
            firePoint.Rotate(0, 180, 0);
            
        }
    }


}
