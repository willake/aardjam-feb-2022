using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WillakeD.CommonPatterns;

namespace Game.Inputs
{
    public class InputManager : Singleton<InputManager>
    {
        public UIInputSet UIInputSet;
        public bool AllowInput { get; private set; }

        private void Update()
        {
            if (AllowInput)
            {
                UIInputSet.DetectInput();
            }
        }

        public void SetAllowInput(bool v)
        {
            AllowInput = v;
        }
    }
}