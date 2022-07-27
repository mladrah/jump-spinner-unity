using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadGround : MonoBehaviour
{
    public Camera cam;
    public float yOffset;
    private Vector3 vecOffset;
    void Start()
    {
        vecOffset = new Vector3(transform.position.x, yOffset, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = cam.transform.position + vecOffset;
    }
}
