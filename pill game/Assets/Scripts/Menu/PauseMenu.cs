using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused = false;

    
    [SerializeField] private GameObject _pauseMenu;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        _pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Pause()
    {
        _pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void RestartLevel()
    {
        GameManager.instance.ReloadScene();
        
    }

    /*public void EditFov(string fovToEdit)
    {
        
        float fov = float.Parse(fovToEdit);
        if (fov > 0) 
        {
            Player._defaultFOV = fov;
            GameManager.instance.cam.fieldOfView = Player._defaultFOV;
        }
    }*/
    
}
