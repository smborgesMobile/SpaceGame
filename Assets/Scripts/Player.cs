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
    public bool canTripleShot;

    // Holds the if speed boost can be applied.
    public bool canBoostSpeed;

    public bool shieldsActivity = false;

    // Holds the instance of player life;
    public int playerLife = 100;

    // Update is called once per frame
    private void Update()
    {
        // Movement player
        Movement();

        if (!Input.GetMouseButtonDown(0) && !Input.GetKeyDown(KeyCode.Space)) return;

        if (canTripleShot)
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

        playerLife--;

        if (playerLife < 1)
        {
            Vector3 playerPosition = transform.position;
            Instantiate(playerExplosion, playerPosition, Quaternion.identity);
            Destroy(this.gameObject);
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

        if (canBoostSpeed)
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
    public void BoostSpeedPowerUpOn(int id)
    {
        canBoostSpeed = true;
        StartCoroutine(PowerDownRoutine(id));
    }

    /**
     * Enable triple shoot.
     */
    public void TripleShotPowerUpOn(int id)
    {
        canTripleShot = true;
        StartCoroutine(PowerDownRoutine(id));
    }

    /**
     * Create coroutine to wait 5 seconds then disable the
     * super power.
     */
    private IEnumerator PowerDownRoutine(int id)
    {
        // Wait for five seconds
        yield return new WaitForSeconds(5.0f);
        switch (id)
        {
            case 0:
                canTripleShot = false;
                break;
            case 1:
                canBoostSpeed = false;
                break;
            case 3:
                //todo implement shield
                break;
        }
    }
}