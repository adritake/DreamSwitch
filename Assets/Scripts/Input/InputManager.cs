using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class InputManager: Singleton<InputManager>
{
    public InputController activeInputController;
    private InputActions input;
    
    public delegate void OnInputDeviceChangeDelegate();
    public OnInputDeviceChangeDelegate onInputDeviceChangeDelegate;

    private bool isInputActive;
    private bool inSceneCooldown;

    private void Awake()
    {
        activeInputController = InputController.Touch;
    }

    private IEnumerator Start()
    {
        input = new InputActions();
        
        input.Player.Enable();
        
        isInputActive = false;
        
        yield return new WaitUntil(() => SplashScreen.isFinished);
        
        isInputActive = true;
    }

    private void Update()
    {
        switch (activeInputController)
        {
            case InputController.Touch:
                Cursor.visible = false;
                break;
            case InputController.Mouse:
                Cursor.visible = true;
                break;
            case InputController.Gamepad:
                Cursor.visible = false;
                break;
        }
    }

    public void ToggleInput(bool active)
    {
        isInputActive = active;
    }

    public bool IsInputActive()
    {
        return isInputActive;
    }
    
    //Inputs

    public Vector2 Movement()
    {
        if (!isInputActive) { return new Vector2(); }
        
        return input.Player.Movement.ReadValue<Vector2>();
    }
}

public enum InputController
{
    Gamepad, Touch, Mouse
}

public enum InputTypes
{
    WasPressedThisFrame, WasReleasedThisFrame, IsPressed
}