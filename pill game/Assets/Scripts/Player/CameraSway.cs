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
        int playerMask = ~ LayerMask.GetMask("Player");
        
        if (!Qed && Input.GetKeyDown(KeyCode.Q) && !Physics.Raycast(transform.position, -transform.right, _raycastDistance,playerMask))
        {
            //rotationX = 1 * _maxAmount;
            //movementX = -1;
            //movementX = Mathf.Lerp(movementX, -1, Time.deltaTime * 6);
            
            
            Qed = true;
            Eed = false;

        }
        else if (!Eed && Input.GetKeyDown(KeyCode.E) && !Physics.Raycast(transform.position, transform.right, _raycastDistance,playerMask))
        {
            //rotationX = 1 * -_maxAmount;
            //movementX = 1;
            //movementX = Mathf.Lerp(movementX, 1, Time.deltaTime * 6);
            
            Eed = true;
            Qed = false;
        }
        else if (Qed && Input.GetKeyDown(KeyCode.Q))
        {
            //rotationX = 0;
            //movementX = 0;
            //movementX = Mathf.Lerp(movementX, 0, Time.deltaTime * 6);
            
            Qed = false;
        }
        else if (Eed && Input.GetKeyDown(KeyCode.E))
        {
            //rotationX = 0;
            //movementX = 0;
            //movementX = Mathf.Lerp(movementX, 0, Time.deltaTime * 6);
            
            Eed = false;
        }
        
        if (Physics.Raycast(transform.position, -transform.right, _raycastDistance,playerMask)
            || Physics.Raycast(transform.position, transform.right, _raycastDistance,playerMask))
        {
            //rotationX = 0;
            //movementX = 0;
            //movementX = Mathf.Lerp(movementX, 0, Time.deltaTime * 6);
            
            Qed = false;
            Eed = false;
        }

        if (Qed)
        {
            movementX = Mathf.Lerp(movementX, -1, Time.deltaTime * _smooth);
            rotationX = Mathf.Lerp(rotationX, 1 * _maxAmount, Time.deltaTime * _smooth);
        }

        else if (Eed)
        {
            movementX = Mathf.Lerp(movementX, 1, Time.deltaTime * _smooth);
            rotationX = Mathf.Lerp(rotationX, 1 * -_maxAmount, Time.deltaTime * _smooth);
        }
        else
        {
            movementX = Mathf.Lerp(movementX, 0, Time.deltaTime * _smooth);
            rotationX = Mathf.Lerp(rotationX, 0, Time.deltaTime * _smooth);
        }
        
        
        

        transform.localPosition = initialPosition + new Vector3(movementX * .5f,0,0);
        
        SwayRot = new Vector3(0,0,rotationX);
    }
}
