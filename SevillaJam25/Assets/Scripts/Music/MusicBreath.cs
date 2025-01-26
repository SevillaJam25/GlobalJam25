using UnityEngine;

public class MusicBreath : MonoBehaviour
{
    public AK.Wwise.Event breathInh; // Asigna el evento de música en el Inspector
    public AK.Wwise.Event breathExh; // Asigna el evento de música en el Inspector
    private uint playingId;

    void Start()
    {
        OxygenSystem.onInhale += onInhale;
        OxygenSystem.onExhale += onExhale;
    }

    private void onInhale()
    {
        executeMusic(breathInh);
    }

    private void onExhale()
    {
        executeMusic(breathExh);
    }

    private void executeMusic(AK.Wwise.Event music)
    {
        AkSoundEngine.StopPlayingID(playingId);
        playingId = music.Post(gameObject); // Reproduce la música al iniciar
    }
}