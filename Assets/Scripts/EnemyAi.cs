using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyAi : MonoBehaviour
{
    private float _speed = 5.0f;

    // The instance of lase prefab
    [SerializeField] private GameObject enemyPrefab;

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
            transform.position = new Vector3(Random.Range(-9.8f, 9.73f), 8f, 0f);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Enemy crash with: " + other.name);
        if (other.CompareTag("Player"))
        {
            var player = other.GetComponent<Player>();
            if (player != null)
            {
                Destroy(this.gameObject);
                Instantiate(enemyPrefab, player.transform.position, Quaternion.identity);
                player.Damage();
                Destroy(other);
            }
        }
        else if (other.CompareTag("Laser"))
        {
            Instantiate(enemyPrefab, other.GetComponent<Laser>().transform.position, Quaternion.identity);
            Destroy(other);
            Destroy(this.gameObject);
        }
    }
}