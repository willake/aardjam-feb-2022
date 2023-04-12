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
        public WDText textVillager;
        public Clock clock;

        private void Start()
        {
            btnSettings
                .OnClickObservable
                .ObserveOnMainThread()
                .Subscribe(async _ =>
                {
                    GameManager.instance.PauseGame();
                    await UIManager.instance.OpenUIAsync(AvailableUI.PausePanel);
                })
                .AddTo(this);
        }

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
        }
        public override void Close()
        {
            gameObject.SetActive(false);
        }
        public override async UniTask CloseAsync()
        {
            gameObject.SetActive(false);
        }

        public void SetTime(DayState dayState)
        {
            clock.SetTime(dayState);
        }

        public void SetDay(int day)
        {
            textDay.SetText($"Day {day}");
        }

        public void SetFloor(int floor)
        {
            textFloor.SetText($"Floor {floor}");
        }

        public void SetVillager(int villager)
        {
            textVillager.SetText($"Villager {villager}");
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