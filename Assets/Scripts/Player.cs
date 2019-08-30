using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed;

    [SerializeField] private GameObject laserPrefab;

    // Start is called before the first frame update
    private void Start()
    {
        Debug.Log("Start is called" + transform.position);
    }

    // Update is called once per frame
    private void Update()
    {
        // Movement player
        Movement();

        //retrieves the user input
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(laserPrefab, transform.position + new Vector3(0, 0.9f, 0), Quaternion.identity);
            //spawm my lase
        }
    }

    private void Movement()
    {
        var horizontalInput = Input.GetAxis("Horizontal");
        var verticalInput = Input.GetAxis("Vertical");

        // Moving left to right
        transform.Translate(horizontalInput * Time.deltaTime * speed * Vector3.right);
        // Moving bottom to top
        Transform transform1;
        (transform1 = transform).Translate(verticalInput * Time.deltaTime * speed * Vector3.up);

        var currentTransform = transform1;
        var position = currentTransform.position;
        var currentPosition = position.y;
        var currentPositionX = position.x;

        // Block movement top/bottom
        if (currentPosition < -4.0f)
        {
            currentTransform.position = new Vector3(currentTransform.position.x, -4.0f);
        }
        else if (currentPosition > 5.86f)
        {
            currentTransform.position = new Vector3(currentTransform.position.x, 5.86f);
        }

        // Block movement right/left
        if (currentPositionX >= 9.45f)
        {
            currentTransform.position = new Vector3(9.45f, transform.position.y, 0);
        }
        else if (currentPositionX <= -9.45f)
        {
            currentTransform.position = new Vector3(-9.45f, transform.position.y, 0);
        }
    }
}