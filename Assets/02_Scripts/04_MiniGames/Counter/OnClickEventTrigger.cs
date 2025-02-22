using UnityEngine;
using UnityEngine.Events;

public class OnClickEventTrigger : MonoBehaviour
{
    [SerializeField] private UnityEvent _onClickEvent;

    public void OnMouseDown()
    {
        _onClickEvent.Invoke();
    }
}
