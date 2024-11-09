using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleProjectile : MonoBehaviour
{
    public float speed = 5f;
    private Vector3 direction;

    public void SetDirection(Vector3 newDireciton)
    {
        direction = newDireciton.normalized;
    }

    private void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }
}
