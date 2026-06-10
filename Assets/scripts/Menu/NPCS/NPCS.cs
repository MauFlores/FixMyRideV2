using UnityEngine;

public class NPCS : MonoBehaviour
{
    public Transform[] waypoints;
    public float velocidad = 8f;
    public float velocidadGiro = 4f;

    int indiceActual = 0;

    void Update()
    {
        if (waypoints.Length == 0)
            return;

        Transform destino = waypoints[indiceActual];

        Vector3 direccion =
            (destino.position - transform.position).normalized;

        transform.position +=
            direccion * velocidad * Time.deltaTime;

        Quaternion rotacionObjetivo =
            Quaternion.LookRotation(direccion);

        transform.rotation =
            Quaternion.Slerp(
                transform.rotation,
                rotacionObjetivo,
                velocidadGiro * Time.deltaTime);

        if (Vector3.Distance(
                transform.position,
                destino.position) < 1f)
        {
            indiceActual++;

            if (indiceActual >= waypoints.Length)
                indiceActual = 0;
        }
    }
}