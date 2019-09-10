using UnityEngine;

public class EnemyAi : MonoBehaviour
{
    private float _speed = 5.0f;
    private UIManager _uiManager;
    [SerializeField] private AudioClip _audioClip;

    // The instance of lase prefab
    [SerializeField] private GameObject enemyPrefab;


    private void Start()
    {
        // Retrieves the UI manager
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveEnemy();
    }

    private void MoveEnemy()
    {
        Transform transform1;
        (transform1 = transform).Translate(Time.deltaTime * _speed * Vector3.down);

        var positionY = transform1.position.y;

        if (positionY < -8f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var player = other.GetComponent<Player>();
            if (player != null)
            {
                Instantiate(enemyPrefab, player.transform.position, Quaternion.identity);
                player.Damage();
                AudioSource.PlayClipAtPoint(_audioClip, Camera.main.transform.position);
                Destroy(gameObject);
            }
        }
        else if (other.CompareTag("Laser"))
        {
            if (_uiManager != null)
            {
                _uiManager.UpdateScore();
            }

            Instantiate(enemyPrefab, other.GetComponent<Laser>().transform.position, Quaternion.identity);
            Destroy(other);
            AudioSource.PlayClipAtPoint(_audioClip, Camera.main.transform.position);
            Destroy(gameObject);
        }
    }
}