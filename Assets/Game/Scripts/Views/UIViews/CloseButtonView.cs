using Game.Scripts.Managers;
using UnityEngine;

namespace Game.Scripts.Views.UIViews
{
    public class CloseButtonView : ButtonView
    {
        protected override void ButtonClick()
        {
            GameManager.Instance.PathFinder.BuyState = "Clear";
            GameManager.Instance.GridManager.CanClick = true;
            GameManager.Instance.UIManager.TowerBuyPanelView.CloseOpenImages(false);
        }
    }
}
