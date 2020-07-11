using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSway : MonoBehaviour
{
    [SerializeField] private float _maxAmount = 5f;


    private Quaternion initialRotation;
    private Vector3 initialPosition;
    private float rotationX;
    private float movementX;
    
    bool Qed;
    bool Eed;
    
    public static Vector3 SwayRot;
    
    private void Start()
    {
        initialRotation = transform.localRotation;
        initialPosition = transform.localPosition;
    }

    void Update()
    {
        //float movementX = -Input.GetAxis("Horizontal") * _maxAmount;

        if (!Qed && Input.GetKeyDown(KeyCode.Q))
        {
            rotationX = 1 * _maxAmount;
            movementX = -1;
            Qed = true;
            Eed = false;
        }
        else if (!Eed && Input.GetKeyDown(KeyCode.E))
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

        transform.localPosition = initialPosition + new Vector3(1 * movementX,0,0);
        
        SwayRot = new Vector3(0,0,rotationX);
    }
}
