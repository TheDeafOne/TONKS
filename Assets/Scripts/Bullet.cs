using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20;
    public Rigidbody2D _rbody;
    // Update is called once per frame
    void Start()
    {
        _rbody.velocity = transform.right * speed;
    }

    private void Update()
    {
        _rbody.velocity = _rbody.velocity.normalized * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("Tank"))
        {
            Destroy(gameObject);
        }
    }
}
