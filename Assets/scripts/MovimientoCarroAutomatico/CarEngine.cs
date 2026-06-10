using UnityEngine;

public class CarEngine : MonoBehaviour
{
    CarInput carInput;
    Transimisiones transmisiones;
    WheelController wheelController;

    Rigidbody rb;

    float direccion;
    float acelerador;
    float freno;
    float clutch;

    public float maxSteerAngle = 30f;
    public float motorForce = 1500f;
    public float brakeForce = 3000f;

    public float velocidadActual;
    public float rpmActual;

    void Start()
    {
        carInput = GetComponent<CarInput>();
        transmisiones = GetComponent<Transimisiones>();
        wheelController = GetComponent<WheelController>();

        rb = GetComponentInChildren<Rigidbody>();
    }

    void Update()
    {
        direccion = carInput.direccion;
        acelerador = carInput.acelerador;
        freno = carInput.freno;

        // Velocidad
        if (rb != null)
        {
            velocidadActual = rb.linearVelocity.magnitude * 3.6f;
        }

        // RPM suavizadas
        float rpmObjetivo =
            Mathf.Clamp(
                Mathf.Abs(wheelController.rearLeftWheelCollider.rpm * 15f),0,8000);

        rpmActual = Mathf.Lerp(rpmActual,rpmObjetivo,Time.deltaTime * 4f);
    }

    void FixedUpdate()
    {
        transmisiones.cajaDeCambios();
    }

    public void Parking()
    {
        wheelController.FrenoDrive(brakeForce);
    }

    public void Drive()
    {
        Mover(1);
    }

    public void reverse()
    {
        Mover(-1);
    }

    public void Neutro()
    {
        wheelController.torqueDrive(0);
    }

    void Mover(float multiplicador)
    {
        float steerAngle = direccion * maxSteerAngle;

        wheelController.girarRueda(steerAngle);

        float torque =
            multiplicador *
            (0.1f * motorForce + acelerador * motorForce);

        wheelController.torqueDrive(torque);

        float brakeTorque = freno * brakeForce;

        wheelController.FrenoDrive(brakeTorque);
    }
}