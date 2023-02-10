using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Inputs
{
    [CreateAssetMenu(menuName = "MyGame/InputSets/UIInputSet")]
    public class UIInputSet : ScriptableObject, IInputSet
    {
        public bool IsActive { get => true; }
        public KeyCode KeyboardUp = KeyCode.UpArrow;
        public KeyCode KeyboardDown = KeyCode.DownArrow;
        public KeyCode KeyboardConfirm = KeyCode.Space;
        public KeyCode KeyboardCancel = KeyCode.Escape;
        public string JoyStickVertical = "Y Axis";
        public KeyCode JoyStickConfirm = KeyCode.Joystick1Button0;
        public KeyCode JoyStickCancel = KeyCode.Joystick1Button1;
        public UnityEvent PressUpEvent = new UnityEvent();
        public UnityEvent PressDownEvent = new UnityEvent();
        public UnityEvent PressConfirmEvent = new UnityEvent();
        public UnityEvent PressCancelEvent = new UnityEvent();

        public void PressUp() { PressUpEvent.Invoke(); }
        public void PressDown() { PressDownEvent.Invoke(); }
        public void PressConfirm() { PressConfirmEvent.Invoke(); }
        public void PressCancel() { PressCancelEvent.Invoke(); }

        public void Activate() { }
        public void Deactivate() { }
        public void DetectInput()
        {
            if (Input.GetKeyDown(KeyboardUp)) PressUp();
            if (Input.GetKeyDown(KeyboardDown)) PressDown();
            if (Input.GetKeyDown(KeyboardConfirm)) PressConfirm();
            if (Input.GetKeyDown(KeyboardCancel)) PressCancel();

            // float verticalAxis = Input.GetAxis(JoyStickVertical);

            // if (verticalAxis > 0) PressUp();
            // if (verticalAxis < 0) PressDown();

            if (Input.GetKeyDown(JoyStickConfirm)) PressConfirm();
            if (Input.GetKeyDown(JoyStickCancel)) PressCancel();
        }
    }
}