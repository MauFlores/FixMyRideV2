using UnityEngine;

public class HumanNPC : MonoBehaviour
{
    public Transform[] waypoints;

    public float speed = 10f;
    public float rotationSpeed = 5f;
    public float reachDistance = 2f;

    private int currentWaypoint = 0;

    void Update()
    {
        if (waypoints.Length == 0)
            return;

        // Objetivo actual
        Transform target = waypoints[currentWaypoint];

        // Posición del objetivo SIN cambiar altura
        Vector3 targetPosition = new Vector3(
            target.position.x,
            transform.position.y,
            target.position.z
        );

        // Dirección al waypoint
        Vector3 direction = targetPosition - transform.position;

        // Rotar hacia el waypoint
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation =
                Quaternion.LookRotation(direction);

            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );
        }

        // Movimiento RECTO
        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPosition,
            speed * Time.deltaTime
        );

        // Distancia
        float distance = Vector3.Distance(
            transform.position,
            targetPosition
        );

        // Siguiente waypoint
        if (distance < reachDistance)
        {
            currentWaypoint++;

            if (currentWaypoint >= waypoints.Length)
            {
                currentWaypoint = 0;
            }
        }
    }
}