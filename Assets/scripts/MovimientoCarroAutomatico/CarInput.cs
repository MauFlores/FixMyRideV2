using System;
using UnityEngine;
using Logitech;

public class CarInput : MonoBehaviour
{
    LogitechGSDK.DIJOYSTATE2ENGINES rec;
    LogitechGSDK.LogiControllerPropertiesData properties;

    bool sdkDisponible = false;
    bool carroPrendido = false;
    bool subirMarcha;

    bool botonEncendidoPresionado = false;

    public bool direccionalIzquierda = false;
    public bool direccionalDerecha = false;

    bool botonIzquierdaPresionado = false;
    bool botonDerechaPresionado = false;

    Transimisiones transmisiones;
    public MenuPausa menuPausa;

    public bool condicionCarroSubirMarcha;
    public bool condicionCarroBajarMarcha;

    public float acelerador;
    public float freno;
    public float clutch;

    public int botonPaletaDerecha;
    public int botonPaletaIzquierda;

    public float direccion;

    public bool carroEncendido = false;
    public int marchas2;

    public bool prenderMenu = false;

    bool botonMenuPresionado = false;
    bool menuAbierto = false;

    void Start()
    {
        InicializarSDKSeguro();

        transmisiones = GetComponent<Transimisiones>();
        menuPausa = FindFirstObjectByType<MenuPausa>();
        //menuPausa = GetComponent<MenuPausa>();

        Debug.Log("Yo soy: " + gameObject.name);
        Debug.Log("Menu encontrado: " + menuPausa);

        MenuPausa[] menus = FindObjectsByType<MenuPausa>(FindObjectsSortMode.None);

        Debug.Log("Menus encontrados: " + menus.Length);

        foreach (MenuPausa m in menus)
        {
            Debug.Log("Encontrado -> " + m.gameObject.name);
        }
    }

    void InicializarSDKSeguro()
    {
        try
        {
            if (!LogitechGSDK.LogiIsConnected(0))
            {
                LogitechGSDK.LogiSteeringShutdown();
                LogitechGSDK.LogiSteeringInitialize(false);
                Debug.Log("🔄 Intentando reconectar el volante...");
            }

            if (LogitechGSDK.LogiIsConnected(0))
            {
                Debug.Log("✅ Volante Logitech inicializado correctamente.");
                sdkDisponible = true;
            }
            else
            {
                Debug.LogWarning("⏳ Volante aún no detectado. Esperando conexión...");
            }
        }
        catch (DllNotFoundException e)
        {
            Debug.LogError($"❌ DLL no encontrada: {e.Message}");
            sdkDisponible = false;
        }
        catch (Exception e)
        {
            Debug.LogWarning($"⚠️ Error al inicializar el SDK: {e.Message}");
            sdkDisponible = false;
        }
    }

    void Update()
    {
        marchas2 = transmisiones.marchas;


        if (!sdkDisponible)
        {
            InicializarSDKSeguro();
            return;
        }

        try
        {
            if (LogitechGSDK.LogiUpdate() && LogitechGSDK.LogiIsConnected(0))
            {
                rec = LogitechGSDK.LogiGetStateUnity(0);

                LogitechGSDK.LogiPlaySpringForce(0, 0, 80, 70);

                carroEncendido2();

                condicionCarroSubirMarcha = (rec.lRz < -30000 && rec.rgbButtons[4] != 0);

                condicionCarroBajarMarcha = (rec.lRz < -30000 && rec.rgbButtons[5] != 0);

                acelerador = acelerador2();
                freno = freno2();

                subirMarcha = (rec.lRz < 30000 && rec.rgbButtons[4] != 0);

                botonPaletaDerecha = rec.rgbButtons[4];
                botonPaletaIzquierda = rec.rgbButtons[5];

                direccion = rec.lX / 32767.0f;

                // Direccionales
                direccionales();

                for (int i = 0; i < 128; i++)
                {
                    if (rec.rgbButtons[i] != 0)
                    {
                        Debug.Log("Botón presionado: " + i);
                    }
                }


            }
            else
            {
                Debug.LogWarning("⚠️ Volante no detectado. Reintentando inicialización...");
                sdkDisponible = false;
            }
        }
        catch (DllNotFoundException e)
        {
            Debug.LogError($"❌ DLL Logitech no encontrada: {e.Message}");
            sdkDisponible = false;
        }
        catch (Exception e)
        {
            Debug.LogWarning($"⚠️ Error inesperado en Update(): {e.Message}");
            sdkDisponible = false;
        }


        if (menuPausa != null)
        {
            if (rec.rgbButtons[11] != 0 && !botonMenuPresionado)
            {
                menuAbierto = !menuAbierto;

                if (menuAbierto)
                {
                    menuPausa.AbrirMenuPrincipal();
                }
                else
                {
                    menuPausa.Reanudar();
                }

                botonMenuPresionado = true;
            }
            else if (rec.rgbButtons[11] == 0)
            {
                botonMenuPresionado = false;
            }
        }
        else
        {
            Debug.LogError("No se encontró MenuPausa en la escena");
        }

    }

    void carroEncendido2()
    {
        bool botonStart = rec.lRz < -30000 && rec.rgbButtons[23] != 0 && marchas2 == 1;

        if (botonStart && !botonEncendidoPresionado)
        {
            carroEncendido = !carroEncendido;

            Debug.Log(
                carroEncendido
                ? "🚗 Motor encendido"
                : "🛑 Motor apagado"
            );

            botonEncendidoPresionado = true;
        }
        else if (rec.rgbButtons[23] == 0)
        {
            botonEncendidoPresionado = false;
        }
    }

    void direccionales()
    {
        // IZQUIERDA (Botón 1)

        if (rec.rgbButtons[1] != 0 && !botonIzquierdaPresionado)
        {
            direccionalIzquierda = !direccionalIzquierda;

            if (direccionalIzquierda)
            {
                direccionalDerecha = false;
            }

            botonIzquierdaPresionado = true;

            Debug.Log("⬅ Direccional Izquierda: " + direccionalIzquierda);
        }
        else if (rec.rgbButtons[1] == 0)
        {
            botonIzquierdaPresionado = false;
        }

        // DERECHA (Botón 2)

        if (rec.rgbButtons[2] != 0 && !botonDerechaPresionado)
        {
            direccionalDerecha = !direccionalDerecha;

            if (direccionalDerecha)
            {
                direccionalIzquierda = false;
            }

            botonDerechaPresionado = true;

            Debug.Log("➡ Direccional Derecha: " + direccionalDerecha);
        }
        else if (rec.rgbButtons[2] == 0)
        {
            botonDerechaPresionado = false;
        }
    }

    float acelerador2()
    {
        return Mathf.Clamp((32767 - rec.lY) / 65535f, 0f, 1f);
    }

    float freno2()
    {
        return Mathf.Clamp01((32767f - rec.lRz) / 65535f);
    }

    void OnApplicationQuit()
    {
        if (sdkDisponible)
        {
            LogitechGSDK.LogiSteeringShutdown();
        }
    }
}