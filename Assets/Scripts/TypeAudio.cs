using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypeAudio : MonoBehaviour 
{
    [SerializeField] private List<AudioClip> audioclips;

    private AudioSource aSource;
    private bool isLast;

    private void Awake()
    {
        aSource = GetComponent<AudioSource>();
    }

    public void Play(float t)
    {
        if (aSource.isPlaying || isLast)
            return;

        if (t > 0.9f)
            isLast = true;

        var randomNumber = isLast ? 10 : Random.Range(0, audioclips.Count);
        aSource.clip = audioclips[randomNumber];
        aSource.Play();
    }
}
