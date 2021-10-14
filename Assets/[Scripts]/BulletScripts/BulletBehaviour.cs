using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    public BulletType type;

    [Header("Bullet Movement")]
    [Range(0.0f, 0.5f)]
    public float speed;
    public Bounds bulletBounds;
    public BulletDirection direction;

    private BulletManager bulletManager;
    private Vector3 bulletVelocity;

    // Start is called before the first frame update
    void Start()
    {
        bulletManager = GameObject.FindObjectOfType<BulletManager>();

        switch (direction)
        {
            case BulletDirection.UP:
                bulletVelocity = new Vector3(0.0f, speed, 0.0f);
                break;
            case BulletDirection.RIGHT:
                bulletVelocity = new Vector3(speed, 0.0f, 0.0f);
                break;
            case BulletDirection.DOWN:
                bulletVelocity = new Vector3(0.0f, -speed, 0.0f);
                break;
            case BulletDirection.LEFT:
                bulletVelocity = new Vector3(-speed, 0.0f, 0.0f);
                break;

        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
        CheckBounds();
    }

    private void Move()
    {
        transform.position += bulletVelocity;
    }

    private void CheckBounds()
    {
        if (transform.position.y < bulletBounds.max)
        {

            bulletManager.ReturnBullet(this.gameObject, type);
        }

        if (transform.position.y > bulletBounds.min)
        {

            bulletManager.ReturnBullet(this.gameObject, type);
        }
    }
}
