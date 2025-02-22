using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private bool _loadOnStart = false;
    [SerializeField] private int _loadOnStartNum;

    private void Start()
    {
        if (_loadOnStart)
        {
            var blackOut = FindFirstObjectByType<BlackOutScreen>();
            if (blackOut)
            {
                blackOut.Blackout(1);
            }
            LoadSceneByIndexWithDelay(_loadOnStartNum);
        }
    }

    public void LoadSceneByIndex(int num)
    {
        SceneManager.LoadScene(num);
    }

    public void LoadSceneByIndexWithDelay(int num)
    {
        StartCoroutine(DelayedLoading(num));
    }

    private IEnumerator DelayedLoading(int num)
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(num);
    }
}
