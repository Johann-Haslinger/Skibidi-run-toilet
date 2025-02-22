using UnityEngine;
using UnityEngine.Video;

public class VideoPlayerUnmute : MonoBehaviour
{
    // Referenz zum VideoPlayer
    private VideoPlayer videoPlayer;

    void Start()
    {
        // Versucht, den VideoPlayer auf diesem GameObject zu finden
        videoPlayer = GetComponentInChildren<VideoPlayer>();
        
        if (videoPlayer != null)
        {
            // VideoPlayer unmuten
            videoPlayer.SetDirectAudioMute(0, false); // Index 0 für den ersten Audio-Track, false um es zu unmuten
            
        }
        else
        {
            Debug.LogWarning("Kein VideoPlayer auf diesem GameObject gefunden.");
        }
    }
}
