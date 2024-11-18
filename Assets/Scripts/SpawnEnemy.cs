using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{

    public GameObject BasicEnemy;
    private static float minDistance = 10f;
    private static float maxDistance = 12f;
    public static float spawnDelay = 2f;
    private float spawnTimer = 0f;
    Vector3 cameraPointOnFloor;

    private Vector3 findRayPointOnFloor(Ray ray){
        return ray.origin + ((ray.origin.y / -ray.direction.y) * ray.direction);
    }

    private Vector3 findCameraPointOnFloor(){
        Vector3 intersection = new Vector3(0, 0, 0);
        Camera cam = GameObject.Find("Main Camera").GetComponent<Camera>();

        GameObject floor = GameObject.Find("SpawnPlane");
        Plane plane = new Plane(floor.transform.up, floor.transform.position);

        Vector3 cameraPosition = cam.transform.position;
        Vector3 cameraForward = cam.transform.forward;

        Ray ray = new Ray (cameraPosition, cameraForward);

        if(plane.Raycast(ray, out float distance)) {
            intersection = ray.GetPoint(distance);
        } else {
            Debug.Log("No intersection found -- not supposed to happen");
        }

        return intersection ;
    }

    private Vector3 generateRandomSpawnPoint(Vector3 center) {
        float offset = 1f;
        Camera cam = GameObject.Find("Main Camera").GetComponent<Camera>();

        Ray topLeft = cam.ViewportPointToRay(new Vector3(0, 1, 0));

        Vector3 topLeftPoint = findRayPointOnFloor(topLeft);

        float radius = Vector3.Distance(topLeftPoint, center);
        float maxDistance = radius + offset;
        Debug.Log(radius);

        Vector3 randomSpawnPoint = Random.insideUnitCircle * maxDistance;

        while(Vector3.Distance(randomSpawnPoint, center) < radius) {
            randomSpawnPoint = Random.insideUnitSphere * maxDistance;
            
            randomSpawnPoint += center;
        }
        randomSpawnPoint.y = 1;


        // Vector3 randomSpawnPoint = Random.insideUnitSphere * maxDistance;;

        // while(Vector3.Distance(randomSpawnPoint, center) < minDistance) {
        //     randomSpawnPoint = Random.insideUnitSphere * maxDistance;
            
        //     randomSpawnPoint.y = 1;
        //     randomSpawnPoint += center;
        // }

        return randomSpawnPoint;
    }

    void Start() {
        cameraPointOnFloor = findCameraPointOnFloor();
    }

    void FixedUpdate()
    {
        cameraPointOnFloor = findCameraPointOnFloor();

        //For Testing Purposes
    //     if(Input.GetKeyDown(KeyCode.Space)){

    //         Vector3 randomSpawnPoint = generateRandomSpawnPoint(cameraPointOnFloor);

    //         // Vector3 randomSpawnPosition = new Vector3(Random.Range(-10, 11), 1, Random.Range(-10, 11));
    //         Instantiate(BasicEnemy, randomSpawnPoint, Quaternion.identity);
    //     }

        spawnTimer += Time.deltaTime;

        if(spawnTimer >= spawnDelay) {
            spawnTimer = 0;
            Vector3 randomSpawnPoint = generateRandomSpawnPoint(cameraPointOnFloor);
            Vector3 randomSpawnPosition = new Vector3(Random.Range(-10, 11), 1, Random.Range(-10, 11));
            Instantiate(BasicEnemy, randomSpawnPoint, Quaternion.identity);
        }
    }


}
