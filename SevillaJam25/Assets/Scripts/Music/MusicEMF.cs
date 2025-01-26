using UnityEngine;

public class MusicEMF : MonoBehaviour
{
    // public AK.Wwise.Event emf0; 
    public AK.Wwise.Event emf1;

    public AK.Wwise.Event emf2; 

    public AK.Wwise.Event emf3; 
    public AK.Wwise.Event emf4; 
    public AK.Wwise.Event emf5; 

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