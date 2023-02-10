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

        // [Title("References")]
        // public WDButton btnMenu;

        void Start()
        {
            // btnMenu
            //     .ButtonDidClick
            //     .ObserveOnMainThread()
            //     .Subscribe(_ => SwitchToMainGame())
            //     .AddTo(this);
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

        private void OnDestroy()
        {
            //btnMenu.StopAnimation();
        }
    }
}