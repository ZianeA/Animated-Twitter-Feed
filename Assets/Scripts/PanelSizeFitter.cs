using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PanelSizeFitter : MonoBehaviour
{
    [SerializeField] private RectTransform tweet;
    [SerializeField] private TypeOn typeOn;
    [SerializeField] private AnimationCurve curve;
    [SerializeField] private float animationSpeed = 3f;
    [SerializeField] private bool isInstagram;

    private static float panelMinSize = 148f;

    public static float PanelMinSize
    {
        get
        {
            return panelMinSize;
        }
    }

    private RectTransform trans;
    private float panelSize;
    private float lineSize;

    private void Awake()
    {
        if (isInstagram && panelMinSize <= 148)
        {
            panelMinSize += 20;
        }
    }

    private void Start()
    {
        var textLabel = typeOn.GetComponent<TextMeshProUGUI>();
        lineSize = textLabel.preferredHeight / textLabel.textInfo.lineCount;

        panelSize = PanelMinSize + lineSize;
        trans = GetComponent<RectTransform>();
        trans.sizeDelta = new Vector2(trans.sizeDelta.x, panelSize);

        typeOn.JumpLine += () =>
        {
            StartCoroutine(ScalePanel());
        };
    }

    private IEnumerator ScalePanel()
    {
        var t = 0f;
        var currentPanelSize = panelSize;
        var targetPanelSize = panelSize + lineSize;

        while (t <= 1f)
        {
            t += Time.deltaTime * animationSpeed;
            panelSize = Mathf.Lerp(currentPanelSize, targetPanelSize, curve.Evaluate(t));
            trans.sizeDelta = new Vector2(trans.sizeDelta.x, panelSize);

            yield return null;
        }

        trans.sizeDelta = new Vector2(trans.sizeDelta.x, targetPanelSize);
        panelSize = targetPanelSize;
    }
}
