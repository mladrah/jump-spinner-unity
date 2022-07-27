using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement: MonoBehaviour
{
    public float dampTime = 1f;
    public Vector3 velocity = Vector3.zero;
    public Transform target;
    public bool follow = true;
    // Update is called once per frame
    void Start() {

    }
    void Update() {
        if (follow) {
            if (target) {
                Vector3 playerPosition = new Vector3(0f, target.position.y, target.position.z);
                Vector3 point = GetComponent<Camera>().WorldToViewportPoint(playerPosition);
                Vector3 delta = playerPosition - GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.5f, 0.1f, point.z)); //(new Vector3(0.5, 0.5, point.z));

                Vector3 destination = transform.position + delta;
                transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
            }
        }
        if (target.gameObject.activeInHierarchy == false) {
            Debug.Log("X: "+target.transform.position.x+" Y: "+target.transform.position.y);
            GetComponent<RippleEffect>().pos = new Vector2(target.transform.position.x, target.transform.position.y);
            GetComponent<RippleEffect>().enabled = true;
        }
    }
}


