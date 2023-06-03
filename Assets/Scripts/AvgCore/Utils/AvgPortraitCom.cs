using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class AvgPortraitCom : MonoBehaviour
{
    public Image image;

    public void SetPosition(Vector3 position)
    {
        this.transform.localPosition = position;
    }

    public void FadeTo(float alpha, float duration = 0)
    {
        image.DOFade(alpha, duration);
    }

    public void SetImage(Sprite sprite)
    {
        image.sprite = sprite;
    }

    public void MoveTo(Vector3 targetPosition, float moveTime)
    {
        transform.DOLocalMove(targetPosition, moveTime);
    }

    public void Focus(bool isFocus)
    {
        image.color = isFocus ? Color.white : Color.gray;
    }

}