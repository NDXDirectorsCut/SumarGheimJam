using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchMusic : MonoBehaviour
{
    public AudioSource songOne;
    public AudioSource songTwo;
    [Range(0,1)]
    public float transition;
    // Start is called before the first frame update
    void Start()
    {
        transition = 1;
    }

    // Update is called once per frame
    void Update()
    {
        transition = Mathf.Clamp(transition,0,1);
        songOne.volume = Mathf.Lerp(0,1,transition);
        songTwo.volume = Mathf.Lerp(0,1,1-transition);
    }
}
