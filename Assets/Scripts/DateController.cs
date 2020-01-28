using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DateController : MonoBehaviour 
{
    [SerializeField] private TypeOn typeOn;
    [SerializeField] private GameObject date;

    private void Start()
    {
        typeOn.AnimationCompleted += () => date.SetActive(true);
    }
}
