using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BackgroundMusicScript : MonoBehaviour
{
    public Button soundOff;
    public bool soundPlaying = true;
    public AudioSource audioSource;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (PlayerPrefs.HasKey("Sound"))
        {
            soundPlaying = PlayerPrefs.GetInt("Sound") == 1;
            if (soundPlaying)
            {
                audioSource.Play();
            }
            else
            {
                audioSource.Stop();
            }
        }
        if (soundPlaying)
        {
            soundOff.GetComponentInChildren<Text>().text = "Sound Off";
        } else
        {
            soundOff.GetComponentInChildren<Text>().text = "Sound On";
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
    public void SoundOff()
    {
        print("WORKING");
        soundPlaying = !soundPlaying;
        if (soundPlaying)
        {
            soundOff.GetComponentInChildren<Text>().text = "Sound Off";
            audioSource.Play();
            PlayerPrefs.SetInt("Sound", 1);
        }
        else
        {
            soundOff.GetComponentInChildren<Text>().text = "Sound On";
            audioSource.Pause();
            PlayerPrefs.SetInt("Sound", 0);
        }

    }
}
