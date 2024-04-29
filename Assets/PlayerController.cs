using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public float speed = 5f;
    public float rotationSpeed = 200f;
    public Animator animator;
    [HideInInspector]
    public GameObject currentEnemy;

    public float gravity = 30f;
    private CharacterController characterController;
    float health;

    public float jumpForce = 8f;
    public Slider healthSlider;
    bool hasCollided = false;

    private Vector3 verticalVelocity;

    private void Awake()
    {
    }
    private void Start()
    {
        
        instance = this;
        Time.timeScale = 1f;
        characterController = GetComponent<CharacterController>();

        
    }

    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        UpdateHealth();

        MovePlayer(horizontal, vertical);
        RotatePlayer(horizontal, vertical);
        UpdateAnimatorParameters(horizontal, vertical);
        if (currentEnemy != null)
        {
            if (!IsPlayerMovingAway(horizontal, vertical))
            {
                Vector3 targetPosition = new Vector3(currentEnemy.transform.position.x, transform.position.y, currentEnemy.transform.position.z);
                transform.LookAt(targetPosition);
            }
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            Jump();
        }
        verticalVelocity.y -= gravity * Time.deltaTime;
        characterController.Move(verticalVelocity * Time.deltaTime);
        ApplyGravity();
    }
    private void ApplyGravity()
    {
        if (!characterController.isGrounded)
        {
            verticalVelocity.y -= gravity * Time.deltaTime;
            characterController.Move(verticalVelocity * Time.deltaTime);
        }
        else
        {
            verticalVelocity.y = 0f;
        }
    }

    private void MovePlayer(float horizontal, float vertical)
    {
        Vector3 movement = new Vector3(horizontal, 0f, vertical) * speed * Time.deltaTime;
        characterController.Move(movement);
    }

    private void RotatePlayer(float horizontal, float vertical)
    {
        Vector3 movementDirection = new Vector3(horizontal, 0f, vertical).normalized;

        if (movementDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    }

    private void UpdateAnimatorParameters(float horizontal, float vertical)
    {
        //animator.SetFloat("Speed", Mathf.Abs(horizontal) + Mathf.Abs(vertical));
    }

    public void HitActive()
    {
        //animator.SetTrigger("Hit");

    }

    private void OnTriggerEnter(Collider other)
    {
        if (hasCollided)
        {
            return;
        }
        if (other.tag == "Enemy")
        {
            currentEnemy = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == currentEnemy)
        {
            currentEnemy = null;
        }
        hasCollided = false;
    }

    public void currEnemNull()
    {
        currentEnemy = null;
    }
    public void TakeDamage(float val)
    {
        health -= val;
        PlayerPrefs.SetFloat("PlayerHealth", health);
        if (health < 0)
        {
            healthSlider.value = 0f;
        }
        UpdateHealth();
    }

    private void UpdateHealth()
    {
        if (healthSlider != null)
        {
            healthSlider.value = health / 100;
            if (health <= 0)
            {
                //FailPanel
            }
        }
    }
    
    public void Jump()
    {
        verticalVelocity.y = Mathf.Sqrt(2 * jumpForce * gravity);
    }

    private bool IsPlayerMovingAway(float horizontal, float vertical)
    {
        float threshold = 0.3f;

        return Mathf.Abs(horizontal) > threshold || Mathf.Abs(vertical) > threshold;
    }

}
