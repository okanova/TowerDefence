using Game.Scripts.Managers;
using UnityEngine;

namespace Game.Scripts.Views.UIViews
{
    public class CloseButtonView : ButtonView
    {
        [SerializeField] private GameObject _closeObject;

        protected override void ButtonClick()
        {
            _closeObject.SetActive(false);
            GameManager.Instance.EventManager.TriggerPathFindEvent();
        }
    }
}
