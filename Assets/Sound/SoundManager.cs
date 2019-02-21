using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour {

    public AudioSource[] audioSources;
    public AudioSource efxSource, ambientSource;
    public AudioClip background;
    public static SoundManager instance = null;
    public float lowPitchRange = 0.95f;
    public float highPitchRange = 1.05f;
    public AudioMixerGroup ambientAudio, playerBullets, enemyBullets;
    public AudioMixer m_audioMixer;

    public enum MixerGroups {AMBIENT_AUDIO, PLAYER_BULLETS, ENEMY_BULLETS }
    public enum Audio { BACKGROUND, SOUND_EFFECT}

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

        audioSources = Camera.main.GetComponents<AudioSource>();

        ambientSource = audioSources[0];
        efxSource = audioSources[1];
        ambientSource.outputAudioMixerGroup = ambientAudio;
        PlaySingle(background,Audio.BACKGROUND, MixerGroups.AMBIENT_AUDIO);
        
	}

    public void PlaySingle(AudioClip clip, Audio source, MixerGroups mix)
    {
        

        AudioSource temp = ambientSource;

        if(source == Audio.BACKGROUND)
        {
            temp = ambientSource;
        }
        else if(source == Audio.SOUND_EFFECT)
        {
            temp = efxSource;
        }

        temp.clip = clip;

        switch (mix){
            case MixerGroups.AMBIENT_AUDIO:
                temp.outputAudioMixerGroup = ambientAudio;
                break;
            case MixerGroups.PLAYER_BULLETS:
                temp.outputAudioMixerGroup = playerBullets;
                break;
            case MixerGroups.ENEMY_BULLETS:
                temp.outputAudioMixerGroup = enemyBullets;
                break;
        }


        
        temp.pitch = Random.Range(lowPitchRange, highPitchRange);
        temp.Play();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
