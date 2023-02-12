using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class RingBellButton : WDButton, IWDText
    {
        public Image background;
        public WDText text;
        public Color textColor { get => text.text.color; }

        public void SetButtonState(bool enabled)
        {
            if (enabled)
            {
                background.color = new Color(1f, 0.86f, 0.137f);
                text.SetTextColor(new Color(0.37f, 0.37f, 0.37f));
            }
            else
            {
                background.color = new Color(0.3f, 0.3f, 0.3f);
                text.SetTextColor(new Color(1, 1, 1));
            }
        }

        public void SetText(string t)
        {
            text.SetText(t);
        }

        public void SetTextColor(Color32 color)
        {
            text.SetTextColor(color);
        }
    }
}