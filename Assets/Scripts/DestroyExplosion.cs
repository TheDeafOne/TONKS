using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyExplosion : MonoBehaviour
{
    public float timeToDestruction;
    private void FixedUpdate()
    {
        Destroy(gameObject, timeToDestruction);
    }
}
