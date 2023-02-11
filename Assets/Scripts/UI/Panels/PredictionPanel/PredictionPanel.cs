using UniRx;
using System;
using Cysharp.Threading.Tasks;
using Game.Events;
using Sirenix.OdinInspector;

namespace Game.UI
{
    public class PredictionPanel : UIPanel
    {
        public override AvailableUI Type { get => AvailableUI.PredictionPanel; }

        [Title("References")]
        public WDButton btnEndDay;

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
        public override void Close()
        {
            gameObject.SetActive(false);
        }
        public override void CloseImmediately()
        {
            gameObject.SetActive(false);
        }

        public async UniTask ShowEndDayButton()
        {
            btnEndDay.gameObject.SetActive(true);
            await btnEndDay.OnClickAsync();
            btnEndDay.gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            //btnMenu.StopAnimation();
        }
    }
}