using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Sirenix.OdinInspector;

namespace Game
{
    [CreateAssetMenu(menuName = "MyGame/Resources/UIPanelResources")]
    public class UIPanelResources : ScriptableObject
    {
        [Title("UI Panels")]
        [Required("Must link a ui panel asset")]
        [AssetsOnly]
        public GameObject menuPanel;
        [Required("Must link a ui panel asset")]
        [AssetsOnly]
        public GameObject gameHUDPanel;
        [Required("Must link a ui panel asset")]
        [AssetsOnly]
        public GameObject PredictionPanel;
    }
}