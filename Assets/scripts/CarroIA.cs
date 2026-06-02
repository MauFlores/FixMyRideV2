using UnityEngine;

public class CarroIA : MonoBehaviour
{
    public float velocidad = 20f;
    public float limiteFinal = 300f;
    public float posicionInicial =-300f;


    void Update()
    {
         if(transform.position.z > limiteFinal)
        {
            Vector3 p = transform.position;
            p.z = posicionInicial;
            transform.position = p;
        }
    }


}