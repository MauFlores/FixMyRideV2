using UnityEngine;

public class CarEngine : MonoBehaviour
{

    CarInput carInput;
    Transimisiones transmisiones;
    WheelController wheelController;

    float direccion;
    float acelerador;
    float freno;
    float clutch;
    public float maxSteerAngle = 30f;
    public float motorForce = 1500f;
    public float brakeForce = 3000f;
    void Start()
    {
        carInput = GetComponent<CarInput>();
        transmisiones = GetComponent<Transimisiones>();
        wheelController = GetComponent<WheelController>();
        
        
    }

    
    void Update()
    {
     direccion = carInput.direccion;
     acelerador = carInput.acelerador;
     freno = carInput.freno;
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
        /*
        //volante
      float steerAngle = direccion * maxSteerAngle;
      wheelController.girarRueda(steerAngle);
      //acelerador
      float idleTorque = 0.1f * motorForce;
      float torque = (-idleTorque - acelerador * motorForce);
      wheelController.torqueDrive(torque);
      //freno 
      float frenoTorque = freno * brakeForce;
      wheelController.FrenoDrive(frenoTorque);
      */
    }
    public void reverse()
    {
        Mover(-1);
        /*
      float steerAngle = direccion * maxSteerAngle;
      wheelController.girarRueda(steerAngle);
      //acelerador
      float idleTorque = 0.1f * motorForce;
      float torque = (idleTorque - acelerador * motorForce);
      wheelController.torqueDrive(torque);
      //freno 
      float frenoTorque = freno * brakeForce;
      wheelController.FrenoDrive(frenoTorque);  
      */
    }
    
    public void Neutro()
    {
        wheelController.torqueDrive(0);
    }

    void Mover(float multiplicador)
{
    float steerAngle = carInput.direccion * maxSteerAngle;

    wheelController.girarRueda(steerAngle);

    float torque =
        multiplicador *
        (0.1f * motorForce + carInput.acelerador * motorForce);

    wheelController.torqueDrive(torque);

    float brakeTorque = carInput.freno * brakeForce;

    wheelController.FrenoDrive(brakeTorque);
}

    

   
}
