using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.Views.UIViews
{
    public class TowerBuyPanelView : BaseView
    {
        public Image[] Images;
        public TextMeshProUGUI[] Texts;
        
        public override void Initialize()
        {
            Images = GetComponentsInChildren<Image>();
            Texts = GetComponentsInChildren<TextMeshProUGUI>();
            CloseOpenImages(false);
        }

        public void CloseOpenImages(bool value)
        {
            for (int i = 0; i < Images.Length; i++)
            {
                Images[i].enabled = value;
            }

            for (int i = 0; i < Texts.Length; i++)
            {
                Texts[i].enabled = value;
            }
        }
    }
}
