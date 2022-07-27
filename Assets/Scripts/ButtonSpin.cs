using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSpin : MonoBehaviour
{
    public float rotateSpeed;
    private Vector3 rotatorVec;
    void Start()
    {
        rotatorVec = new Vector3(0, 0, rotateSpeed);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Rotate(rotatorVec * Time.fixedDeltaTime);
    }
}
