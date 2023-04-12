using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.UI;
using Game.Audios;

namespace Game.Gameplay
{
    public class MenuScene : GameScene
    {
        void Start()
        {
            UIManager.instance.OpenUI(AvailableUI.MenuPanel);

            if (AudioManager.instance.IsMusicPlaying == false)
            {
                WrappedAudioClip clip = ResourceManager.instance.AudioResources.musicAudios.BackgroundMusic;
                AudioManager.instance.PlayMusic(
                    clip.clip,
                    clip.volume
                );
            }
        }
    }
}
