using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Vehicle : MonoBehaviour
{
    public float speed = 5f;

    void Update()
    {
        transform.Translate(0f, 0f, speed * Time.deltaTime);
    }
}
