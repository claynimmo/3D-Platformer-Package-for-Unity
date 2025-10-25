using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ExitManager : MonoBehaviour
{
    public Escape inputSystem;

    private InputAction exitGame;
    private InputAction enterGame;

    public MouseManager mouseManager;

    private bool gameLeft;

    void Awake(){
        inputSystem = new();
    }
    void OnEnable(){
        exitGame  = inputSystem.Keybinds.ExitGame;
        enterGame = inputSystem.Keybinds.EnterGame;

        exitGame.performed  += OnExitPressed;
        enterGame.performed += OnEnterPressed;

        exitGame.Enable();
        enterGame.Enable();
    }

    void OnDisable(){
        exitGame.performed  -= OnExitPressed;
        enterGame.performed -= OnEnterPressed;

        exitGame.Disable();
        enterGame.Enable();
    }

    private void OnExitPressed(InputAction.CallbackContext context){
        Time.timeScale = 0;
        mouseManager.ShowCursor();
        gameLeft = true;
    }

    private void OnEnterPressed(InputAction.CallbackContext context){
        Time.timeScale = 1;
        mouseManager.HideCursor();
        gameLeft = false;
    }


}
