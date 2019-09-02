using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] private float speed = 3.0f;
    [SerializeField] private int powerupid; //0 =  triple shot, 1 = Speed boost, 2 = Shield

    // Update is called once per frame
    void Update()
    {
        transform.Translate(speed * Time.deltaTime * Vector3.down);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collider with: " + other.name);
        if (other.name == "Player")
        {
            var player = other.GetComponent<Player>();
            if (player != null)
            {
                player.canTripleShot = true;
                if (powerupid == 0)
                {
                    player.TripleShotPowerUpOn(powerupid);
                }
                else if (powerupid == 1)
                {
                    player.BoostSpeedPowerUpOn(powerupid);
                }
                else
                {
                    //todo create shield action
                }
            }

            // Destroy that object
            Destroy(this.gameObject);
        }
    }
}