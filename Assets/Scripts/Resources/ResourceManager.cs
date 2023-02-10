using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using WillakeD.CommonPatterns;

namespace Game
{
    public class ResourceManager : Singleton<ResourceManager>
    {
        [Title("Resources")]
        public AudioResources AudioResources;
        public SceneResources SceneResources;
        public UIPanelResources UIPanelResources;
    }
}