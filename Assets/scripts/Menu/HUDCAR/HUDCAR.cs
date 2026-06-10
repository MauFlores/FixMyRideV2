using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDCar : MonoBehaviour
{
    [Header("Referencias")]
    public CarEngine carEngine;
    public CarInput carInput;
    public Transimisiones transmisiones;

    [Header("Textos")]
    public TMP_Text VelocidadTMP;
    public TMP_Text RpmTMP;
    public TMP_Text MarchaTMP;
    public TMP_Text FrenoTMP;
    public TMP_Text EstadoMotorTMP;

    [Header("Direccionales")]
    public TMP_Text FlechaIzquierdaTMP;
    public TMP_Text FlechaDerechaTMP;

    [Header("Indicadores")]
    public Image TacometroFill;
    public Image VelocimetroFill;
    public Image FrenoFill;

    float temporizadorDireccionales;
    bool parpadeo;
    public Image Encendido;

    private void Start()
    {
        if (carEngine == null)
            carEngine = FindFirstObjectByType<CarEngine>();

        if (carInput == null)
            carInput = FindFirstObjectByType<CarInput>();

        if (transmisiones == null)
            transmisiones = FindFirstObjectByType<Transimisiones>();
    }

    void Update()
    {
        if (carEngine == null || carInput == null || transmisiones == null)
        {
            Debug.LogWarning("HUD: Faltan referencias.");
            return;
        }

        float velocidad = carEngine.velocidadActual;
        float rpm = carEngine.rpmActual;
        float freno = carInput.freno;

        char marcha = transmisiones.obtenerMarcha();

        // PARPADEO DIRECCIONALES
        temporizadorDireccionales += Time.deltaTime;

        if (temporizadorDireccionales >= 0.5f)
        {
            parpadeo = !parpadeo;
            temporizadorDireccionales = 0f;
        }

        // DIRECCIONAL IZQUIERDA
        if (FlechaIzquierdaTMP != null)
        {
            FlechaIzquierdaTMP.enabled =
                carInput.direccionalIzquierda && parpadeo;
        }

        // DIRECCIONAL DERECHA
        if (FlechaDerechaTMP != null)
        {
            FlechaDerechaTMP.enabled =
                carInput.direccionalDerecha && parpadeo;
        }

        // VELOCIDAD
        if (VelocidadTMP != null)
            VelocidadTMP.text = Mathf.RoundToInt(velocidad) + " KM/H";

        // RPM
        if (RpmTMP != null)
            RpmTMP.text = Mathf.RoundToInt(rpm).ToString();

        // MARCHA
        if (MarchaTMP != null)
            MarchaTMP.text = marcha.ToString();

        // FRENO
        if (FrenoTMP != null)
            FrenoTMP.text = Mathf.RoundToInt(freno * 100f) + "%";

        // ESTADO DEL MOTOR
        if (EstadoMotorTMP != null)
        {
            EstadoMotorTMP.text =
                carInput.carroEncendido
                ? "ENCENDIDO"
                : "APAGADO";

            if (EstadoMotorTMP.text == "ENCENDIDO")
            {
                Encendido.gameObject.SetActive(true);
            }
            else 
            {
                Encendido.gameObject.SetActive(false);
            }

        }

        // TACÓMETRO
        if (TacometroFill != null)
        {
            TacometroFill.fillAmount =
                Mathf.Clamp01(rpm / 15000f);
        }

        // VELOCÍMETRO
        if (VelocimetroFill != null)
        {
            VelocimetroFill.fillAmount =
                Mathf.Clamp01(velocidad / 240f);
        }

        // FRENO
        if (FrenoFill != null)
        {
            FrenoFill.fillAmount =
                Mathf.Clamp01(freno);
        }
    }
}