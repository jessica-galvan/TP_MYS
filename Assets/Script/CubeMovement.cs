
using UnityEngine;

public class CubeMovement : MonoBehaviour
{
    //variable publica, es como exportar la variable a Unity
    public float _speed;
    public Transform target;
    private Vector3 direction;

private void Awake()
    {
    }
    // Start is called before the first frame update
    void Start()
    {
        //Acá lo que se está haciendo es llamar a un nuevo Vector3 para cambiar la posición
        Debug.Log(transform.position);
        transform.position = new Vector3(1, 2);
    }

    // Update is called once per frame
    void Update()
    {

        //VA HACIA LA DERECHA DEL MUNDO 
        //transform.position += Vector3.right * Time.deltaTime;

        //VA HACIA SU DERECHA - TRANSFORM DE SU OBJETO

        //si se cambia el right por cualquier otro valor, cambia el movimiento, por ej, en vez de right, up

        //MINUTO 45 DE LA CLASE

        //el *= significa sí mismo por x
        //los nros escritos equivalen a unidades de Unity


        //Time.deltaTime nos devuelve los SEGUNDOS que suceden entre frame y frame. sirve para asegurarse que la velocidad sea independiente de los FPS de la computadora
        direction = Vector3.zero;


        if (Input.GetKey(KeyCode.W))
            direction += transform.up;
        if (Input.GetKey(KeyCode.D))
            direction += transform.right;
        if (Input.GetKey(KeyCode.A))
            direction += -transform.right;
        if (Input.GetKey(KeyCode.S))
            direction += -transform.up;

        
        transform.position += direction * (_speed * Time.deltaTime); 

    }
}
