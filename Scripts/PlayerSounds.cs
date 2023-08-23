using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{

    AudioSource aud;
    // Start is called before the first frame update
    void Start()
    {
        aud = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayStepSound()
    {
        aud.Play();
    }
}
