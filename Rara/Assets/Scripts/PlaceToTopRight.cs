using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceToTopRight : MonoBehaviour
{
    public Camera Camera;

    void Start()
    {
        Vector3 worldPoint = Camera.ViewportToWorldPoint(new Vector3(1, 1, 10));
        transform.position = worldPoint;
    }
}