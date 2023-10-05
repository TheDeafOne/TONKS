using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BackgroundMusicScript : MonoBehaviour
{
    public Button soundOff;
    public bool soundPlaying;
    public AudioSource audioSource;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
    public void SoundOff()
    {
        soundPlaying = !soundPlaying;
        if (soundPlaying)
        {
            soundOff.GetComponentInChildren<Text>().text = "Sound Off";
            audioSource.Play();
        }
        else
        {
            soundOff.GetComponentInChildren<Text>().text = "Sound On";
            audioSource.Stop();
        }

    }
}
