using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    private bool isActive = false;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(isActive);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleCrosshair() {
        isActive = !isActive;
        gameObject.SetActive(isActive); // Show the health bar
    }

    
}
