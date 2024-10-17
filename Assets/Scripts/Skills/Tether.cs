using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tether : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;
    public float offsetY = 0.2f;
    

    private float springStrength = 200f; // Strength of the spring (tether)
    private float maxTetherDistance = 5f; // Maximum distance before tension
    private LineRenderer lineRenderer;
    private SpringJoint springJoint;

    private BoxCollider boxCollider;
    // Start is called before the first frame update
    void Start()
    {
        // UpdateTetherPosition();
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.startWidth = 0.1f; // Adjust width as needed
        lineRenderer.endWidth = 0.1f;
        lineRenderer.positionCount = 2; // Two points (start and end)

                // Attach a SpringJoint to player1 and connect it to player2
        springJoint = player1.AddComponent<SpringJoint>();
        springJoint.autoConfigureConnectedAnchor = false;
        springJoint.connectedBody = player2.GetComponent<Rigidbody>();
        springJoint.spring = springStrength;
        springJoint.maxDistance = maxTetherDistance; // The maximum allowed distance

        boxCollider = gameObject.AddComponent<BoxCollider>();
        boxCollider.isTrigger = true;
        boxCollider.center = new Vector3(0, 0, 0);
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {   Vector3 startPos = player1.transform.position;
        Vector3 endPos = player2.transform.position;
        // Vector3 direction = (startPos - endPos).normalized;
        // startPos += direction * shortenLength;
        // endPos = startPos + direction * (Vector3.Distance(startPos, endPos) - shortenLength);
        // Update the line to connect player1 and player2
        lineRenderer.SetPosition(0, startPos + new Vector3(0, offsetY, 0));
        lineRenderer.SetPosition(1, endPos + new Vector3(0, offsetY, 0));

        UpdateTetherPosition();
    }

     void UpdateTetherPosition()
    {

        Vector3 midPoint = (player1.transform.position + player2.transform.position) / 2;
        gameObject.transform.position = midPoint;

        Vector3 direction = player2.transform.position - player1.transform.position;
        gameObject.transform.rotation = Quaternion.LookRotation(direction);

        // Vector3 tetherDirection = player2.transform.position - player1.transform.position;
        float distance = Vector3.Distance(player1.transform.position, player2.transform.position);
        boxCollider.size = new Vector3(0.2f, 0.2f, distance); // Make it long enough to cover the distance
        // boxCollider.center = new Vector3(0, 0, 0); // Position it correctly along the tether
    }
}
