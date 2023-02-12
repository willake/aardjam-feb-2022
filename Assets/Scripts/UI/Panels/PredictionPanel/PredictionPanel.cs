using UniRx;
using System;
using Cysharp.Threading.Tasks;
using Game.Events;
using UnityEngine;
using Sirenix.OdinInspector;
using DG.Tweening;
using TMPro;
using Game.Gameplay.Weathers;

namespace Game.UI
{
    public class PredictionPanel : UIPanel
    {
        public override AvailableUI Type { get => AvailableUI.PredictionPanel; }

        [Title("References")]
        public GameObject panelWeather;
        public WDButton btnEndDay;
        public WDText weatherForecasterStatement;
        public WDText weatherForecasterName;
        public Transform villagerRiddleHolder;
        public RingBellButton btnRing;
        private bool _ringTheBall;

        private void Start()
        {
            btnRing
                .OnClickObservable
                .ObserveOnMainThread()
                .Subscribe(_ => ToggleBell())
                .AddTo(this);
        }

        private void ToggleBell()
        {
            if (_ringTheBall)
            {
                _ringTheBall = false;
                btnRing.SetButtonState(false);
            }
            else
            {
                _ringTheBall = true;
                btnRing.SetButtonState(true);
            }
        }

        public override WDButton[] GetSelectableButtons()
        {
            return new WDButton[0];
        }

        public override void PerformCancelAction()
        {

        }

        public override void Open()
        {
            _ringTheBall = false;
            btnRing.SetButtonState(false);
            gameObject.SetActive(true);
        }
        public override async UniTask OpenAsync()
        {
            _ringTheBall = false;
            btnRing.SetButtonState(false);
            gameObject.SetActive(true);
            panelWeather.transform.localScale =
                new Vector3(1, 0, 1);
            await panelWeather.transform
                .DOScaleY(1, 0.4f).SetEase(Ease.InSine).AsyncWaitForCompletion();
            await UniTask.RunOnThreadPool(() => { });
        }
        public override void Close()
        {
            foreach (Transform t in villagerRiddleHolder)
            {
                GameObject.Destroy(t.gameObject);
            }

            gameObject.SetActive(false);
        }
        public override async UniTask CloseAsync()
        {
            gameObject.SetActive(false);
            await UniTask.RunOnThreadPool(() => { });
        }

        public async UniTask<bool> ShowEndDayButton()
        {
            btnEndDay.gameObject.SetActive(true);
            await btnEndDay.OnClickAsync();
            btnEndDay.gameObject.SetActive(false);

            return _ringTheBall;
        }

        public void SetForecast(ForecastRiddle riddle)
        {

            weatherForecasterName.SetText(riddle.weatherRiddle.toldBy.Name);
            weatherForecasterStatement.SetText(riddle.GenerateWeatherPredictionText());

            //Instead of iterating over, for now just add two riddles manually.
            if (riddle.firstvillagerRiddle != null)
            {
                VillagerRiddle_UI villagerRiddle = Instantiate(
                    ResourceManager.instance.GameplayResources.Weathers.VillagerRiddleUI, villagerRiddleHolder);
                villagerRiddle.SetValues(riddle.firstvillagerRiddle.toldBy.Name, riddle.GenerateVillagerRiddleText(riddle.firstvillagerRiddle), (riddle.firstvillagerRiddle.statementCredibility == RiddleElement.StatementCredibility.Lie));
            }

            if (riddle.secondvillagerRiddle != null)
            {
                VillagerRiddle_UI villagerRiddle = Instantiate(
                    ResourceManager.instance.GameplayResources.Weathers.VillagerRiddleUI, villagerRiddleHolder);
                villagerRiddle.SetValues(riddle.secondvillagerRiddle.toldBy.Name, riddle.GenerateVillagerRiddleText(riddle.secondvillagerRiddle), (riddle.secondvillagerRiddle.statementCredibility == RiddleElement.StatementCredibility.Lie));
            }
        }

        private void OnDestroy()
        {
            //btnMenu.StopAnimation();
        }
    }
}