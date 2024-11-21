using UnityEngine;

public class CameraPanning : MonoBehaviour
{
    public Vector3 startPosition;      // The camera's starting position
    public Vector3 endPosition;        // The camera's ending position
    public float playerStartPosition = 251f; // Position to start moving players
    public GameObject player1;         // Reference to player 1 GameObject
    public GameObject player2;         // Reference to player 2 GameObject

    private GameObject instantiatedPlayer1;
    private GameObject instantiatedPlayer2;
    private Vector3 p1OriginalPos = new Vector3(294.12f, 0, 259.88f);        // The camera's ending position
    private Vector3 p2OriginalPos = new Vector3(303f, 0, 259.88f); // Position to start moving players

    [SerializeField]
    public float duration = 100f;      // Time it takes to pan
    private float elapsedTime = 0f;

    private Animator animator;
    private Animator animator2;

    void Start()
    {
        CreatePlayers();
        ResetPosition();
    }

    void Update()
    {
        // Keep track of how much time has passed
        elapsedTime += Time.deltaTime;

        // Calculate the percentage of completion based on elapsed time
        float progress = Mathf.Clamp01(elapsedTime / duration);

        // Interpolate between the start and end positions for the camera
        Camera.main.transform.position = Vector3.Lerp(startPosition, endPosition, progress);

        // Calculate the total distance the camera needs to move
        float totalDistance = Vector3.Distance(startPosition, endPosition);

        // Calculate the camera's current speed
        float cameraSpeed = totalDistance / duration;

        // Check if the camera has moved past playerStartPosition
        if (Camera.main.transform.position.z >= playerStartPosition)
        {
            MovePlayers(cameraSpeed);
        }

        // Reset the camera position if the panning is complete
        if (progress >= 1f)
        {
            ResetPosition();
            elapsedTime = 0f; // Reset elapsed time for the next panning
        }
    }

    void ResetPosition()
    {
        Camera.main.transform.position = startPosition;
        instantiatedPlayer1.transform.position = p1OriginalPos;
        instantiatedPlayer2.transform.position = p2OriginalPos;
    }

    void CreatePlayers()
    {
        instantiatedPlayer1 = Instantiate(player1, p1OriginalPos, Quaternion.identity);  
        instantiatedPlayer2 = Instantiate(player2, p2OriginalPos, Quaternion.identity);

        // Initialize Animator from instantiated players
        animator = instantiatedPlayer1.GetComponentInChildren<Animator>();
        animator2 = instantiatedPlayer2.GetComponentInChildren<Animator>();

    }

    void MovePlayers(float cameraSpeed)
    {
        // Move player1 and player2 forward in the positive Z direction
        instantiatedPlayer1.transform.Translate(Vector3.forward * cameraSpeed * Time.deltaTime);
        instantiatedPlayer2.transform.Translate(Vector3.forward * cameraSpeed * Time.deltaTime);

        // Calculate the amount moved in the Z direction
        float zMovementP1 = instantiatedPlayer1.transform.position.z - (instantiatedPlayer1.transform.position.z - cameraSpeed * Time.deltaTime);
        float zMovementP2 = instantiatedPlayer2.transform.position.z - (instantiatedPlayer2.transform.position.z - cameraSpeed * Time.deltaTime);

        // Set the ZInput animator parameter based on the movement
        animator.SetFloat("ZInput", zMovementP1 > 0 ? 1 : 0); // Set to 1 if moving forward, else 0
        animator2.SetFloat("ZInput", cameraSpeed * Time.deltaTime > 0 ? 1 : 0);
    }
}
