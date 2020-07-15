using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSway : MonoBehaviour
{
    [SerializeField] private float _amount = 0.02f;
    [SerializeField] private float _maxAmount = 0.06f;
    [SerializeField] private float _smoothAmount = 6f;

    private Vector3 initialPosition;

    private void Start()
    {
        initialPosition = transform.localPosition;
        
    }

    private void Update()
    {
        float movementX = (-Input.GetAxis("Mouse X") + (Input.GetAxis("Horizontal") * 2)) * _amount;
        float movementY = (-Input.GetAxis("Mouse Y") + (-Input.GetAxis("Vertical") * .1f)) * _amount;
        movementX = Mathf.Clamp(movementX, -_maxAmount, _maxAmount);
        movementY = Mathf.Clamp(movementY, -_maxAmount, _maxAmount);
        
        Vector3 finalPosition = new Vector3(movementX,movementY,0);
        transform.localPosition = Vector3.Lerp(transform.localPosition, finalPosition + initialPosition,
            Time.deltaTime * _smoothAmount);

    }
}
