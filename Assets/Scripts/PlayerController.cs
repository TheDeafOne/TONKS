using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    // serialized inputs
    public float moveSpeed;
    public float rotationSpeed;

    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public string playerString;

    private Rigidbody2D _rbody;
    private float moveDirection;
    private float rotationDirection;

    // Start is called before the first frame update
    void Start()
    {
        _rbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        AxisControl();
        if (Input.GetButtonDown(playerString + "_shoot"))
        {
            Shoot();
        }
    }

    void AxisControl()
    {
        moveDirection = Input.GetAxis(playerString + "_vertical") * moveSpeed * Time.deltaTime;
        rotationDirection = Input.GetAxis(playerString + "_horizontal") * -rotationSpeed * Time.deltaTime;
    }

    private void LateUpdate()
    {
        transform.Translate(0f, moveDirection, 0f);
        transform.Rotate(0f, 0f, rotationDirection);
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        bullet.name = playerString + "_bullet";
        
    }
}
