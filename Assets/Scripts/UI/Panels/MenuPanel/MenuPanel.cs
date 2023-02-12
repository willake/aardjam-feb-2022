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
        public WDTextButton btnSettings;
        public WDTextButton btnExit;

        void Start()
        {
            btnPlay
                .OnClickObservable
                .ObserveOnMainThread()
                .Subscribe(_ => SwitchToMainGame())
                .AddTo(this);

            btnSettings
                .OnClickObservable
                .ObserveOnMainThread()
                .Subscribe(async _ =>
                {
                    await UIManager.instance.OpenUIAsync(AvailableUI.SettingsPanel);
                })
                .AddTo(this);

            btnExit
                .OnClickObservable
                .ObserveOnMainThread()
                .Subscribe(_ => GameManager.instance.ExitGame())
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