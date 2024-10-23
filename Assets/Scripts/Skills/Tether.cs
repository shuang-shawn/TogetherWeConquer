using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Threading;
using UnityEngine;

public class Tether : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;
    public float offsetY = 0.2f;

    public int frameCount = 6;
    public float animationSpeed = 12.0f;
    public ParticleSystem onHit;
    public Material tetherMaterial;
    
    public float defaultSpringStrength = 500f;
    public float defaultMaxDistance = 10f;
    public float tetherSpringStrength = 200f; // Strength of the spring (tether)
    public float tetherMaxTetherDistance = 5f; // Maximum distance before tension
    private LineRenderer lineRenderer;
    private SpringJoint defaultSpringJoint;
    private SpringJoint tetherSpringJoint;

    private BoxCollider boxCollider;

    public bool tetherToggle = false;
    private bool previousToggle = false;
    // Start is called before the first frame update
    void Start()
    {
        // Default distance limiter
        defaultSpringJoint = player1.AddComponent<SpringJoint>();
        defaultSpringJoint.autoConfigureConnectedAnchor = false;
        defaultSpringJoint.connectedBody = player2.GetComponent<Rigidbody>();
        defaultSpringJoint.spring = defaultSpringStrength;
        defaultSpringJoint.maxDistance = defaultMaxDistance;

        


        

        
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {   
        if (tetherToggle != previousToggle) {
            ToggleTether(tetherToggle);
            previousToggle = tetherToggle;
        }
        if (tetherToggle) {
            UpdateLinePosition();
            UpdateTetherPosition();
        }
    }

     void UpdateTetherPosition()
    {

        Vector3 midPoint = (player1.transform.position + player2.transform.position) / 2;
        gameObject.transform.position = midPoint;
        Vector3 direction = player2.transform.position - player1.transform.position;
        gameObject.transform.rotation = Quaternion.LookRotation(direction);

        float distance = Vector3.Distance(player1.transform.position, player2.transform.position);
        boxCollider.size = new Vector3(0.2f, 0.2f, distance); // Make it long enough to cover the distance
    }

    void DrawLine() {
        // Draw line
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.startWidth = 0.1f; // Adjust width as needed
        lineRenderer.endWidth = 0.1f;
        lineRenderer.positionCount = 2; // Two points (start and end)

        lineRenderer.material = tetherMaterial;
        tetherMaterial.SetFloat("_FrameCount", frameCount);
        tetherMaterial.SetFloat("_AnimationSpeed", animationSpeed);
    }

    void CreateTether() {
        // Attach a SpringJoint to player1 and connect it to player2
        tetherSpringJoint = player1.AddComponent<SpringJoint>();
        tetherSpringJoint.autoConfigureConnectedAnchor = false;
        tetherSpringJoint.connectedBody = player2.GetComponent<Rigidbody>();
        tetherSpringJoint.spring = tetherSpringStrength;
        tetherSpringJoint.maxDistance = tetherMaxTetherDistance; // The maximum allowed distance
    }

    void CreateCollider() {
        // create collider
        boxCollider = gameObject.AddComponent<BoxCollider>();
        boxCollider.isTrigger = true;
        boxCollider.center = new Vector3(0, 0, 0);
    }

    void UpdateLinePosition() {
        Vector3 startPos = player1.transform.position;
        Vector3 endPos = player2.transform.position;

        // Update the line to connect player1 and player2
        lineRenderer.SetPosition(0, startPos + new Vector3(0, offsetY, 0));
        lineRenderer.SetPosition(1, endPos + new Vector3(0, offsetY, 0));

        float distance = Vector3.Distance(startPos, endPos);
        lineRenderer.material.mainTextureScale = new Vector3(distance, 1);
    }

    void ToggleTether(bool toggle) {
        if (toggle) {
            DrawLine();
            CreateTether();
            CreateCollider();
        } else {
            Destroy(lineRenderer);
            Destroy(tetherSpringJoint);
            Destroy(boxCollider);
        }
    }
        
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            Vector3 hit = other.transform.position;
            hit.y += 0.5f;

            ParticleSystem newParticle = Instantiate(onHit, hit, Quaternion.identity);
            newParticle.Play();

            Destroy(newParticle.gameObject, newParticle.main.duration + newParticle.main.startLifetime.constantMax);
        }
    }
}
