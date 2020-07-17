using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;



public class GameManager : MonoBehaviour
{
    public float _explosiveBarrelRadius = 10f;
    
    
    private static GameManager _instance;
    private Camera _cam;
    private GameObject _player;
    public LayerMask _weaponIgnoreLayers;

    public LayerMask _enemyLayerMask;
    public Player playerClass;


    public static GameManager instance => _instance;


    public Camera cam => _cam;
    public GameObject player => _player;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        if (_instance == null)
        {
            _instance = this;
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
        _cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        if (_player == null)
        {
            _player = GameObject.FindGameObjectWithTag("Player");
            playerClass = _player.GetComponent<Player>();
        }
    }


    private void Update()
    {
        if (_player == null)
        {
            _player = GameObject.FindGameObjectWithTag("Player");
        }
    }

    public void LoadMenuScene()
    {
        SceneManager.LoadScene("Menu");
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
        PauseMenu.isPaused = false;
    }

    public Vector3 GenerateRandomMoveVector(Vector3 startingPos)
    {
        Vector3 randomPos = startingPos + new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5));

        return randomPos;
    }
    
}
