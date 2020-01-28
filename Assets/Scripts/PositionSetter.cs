using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PositionSetter : MonoBehaviour 
{
    [SerializeField] private Position position = Position.Center;

    public enum Position { Top, Center, Bottom }

    private void Start()
    {
        var textLabel = GetComponentInChildren<TweetTimer>().GetComponent<TextMeshProUGUI>();
        var panelHeight = PanelSizeFitter.PanelMinSize + textLabel.preferredHeight;
        var trans = GetComponent<RectTransform>();
        var canvasHeight = trans.parent.GetComponent<RectTransform>().rect.height;

        switch (position)
        {
            case Position.Top:
                trans.anchoredPosition = new Vector2(trans.anchoredPosition.x, canvasHeight - TwitterConversationController.gap);
                break;
            case Position.Bottom:
                trans.anchoredPosition = new Vector2(trans.anchoredPosition.x, panelHeight + TwitterConversationController.gap);
                break;
            case Position.Center:
                trans.anchoredPosition = new Vector2(trans.anchoredPosition.x, panelHeight / 2 + canvasHeight / 2);
                break;
            default:
                break;
        }
    }
}
