using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Bullet : MonoBehaviour
{
    public float _speed = 20;
    public Rigidbody2D _rbody;
    public int _maxBounceNumber;
    public AudioClip _wallBounce;
    public AudioClip _bulletSound;

    public AudioSource _audioSource;

    public GameObject soundManager;
    

    private int _bounceNumber = 0;
    // Update is called once per frame
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _rbody.velocity = transform.right * _speed;
        GameObject s = Instantiate(soundManager, gameObject.transform.position, Quaternion.identity);
        SoundManagerScript script = s.GetComponent<SoundManagerScript>();
        script.PlayAndDestroy(script._bulletSound);
    }

    private void Update()
    {
        _rbody.velocity = _rbody.velocity.normalized * _speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _bounceNumber++;
        GameObject s = Instantiate(soundManager, gameObject.transform.position, Quaternion.identity);
        SoundManagerScript script = s.GetComponent<SoundManagerScript>();
        
        if (collision.gameObject.tag.Equals("Tank"))
        {
            if (Random.value <= 0.1)
            {
                script.PlayAndDestroy(script._tankOw);

            }
            else
            {
                script.PlayAndDestroy(script._tankHit);
            }
        }else if (_bounceNumber >= _maxBounceNumber){
            Destroy(gameObject);
        }
        else
        {
            script.PlayAndDestroy(script._wallBounce);
        }

        
        
    }

}
