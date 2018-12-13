using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour {

    public float bounceForce;

    private Rigidbody2D rb;
    private CameraController cameraController;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cameraController = GetComponent<CameraController>();
    }

    private void FixedUpdate()
    {
        //rb.velocity = Vector2.ClampMagnitude(rb.velocity, 14);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        SoundManagerController.PlaySound("Bounce");
        if (collision.gameObject.name == "Shield" || collision.gameObject.name == "RedShield")
        {
            cameraController.CamShake();

            Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            direction.Normalize();
            rb.AddForce(direction * bounceForce);
        }
    }
}
