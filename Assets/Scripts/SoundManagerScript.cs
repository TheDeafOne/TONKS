using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class SoundManagerScript : MonoBehaviour
{
    public AudioSource _audioSource;
    public AudioClip _tankExplosion;
    public AudioClip _tankMove;
    public AudioClip _tankHit;
    public AudioClip _tankOw;
    public AudioClip _bulletSound;
    public AudioClip _wallBounce;

    public GameObject backgroundMusic;

    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayAndDestroy(AudioClip sound)
    {
        _audioSource.PlayOneShot(sound);
        Destroy(gameObject, sound.length);
    }
    public void Play(AudioClip sound)
    {
        _audioSource.PlayOneShot(sound);
    }
    public void LoadLevelAfterDelay(float delay)
    {
        BackgroundMusicScript script = backgroundMusic.GetComponent<BackgroundMusicScript>();
        StartCoroutine(FadeOut(script.audioSource, 1));

        Invoke("MyLoadingFunction", delay);
    }
    void MyLoadingFunction()
    {
        SceneManager.LoadScene("EndScreen");
        Destroy(gameObject);
    }
    public static IEnumerator FadeOut(AudioSource audioSource, float FadeTime)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;
    }
}
