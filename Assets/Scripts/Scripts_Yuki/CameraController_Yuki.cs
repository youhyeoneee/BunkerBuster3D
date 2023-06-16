using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController_Yuki : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] float smoothTime;
    private Vector3 velocity = Vector3.zero;
    private float offsetY;
    bool camChange = false;

    [Header("CamRotation")]
    [SerializeField] float targetRotation;
    [SerializeField] float rotationSpeed = 10f;

    private float currentRotation;

    // Start is called before the first frame update
    void Start()
    {
        offsetY = transform.position.y - target.transform.position.y;
        currentRotation = transform.eulerAngles.x;
        Debug.Log($"currentRotation: {currentRotation}");
    }

    // LateUpdate is called once per frame, after other updates
    void LateUpdate()
    {
        Vector3 newPosition = transform.position;
        newPosition.y = target.transform.position.y + offsetY;
        transform.position = newPosition;

        if (camChange)
        {
            ChangeCam();
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            camChange = true;
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            targetRotation = 0;
            smoothTime = 4f;
        }
        
    }

    void ChangeCam()
    {
        // Quaternion targetRotation = Quaternion.Euler(0, 0, 0);
        // transform.rotation = targetRotation;

        Vector3 newPosition = transform.position;
        newPosition.z = -450f;
        // transform.position = newPosition;
        transform.position = Vector3.SmoothDamp(transform.position,
                                                newPosition,
                                                ref velocity,
                                                smoothTime);

        if (currentRotation > targetRotation)
        {
            // Calculate the new rotation
            float newRotation = currentRotation + rotationSpeed * Time.deltaTime;

            // Clamp the rotation to the target rotation
            newRotation = Mathf.Clamp(newRotation, 0f, targetRotation);

            // Apply the new rotation
            transform.rotation = Quaternion.Euler(newRotation, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);

            // Update the current rotation
            currentRotation = newRotation;
        }

    }
}
