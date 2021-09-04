using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour {
    public float movementSpeed;
    public LayerMask groundLayer;
    public Transform groundChecker;
    public float jumpForce;
    public AudioClip jumpSound;
    public AudioClip slideSound;
    
    private bool isGrounded;
    private float groundCheckerRadius = 0.2f;
    private bool isCrouching = false;
    private Rigidbody2D rb;
    private CapsuleCollider2D playerCollider;
    private AudioSource audioSource;
    private Animator animator;

    private void Start() {
        rb = transform.GetComponent<Rigidbody2D>();
        playerCollider = transform.GetComponent<CapsuleCollider2D>();
        audioSource = transform.GetComponent<AudioSource>();
        animator = transform.GetComponent<Animator>();
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
        animator.SetBool("Jump", !isGrounded);
    }

    private void Jump() {
        if(isGrounded && Input.GetKeyDown(KeyCode.Space)) {
            audioSource.clip = jumpSound;
            audioSource.Play();
            
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    private void Crouch() {
        if(Input.GetKey(KeyCode.LeftControl) && !isCrouching) {
            audioSource.clip = slideSound;
            audioSource.Play();

            StartCoroutine(CrouchCoroutine());
        }
    }

    private IEnumerator CrouchCoroutine() {
        isCrouching = true;
        animator.SetBool("Crouch", isCrouching);

        playerCollider.offset = new Vector2(-0.005334139f, -0.03735948f);
        playerCollider.size = new Vector2(0.06106615f, 0.06106615f);

        yield return new WaitForSeconds(1.5f);

        playerCollider.offset = new Vector2(-0.005334139f, -0.009514809f);
        playerCollider.size = new Vector2(0.06106615f, 0.1167555f);

        isCrouching = false;
        animator.SetBool("Crouch", isCrouching);
    } 

    private void Lose() {
        Debug.Log("Lose");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void Win() {
        Debug.Log("Win");
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Obstacle")
            Lose();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Objective")
            Win();
    }
}
