using UnityEngine;

public class CarroNPC : MonoBehaviour
{
    [Header("Ruta")]
    public Transform[] waypoints;

    [Header("Movimiento")]
    public float velocidad = 8f;
    public float velocidadGiro = 4f;

    [Header("Detección")]
    public float distanciaDeteccion = 15f;
    public LayerMask capaNPC;

    private int waypointActual = 0;

    void Update()
    {
        if (waypoints.Length == 0)
            return;

        // DETECCIÓN DE CARRO DELANTE
        RaycastHit hit;

        Vector3 origen = transform.position + Vector3.up * 0.5f;

        if (Physics.Raycast(
            origen,
            transform.forward,
            out hit,
            distanciaDeteccion,
            capaNPC))
        {
            Debug.DrawRay(
                origen,
                transform.forward * distanciaDeteccion,
                Color.red);

            return; // se detiene
        }

        Debug.DrawRay(origen,transform.forward * distanciaDeteccion,Color.green);

        Transform destino = waypoints[waypointActual];

        Vector3 direccion = (destino.position - transform.position).normalized;

        transform.position += direccion * velocidad * Time.deltaTime;

        if (direccion != Vector3.zero)
        {
            Quaternion rotacionObjetivo = Quaternion.LookRotation(direccion);

            transform.rotation = Quaternion.Slerp(transform.rotation,rotacionObjetivo,velocidadGiro * Time.deltaTime);
        }

        if (Vector3.Distance(transform.position,destino.position) < 1f)
        {
            waypointActual++;

            if (waypointActual >= waypoints.Length)
            {
                waypointActual = 0;
            }
        }
    }
}