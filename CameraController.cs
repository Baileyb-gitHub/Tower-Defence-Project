using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float upperHeightCap= 5f;
    [SerializeField] private float lowerHeightCap= 1f;

    [SerializeField] private float upperXCap;
    [SerializeField] private float lowerXCap;

    [SerializeField] private float horizontalMoveSpeed = 1f;
    [SerializeField] private float verticalMoveSpeed = 1f;
    [SerializeField] private float rotationMoveSpeed = 1f;

    static float moveTime = 1;

     Vector3 targetPosition;
     Quaternion targetRotation;

    // Start is called before the first frame update
    void Start()
    {
        targetPosition = gameObject.transform.position;
        targetRotation = gameObject.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
    }

    private void HandleMovement() 
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) 
        {
            targetPosition += (transform.right * -horizontalMoveSpeed);
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            targetPosition += (transform.right * horizontalMoveSpeed);
        }

        if (Input.GetKey(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            targetPosition += (transform.forward * horizontalMoveSpeed);
        }

        if (Input.GetKey(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            targetPosition += (transform.forward * -horizontalMoveSpeed);
        }



        if (Input.GetKey(KeyCode.LeftControl))
        {
            targetPosition += (transform.up * verticalMoveSpeed);
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            targetPosition += (transform.up * -verticalMoveSpeed);
        }


        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(0, -rotationMoveSpeed * Time.deltaTime, 0);
        }

        if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(0, rotationMoveSpeed * Time.deltaTime, 0);
        }

        
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * moveTime);
    }

    private void ClampPosition() 
    {
    
    }
}
