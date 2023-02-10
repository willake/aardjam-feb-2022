using UnityEngine;

namespace Game.UI
{
    public abstract class UIPanel : MonoBehaviour
    {
        public abstract AvailableUI Type { get; }
        public abstract void Open();
        public abstract void Close();
        public abstract void CloseImmediately();
        public abstract WDButton[] GetSelectableButtons();
        public abstract void PerformCancelAction();
    }
}