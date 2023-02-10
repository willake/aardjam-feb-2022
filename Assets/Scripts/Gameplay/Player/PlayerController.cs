using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay
{
    public class PlayerController : Controller
    {
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A)) { }
            if (Input.GetKeyDown(KeyCode.S)) { }
            if (Input.GetKeyDown(KeyCode.D)) { }
        }
    }
}