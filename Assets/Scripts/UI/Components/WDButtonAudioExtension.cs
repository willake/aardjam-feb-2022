using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Game.Audios;

namespace Game.UI
{
    public class WDButtonAudioExtension : MonoBehaviour
    {
        private WDButton button;

        private void Start()
        {
            if (button == null)
            {
                button = GetComponent<WDButton>();
            }

            button
                .ButtonWillClick
                .ObserveOnMainThread()
                .Subscribe(_ => PlayAudio())
                .AddTo(this);
        }

        protected void PlayAudio()
        {
            WrappedAudioClip wrappedClip = ResourceManager.instance
                .AudioResources.uiAudios.ButtonClick;

            AudioManager.instance.PlaySFX(
                wrappedClip.clip,
                wrappedClip.volume
            );
        }
    }
}