using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface CollisionDetector
{

    // public string hitMessage = "Hit Collider!";
    // private void OnCollisionEnter(Collision collision){
    //     Debug.Log(hitMessage);
    // }

    void handleCollision(Collision collision);
    void changeHealth(int amount);
}
