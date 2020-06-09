using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class CursorManager : MonoBehaviour
{
    public Texture2D cursorReady;
    public Texture2D cursorCharging;
    public Texture2D cursorMenu;
    public Vector2 readyVector;
    public Vector2 chargingVector;
    public Vector2 menuVector;
    public bool canAttack;
    public bool menu;

    void Start()
    {
        chargingVector = new Vector2(12, 12);
        readyVector = new Vector2(72, 72);
        menuVector = new Vector2(1, 1);
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

        if(menu)
        {
            Cursor.SetCursor(cursorCharging, chargingVector, CursorMode.Auto);
        }
    }
}
