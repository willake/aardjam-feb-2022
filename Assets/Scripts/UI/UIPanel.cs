using UnityEngine;
using Cysharp.Threading.Tasks;

namespace Game.UI
{
    public abstract class UIPanel : MonoBehaviour
    {
        public abstract AvailableUI Type { get; }
        public abstract void Open();
        public abstract UniTask OpenAsync();
        public abstract void Close();
        public abstract UniTask CloseAsync();
        public abstract WDButton[] GetSelectableButtons();
        public abstract void PerformCancelAction();
    }
}