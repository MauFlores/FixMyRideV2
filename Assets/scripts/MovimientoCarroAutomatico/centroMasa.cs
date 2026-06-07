using UnityEngine;

public class centroMasa : MonoBehaviour
{

    public Rigidbody rb;
    public Vector3 centerOfMass;

    void Start()
    {
        rb.centerOfMass = centerOfMass;
    }

    void Update()
    {
        
    }
}
