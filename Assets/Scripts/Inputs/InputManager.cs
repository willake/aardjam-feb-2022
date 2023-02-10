using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WillakeD.CommonPatterns;

namespace Game.Inputs
{
    public class InputManager : Singleton<InputManager>
    {
        public UIInputSet UIInputSet;

        private void Update()
        {
            UIInputSet.DetectInput();
        }
    }
}