using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField]
    private float speed = 10f;  // Speed of the arrow

    private void FixedUpdate()
    {
        // Move the arrow forward in a straight line
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }
}
