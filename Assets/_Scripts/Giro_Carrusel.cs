﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Giro_Carrusel : MonoBehaviour
{
    public float speed;

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(Vector3.up * speed * Time.deltaTime, Space.World);
    }
}
