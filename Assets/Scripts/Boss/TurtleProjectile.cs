using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleProjectile : MonoBehaviour
{
    public float speed = 5f;
    private Vector3 direction;

    [SerializeField]
    private int damageAmount;
    public void SetDirection(Vector3 newDireciton)
    {
        direction = newDireciton.normalized;
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
}
