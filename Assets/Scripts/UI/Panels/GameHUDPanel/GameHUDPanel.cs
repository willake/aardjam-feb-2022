using UniRx;
using System;
using Cysharp.Threading.Tasks;
using Game.Events;
using Sirenix.OdinInspector;

namespace Game.UI
{
    public class GameHUDPanel : UIPanel
    {
        public override AvailableUI Type { get => AvailableUI.GameHUDPanel; }
        private Lazy<EventManager> _eventManager = new Lazy<EventManager>(
            () => DIContainer.instance.GetObject<EventManager>(),
            true
        );
        protected EventManager EventManager { get => _eventManager.Value; }

        [Title("References")]
        public WDButton btnMenu;

        void Start()
        {
            btnMenu
                .ButtonDidClick
                .ObserveOnMainThread()
                .Subscribe(_ => SwitchToMainGame())
                .AddTo(this);
        }

        public override WDButton[] GetSelectableButtons()
        {
            return new WDButton[] { btnMenu };
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

        public void SwitchToMainGame()
        {
            GameManager.instance.SwitchScene(AvailableScene.Menu);
        }

        private void OnDestroy()
        {
            btnMenu.StopAnimation();
        }
    }
}