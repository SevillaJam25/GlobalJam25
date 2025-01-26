using UnityEngine;

public class MusicStinger : MonoBehaviour
{
    public AK.Wwise.Event bubbles; // Asigna el evento de música en el Inspector
    public AK.Wwise.Event crucifix; // Asigna el evento de música en el Inspector
    public AK.Wwise.Event vial; // Asigna el evento de música en el Inspector

    void Start()
    {
        onSeaLeave();
        PlayerTrigger.onSeaEnter += onSeaEnter;
        PlayerTrigger.onSeaLeave += onSeaLeave;
    }

    private void onSeaEnter()
    {
        Debug.Log("MUSIC - ENTRA OCEANO");
        // executeMusic(sea);
    }

    private void onSeaLeave()
    {
        Debug.Log("MUSIC - SALE OCEANO");
        // executeMusic(surface);
    }

    private void executeMusic(AK.Wwise.Event music)
    {
        music.Post(gameObject); // Reproduce la música al iniciar
    }
}