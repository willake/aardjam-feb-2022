using UniRx;
using System;
using Cysharp.Threading.Tasks;
using Game.Events;
using Sirenix.OdinInspector;

namespace Game.UI
{
    public class MenuPanel : UIPanel
    {
        public override AvailableUI Type { get => AvailableUI.MenuPanel; }
        private Lazy<EventManager> _eventManager = new Lazy<EventManager>(
            () => DIContainer.instance.GetObject<EventManager>(),
            true
        );
        protected EventManager EventManager { get => _eventManager.Value; }

        [Title("References")]
        public WDTextButton btnPlay;

        void Start()
        {
            btnPlay
                .ButtonDidClick
                .ObserveOnMainThread()
                .Subscribe(_ => SwitchToMainGame())
                .AddTo(this);

            btnPlay.SetText("Play");
        }

        public override WDButton[] GetSelectableButtons()
        {
            return new WDButton[] {
                btnPlay
            };
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
            GameManager.instance.SwitchScene(AvailableScene.MainGame);
        }

        private void OnDestroy()
        {
            btnPlay.StopAnimation();
        }
    }
}