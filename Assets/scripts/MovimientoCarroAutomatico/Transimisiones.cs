using UnityEngine;

public class Transimisiones : MonoBehaviour
{
    CarInput carInput;
    CarEngine carEngine;

    int marchas = 1;

    bool botonSubirPresionado = false;
    bool botonBajarPresionado = false;

    void Start()
    {
        carInput = GetComponent<CarInput>();
        carEngine = GetComponent<CarEngine>();
    }

    void Update()
    {
        bool condicionSubir = carInput.condicionCarroSubirMarcha;
        bool condicionBajar = carInput.condicionCarroBajarMarcha;

        int botonSubirMarcha = carInput.botonPaletaDerecha;
        int botonBajarMarcha = carInput.botonPaletaIzquierda;

        bool carroPrendido = carInput.carroEncendido;

        obtenerMarcha();

        if (carroPrendido)
        {
            subirMarcha(condicionSubir, botonSubirMarcha);
            bajarMarcha(condicionBajar, botonBajarMarcha);
        }
    }

    void subirMarcha(bool subir, int botonDeMarchaArriba)
    {
        if (subir && !botonSubirPresionado)
        {
            marchas++;
            Debug.Log("Marcha actual = " + marchas);

            botonSubirPresionado = true;
        }
        else if (botonDeMarchaArriba == 0)
        {
            botonSubirPresionado = false;
            Debug.Log("Liberada");
        }

        if (marchas > 6)
            marchas = 6;
    }

    void bajarMarcha(bool bajar, int botonDeMarchaAbajo)
    {
        if (bajar && !botonBajarPresionado)
        {
            marchas--;

            Debug.Log("Marcha actual = " + marchas);

            botonBajarPresionado = true;
        }
        else if (botonDeMarchaAbajo == 0)
        {
            botonBajarPresionado = false;
        }

        if (marchas < 1)
            marchas = 1;
    }

    char obtenerMarcha()
    {
        char[] cambios = { ' ', 'P', 'R', 'N', 'D', '1', '2' };

        if (marchas >= 1 && marchas < cambios.Length)
        {
            return cambios[marchas];
        }

        return ' ';
    }

    public void cajaDeCambios()
    {
        switch (marchas)
        {
            case 1:
                carEngine.Parking();
                break;

            case 2:
                carEngine.reverse();
                break;

            case 3:
                carEngine.Neutro();
                break;

            case 4:
                carEngine.Drive();
                break;

            case 5:
                carEngine.Drive();
                break;

            case 6:
                carEngine.Drive();
                break;
        }
    }
}
