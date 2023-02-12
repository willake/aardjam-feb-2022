using UnityEngine;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using WillakeD.CommonPatterns;
using System.Linq;
using Game.Inputs;

namespace Game.UI
{
    public class UIManager : Singleton<UIManager>
    {
        [Header("references")]
        public Canvas canvas;
        public GameObject inputBlocker;

        private Stack<UIPanel> _panelStack = new Stack<UIPanel>();

        private HashSet<AvailableUI> _openedUIList = new HashSet<AvailableUI>();

        private Dictionary<AvailableUI, UIPanel> _panelPool
            = new Dictionary<AvailableUI, UIPanel>();

        private UIPanel _focusing;
        private WDButton[] _selectableButtons;
        private int _selectedIndex;

        public bool enableKeyboardControl = false;

        private void Start()
        {
            if (enableKeyboardControl)
            {
                UIInputSet inputSet = InputManager.instance.UIInputSet;
                inputSet.PressUpEvent.AddListener(KeyboardSelectPrev);
                inputSet.PressDownEvent.AddListener(KeyboardSelectNext);
                inputSet.PressConfirmEvent.AddListener(ClickSelectedButton);
                inputSet.PressCancelEvent.AddListener(PerformCancelAction);
            }
        }

        public UIPanel OpenUI(AvailableUI ui)
        {
            if (_openedUIList.Contains(ui))
            {
                Debug.LogError("UI has been opened. There might be wrong implementation. ");
                return null;
            }
            if (_panelPool.TryGetValue(ui, out UIPanel panel) == false)
            {
                GameObject prefab = GetUIPrefab(ui);
                if (prefab != null)
                {
                    GameObject go = Instantiate(prefab, canvas.transform);
                    panel = go.GetComponent<UIPanel>();
                    _openedUIList.Add(ui);
                }
            }

            if (panel)
            {
                _panelStack.Push(panel);
                panel.transform.SetAsLastSibling();
                panel.Open();
                SetFocusing(panel);
            }

            return panel;
        }

        public async UniTask<UIPanel> OpenUIAsync(AvailableUI ui)
        {
            if (_openedUIList.Contains(ui))
            {
                Debug.LogError("UI has been opened. There might be wrong implementation. ");
                return null;
            }
            if (_panelPool.TryGetValue(ui, out UIPanel panel) == false)
            {
                GameObject prefab = GetUIPrefab(ui);
                if (prefab != null)
                {
                    GameObject go = Instantiate(prefab, canvas.transform);
                    panel = go.GetComponent<UIPanel>();
                    _openedUIList.Add(ui);
                }
            }

            if (panel)
            {
                _panelStack.Push(panel);
                panel.transform.SetAsLastSibling();
                BlockUIInput();
                await panel.OpenAsync();
                SetFocusing(panel);
            }
            UnblockUIInput();
            return panel;
        }

        private void SetFocusing(UIPanel panel)
        {
            _focusing = panel;

            if (enableKeyboardControl == false) return;

            _selectableButtons = _focusing.GetSelectableButtons();

            if (_selectableButtons.Length == 0)
            {
                _selectableButtons = null;
            }
            else
            {
                DeselectAllButtons();
                _selectedIndex = 0;
                SelectButton(0);
            }
        }

        public void CloseAllUI()
        {
            while (_panelStack.Count > 0)
            {
                UIPanel panel = _panelStack.Pop();
                panel.Close();
                _panelPool[panel.Type] = panel;
            }

            ClearUICache();
        }

        public async UniTask CloseAllUIAsync()
        {
            BlockUIInput();
            while (_panelStack.Count > 0)
            {
                UIPanel panel = _panelStack.Pop();
                await panel.CloseAsync();
                _panelPool[panel.Type] = panel;
            }

            ClearUICache();
            UnblockUIInput();
        }

        public void ClearUICache()
        {
            var keys = _panelPool.Keys;
            var list = keys.ToList();

            foreach (AvailableUI type in list)
            {
                if (_panelPool[type] != null)
                {
                    UIPanel panel = _panelPool[type];
                    _panelPool.Remove(type);
                    Destroy(panel.gameObject);
                }
            }
            _openedUIList.Clear();
        }

        public GameObject GetUIPrefab(AvailableUI ui)
        {
            switch (ui)
            {
                case AvailableUI.MenuPanel:
                    return ResourceManager.instance.UIPanelResources.menuPanel;
                case AvailableUI.GameHUDPanel:
                    return ResourceManager.instance.UIPanelResources.gameHUDPanel;
                case AvailableUI.PredictionPanel:
                    return ResourceManager.instance.UIPanelResources.PredictionPanel;
                case AvailableUI.PausePanel:
                    return ResourceManager.instance.UIPanelResources.PausePanel;
                case AvailableUI.SettingsPanel:
                    return ResourceManager.instance.UIPanelResources.SettingsPanel;
                default:
                    return null;
            }
        }

        public void Prev()
        {
            UIPanel panel = _panelStack.Pop();
            panel.Close();
            _openedUIList.Remove(panel.Type);
            _panelPool[panel.Type] = panel;

            if (_panelStack.Count > 0) SetFocusing(_panelStack.Last());
        }

        public async UniTask PrevAsync()
        {
            BlockUIInput();
            UIPanel panel = _panelStack.Pop();
            await panel.CloseAsync();
            _openedUIList.Remove(panel.Type);
            _panelPool[panel.Type] = panel;

            if (_panelStack.Count > 0) SetFocusing(_panelStack.Last());
            UnblockUIInput();
        }

        private void KeyboardSelectPrev()
        {
            if (_selectableButtons == null) return;
            int index = _selectedIndex - 1;

            if (index < 0)
            {
                index = _selectableButtons.Length - 1;
            }

            if (_selectableButtons[index].IsInteractable == false)
            {
                index = index - 1;
            }

            if (index < 0)
            {
                index = _selectableButtons.Length - 1;
            }

            DeselectButton(_selectedIndex);
            SelectButton(index);
            _selectedIndex = index;
        }

        private void KeyboardSelectNext()
        {
            if (_selectableButtons == null) return;
            int index = _selectedIndex + 1;

            if (index > _selectableButtons.Length - 1)
            {
                index = 0;
            }

            if (_selectableButtons[index].IsInteractable == false)
            {
                index = index + 1;
            }

            if (index > _selectableButtons.Length - 1)
            {
                index = 0;
            }

            DeselectButton(_selectedIndex);
            SelectButton(index);
            _selectedIndex = index;
        }

        private void DeselectButton(int index)
        {
            if (_selectableButtons == null) return;
            WDButton button = _selectableButtons[index];
            button.Deselect();
        }

        private void DeselectAllButtons()
        {
            if (_selectableButtons == null) return;

            foreach (WDButton button in _selectableButtons)
            {
                button.Deselect();
            }
        }

        private void SelectButton(int index)
        {
            if (_selectableButtons == null) return;

            WDButton button = _selectableButtons[index];
            button.Select();
        }

        private void ClickSelectedButton()
        {
            if (_selectableButtons == null) return;
            WDButton button = _selectableButtons[_selectedIndex];
            button.Click();
        }

        private void PerformCancelAction()
        {
            _focusing.PerformCancelAction();
        }

        private void BlockUIInput()
        {
            InputManager.instance.SetAllowInput(false);
            inputBlocker.SetActive(true);
            inputBlocker.transform.SetAsLastSibling();
        }

        private void UnblockUIInput()
        {
            InputManager.instance.SetAllowInput(true);
            inputBlocker.SetActive(false);
        }
    }
}