using UnityEngine;

public class CarNPC : MonoBehaviour
{
    [Header("Ruta")]
    public Transform[] waypoints;

    [Header("Movimiento")]
    public float speed = 10f;
    public float currentSpeed;
    public float rotationSpeed = 5f;
    public float reachDistance = 2f;

    [Header("Detección")]
    public float detectionDistance = 8f;
    public float brakeForce = 15f;
    public float acceleration = 5f;
    public LayerMask stopLayers;

    private int currentWaypoint = 0;

    void Start()
    {
        currentSpeed = speed;
    }

    void Update()
    {
        if (waypoints.Length == 0)
            return;

        // Detectar obstáculos delante
        bool shouldStop = Physics.Raycast(
            transform.position + Vector3.up * 0.5f,
            transform.forward,
            detectionDistance,
            stopLayers
        );

        // Frenar o acelerar
        if (shouldStop)
        {
            currentSpeed = Mathf.MoveTowards(
                currentSpeed,
                0f,
                brakeForce * Time.deltaTime
            );
        }
        else
        {
            currentSpeed = Mathf.MoveTowards(
                currentSpeed,
                speed,
                acceleration * Time.deltaTime
            );
        }

        // Objetivo actual
        Transform target = waypoints[currentWaypoint];

        Vector3 targetPosition = new Vector3(
            target.position.x,
            transform.position.y,
            target.position.z
        );

        Vector3 direction = targetPosition - transform.position;

        // Rotación
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

        // Movimiento usando currentSpeed
        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPosition,
            currentSpeed * Time.deltaTime
        );

        // Cambiar waypoint
        float distance = Vector3.Distance(
            transform.position,
            targetPosition
        );

        if (distance < reachDistance)
        {
            currentWaypoint++;

            if (currentWaypoint >= waypoints.Length)
                currentWaypoint = 0;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawRay(
            transform.position + Vector3.up * 0.5f,
            transform.forward * detectionDistance
        );
    }
}