using UnityEngine;

public class MovimientoCarrosGeneral : MonoBehaviour
{
    public Transform puntoA;
    public Transform puntoB;

    public float velocidad = 5f;
    private Transform destino;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        destino = puntoB;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position,destino.position, velocidad * Time.deltaTime);


        if(Vector3.Distance(transform.position, destino.position) < 0.1f)
        {
            transform.position = puntoA.position;
            return;
        }
    }
}
