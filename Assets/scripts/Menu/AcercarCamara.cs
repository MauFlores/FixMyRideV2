using UnityEngine;
using UnityEngine.SceneManagement;

public class AcercarCamara : MonoBehaviour
{
    [Header("Menus")]
    public GameObject MenuPrincipal;
    public GameObject MenuJugar;
    public GameObject MenuEducativo;
    public GameObject MenuLibre;
    public GameObject MenuAjustes;
    public GameObject MenuSalir;

    [Header("Zoom")]
    public Transform MenuVisual;
    public Vector3 EscalaPrincipal = Vector3.one;
    public Vector3 EscalaSecundaria = new Vector3(1.2f, 1.2f, 1f);
    

    void Start()
    {
        MostrarSolo(MenuPrincipal);

        if (MenuVisual != null)
        {
            MenuVisual.localScale = EscalaPrincipal;
        }
    }

    void MostrarSolo(GameObject menu)
    {
        MenuPrincipal.SetActive(false);
        MenuJugar.SetActive(false);
        MenuEducativo.SetActive(false);
        MenuLibre.SetActive(false);
        MenuAjustes.SetActive(false);
        MenuSalir.SetActive(false);

        menu.SetActive(true);
       
    }

    // MENU PRINCIPAL

    public void AbrirJugar()
    {
        MostrarSolo(MenuJugar);

        if (MenuVisual != null)
        {
            MenuVisual.localScale = EscalaSecundaria;
           
        }
    }

    public void AbrirAjustes()
    {
        MostrarSolo(MenuAjustes);

        if (MenuVisual != null)
        {
            MenuVisual.localScale = EscalaSecundaria;
        }
    }

    public void AbrirSalir()
    {
        MostrarSolo(MenuSalir);

        if (MenuVisual != null)
        {
            MenuVisual.localScale = EscalaSecundaria;
        }
    }

    // MENU JUGAR

    public void AbrirEducativo()
    {
        MostrarSolo(MenuEducativo);
    }

    public void AbrirLibre()
    {
        MostrarSolo(MenuLibre);
    }

    // VOLVER

    public void VolverPrincipal()
    {
        MostrarSolo(MenuPrincipal);

        if (MenuVisual != null)
        {
            MenuVisual.localScale = EscalaPrincipal;
        }
    }
    

    public void NivelUno(){SceneManager.LoadScene("Nivel 1");}
    public void NivelDos(){SceneManager.LoadScene("Nivel 2");}
    public void NivelTres(){SceneManager.LoadScene("Nivel 3");}
    public void NivelCuatro(){SceneManager.LoadScene("Nivel 4");}
    public void NivelCinco(){SceneManager.LoadScene("Nivel 5");}

    // SALIR

    public void exitGame(){ Application.Quit(); }

    public void ConfirmarSalida()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
