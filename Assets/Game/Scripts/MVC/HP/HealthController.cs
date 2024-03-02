using Game.Scripts.Managers;
using UnityEngine;

namespace Game.Scripts.MVC.HP
{
    public class HealthController : MonoBehaviour
    {
        private HealthView _healthView;
        
        public void Initialize()
        {
            _healthView = GetComponent<HealthView>();
            _healthView.SetText(HealthModel.HPValue);
        }

        public void AddValue(int value = 1)
        {
            HealthModel.HPValue += value;
            _healthView.SetText(HealthModel.HPValue);
            GameManager.Instance.EventManager.HPEvent();
        }

        public void RemoveValue(int value = 1)
        {
            HealthModel.HPValue -= value;
            HealthModel.HPValue = Mathf.Max(0, HealthModel.HPValue);
            _healthView.SetText(HealthModel.HPValue);

            if (HealthModel.HPValue == 0)
            {
                GameManager.Instance.EventManager.GameLoseEvent();
            }
            else
            {
                GameManager.Instance.EventManager.HPEvent();
            }
        }
    }
}
