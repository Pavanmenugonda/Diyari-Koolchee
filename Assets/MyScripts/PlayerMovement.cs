using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Adjust this to set the player's movement speed.
    public float moveDistance = 2f; // Adjust this to set the distance to move.

    private Vector3 targetPosition;
    private bool isMoving = false;

    void Update()
    {
        if (isMoving)
        {
            MoveTowardsTarget();
        }
    }

    public void OnMoveButtonClick()
    {
        if (!isMoving)
        {
            // Calculate the target position based on the current position and desired movement distance.
            targetPosition = transform.position + Vector3.right * moveDistance;

            // Start moving.
            isMoving = true;
        }
    }

    void MoveTowardsTarget()
    {
        // Calculate the step to move this frame.
        float step = moveSpeed * Time.deltaTime;

        // Move towards the target position.
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);

        // Check if we've reached the target position.
        if (Vector3.Distance(transform.position, targetPosition) < 0.001f)
        {
            // Stop moving.
            isMoving = false;
        }
    }
}