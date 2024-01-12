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
    private InputActions input;

    private bool isInputActive;
    private bool inSceneCooldown;

    private IEnumerator Start()
    {
        input = new InputActions();
        
        input.Player.Enable();
        
        isInputActive = false;
        
        yield return new WaitUntil(() => SplashScreen.isFinished);
        
        isInputActive = true;
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
    
    public bool TaskMenu()
    {
        if (!isInputActive) { return false; }

        return input.Player.TaskMenu.WasPressedThisFrame();
    }
    
    public bool Interact()
    {
        if (!isInputActive) { return false; }

        return input.Player.Interact.WasPressedThisFrame();
    }
    
    public Vector2 Camera()
    {
        if (!isInputActive) { return new Vector2(); }

        return input.Player.Camera.ReadValue<Vector2>();
    }
}