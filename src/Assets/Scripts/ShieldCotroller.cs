using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldCotroller : MonoBehaviour {

    public float distanceFromPlayer;

    private Animator animator;
    private PolygonCollider2D poligonCollider;
    private bool activated;
    public GameObject shieldParticle;


    void Start () {
        animator = GetComponent<Animator>();
        poligonCollider = GetComponent<PolygonCollider2D>();
        poligonCollider.enabled = false;
    }
	
	
	void Update () {
    }


    public void UpdatePosition(Vector3 playerPosition)
    {
        //rotation
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - playerPosition;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = rotation;

        //position
        float cos = Mathf.Cos(angle * Mathf.Deg2Rad);
        float sin = Mathf.Sin(angle * Mathf.Deg2Rad);
        Vector2 newPosition = new Vector2(cos * distanceFromPlayer + playerPosition.x,
            sin * distanceFromPlayer + playerPosition.y);
        transform.position = newPosition;
    }

    public void Activate()
    {
        SoundManagerController.PlaySound("Shield");
        poligonCollider.enabled = true;
        activated = true;
        animator.SetBool("Casting", true);
    }

    public void Deactivate()
    {
        animator.SetBool("Casting", false);
        activated = false;
        turnShieldOff();
        
    }

    private void turnShieldOff()
    {
        if (!activated)
        {
            poligonCollider.enabled = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Ball")
        {
            Instantiate(shieldParticle, transform.position, shieldParticle.transform.rotation);
        }
    }
}
