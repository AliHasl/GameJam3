using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour {

    public AudioSource efxSource;
    public AudioSource ambientSounds;
    public static SoundManager instance = null;
    public float lowPitchRange = 0.95f;
    public float highPitchRange = 1.05f;
    public AudioMixerGroup ambientAudio, playerBullets, enemyBullets;


	// Use this for initialization
	void Start () {
		
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        
	}

    public void PlaySingle(AudioClip clip)
    {
        efxSource.clip = clip;
        efxSource.pitch = Random.Range(lowPitchRange, highPitchRange);
        efxSource.Play();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
