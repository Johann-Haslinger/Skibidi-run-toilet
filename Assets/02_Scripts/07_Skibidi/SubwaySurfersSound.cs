using UnityEngine;
using UnityEngine.Video;

public class SetVideoVolume : MonoBehaviour
{
    private void Start()
    {
        // Finde den VideoPlayer in der Szene
        VideoPlayer videoPlayer = FindObjectOfType<VideoPlayer>();

        // Falls ein VideoPlayer existiert, setze die Lautstärke
        if (videoPlayer != null)
        {
            videoPlayer.SetDirectAudioVolume(0, 1f); // Audio Track 0 auf Volume 1 setzen
        }
        else
        {
            Debug.LogWarning("Kein VideoPlayer in der Szene gefunden!");
        }
    }
}
