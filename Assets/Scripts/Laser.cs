using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public float speed = 10.0f;

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        MoveLaser();
    }

    private void MoveLaser()
    {
        transform.Translate(Time.deltaTime * speed * Vector3.up);

        var positionY = transform.position.y;

        if (positionY >= 7.0f)
        {
            //to the object that are out of screen
            Destroy(this.gameObject);
        }
    }
}