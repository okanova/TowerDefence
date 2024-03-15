using Game.Scripts.Managers;
using UnityEngine;

namespace Game.Scripts.Views.UIViews
{
    public class CloseButtonView : ButtonView
    {
        [SerializeField] private GameObject _closeObject;

        protected override void ButtonClick()
        {
            GameManager.Instance.PathFinder.BuyState = "Clear";
            GameManager.Instance.GridManager.CanClick = true;
            _closeObject.SetActive(false);
        }
    }
}
