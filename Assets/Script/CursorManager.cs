using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;
using Vector2 = UnityEngine.Vector2;

public class CursorManager : MonoBehaviour
{
    public Texture2D cursorReady;
    public Texture2D cursorCharging;
    public Vector2 readyVector;
    public Vector2 chargingVector;
    public bool canAttack;

    void Start()
    {
        chargingVector = new Vector2(12, 12);
        readyVector = new Vector2(72, 72);
        Cursor.SetCursor(cursorReady, readyVector, CursorMode.Auto);
    }

    private void Update()
    {
        canAttack = PlayerBehaviour.canAttack;
        if (canAttack)
        {
            Cursor.SetCursor(cursorReady, readyVector, CursorMode.Auto);
        }
        else
        {
            Cursor.SetCursor(cursorCharging, chargingVector, CursorMode.Auto);
        }
    }
}
