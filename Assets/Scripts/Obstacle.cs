using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle: MonoBehaviour
{
    public float moveSpeed;
    public float rightBorder;
    public float leftBorder;
    private Vector3 movementLaser;

    private Vector3 spawnPoint;
    private bool randomStart;

    public bool contact = false;

    public GameObject left;
    public GameObject leftGlow;
    public GameObject right;
    public GameObject rightGlow;

    public Color col;
    void Start() {
        movementLaser = new Vector3(moveSpeed, 0f, 0f);
        spawnPoint = new Vector3(Random.Range(leftBorder, rightBorder), transform.position.y, 0);
        transform.position = spawnPoint;

        randomStart = (Random.Range(0, 2) == 0);
        if(randomStart)
            movementLaser = new Vector3((-1) * moveSpeed, 0f, 0f);

        ChangeColorValue();      

    }

    void Update() {
        transform.position += movementLaser * Time.deltaTime * moveSpeed;
        if (transform.position.x > rightBorder) {
            movementLaser = new Vector3((-1) * moveSpeed, 0f, 0f);
        }
        if (transform.position.x < leftBorder) {
            movementLaser = new Vector3(moveSpeed, 0f, 0f);
        }

    }

    private void ChangeColorValue() {
        col = ColorManagerObstacle.ChangeObstacleColor();

        left.GetComponent<SpriteRenderer>().color = col;
        right.GetComponent<SpriteRenderer>().color = col;

        leftGlow.GetComponent<SpriteRenderer>().color = col;
        rightGlow.GetComponent<SpriteRenderer>().color = col;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        contact = true;
    }
}
