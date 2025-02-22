using UnityEngine;
using UnityEngine.Video;

public class UnmuteVideo : MonoBehaviour
{
    [SerializeField] private VideoPlayer videoPlayer;

    void Start()
    {
        if (videoPlayer != null)
        {
            videoPlayer.audioOutputMode = VideoAudioOutputMode.Direct; // Direkte Audioausgabe
            videoPlayer.SetDirectAudioMute(0, false); // Stellt sicher, dass der Ton aktiv ist
            videoPlayer.SetDirectAudioVolume(0, 1.0f); // Maximale Lautstärke

            Debug.Log("VideoPlayer wurde unmuted.");
        }
        else
        {
            Debug.LogWarning("Kein VideoPlayer zugewiesen!");
        }
    }
}
