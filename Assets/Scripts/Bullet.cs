using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float _speed = 20;
    public Rigidbody2D _rbody;
    public int _maxBounceNumber;

    private int _bounceNumber = 0;
    // Update is called once per frame
    void Start()
    {
        _rbody.velocity = transform.right * _speed;
    }

    private void Update()
    {
        _rbody.velocity = _rbody.velocity.normalized * _speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _bounceNumber++;
        if (collision.gameObject.tag.Equals("Tank") || _bounceNumber >= _maxBounceNumber)
        {
            Destroy(gameObject);
        }
    }
}
