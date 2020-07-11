using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSway : MonoBehaviour
{
    [SerializeField] private float _maxAmount = 5f;


    private Quaternion initialRotation;

    public static Vector3 SwayRot;
    
    private void Start()
    {
        initialRotation = transform.localRotation;
        
    }

    void FixedUpdate()
    {
        float movementX = -Input.GetAxis("Horizontal") * _maxAmount;

        SwayRot = new Vector3(0,0,movementX);
    }
}
