using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puntero : MonoBehaviour
{
    void Update()
    {
        transform.position = Input.mousePosition;
    }
}
