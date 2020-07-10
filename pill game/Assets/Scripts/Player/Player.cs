﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
        [SerializeField] private float _moveSpeed = 12.5f;

        [SerializeField] private float _crouchSpeed = 5f;
        
        [SerializeField] private float _mouseSensitivity = .65f;
        
        [SerializeField] private float _jumpHeight = 3f;
        
        [SerializeField] private float _health = 100f;
        
        [SerializeField] private float _gravity = -9.81f;

        
        
        private CharacterController _char;
        private Camera _cam;
        public static float xRotation = 0f;
        private Vector3 _velocity;
        private float _defaultMoveSpeed;
        
        

        public Camera playerCamera
        {
                get { return _cam; }
        }

        private void Awake()
        {
                _defaultMoveSpeed = _moveSpeed;
        }


        private void Start()
        {
                _char = GetComponent<CharacterController>();
                _cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
        }

        private void Update()
        {
                if (!PauseMenu.isPaused)
                {
                        Move();  
                        MouseLook();
                        Crouch();
                        Gravity();
                        GroundStickiness();
                        if (Input.GetButtonDown("Jump") && IsGrounded())
                        {
                                Jump();
                        }
                        
                }
        }

        private void Move()
        {
                float _horizontal = Input.GetAxisRaw("Horizontal");
                float _vertical = Input.GetAxisRaw("Vertical");

                Vector3 _moveVector = Vector3.ClampMagnitude(transform.right * _horizontal + transform.forward * _vertical,1);

                _char.Move(_moveVector * _moveSpeed * Time.deltaTime);
                
        }

        private void MouseLook()
        {
                float _mouseX = Input.GetAxis("Mouse X") * _mouseSensitivity;
                float _mouseY = Input.GetAxis("Mouse Y") * _mouseSensitivity;

                xRotation -= _mouseY;
                xRotation = Mathf.Clamp(xRotation, -90, 90);
                
                //_cam.transform.localRotation = Quaternion.Euler(xRotation);
                transform.Rotate(Vector3.up * _mouseX);
        }

        private bool IsGrounded()
        {
                return Physics.Raycast(transform.position, Vector3.down, 1.35f);
        }

        private void Jump()
        {
                _velocity.y = Mathf.Sqrt(_jumpHeight * -2f * _gravity);
        }

        private void GroundStickiness()
        {
                if (IsGrounded() && _velocity.y < 0)
                {
                        _velocity.y = -2f;
                }
        }

        private void Crouch()
        {
                if (Input.GetButton("Crouch"))
                {
                        transform.localScale = new Vector3(1,.5f,1);
                        
                }
                else
                {
                        transform.localScale = new Vector3(1,1,1);
                }
        }

        private void Gravity()
        {
                _velocity.y += _gravity * Time.deltaTime;
                _char.Move(_velocity * Time.deltaTime);
        }
        public void PlayerDeath()
        {
                GameManager.instance.ReloadScene();
        }
        
}
