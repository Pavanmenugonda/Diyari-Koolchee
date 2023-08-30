using UnityEngine;
using Cinemachine;

public class CameraMovement : MonoBehaviour
{
    public float moveSpeed = 5.0f; // Adjust as needed
    public float minDistance = 5.0f; // Adjust as needed
    public float maxDistance = 20.0f; // Adjust as needed

    private CinemachineVirtualCamera virtualCamera;

    private void Start()
    {
        // Get the CinemachineVirtualCamera component from the child
        virtualCamera = transform.GetChild(0).GetComponent<CinemachineVirtualCamera>();
    }

    private void Update()
    {
        // Get the mouse scroll input
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        // Calculate the new camera distance
        float newDistance = virtualCamera.m_Lens.Orthographic ?
            virtualCamera.m_Lens.OrthographicSize - scroll * moveSpeed :
            virtualCamera.m_Lens.FieldOfView - scroll * moveSpeed;

        // Clamp the new distance within the specified range
        newDistance = Mathf.Clamp(newDistance, minDistance, maxDistance);

        // Apply the new camera distance
        if (virtualCamera.m_Lens.Orthographic)
        {
            virtualCamera.m_Lens.OrthographicSize = newDistance;
        }
        else
        {
            virtualCamera.m_Lens.FieldOfView = newDistance;
        }
    }
}