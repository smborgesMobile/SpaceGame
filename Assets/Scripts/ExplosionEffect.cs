using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class ExplosionEffect : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //delay
        Destroy(this.gameObject, 4f);
    }

    // Update is called once per frame
    void Update()
    {
    }
}