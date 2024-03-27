using Game.Scripts.Controller.TowerSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.Views.UIViews
{
    public class TowerUpgradePanelView : BaseView
    {
        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private Image _image;
        [SerializeField] private UpgradeButtonView _upgradeButton;
        [SerializeField] private SellButtonView _sellButton;

        private Image[] _images;
        private TextMeshProUGUI[] _texts;

        public override void Initialize()
        {
            _images = GetComponentsInChildren<Image>();
            _texts = GetComponentsInChildren<TextMeshProUGUI>();
            ClosePanel();
        }

        public void OpenPanel(Tower tower)
        {
            _name.text = tower.Name;
            _image.sprite = tower.Sprite;
            _upgradeButton.SetCost(tower.UpgradeValue);
            _sellButton.SetCost(tower.SellValue);
            
            for (int i = 0; i < _images.Length; i++)
            {
                _images[i].enabled = true;
            }
            
            for (int i = 0; i < _texts.Length; i++)
            {
                _texts[i].enabled = true;
            }
        }

        public void ClosePanel()
        {
            for (int i = 0; i < _images.Length; i++)
            {
                _images[i].enabled = false;
            }
            
            for (int i = 0; i < _texts.Length; i++)
            {
                _texts[i].enabled = false;
            }
        }
    }
}
