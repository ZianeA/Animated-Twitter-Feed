using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using System;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TypeOn : MonoBehaviour
{
    [SerializeField] private AnimationCurve curve;
    [SerializeField] private float delay = 0.2f;

    private TextMeshProUGUI textLabel;
    private TypeAudio typeSound;

    private static readonly float minSpeed = 0.18f;
    private static readonly float maxSpeed = 1f;
    public static readonly int tweetMaxCharacters = 280;

    public event Action JumpLine;
    public event Action AnimationCompleted;

    public void Awake()
    {
        // in case play is called early.
        textLabel = GetComponent<TextMeshProUGUI>();
        typeSound = GetComponent<TypeAudio>();

        textLabel.maxVisibleCharacters = 0;
    }

    public void Play()
    {
        StartCoroutine(AnimateTypeOn());
    }

    private IEnumerator AnimateTypeOn()
    {
        textLabel.ForceMeshUpdate();

        var textInfo = textLabel.textInfo;
        int totalVisibleCharacters = textInfo.characterCount; // Get # of Visible Character in text object
        var t = 0f;
        var i = 0;
        var previousLinesCharacterCount = 0;
        var typeOnSpeed = Map(totalVisibleCharacters, 0, tweetMaxCharacters, maxSpeed, minSpeed);
        textLabel.maxVisibleLines = 1;

        yield return new WaitForSeconds(delay);

        while (t <= 1)
        {
            t += Time.deltaTime * typeOnSpeed;

            var currentLineInfo = textInfo.lineInfo[i];

            if (textLabel.maxVisibleCharacters - previousLinesCharacterCount >= currentLineInfo.characterCount)
            {
                if (textLabel.maxVisibleLines < textInfo.lineCount)
                {
                    previousLinesCharacterCount += currentLineInfo.characterCount;
                    OnJumpLine();
                    var j = 0f;

                    while (j <= 0.2f)
                    {
                        j += Time.deltaTime;
                        yield return null;
                    }

                    i++;
                    textLabel.maxVisibleLines = i + 1; 
                }
            }

            textLabel.maxVisibleCharacters = Mathf.RoundToInt(Mathf.Lerp(0, totalVisibleCharacters, curve.Evaluate(t)));
            typeSound.Play(t);

            yield return null;
        }

        textLabel.maxVisibleCharacters = totalVisibleCharacters;
        OnAnimationCompleted();
    }

    public static float Map(float value, float inFrom, float inTo, float outFrom, float outTo)
    {
        return (value - inFrom) * (outTo - outFrom) / (inTo - inFrom) + outFrom;
    }

    private void OnJumpLine()
    {
        var handler = JumpLine;

        if(handler != null)
        {
            handler();
        }
    }

    private void OnAnimationCompleted()
    {
        var handler = AnimationCompleted;

        if(handler != null)
        {
            handler();
        }
    }
}
