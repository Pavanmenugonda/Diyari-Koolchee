using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class PlayerThrow : MonoBehaviour
{
    public GameObject objectToThrow;
    public Transform ballPosition;
    public Slider throwSpeedSlider;
    public TextMeshProUGUI forceNumberText;
    public List<Collider> validClickAreas;
    public float minThrowForce = 5f;
    public float maxThrowForce = 20f;
    public float ballDisappearTime = 5f;
    public float throwCooldown = 3f; // Cooldown time between throws

    private bool isSelectingBounceTarget = false;
    private Vector3 bounceTargetPosition;
    private float currentThrowForce;

    private bool hasThrown = false;
    private bool isAnimationPlaying = false;
    private float lastThrowTime = 0f; // Track the time of the last throw

    private void Start()
    {
        currentThrowForce = minThrowForce;
        throwSpeedSlider.minValue = minThrowForce;
        throwSpeedSlider.maxValue = maxThrowForce;
        throwSpeedSlider.value = currentThrowForce;
    }

    void Update()
    {
        if (!isAnimationPlaying && !hasThrown && Input.GetMouseButtonDown(0))
        {
            Vector3 clickPosition = GetMouseWorldPosition();
            if (IsValidClickPosition(clickPosition))
            {
                isSelectingBounceTarget = true;
                isAnimationPlaying = true; // Simulate animation playing
            }
        }

        if (isAnimationPlaying && Input.GetMouseButtonUp(0))
        {
            isSelectingBounceTarget = false;
            ReleaseThrow();
        }

        if (isSelectingBounceTarget)
        {
            bounceTargetPosition = GetMouseWorldPosition();
        }

        forceNumberText.text = currentThrowForce.ToString("F1");

        currentThrowForce = throwSpeedSlider.value;
    }

    void ReleaseThrow()
    {
        // Check if enough time has passed since the last throw
        if (Time.time - lastThrowTime >= throwCooldown)
        {
            GameObject thrownObject = Instantiate(objectToThrow, ballPosition.position, Quaternion.identity);

            Rigidbody rb = thrownObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 throwDirection = (bounceTargetPosition - ballPosition.position).normalized;
                rb.AddForce(throwDirection * currentThrowForce, ForceMode.Impulse);
            }

            StartCoroutine(DisappearAfterTime(thrownObject));
            ResetThrowingFlags();
            lastThrowTime = Time.time; // Update the last throw time
        }
    }

    void ResetThrowingFlags()
    {
        hasThrown = false;
        isAnimationPlaying = false; // Reset animation playing flag
    }

    IEnumerator DisappearAfterTime(GameObject ball)
    {
        yield return new WaitForSeconds(ballDisappearTime);
        Destroy(ball);
    }

    Vector3 GetMouseWorldPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            return hit.point;
        }

        return Vector3.zero; // Fallback value
    }

    bool IsValidClickPosition(Vector3 position)
    {
        foreach (Collider collider in validClickAreas)
        {
            if (collider.bounds.Contains(position))
            {
                return true;
            }
        }
        return false;
    }
}
