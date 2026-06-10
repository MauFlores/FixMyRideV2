using UnityEngine;

public class TrafficLight : MonoBehaviour
{
    public GameObject redLight;
    public GameObject yellowLight;
    public GameObject greenLight;

    private enum LightState
    {
        Red,
        Yellow,
        Green,
        On
    }

    private LightState currentState;

    void Start()
    {
        SetLight(LightState.On);

        StartCoroutine(TrafficCycle());
    }

    public GameObject stopZone;

    void SetRed()
    {
        stopZone.SetActive(false);
    }

    void SetGreen()
    {
        stopZone.SetActive(true);
    }

    System.Collections.IEnumerator TrafficCycle()
    {
        while (true)
        {
            SetLight(LightState.Green);
            yield return new WaitForSeconds(10f);

            SetLight(LightState.Yellow);
            yield return new WaitForSeconds(5f);

            SetLight(LightState.Red);
            yield return new WaitForSeconds(10f);

            SetLight(LightState.On);
            new WaitForSeconds(4f);
        }
    }

    void SetLight(LightState state)
    {
        currentState = state;

        // Apaga todas primero
        redLight.SetActive(false);
        yellowLight.SetActive(false);
        greenLight.SetActive(false);

        // Enciende solo la que corresponde
        switch (state)
        {
            case LightState.Red:
                redLight.SetActive(false);
                yellowLight.SetActive(true);
                greenLight.SetActive(true);

                break;
            case LightState.Yellow:
                yellowLight.SetActive(false);
                redLight.SetActive(true);
                greenLight.SetActive(true);
                break;
            case LightState.Green:
                greenLight.SetActive(false);
                redLight.SetActive(true);
                yellowLight.SetActive(true);
                break;
            case LightState.On:
                greenLight.SetActive(false);
                redLight.SetActive(false);
                yellowLight.SetActive(false);
                break;
        }
    }
}