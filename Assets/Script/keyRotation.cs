using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keyRotation : MonoBehaviour
{
    void Update()
    {
        if (!PauseMenuBehaviour.GameIsPause)
        {
            transform.Rotate(1, 2, 0);
        }
    }
}
