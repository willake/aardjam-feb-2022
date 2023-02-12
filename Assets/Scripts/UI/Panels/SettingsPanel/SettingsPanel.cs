using UniRx;
using System;
using Cysharp.Threading.Tasks;
using Game.Events;
using UnityEngine;
using Sirenix.OdinInspector;
using DG.Tweening;
using UnityEngine.UI;

namespace Game.UI
{
    public class SettingsPanel : UIPanel
    {
        public override AvailableUI Type { get => AvailableUI.SettingsPanel; }

        [Title("References")]
        public Transform panel;
        public WDTextButton btnScreenmode;
        public WDTextButton btnResolution;
        public Slider sliderMusic;
        public Slider sliderSFX;
        public WDTextButton btnApply;
        public WDTextButton btnClose;

        public override WDButton[] GetSelectableButtons()
        {
            return new WDButton[0];
        }

        public override void PerformCancelAction()
        {

        }

        public override void Open()
        {
            gameObject.SetActive(true);
        }
        public override async UniTask OpenAsync()
        {
            gameObject.SetActive(true);
            await UniTask.RunOnThreadPool(() => { });
        }
        public override void Close()
        {
            gameObject.SetActive(false);
        }
        public override async UniTask CloseAsync()
        {
            gameObject.SetActive(false);
            await UniTask.RunOnThreadPool(() => { });
        }

        private void OnDestroy()
        {
        }
    }
}