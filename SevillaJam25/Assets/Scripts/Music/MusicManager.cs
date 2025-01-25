using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AK.Wwise.Event musicEvent; // Asigna el evento de música en el Inspector

    void Start()
    {
        musicEvent.Post(gameObject); // Reproduce la música al iniciar
    }
}
