using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TweetTimer : MonoBehaviour 
{
    private static readonly int maxReadTime = 18;
    private static readonly int minReadTime = 3;

    public event Action TimesUp;

    public void StartTimer()
    {
        StartCoroutine(Timer());
    }

    private IEnumerator Timer()
    {
        yield return new WaitForEndOfFrame();

        var tweetLength = GetComponent<TextMeshProUGUI>().textInfo.characterCount;
        var readTime = TypeOn.Map(tweetLength, 0, TypeOn.tweetMaxCharacters, minReadTime, maxReadTime);
        var t = 0f;

        while (t <= readTime)
        {
            t += Time.deltaTime;

            yield return null;
        }

        OnTimesUp();
    }

    private void OnTimesUp()
    {
        var handler = TimesUp;

        if(handler != null)
        {
            handler();
        }
    }
}
