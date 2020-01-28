using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour 
{
    [SerializeField] private bool playOnAwake = true;

    private static readonly string boolName = "Animate";

    private void Start()
    {
        if (playOnAwake)
        {
            Play();
        }
    }

    public void Play()
    {
        var animators = GetComponentsInChildren<Animator>();

        foreach (var animator in animators)
        {
            animator.SetBool(boolName, true);
        }

        GetComponentInChildren<TypeOn>().Play();
        GetComponentInChildren<TweetTimer>().StartTimer();
    }
}
