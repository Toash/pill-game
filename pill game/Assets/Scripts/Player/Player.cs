using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
        [SerializeField] private float _moveSpeed = 12.5f;

        [SerializeField] private float _crouchSpeed = 5f;

        [SerializeField] private float _aimSpeed = 4f;
        
        [SerializeField] private float _mouseSensitivity = .65f;
        
        [SerializeField] private float _jumpHeight = 3f;
        
        public  float _playerHealth = 100f;
        
        [SerializeField] private float _gravity = -9.81f;

        [SerializeField] private GameObject _sensor;
        
        private CharacterController _char;
        private Camera _cam;
        public static float xRotation = 0f;
        private Vector3 _velocity;
        private float _defaultMoveSpeed;
        
        public static bool _isAiming;
        public static bool _isCrouching = false;

        private SphereCollider sphereCollider;

        public static float _horizontal;
        public static float _vertical;
        public static float _defaultFOV;

        public static bool hasShield;
        public static bool hasMelee;
        public static bool hasPistol;
        public static bool hasSmg;
        public static bool hasShotgun;
        
        [SerializeField] private bool _hasShield;
        [SerializeField] private bool _hasMelee;
        [SerializeField] private bool _hasPistol;
        [SerializeField] private bool _hasSmg;
        [SerializeField] private bool _hasShotgun;
        

        private bool _sceneReloaded;

        private AudioSource _footSteps;

        public Camera playerCamera
        {
                get { return _cam; }
        }

        private void Awake()
        {
                _footSteps = GetComponent<AudioSource>();

                hasShield = _hasShield;
                hasMelee = _hasMelee;
                hasPistol = _hasPistol;
                hasSmg = _hasSmg;
                hasShotgun = _hasShotgun;
        }


        private void Start()
        {
                //_playerHealth = _playerHealth;
                _cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
                _char = GetComponent<CharacterController>();
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                UIManager.instance.UpdateUI(UIManager.instance._healthText, "HEALTH: " + _playerHealth);
                _defaultMoveSpeed = _moveSpeed;
                sphereCollider = _sensor.GetComponent<SphereCollider>();

                _defaultFOV = _cam.fieldOfView;
        }

        private void Update()
        {



                if (!PauseMenu.isPaused)
                {
                        _cam.transform.localRotation = Quaternion.Euler(new Vector3(Player.xRotation,0,0) + Gun.Rot + CameraSway.SwayRot);
                        Move();  
                        MouseLook();
                        Crouch();
                        Gravity();
                        GroundStickiness();
                        if (Input.GetButton("Jump") && IsGrounded())
                        {
                                Jump();
                        }

                        if (_horizontal > 0 || _vertical > 0 || _horizontal < 0 || _vertical < 0)
                        {
                                _footSteps.enabled = true;
                        }
                        else
                        {
                                _footSteps.enabled = false;
                        }

                        if (_isAiming || _isCrouching || !IsGrounded())
                        {
                                _footSteps.enabled = false;
                        }

                        if (Input.GetMouseButton(1))
                        {

                                _isAiming = true;
                        }
                        else
                        {
                                _isAiming = false;
                        }
                        
                }
        }

        private void Move()
        {
                _horizontal = Input.GetAxisRaw("Horizontal");
                _vertical = Input.GetAxisRaw("Vertical");

                Vector3 _moveVector = Vector3.ClampMagnitude(transform.right * _horizontal + transform.forward * _vertical,1);


                if (_isAiming)
                {
                        _moveSpeed = _aimSpeed;
                }

                else if (_isCrouching)
                {
                        _moveSpeed = _crouchSpeed;
                }
                else
                {
                        _moveSpeed = _defaultMoveSpeed;
                }
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
                if (Input.GetButtonDown("Crouch"))
                {
                        if (!_isCrouching)
                        {
                                _char.height = .5f;
                                _isCrouching = true;
                        }
                        else
                        {
                                _char.height = 2f;
                                _isCrouching = false;
                        }
                }
        }

        private void Gravity()
        {
                _velocity.y += _gravity * Time.deltaTime;
                _char.Move(_velocity * Time.deltaTime);
        }

        public void PlayerDamage(int damageAmount)
        {
                Gun.currentRotation += new Vector3(15,0,15);
                _playerHealth = _playerHealth - damageAmount;
                UIManager.instance.UpdateUI(UIManager.instance._healthText, "HEALTH: " + _playerHealth);
                if (_playerHealth <= 0)
                {
                        PlayerDeath();
                        
                        

                }
        }
        public void PlayerDeath()
        {

                        GameManager.instance.ReloadScene();

        }

        private void OnTriggerEnter(Collider other)
        {
                if (other.CompareTag("AmmoPickup"))
                {
                        other.GetComponent<Pickup>().AddPickup();
                        AudioManager.PlaySoundAtPosition(AudioManager.instance.PickupSFX,.25f,other.transform.position, AudioManager.instance.Mixer.FindMatchingGroups("Effects")[0]);
                        Destroy(other.gameObject);
                }
                if (other.CompareTag("ObjectPickup"))
                {
                        other.GetComponent<Pickup>().AddObject();
                        AudioManager.PlaySoundAtPosition(AudioManager.instance.PickupSFX,.25f,other.transform.position, AudioManager.instance.Mixer.FindMatchingGroups("Effects")[0]);
                        Destroy(other.gameObject);
                }
        }

        public void ActivateSensorTrigger(float radius)
        {
                StartCoroutine(ActivateSensorTriggerCoroutine(radius));
        }

        IEnumerator ActivateSensorTriggerCoroutine(float radius)
        {
                sphereCollider.radius = radius;
                _sensor.SetActive(true);
                yield return new WaitForSeconds(.05f);
                _sensor.SetActive(false);
                sphereCollider.radius = 0;
        }
        
}
