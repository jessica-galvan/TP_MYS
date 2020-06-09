using UnityEngine;
using UnityEngine.UI;

public class scoreScript : MonoBehaviour
{
    Text text;
    public static int coins;
    public static int health;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emtyHeart;
    public static bool key;
    public Text coinText;
    public static bool llave;
    public Text keyText;

    // Start is called before the first frame update
    void Start()
    {
        health = 3;
        llave = false;
        keyText.text = "x0";
    }

    // Update is called once per frame
    void Update()
    {
        //Contador de monedas
        coinText.text = coins.ToString();

        //Contador corazones HUD
        for (int i = 0; i <hearts.Length; i++) 
        {
            if(i < health)
            {
                hearts[i].sprite = fullHeart;
            } 
            else
            {
                hearts[i].sprite = emtyHeart;
            }
        }

        //Cambiar cantidad llave
        if (llave)
        {
            keyText.text = "x1";
        }
    }
}
