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
}
