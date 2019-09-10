using System;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] private float speed = 3.0f;
    [SerializeField] private int powerupid; //0 =  triple shot, 1 = Speed boost, 2 = Shield
    [SerializeField] private AudioClip audioClip;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(speed * Time.deltaTime * Vector3.down);

        if (transform.position.y < -8)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Player Collision: " + other.tag);
        if (other.CompareTag("Player"))
        {
            var player = other.GetComponent<Player>();
            if (player != null)
            {
                AudioSource.PlayClipAtPoint(audioClip, Camera.main.transform.position);

                if (powerupid == 0)
                {
                    player.TripleShotPowerUpOn();
                }

                if (powerupid == 1)
                {
                    player.BoostSpeedPowerUpOn();
                }

                if (powerupid == 2)
                {
                    player.EnableShield();
                }
            }

            // Destroy that object
            Destroy(this.gameObject);
        }
    }
}