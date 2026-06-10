using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPausa : MonoBehaviour
{
    public GameObject MenuPrincipal;
    public GameObject MenuAjustes;
    public bool setAutomatico;
    public bool setManual;

    private bool pausado = false;

    void Start()
    {
        //MenuPrincipal.SetActive(false);
       // MenuAjustes.SetActive(false);
    }

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pausado = !pausado;

            MenuPrincipal.SetActive(pausado);

            Time.timeScale = pausado ? 0f : 1f;
        }
        
    }

    void MostrarSolo(GameObject menu)
    {
        MenuPrincipal.SetActive(false);
        MenuAjustes.SetActive(false);

        menu.SetActive(true);
    }

    public void AbrirAjustes()
    {
        MostrarSolo(MenuAjustes);
    }

    public void AbrirMenuPrincipal()
    {
        MostrarSolo(MenuPrincipal);
    }

    public void Reanudar()
    {
        pausado = false;

        MenuPrincipal.SetActive(false);
        MenuAjustes.SetActive(false);

        Time.timeScale = 1f;
    }

    public void Saludar()
    {
        Debug.Log("Hola");
    }

    public void NivelVolver()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("SampleScene");
    }

    public void SalirJuego()
    {
        Application.Quit();

        Debug.Log("Saliendo del juego...");
    }

    public void menuJugarDesactivado() 
    {
        MenuPrincipal.SetActive(false);
    }

    public void menuJugarActivado() 
    {
        MenuPrincipal.SetActive(true);
    }
}