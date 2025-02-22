using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
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
