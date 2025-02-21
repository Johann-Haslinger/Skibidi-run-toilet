using DG.Tweening;
using UnityEngine;

public class ObstacleReactions : MonoBehaviour
{
    public void FlyTop()
    {
        float flyHeight = 10f;
        float yFlyGoal = transform.localPosition.y + flyHeight;
        
        transform.DOLocalMoveY(yFlyGoal, 0.2f);
    }

    public void ScaleDown()
    {
        transform.DOScale(0, 0.2f);
    }

    public void FadeOut()
    {
        var temp = transform.GetComponent<SpriteRenderer>();
        temp.DOFade(0, 0.2f);
    }

    public void SquishOut()
    {
        var temp = transform.localScale;
        transform.DOScale(new Vector3(temp.x, 0, temp.z), 0.2f);
    }
}
