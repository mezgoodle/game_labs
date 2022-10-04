using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovementPlayer : MonoBehaviour
{
    private CharacterController controller;

    private Vector3 velocity;
    private bool isGrounded;

    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float rotationSpeed = 2f;
    [SerializeField]
    private Camera followCamera;
    [SerializeField]
    private float jumpForce = 1.0f;
    [SerializeField]
    private float gravity = -9.81f;

    private void Start() 
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update() 
    {
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0) 
        {
            velocity.y = 0f;
        }

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = Quaternion.Euler(0, followCamera.transform.eulerAngles.y, 0) * new Vector3(horizontal, 0, vertical);
        Vector3 direction = movement.normalized;

        controller.Move(direction * speed * Time.deltaTime);

        if (direction != Vector3.zero) 
        {
            Quaternion desiredRotation = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, rotationSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            velocity.y += Mathf.Sqrt(jumpForce * -3.0f * gravity);
        }
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "Level1" && ScoreManager.scoreValue == 3) SceneManager.LoadScene(1);
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Keys")
        {
            ScoreManager.scoreValue += 1;
            Destroy(collision.gameObject);
        }
    }
}
