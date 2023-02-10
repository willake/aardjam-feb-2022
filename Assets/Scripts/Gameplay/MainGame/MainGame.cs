using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.UI;

namespace Game.Gameplay
{
    public class MainGame : GameScene
    {
        void Start()
        {
            UIManager.instance.OpenUI(AvailableUI.GameHUDPanel);
        }
    }
}
