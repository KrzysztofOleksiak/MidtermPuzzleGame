using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private bool pauseMenuOpen;
    // Start is called before the first frame update
    void Start()
    {
        HideMouse();
        pauseMenuOpen = false;
    }

    private void HideMouse()
    {
        //Hide Mouse
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void ShowMouse()
    {
        //Show Mouse
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (pauseMenuOpen) ClosePauseMenu();
            else OpenPauseMenu();
        }
    }

    private void OpenPauseMenu()
    {
        ShowMouse();
        Time.timeScale = 0f;
        pauseMenuOpen = true;
    }

    private void ClosePauseMenu()
    {
        HideMouse();
        Time.timeScale = 1f;
        pauseMenuOpen = false;
    }
}
