using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(AudioSource))]

public class PlayerController : MonoBehaviour
{
    // serialized inputs
    public float moveSpeed;
    public float rotationSpeed;
    public GameObject explosion;
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public string playerString;
    public KeyCode shootKey;

    private Rigidbody2D _rbody;
    private float moveDirection;
    private float rotationDirection;



    private float lastShot = 0;

    public AudioSource _audioSource;
    public AudioClip _tankExplosion;
    public AudioClip _tankMove;
    public AudioClip _tankHit;
    public AudioClip _tankOw;
    public AudioClip _bulletSound;

    public bool moveSoundStarted = false;
    bool isPlaying = false;

    public GameObject soundManager;


    public float volumeSetting = (float)0.3;
    public int lives = 3;
    
    private MSManager _msManager;
    // Start is called before the first frame update
    void Start()
    {
        _rbody = GetComponent<Rigidbody2D>();
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = _tankMove;
        _msManager = FindObjectOfType<MSManager>();
    }

    // Update is called once per frame
    void Update()
    {

        AxisControl();
        if (Input.GetKeyDown(shootKey))
        {
            Shoot();
        }
    }

    void AxisControl()
    {
        moveDirection = Input.GetAxis(playerString + "_vertical");
        rotationDirection = Input.GetAxis(playerString + "_horizontal");
        if ((moveDirection != 0 || rotationDirection != 0))
        {
            if (!isPlaying)
            {
                _audioSource.Stop();
                _audioSource.loop = true;
                _audioSource.clip = _tankMove;
                _audioSource.volume = 0.2f;
                _audioSource.Play();
                isPlaying = true;
            }
        }
        else
        {
            _audioSource.Stop();
            isPlaying = false;
        }
    }

    private void LateUpdate()
    {
        _rbody.MovePosition(transform.position + transform.up * moveDirection * moveSpeed * Time.deltaTime);
        transform.Rotate(0f, 0f, rotationDirection * -rotationSpeed * Time.deltaTime);
        
    }

    private void Shoot()
    {
        if (Time.time - lastShot > 0.5) {
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            bullet.name = playerString + "_bullet";
            lastShot = Time.time;
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("bullet")) {
            lives--;
            Destroy(gameObject.transform.parent.transform.Find("HeartBar").GetChild(0).gameObject);

        }
        if (lives <= 0)
        {
            GameObject s = Instantiate(soundManager, gameObject.transform.position, Quaternion.identity);
            SoundManagerScript script = s.GetComponent<SoundManagerScript>();
            script.Play(script._tankExplosion);
            if (_msManager.ManageWin(playerString))
            {
                script.SetLevelToLoad("SampleScene");
            } else
            {
                PlayerWinController.winner = playerString == "P1" ? "Player 2" : "Player 1";

                script.SetLevelToLoad("EndScreen");
            }
            script.LoadLevelAfterDelay(1);
            Instantiate(explosion, gameObject.transform.position, transform.rotation = Quaternion.identity);
            Destroy(gameObject);
            
        }


    }
}
