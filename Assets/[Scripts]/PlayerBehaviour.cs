using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [Header("Player Movement")] 
    [Range(0.0f, 200.0f)]
    public float horizontalForce;
    [Range(0.0f, 1.0f)]
    public float decay;
    public Bounds bounds;

    [Header("Player Attack")]
    public Transform bulletSpawn;
    public int frameDelay;

    private Rigidbody2D rigidbody;
    private BulletManager bulletManager;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        bulletManager = GameObject.FindObjectOfType<BulletManager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
        CheckBounds();
        CheckFire();
    }

    private void Move()
    {
        var x = Input.GetAxisRaw("Horizontal");

        rigidbody.AddForce(new Vector2(x * horizontalForce, 0.0f));

        rigidbody.velocity *= (1.0f - decay);
    }

    private void CheckBounds()
    {
        // Left Boundary
        if (transform.position.x < bounds.min)
        {
            transform.position = new Vector2(bounds.min, transform.position.y);
        }

        // Right Boundary
        if (transform.position.x > bounds.max)
        {
            transform.position = new Vector2(bounds.max, transform.position.y);
        }
    }

    private void CheckFire()
    {
        if ((Time.frameCount % frameDelay == 0) && (Input.GetAxisRaw("Jump") > 0))
        {
            bulletManager.GetBullet(bulletSpawn.position, BulletType.PLAYER);
        }
    }
}
