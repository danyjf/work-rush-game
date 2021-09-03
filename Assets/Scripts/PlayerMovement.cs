using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public float movementSpeed;
    public LayerMask groundLayer;
    public Transform groundChecker;
    public float jumpForce;
    
    private bool isGrounded;
    private float groundCheckerRadius = 0.2f;
    
    private Rigidbody2D rb;
    private CapsuleCollider2D playerCollider;

    private void Start() {
        rb = transform.GetComponent<Rigidbody2D>();
        playerCollider = transform.GetComponent<CapsuleCollider2D>();
    }

    private void Update() {
        Move();
        IsGrounded();
        Jump();
        Crouch();
    }

    private void Move() {
        rb.velocity = new Vector2(movementSpeed, rb.velocity.y);
    }

    private void IsGrounded() {
        isGrounded = Physics2D.OverlapCircle(groundChecker.position, groundCheckerRadius, groundLayer);
    }

    private void Jump() {
        if(isGrounded && Input.GetKeyDown(KeyCode.Space)) {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    private void Crouch() {
        if(Input.GetKey(KeyCode.LeftControl)) {
            StartCoroutine(CrouchCoroutine());
        }
    }

    private IEnumerator CrouchCoroutine() {
        playerCollider.offset = new Vector2(0, -0.23f);
        playerCollider.size = new Vector2(1, 0.48f);

        yield return new WaitForSeconds(1.5f);

        playerCollider.offset = new Vector2(0, 0);
        playerCollider.size = new Vector2(1, 0.96f);
    } 
}
