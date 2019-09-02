using UnityEngine;

public class Laser : MonoBehaviour
{
    private float _speed = 10.0f;

    // Update is called once per frame
    private void Update()
    {
        MoveLaser();
    }

    private void MoveLaser()
    {
        Transform transform1;
        (transform1 = transform).Translate(Time.deltaTime * _speed * Vector3.up);

        var positionY = transform1.position.y;

        if (positionY >= 7.0f)
        {
            //Destroy this object when it was out of screen.
            Destroy(this.gameObject);
        }
    }
}