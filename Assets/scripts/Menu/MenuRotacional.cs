using UnityEngine;

public class MenuRotacional : MonoBehaviour
{
    public float velocidad = 15f;
    
   

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0,0,velocidad);
    }
}
