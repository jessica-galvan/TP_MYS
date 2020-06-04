using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keyRotation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!PauseMenuBehaviour.GameIsPause)
        {
            transform.Rotate(1, 2, 0);
        }
    }
}
