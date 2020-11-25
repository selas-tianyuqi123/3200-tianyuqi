using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold : MonoBehaviour
{
    //The speed of gold rotate 
    public float _speed = 1f;
    void Update()
    {
        transform.Rotate(Vector3.up * _speed, Space.Self);
    }
}
