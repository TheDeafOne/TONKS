using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public SpriteRenderer arenaContainer;
    // Start is called before the first frame update
    void Start()
    {
        float orthoSize = arenaContainer.bounds.size.x * Screen.height / Screen.width * 0.5f;
        Camera.main.orthographicSize = orthoSize;
    }
}
