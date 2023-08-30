using UnityEngine;

public class ResetTransformAfterHit : MonoBehaviour
{
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private bool hasMoved = false;
    private bool hitOccurred = false;

    public float resetTime = 3.0f; // Adjust as needed
    public float timeAfterHitToReset = 4.0f; // Adjust as needed

    private void Start()
    {
        // Store the original position and rotation when the script starts
        originalPosition = transform.position;
        originalRotation = transform.rotation;
    }

    private void Update()
    {
        // Check if the object has moved from its original position
        if (!hasMoved && Vector3.Distance(transform.position, originalPosition) > 0.01f)
        {
            hasMoved = true;
        }

        // Check if a hit has occurred
        if (!hitOccurred && hasMoved)
        {
            hitOccurred = true;

            // Schedule the reset after a delay
            Invoke("ResetTransform", timeAfterHitToReset);
        }
    }

    private void ResetTransform()
    {
        // Reset position and rotation to original values
        transform.position = originalPosition;
        transform.rotation = originalRotation;

        // Reset flags
        hasMoved = false;
        hitOccurred = false;
    }
}