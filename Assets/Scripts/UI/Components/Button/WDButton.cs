using UnityEngine;
using UnityEngine.EventSystems;
using UniRx;
using System;

namespace Game.UI
{
    public class WDButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
    {
        public bool playAnimation = true;
        public WDButtonAnimator animator;

        [SerializeField]
        private bool _isInteractable = true;
        public bool IsInteractable { get => _isInteractable; }
        private bool _isHovered;

        private Subject<Unit> _buttonDidClickSubject = new Subject<Unit>();
        public IObservable<Unit> ButtonDidClick
        {
            get => _buttonDidClickSubject
            .AsObservable().ThrottleFirst(Consts.THROTTTLE_IN_SCEOND);
        }
        private Subject<Unit> _buttonWillClickSubject = new Subject<Unit>();
        public IObservable<Unit> ButtonWillClick
        {
            get => _buttonWillClickSubject
            .AsObservable().ThrottleFirst(Consts.THROTTTLE_IN_SCEOND);
        }

        private void Start()
        {
            animator.SetBody(this);
        }

        public void StopAnimation()
        {
            animator.Stop();
        }

        public void SetIsInteractable(bool interactable)
        {
            _isInteractable = interactable;
        }

        public void OnPointerEnter(PointerEventData e)
        {
            if (_isInteractable == false) return;
            _isHovered = true;

            if (playAnimation)
            {
                animator.SetState(WDButtonState.Hover);
            }
        }

        public void OnPointerExit(PointerEventData e)
        {
            if (_isInteractable == false) return;
            _isHovered = false;

            if (playAnimation)
            {
                animator.SetState(WDButtonState.Idle);
            }
        }

        public void OnPointerUp(PointerEventData e)
        {
            if (_isInteractable == false) return;

            if (playAnimation)
            {
                if (_isHovered)
                {
                    animator.SetState(WDButtonState.Hover);
                }
                else
                {
                    animator.SetState(WDButtonState.Idle);
                }
            }

            _buttonDidClickSubject.OnNext(Unit.Default);
        }

        public void OnPointerDown(PointerEventData e)
        {
            if (_isInteractable == false) return;
            animator.SetState(WDButtonState.Click);

            _buttonWillClickSubject.OnNext(Unit.Default);
        }

        public void Deselect()
        {
            animator.SetState(WDButtonState.Idle);
        }

        public void Select()
        {
            animator.SetState(WDButtonState.Hover);
        }

        public void Click()
        {
            _buttonDidClickSubject.OnNext(Unit.Default);
        }
    }
}