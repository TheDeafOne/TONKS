using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartScript : MonoBehaviour
{
    public Transform parentTransform;
    void Update()
    {
        transform.position = parentTransform.position + new Vector3(0f, 1f, 0f);
    }
}
