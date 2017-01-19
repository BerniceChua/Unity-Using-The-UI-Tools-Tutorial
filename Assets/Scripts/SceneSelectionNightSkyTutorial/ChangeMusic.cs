using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMusic : MonoBehaviour {

    public AudioClip level1Music;
    public AudioClip level4Music;

    public AudioSource source;

    // Use this for initialization
    void Awake () {
        source = GetComponent<AudioSource>();
	}
	
	void OnLevelWasLoaded (int level) {
		if (level == 1) {
            source.clip = level1Music;
            source.Play();
        }

        if (level == 4) {
            source.clip = level4Music;
            source.Play();
        }
	}
}
