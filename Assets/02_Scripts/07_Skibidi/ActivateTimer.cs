using TMPro;
using UnityEngine;

public class ActivateTimer : MonoBehaviour
{
    private void Start()
    {
        FindAnyObjectByType<Timer>().GetComponent<TMP_Text>().enabled = true;
        Destroy(gameObject);
    }
}
