using UnityEngine;

public class Camare : MonoBehaviour
{
    public Transform Player;
    public Vector3 offset = new Vector3(0, 5, -7);

    private void LateUpdate()
    {
        transform.position = Player.position + offset;
        transform.LookAt(Player);
    }
}
