using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance = null;
    private Camera _cam = null;
    private GameObject _player = null;


    public static GameManager instance => _instance;


    public Camera cam => _cam;
    public GameObject player => _player;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
        _cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        _player = GameObject.FindGameObjectWithTag("Player");
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
    }

    public Vector3 GenerateRandomMoveVector(Vector3 startingPos)
    {
        Vector3 randomPos = startingPos + new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5));

        return randomPos;
    }
    
}
