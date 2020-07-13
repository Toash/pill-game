using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSway : MonoBehaviour
{
    [SerializeField] private float _maxAmount = 5f;
    [SerializeField] private float _smooth = 6f;
    [SerializeField] private float _raycastDistance = .6f;


    private Quaternion initialRotation;
    private Vector3 initialPosition;
    private float rotationX;
    private float movementX;
    
    
    
    [SerializeField] bool Qed;
    [SerializeField] bool Eed;
    
    public static Vector3 SwayRot;
    
    private void Start()
    {
        initialRotation = transform.localRotation;
        initialPosition = transform.localPosition;
    }

    void Update()
    {
        if (!Qed && Input.GetKeyDown(KeyCode.Q) && !Physics.Raycast(transform.position, -transform.right, _raycastDistance))
        {
            rotationX = 1 * _maxAmount;
            movementX = -1;

            Qed = true;
            Eed = false;

        }
        else if (!Eed && Input.GetKeyDown(KeyCode.E) && !Physics.Raycast(transform.position, transform.right, _raycastDistance))
        {
            rotationX = 1 * -_maxAmount;
            movementX = 1;
            
            Eed = true;
            Qed = false;
        }
        else if (Qed && Input.GetKeyDown(KeyCode.Q))
        {
            rotationX = 0;
            movementX = 0;
            
            Qed = false;
        }
        else if (Eed && Input.GetKeyDown(KeyCode.E))
        {
            rotationX = 0;
            movementX = 0;
            
            Eed = false;
        }
        
        if (Physics.Raycast(transform.position, -transform.right, _raycastDistance)
            || Physics.Raycast(transform.position, transform.right, _raycastDistance))
        {
            rotationX = 0;
            movementX = 0;
            
            Qed = false;
            Eed = false;
        }

        transform.localPosition = initialPosition + new Vector3(movementX * .5f,0,0);
        
        SwayRot = new Vector3(0,0,rotationX);
    }
}
