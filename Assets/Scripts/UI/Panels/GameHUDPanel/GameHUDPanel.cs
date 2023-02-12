using UniRx;
using System;
using Cysharp.Threading.Tasks;
using Game.Events;
using Sirenix.OdinInspector;
using Game.Gameplay;
using DG.Tweening;

namespace Game.UI
{
    public class GameHUDPanel : UIPanel
    {
        public override AvailableUI Type { get => AvailableUI.GameHUDPanel; }

        [Title("References")]
        public WDButton btnSettings;
        public WDText textDay;
        public WDText textFloor;
        public Clock clock;

        public override WDButton[] GetSelectableButtons()
        {
            return new WDButton[] { };
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

        public void SetTime(DayState dayState)
        {
            clock.SetTime(dayState);
        }

        public async UniTask SetTimeAsync(DayState dayState)
        {
            await clock.SetTimeAsync(dayState).AsyncWaitForCompletion();
        }

        private void OnDestroy()
        {
            btnSettings.StopAnimation();
        }
    }
}