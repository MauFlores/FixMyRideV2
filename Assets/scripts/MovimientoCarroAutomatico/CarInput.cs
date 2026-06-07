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

    public bool condicionCarroSubirMarcha;
    public bool condicionCarroBajarMarcha;

    public float acelerador;
    public float freno;
    public float clutch;
    public int botonPaletaDerecha;
    public int botonPaletaIzquierda;
    public float direccion;
    public bool carroEncendido;

    void Start()
    {
        InicializarSDKSeguro();
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

                carroEncendido = carroEncendido2();

                condicionCarroSubirMarcha =
                    (rec.lRz < -30000 && rec.rgbButtons[4] != 0);

                condicionCarroBajarMarcha =
                    (rec.lRz < -30000 && rec.rgbButtons[5] != 0);

                acelerador = acelerador2();
                freno = freno2();

                subirMarcha =
                    (rec.lRz < 30000 && rec.rgbButtons[4] != 0);

                botonPaletaDerecha = rec.rgbButtons[4];
                botonPaletaIzquierda = rec.rgbButtons[5];

                direccion = rec.lX / 32767.0f;

                // Debug temporal
                //Debug.Log($"Dir:{direccion:F2} Acc:{acelerador:F2} Freno:{freno:F2}");
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
    }

    bool carroEncendido2()
    {
        return (rec.lRz < -30000 && rec.rgbButtons[2] != 0);
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