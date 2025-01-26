using UnityEngine;
public class MusicCore : MonoBehaviour
{
    public AK.Wwise.Event surface; // Asigna el evento de música en el Inspector
    public AK.Wwise.Event sea; // Asigna el evento de música en el Inspector
    private uint playingId;

    void Start()
{
    onSeaLeave();
    PlayerTrigger.onSeaEnter += onSeaEnter;
    PlayerTrigger.onSeaLeave += onSeaLeave;
}

private void onSeaEnter()
{
    executeMusic(sea);
}

private void onSeaLeave()
{
    executeMusic(surface);
}

private void executeMusic(AK.Wwise.Event music)
{
    AkSoundEngine.StopPlayingID(playingId);
    playingId = music.Post(gameObject); // Reproduce la música al iniciar
}
}