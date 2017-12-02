using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class HomingMissile : MonoBehaviour {

    public Transform target;

    public float speed = 5f;

    public float rotSpeed = 300f;

    Rigidbody2D rb;

	void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
	}
	
	void FixedUpdate ()
    {
        Vector2 direction = (Vector2)target.position - rb.position;

        direction.Normalize();

        float rotAmount = Vector3.Cross(direction, transform.up).z;

        rb.angularVelocity = -rotAmount * rotSpeed;

        rb.velocity = transform.up * speed;
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform == target)
        {
            collision.gameObject.GetComponent<Unit>().TakeDamage(1);
            Destroy(gameObject);
        }
    }
}
