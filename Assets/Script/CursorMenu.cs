using UnityEngine;

public class CursorMenu : MonoBehaviour
{
    public Texture2D cursorMenu;
    public Vector2 menuVector;

    void Start()
    {
        menuVector = new Vector2(1, 1);
        Cursor.SetCursor(cursorMenu, menuVector, CursorMode.Auto);
    }
}
