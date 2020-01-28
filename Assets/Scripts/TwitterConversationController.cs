using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TwitterConversationController : MonoBehaviour
{
    private List<float> panelsHeight;
    private List<TwitterConversationSlider> panelsSlider;
    private List<RectTransform> tweets;
    private int tweetsDoneCount;

    public static readonly int gap = 25;

    private void Start()
    {
        StartCoroutine(Initialize());
    }

    private IEnumerator Initialize()
    {
        yield return new WaitForEndOfFrame();

        panelsHeight = new List<float>();
        panelsSlider = new List<TwitterConversationSlider>();
        tweets = new List<RectTransform>();

        for (int i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i);
            var textLabel = child.GetComponentInChildren<TweetTimer>().GetComponent<TextMeshProUGUI>();
            var panelHeight = PanelSizeFitter.PanelMinSize + textLabel.preferredHeight;
            panelsHeight.Add(panelHeight);

            panelsSlider.Add(child.GetComponentInChildren<TwitterConversationSlider>());
            child.GetComponentInChildren<TweetTimer>().TimesUp += OnTimesUp;
            tweets.Add(child.GetComponent<RectTransform>());
        }
    }

    private void OnTimesUp()
    {
        StartCoroutine(AnimateConversation());
    }

    private IEnumerator AnimateConversation()
    {
        tweetsDoneCount += 1;

        if (tweetsDoneCount < panelsHeight.Count)
        {
            for (int i = 0; i < tweetsDoneCount; i++)
            {
                var panelsRequiredHeight = 0f;

                for (int j = i + 1; j <= tweetsDoneCount; j++)
                {
                    panelsRequiredHeight += panelsHeight[j] + gap;
                }

                var slideDistance = Mathf.Abs(Mathf.Clamp(tweets[i].anchoredPosition.y - panelsHeight[i] - (panelsRequiredHeight + gap), Mathf.NegativeInfinity, 0));
                var panelTargetPos = tweets[i].anchoredPosition.y + slideDistance - panelsHeight[i];
                panelsSlider[i].Slide(slideDistance);

                var t = 0f;
                var timeBetweenTweets = i >= tweetsDoneCount - 1 ? 0.5f : 0.1f;

                while (t <= 1)
                {
                    t += Time.deltaTime * 1 / timeBetweenTweets;

                    yield return null;
                }

                if (i >= tweetsDoneCount - 1)
                {
                    var nextTweet = tweets[i + 1];
                    //nextTweet.anchoredPosition = new Vector2(nextTweet.anchoredPosition.x, panelsHeight[i + 1] + gap);
                    nextTweet.anchoredPosition = new Vector2(nextTweet.anchoredPosition.x, panelTargetPos - gap);
                    nextTweet.GetComponent<AnimationController>().Play();
                }
            }
        }
    }
}
