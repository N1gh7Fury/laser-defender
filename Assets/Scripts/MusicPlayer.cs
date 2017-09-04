using UnityEngine;
using System.Collections;

public class MusicPlayer : MonoBehaviour {
	static MusicPlayer instance = null;

    public AudioClip startClip;
    public AudioClip endClip;
    public AudioClip gameClip;

    private AudioSource music;

    void Start () {
		if (instance != null && instance != this) {
			Destroy (gameObject);
			print ("Duplicate music player self-destructing!");
		} else {
			instance = this;
			GameObject.DontDestroyOnLoad(gameObject);
            music = GetComponent<AudioSource>();
            music.clip = startClip;
            music.loop = true;
            music.Play();
		}
		
	}

    private void OnLevelWasLoaded(int level)
    {
        Debug.Log("level loaded " + level);
        music.Stop();
        if(level == 0)
        {
            music.clip = startClip;
            music.Play();
        } else if (level == 1)
        {
            music.clip = gameClip;
            music.Play();
        } else if (level == 2)
        {
            music.clip = endClip;
            music.Play();
        }

    }
}
