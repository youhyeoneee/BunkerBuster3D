using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController_Yuki : MonoBehaviour
{
    [SerializeField] private GameObject target;
    private float offsetY;

    // Start is called before the first frame update
    void Start()
    {
        offsetY = transform.position.y - target.transform.position.y;
    }

    // LateUpdate is called once per frame, after other updates
    void LateUpdate()
    {
        Vector3 newPosition = transform.position;
        newPosition.y = target.transform.position.y + offsetY;
        transform.position = newPosition;
    }
}
