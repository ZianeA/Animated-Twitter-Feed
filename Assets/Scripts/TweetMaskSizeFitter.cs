using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweetMaskSizeFitter : MonoBehaviour 
{
    [SerializeField] private RectTransform panel;
    [SerializeField] private TypeOn typeOn;

    private static readonly Vector2 gapSize = new Vector2(20, 71);

    private void Start()
    {
        var trans = GetComponent<RectTransform>();

        typeOn.AnimationCompleted += () =>
        {
            trans.sizeDelta = new Vector2(panel.sizeDelta.x + gapSize.x, panel.sizeDelta.y + gapSize.y);    
        };
    }
}
