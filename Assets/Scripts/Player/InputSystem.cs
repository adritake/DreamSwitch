using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using nn.hid;
using TMPro;

public class SwitchButtons{
    public bool A, B, X, Y, Plus, Minus, Up, Down, Left, Right, L, R, ZL, ZR, StickLDown, StickRDown;
    public float StickLX, StickLY, StickRX, StickRY;

    public void Clear()
    {
        A = false; B = false; X = false; Y = false; Plus = false; Minus = false; Up = false; Down = false; Left = false; Right = false; L = false; R = false; ZL = false; ZR = false; StickLDown = false; StickRDown = false;
    }
}

public class InputSystem : MonoBehaviour
{
    public static InputSystem Instance;

    public SwitchButtons switchButtons;

    private NpadId npadId = NpadId.Invalid;
    private NpadStyle npadStyle = NpadStyle.Invalid;
    private NpadState npadState = new NpadState();

    private const int vibrationDeviceCountMax = 2;
    private int vibrationDeviceCount = 0;
    private VibrationDeviceHandle[] vibrationDeviceHandles = new VibrationDeviceHandle[vibrationDeviceCountMax];
    private VibrationDeviceInfo[] vibrationDeviceInfos = new VibrationDeviceInfo[vibrationDeviceCountMax];
    private VibrationValue vibrationValue = VibrationValue.Make();

    void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        switchButtons = new SwitchButtons();

        Npad.Initialize();

        Npad.SetSupportedStyleSet(NpadStyle.Handheld | NpadStyle.JoyDual | NpadStyle.FullKey);
        NpadId[] npadIds = { NpadId.Handheld, NpadId.No1 };
        Npad.SetSupportedIdType(npadIds);
    }

    void Update()
    {
        
        if (UpdatePadState())
        {
            vibrationValue.Clear();

            switchButtons.Clear();

            if (npadState.GetButtonDown(NpadButton.A))
            {
                switchButtons.A = true;
                Debug.Log("A");
            }

            if (npadState.GetButtonDown(NpadButton.B))
            {
                switchButtons.B = true;
            }

            if (npadState.GetButtonDown(NpadButton.X))
            {
                switchButtons.X = true;
            }

            if (npadState.GetButtonDown(NpadButton.Y))
            {
                switchButtons.Y = true;
            }

            if (npadState.GetButtonDown(NpadButton.Plus))
            {
                switchButtons.Plus = true;
            }

            if (npadState.GetButtonDown(NpadButton.Minus))
            {
                switchButtons.Minus = true;
            }

            if (npadState.GetButtonDown(NpadButton.Up))
            {
                switchButtons.Up = true;
            }

            if (npadState.GetButtonDown(NpadButton.Down))
            {
                switchButtons.Down = true;
            }

            if (npadState.GetButtonDown(NpadButton.Left))
            {
                switchButtons.Left = true;
            }

            if (npadState.GetButtonDown(NpadButton.Right))
            {
                switchButtons.Right = true;
            }

            if (npadState.GetButtonDown(NpadButton.L))
            {
                switchButtons.L = true;
            }

            if (npadState.GetButtonDown(NpadButton.R))
            {
                switchButtons.R = true;
            }

            if (npadState.GetButtonDown(NpadButton.ZL))
            {
                switchButtons.ZL = true;
            }

            if (npadState.GetButtonDown(NpadButton.ZR))
            {
                switchButtons.ZR = true;
            }

            if (npadState.GetButtonDown(NpadButton.StickL))
            {
                switchButtons.StickRDown = true;
            }

            if (npadState.GetButtonDown(NpadButton.StickR))
            {
                switchButtons.StickRDown = true;
            }

            switchButtons.StickLX = npadState.analogStickL.fx;
            switchButtons.StickLY = npadState.analogStickL.fy;
            switchButtons.StickRX = npadState.analogStickR.fx;
            switchButtons.StickRY = npadState.analogStickR.fy;

            for (int i = 0; i < vibrationDeviceCount; i++)
            {
                Vibration.SendValue(vibrationDeviceHandles[i], vibrationValue);
            }
        }
    }

    private bool UpdatePadState()
    {
        NpadStyle handheldStyle = Npad.GetStyleSet(NpadId.Handheld);
        NpadState handheldState = npadState;
        if (handheldStyle != NpadStyle.None)
        {
            Npad.GetState(ref handheldState, NpadId.Handheld, handheldStyle);
            if (handheldState.buttons != NpadButton.None)
            {
                if ((npadId != NpadId.Handheld) || (npadStyle != handheldStyle))
                {
                    this.GetVibrationDevice(NpadId.Handheld, handheldStyle);
                }
                npadId = NpadId.Handheld;
                npadStyle = handheldStyle;
                npadState = handheldState;
                return true;
            }
        }

        NpadStyle no1Style = Npad.GetStyleSet(NpadId.No1);
        NpadState no1State = npadState;

        if (no1Style != NpadStyle.None)
        {
            Npad.GetState(ref no1State, NpadId.No1, no1Style);
            if (no1State.buttons != NpadButton.None)
            {
                if ((npadId != NpadId.No1) || (npadStyle != no1Style))
                {
                    this.GetVibrationDevice(NpadId.No1, no1Style);
                }
                npadId = NpadId.No1;
                npadStyle = no1Style;
                npadState = no1State;
                return true;
            }
        }

        if ((npadId == NpadId.Handheld) && (handheldStyle != NpadStyle.None))
        {
            npadId = NpadId.Handheld;
            npadStyle = handheldStyle;
            npadState = handheldState;
        }
        else if ((npadId == NpadId.No1) && (no1Style != NpadStyle.None))
        {
            npadId = NpadId.No1;
            npadStyle = no1Style;
            npadState = no1State;
        }
        else
        {
            npadId = NpadId.Invalid;
            npadStyle = NpadStyle.Invalid;
            npadState.Clear();
            return false;
        }
        return true;
    }
   
    private void GetVibrationDevice(NpadId id, NpadStyle style)
    {
        vibrationValue.Clear();
        for (int i = 0; i < vibrationDeviceCount; i++)
        {
            Vibration.SendValue(vibrationDeviceHandles[i], vibrationValue);
        }

        vibrationDeviceCount = Vibration.GetDeviceHandles(
            vibrationDeviceHandles, vibrationDeviceCountMax, id, style);

        for (int i = 0; i < vibrationDeviceCount; i++)
        {
            Vibration.InitializeDevice(vibrationDeviceHandles[i]);
            Vibration.GetDeviceInfo(ref vibrationDeviceInfos[i], vibrationDeviceHandles[i]);
        }
    }
}

