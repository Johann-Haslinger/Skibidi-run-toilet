using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class OnClickEventTriggerInt : MonoBehaviour
{
    [SerializeField] private UnityEvent _onClickEvent;

    public void OnMouseDown()
    {
        Debug.Log("Clicked" + transform.name);
        _onClickEvent.Invoke();
        GetComponent<SpriteRenderer>().DOFade(0, 0.1f).OnComplete(() =>
        {
            Destroy(gameObject);
        });
    }
}
