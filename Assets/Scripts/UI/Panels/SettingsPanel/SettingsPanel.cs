using UniRx;
using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Sirenix.OdinInspector;
using DG.Tweening;
using UnityEngine.UI;
using Game.Screens;
using System.Linq;
using Game.Audios;

namespace Game.UI
{
    public class SettingsPanel : UIPanel
    {
        public override AvailableUI Type { get => AvailableUI.SettingsPanel; }
        private Lazy<ScreenAspectManager> _screenAspectManager = new Lazy<ScreenAspectManager>(
            () => DIContainer.instance.GetObject<ScreenAspectManager>(),
            true
        );
        protected ScreenAspectManager ScreenAspectManager { get => _screenAspectManager.Value; }

        [Title("References")]
        public Image background;
        public RectTransform panel;
        public WDTextButton btnScreenmode;
        public WDTextButton btnResolution;
        public Slider sliderMusic;
        public Slider sliderSFX;
        public WDTextButton btnApply;
        public WDTextButton btnClose;

        [Header("Settings")]
        public float fadeDuration = 0.2f;
        public Ease fadeEase = Ease.OutSine;

        private ScreenMode _screenMode;
        private Resolution[] _resolutions;
        private int _resolutionsIndex = 0;

        private void Start()
        {
            btnScreenmode
                .OnClickObservable
                .ObserveOnMainThread()
                .Subscribe(_ =>
                {
                    _screenMode = _screenMode == ScreenMode.FullScreen
                        ? ScreenMode.Windowed
                        : ScreenMode.FullScreen;

                    UpdateButtons();
                })
                .AddTo(this);

            btnResolution
                .OnClickObservable
                .ObserveOnMainThread()
                .Subscribe(_ =>
                {
                    int newIndex = _resolutionsIndex - 1;

                    if (newIndex < 0) newIndex = _resolutions.Length - 1;

                    _resolutionsIndex = newIndex;

                    UpdateButtons();
                })
                .AddTo(this);

            sliderMusic
                .OnValueChangedAsObservable()
                .ObserveOnMainThread()
                .Subscribe(v => AudioManager.instance.SetMusicVolumeByPercentage(v))
                .AddTo(this);

            sliderSFX
                .OnValueChangedAsObservable()
                .ObserveOnMainThread()
                .Subscribe(v => AudioManager.instance.SetSFXVolumeByPercentage(v))
                .AddTo(this);

            btnApply
                .OnClickObservable
                .ObserveOnMainThread()
                .Subscribe(_ => ApplyScreenOption())
                .AddTo(this);

            btnClose
                .OnClickObservable
                .ObserveOnMainThread()
                .Subscribe(_ => UIManager.instance.PrevAsync().Forget())
                .AddTo(this);
        }

        private void Init()
        {
            WrappedResolution resolution = ScreenAspectManager.Resolution;
            _resolutions = Screen.resolutions.Where(x => x.refreshRate == resolution.refreshRate).ToArray();
            _resolutionsIndex = _resolutions
                .ToList().FindIndex(x => x.width == resolution.width && x.height == resolution.height);

            if (_resolutionsIndex < 0) _resolutionsIndex = Screen.resolutions.Length - 1;

            _screenMode = ScreenAspectManager.ScreenMode;

            sliderMusic.maxValue = 1.0f;
            sliderMusic.minValue = 0.0f;
            sliderSFX.maxValue = 1.0f;
            sliderSFX.minValue = 0.0f;
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
            Init();

            UpdateButtons();

            gameObject.SetActive(true);
        }
        public override async UniTask OpenAsync()
        {
            Init();

            UpdateButtons();

            gameObject.SetActive(true);
            panel.anchoredPosition = new Vector2(0, -450f);
            Color c = background.color;
            c.a = 0;
            background.color = c;

            Sequence sequence = DOTween.Sequence();

            sequence
                .Append(
                    panel
                        .DOAnchorPosY(0, fadeDuration).SetEase(fadeEase))
                .Join(
                    background.DOFade(0.2f, fadeDuration).SetEase(fadeEase));

            await sequence.AsyncWaitForCompletion();
        }
        public override void Close()
        {
            gameObject.SetActive(false);
        }
        public override async UniTask CloseAsync()
        {
            Sequence sequence = DOTween.Sequence();

            sequence
                .Append(
                    panel
                        .DOAnchorPosY(-450, fadeDuration).SetEase(fadeEase))
                .Join(
                    background.DOFade(0, fadeDuration).SetEase(fadeEase));

            await sequence.AsyncWaitForCompletion();
            gameObject.SetActive(false);
        }

        private void UpdateButtons()
        {
            btnScreenmode.SetText(GetScreenModeText(_screenMode));

#if !UNITY_WEBGL
            if (_screenMode == ScreenMode.FullScreen)
            {
                btnResolution.SetTextColor(new Color(0.6f, 0.6f, 0.6f, 1));
                btnResolution.SetIsInteractable(false);
            }
            else
            {
                btnResolution.SetTextColor(new Color(0.31f, 0.31f, 0.31f, 1));
                btnResolution.SetIsInteractable(true);
            }
#else
            btnResolution.SetTextColor(new Color(0.6f, 0.6f, 0.6f, 1));
            btnResolution.SetIsInteractable(false);
            btnScreenmode.SetTextColor(new Color(0.6f, 0.6f, 0.6f, 1));
            btnScreenmode.SetIsInteractable(false);
#endif

            Resolution resolution = _resolutions[_resolutionsIndex];
            btnResolution.SetText($"{resolution.width} x {resolution.height}");

            sliderMusic.value = AudioManager.instance.MusicVolumePercentage;
            sliderSFX.value = AudioManager.instance.SFXVolumePercentage;
        }

        private void ApplyScreenOption()
        {
#if !UNITY_WEBGL
            if (_screenMode == ScreenMode.FullScreen)
            {
                ScreenAspectManager.SetFullScreen();
            }
            else
            {
                ScreenAspectManager.SetWindow(_resolutions[_resolutionsIndex]);
            }
#endif
        }

        private string GetScreenModeText(ScreenMode mode)
        {
            switch (mode)
            {
                case ScreenMode.FullScreen:
                default:
                    return "FullScreen";
                case ScreenMode.Windowed:
                    return "Windowed";
            }
        }

        private void OnDestroy()
        {
        }
    }
}