using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip[] audioClip = new AudioClip[10];
    public AudioSource[] audioSource = new AudioSource[3];

	void Start ()
    {
		
	}
	
	void Update ()
    {
		
	}

    public void PlayAudio(int index)
    {
        audioSource[index].Play();
    }
}
