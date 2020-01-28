using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TweetSlider : MonoBehaviour 
{
    [SerializeField] private AnimationCurve curve;
    [SerializeField] private float slideSpeed = 1.5f;

    private RectTransform parentRect;

    private void Start()
    {
        parentRect = transform.parent.GetComponent<RectTransform>();
        GetComponentInChildren<TweetTimer>().TimesUp += () => StartCoroutine(Slide());
    }

    private IEnumerator Slide()
    {
        var trans = GetComponent<RectTransform>();
        var slideDistance = parentRect.rect.height;
        var currentPosition = trans.anchoredPosition.y;
        var targetPosition = trans.anchoredPosition.y - slideDistance;
        var t = 0f;
        
        while(t <= 1)
        {
            t += Time.deltaTime * slideSpeed;
            var tempPosition = Mathf.Lerp(currentPosition, targetPosition, curve.Evaluate(t));
            trans.anchoredPosition = new Vector2(trans.anchoredPosition.x, tempPosition);

            yield return null;
        }

        trans.anchoredPosition = new Vector2(trans.anchoredPosition.x, targetPosition);
    }
}
