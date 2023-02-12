using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Game
{
    [CreateAssetMenu(menuName = "MyGame/Resources/AudioResources")]
    public class AudioResources : ScriptableObject
    {
        [Title("UI Audio Assets")]
        public UIAudios uiAudios;
        public GameplayAudios gameplayAudios;

        [Serializable]
        public class UIAudios
        {
            public WrappedAudioClip ButtonClick;
        }

        [Serializable]
        public class GameplayAudios
        {
            public WrappedAudioClip BellRing;
        }
    }
}