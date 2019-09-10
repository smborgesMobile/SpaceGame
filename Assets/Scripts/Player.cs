using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    // The instance of player speed
    [SerializeField] private float speed;

    // The instance of lase prefab
    [SerializeField] private GameObject laserPrefab;

    // This instance of player explosion.
    [SerializeField] private GameObject playerExplosion;

    [SerializeField] private GameObject shieldGameObject;

    //CooldownND$wn system 
    [SerializeField] private float fireRate = 0.25f;
    private float _canFire;

    // Start is called before the first frame update
    private bool _canTripleShot;

    // Holds the if speed boost can be applied.
    private bool _canBoostSpeed;

    public bool shieldsActivity;

    // Holds the instance of player life;
    private int _playerLife = 3;

    private UIManager _uiManager;

    private GameManager _gameManager;
    private SpawnManager _spawnManager;

    private AudioSource _audioSource;

    private int _hitCount;

    [SerializeField] private GameObject[] _engines;

    private void Start()
    {
        // Retrieves the UI manager
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        _audioSource = GetComponent<AudioSource>();
        _hitCount = 0;

        if (_spawnManager != null)
        {
            _spawnManager.StartSpawnRoutines();
        }

        _uiManager?.UpdateLives(_playerLife);
    }

    // Update is called once per frame
    private void Update()
    {
        // Movement player
        Movement();

        if (!Input.GetMouseButtonDown(0) && !Input.GetKeyDown(KeyCode.Space)) return;
        if (_canTripleShot)
        {
            TripleShoot();
        }
        else
        {
            Shoot();
        }
    }

    /**
     * Run the animation to explode player then destroy
     * this current object.
     */
    public void Damage()
    {
        if (shieldsActivity)
        {
            shieldsActivity = false;
            shieldGameObject.SetActive(false);
            // Cancel the method execution.
            return;
        }
        else
        {
            _hitCount++;

            if (_hitCount == 1)
            {
                _engines[0].SetActive(true);
            }
            else if (_hitCount == 2)
            {
                _engines[1].SetActive(true);
            }
        }

        _playerLife--;
        _uiManager.UpdateLives(_playerLife);

        if (_playerLife < 1)
        {
            Vector3 playerPosition = transform.position;
            Instantiate(playerExplosion, playerPosition, Quaternion.identity);
            _gameManager.gameOver = true;
            _gameManager.ShowTitleScreen();
            Destroy(gameObject);
        }
    }

    /**
     * Movement player between all squares of screen.
     */
    private void Movement()
    {
        var localTransform = transform;
        var horizontalInput = Input.GetAxis("Horizontal");
        var verticalInput = Input.GetAxis("Vertical");

        if (_canBoostSpeed)
        {
            // Moving left to right
            localTransform.Translate(horizontalInput * Time.deltaTime * speed * 1.5f * Vector3.right);
            // Moving bottom to top
            localTransform.Translate(verticalInput * Time.deltaTime * speed * 1.5f * Vector3.up);
        }
        else
        {
            // Moving left to right
            localTransform.Translate(horizontalInput * Time.deltaTime * speed * Vector3.right);
            // Moving bottom to top
            localTransform.Translate(verticalInput * Time.deltaTime * speed * Vector3.up);
        }


        var position = transform.position;
        var currentPosition = position.y;
        var currentPositionX = position.x;

        // Block movement top/bottom
        if (currentPosition < -4.0f)
        {
            localTransform.position = new Vector3(transform.position.x, -4.0f);
        }
        else if (currentPosition > 5.86f)
        {
            localTransform.position = new Vector3(transform.position.x, 5.86f);
        }

        // Block movement right/left
        if (currentPositionX >= 9.45f)
        {
            localTransform.position = new Vector3(9.45f, transform.position.y, 0);
        }
        else if (currentPositionX <= -9.45f)
        {
            localTransform.position = new Vector3(-9.45f, transform.position.y, 0);
        }
    }

    /**
     * Make sing shoot.
     */
    private void Shoot()
    {
        if (Time.time > _canFire)
        {
            _audioSource.Play();
            Instantiate(laserPrefab, transform.position + new Vector3(0, 0.9f, 0),
                Quaternion.identity);
            _canFire = Time.time + fireRate;
        }
    }

    /**
     * Make triple shoot when it was enabled.
     */
    private void TripleShoot()
    {
        if (Time.time > _canFire)
        {
            _audioSource.Play();
            var position = transform.position;
            Instantiate(laserPrefab, position + new Vector3(-0.54f, -0.002f, 0f),
                Quaternion.identity);
            Instantiate(laserPrefab, position + new Vector3(0, 0.9f, 0),
                Quaternion.identity);
            Instantiate(laserPrefab, position + new Vector3(0.55f, -0.03f, 0),
                Quaternion.identity);
            _canFire = Time.time + fireRate;
        }
    }

    public void EnableShield()
    {
        //Put the shield on the player.
        shieldGameObject.SetActive(true);
        shieldsActivity = true;
    }

    /**
     * Enabled boost of speed.
     */
    public void BoostSpeedPowerUpOn()
    {
        _canBoostSpeed = true;
        StartCoroutine(PowerUpSpeedShotRoutine());
    }

    /**
     * Enable triple shoot.
     */
    public void TripleShotPowerUpOn()
    {
        _canTripleShot = true;
        StartCoroutine(PowerUpTripleShotRoutine());
    }

    /**
     * Create coroutine to wait 5 seconds then disable the
     * super power.
     */
    private IEnumerator PowerUpTripleShotRoutine()
    {
        // Wait for five seconds
        yield return new WaitForSeconds(5.0f);
        _canTripleShot = false;
    }


    /**
 * Create coroutine to wait 5 seconds then disable the
 * super power.
 */
    private IEnumerator PowerUpSpeedShotRoutine()
    {
        // Wait for five seconds
        yield return new WaitForSeconds(5.0f);
        _canBoostSpeed = false;
    }
}