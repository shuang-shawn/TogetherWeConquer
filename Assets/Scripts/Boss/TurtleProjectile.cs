using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Turtle Boss Projectile Logic
/// </summary>
public class TurtleProjectile : MonoBehaviour
{
    public float speed = 8;
    private Vector3 direction;

    [SerializeField]
    private int damageAmount;
    [SerializeField] private Sprite rightSprite;
    [SerializeField] private Sprite leftSprite;
    [SerializeField] private Sprite upSprite;
    [SerializeField] private Sprite upRightSprite;
    [SerializeField] private Sprite upLeftSprite;

    public void SetDirection(Vector3 newDireciton)
    {
        direction = newDireciton.normalized;
        SetSpriteRotation();
    }

    private void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player1" || other.gameObject.tag == "Player2")
        {
            other.gameObject.GetComponent<PlayerManager>().TakeDamage(damageAmount);
        }
        Destroy(gameObject);
    }

    private void SetSpriteRotation()
    {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();

        float tolerance = 0.0001f; // Small tolerance for floating point comparison

        // Check for right
        if (direction.x > 0 && Mathf.Abs(direction.z) < tolerance)
        {
            sprite.sprite = rightSprite;
        }
        // Check for left
        else if (direction.x < 0 && Mathf.Abs(direction.z) < tolerance)
        {
            sprite.sprite = leftSprite;
        }
        // Check for up
        else if (Mathf.Abs(direction.x) < tolerance && direction.z > 0)
        {
            sprite.sprite = upSprite;
        }
        // Check for down
        else if (Mathf.Abs(direction.x) < tolerance && direction.z < 0)
        {
            sprite.sprite = upSprite;
            sprite.flipY = true;
        }
        // Check for up-right diagonal
        else if (direction.x > 0 && direction.z > 0)
        {
            sprite.sprite = upRightSprite;
        }
        // Check for up-left diagonal
        else if (direction.x < 0 && direction.z > 0)
        {
            sprite.sprite = upLeftSprite;
        }
        // Check for down-right diagonal
        else if (direction.x > 0 && direction.z < 0)
        {
            sprite.sprite = upRightSprite;
            sprite.flipY = true;
        }
        // Check for down-left diagonal
        else if (direction.x < 0 && direction.z < 0)
        {
            sprite.sprite = upLeftSprite;
            sprite.flipY = true;
        }
    }
}
