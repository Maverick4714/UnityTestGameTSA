using UnityEngine;

public class CameraFollowTwoPlayers : MonoBehaviour
{
    public Transform player1; // Reference to Player 1
    public Transform player2; // Reference to Player 2

    public float smoothSpeed = 0.5f; // Smoothing speed for camera movement
    public float minZoom = 5f;      // Minimum camera size
    public float maxZoom = 15f;     // Maximum camera size
    public float zoomLimiter = 10f; // Value to limit how much the camera zooms out

    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void LateUpdate()
    {
        if (player1 != null && player2 != null)
        {
            // Move the camera to the midpoint of the two players
            Vector3 midpoint = GetMidpoint();
            Vector3 smoothPosition = Vector3.Lerp(transform.position, midpoint, smoothSpeed);
            transform.position = new Vector3(smoothPosition.x, smoothPosition.y, transform.position.z);

            // Adjust the camera zoom
            float distance = GetDistance();
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, Mathf.Clamp(distance / zoomLimiter, minZoom, maxZoom), smoothSpeed);
        }
    }

    private Vector3 GetMidpoint()
    {
        return (player1.position + player2.position) / 2;
    }

    private float GetDistance()
    {
        return Vector3.Distance(player1.position, player2.position);
    }
}