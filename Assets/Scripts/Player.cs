using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{

    [Header("Physics")]
    public float jumpForce = 0f;
    public float speed;
    public float rotateSpeed;
    private Vector3 rotatorVec;
    private Rigidbody2D rb;

    [Header("Player State")]
    public bool jumpAllowed = true;
    public bool jumpPressed = false;
    public bool jumpUp = false;
    public bool fallDown = false;
    public bool dead = false;
    public bool immunity = false;

    [Header("Death Animation")]
    public GameObject deathParticlePrefab;
    public bool getMoney = false; 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        rotatorVec = new Vector3(0, 0, rotateSpeed);

        //particle = Instantiate(deathParticlePrefab, transform.position, Quaternion.identity);
        //particle.SetActive(false);
    }

    private void FixedUpdate() {
        if (jumpPressed && !dead) {
            Jump();
            jumpPressed = false;
        }

        transform.Rotate(rotatorVec*Time.fixedDeltaTime);
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space))
            Time.timeScale = 0.5f;
        if (Input.GetKeyUp(KeyCode.Space))
            Time.timeScale = 1f;

        if (jumpAllowed) {
            if (Input.touchCount == 1) {
                if (Input.GetTouch(0).phase == TouchPhase.Began) {
                    rb.constraints &= ~RigidbodyConstraints2D.FreezePositionY;
                    jumpPressed = true;
                }
            }
            if (Input.GetKeyDown("w") || Input.GetMouseButtonDown(0)) {
                rb.constraints &= ~RigidbodyConstraints2D.FreezePositionY;
                jumpPressed = true;
            }
        }
        CurrentState();


    }

    private void Jump() {
        rb.velocity = Vector2.zero;
        rb.velocity = Vector2.Lerp(rb.velocity, (new Vector2(rb.velocity.x, jumpForce)), speed * Time.deltaTime);
    }

    private void CurrentState() {
        if (rb.velocity.y > 0) {
            jumpUp = true;
            fallDown = false;
        } else if (rb.velocity.y < 0) {
            jumpUp = false;
            fallDown = true;
        } else {
            jumpUp = false;
            fallDown = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (!immunity) {
            if (other.tag.Equals("Enemy")) {
                dead = true;
                deathParticlePrefab.transform.position = transform.position;
                deathParticlePrefab.SetActive(true);
                this.gameObject.SetActive(false);
            }
        }
        if (other.tag.Equals("Currency")) {
            getMoney = true;
            other.gameObject.SetActive(false);
        }
    }
}
