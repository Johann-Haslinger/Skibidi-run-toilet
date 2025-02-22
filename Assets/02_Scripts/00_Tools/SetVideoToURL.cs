using UnityEngine;
using UnityEngine.Video;

public class SetVideoToURL : MonoBehaviour
{
    [SerializeField] private VideoPlayer _videoPlayer;
    [SerializeField] private string _videoName;
    
    private void Start()
    {
       _videoPlayer.url = Application.streamingAssetsPath + "/" + _videoName;
    }
}
