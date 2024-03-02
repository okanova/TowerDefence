using TMPro;
using UnityEngine;

namespace Game.Scripts.MVC.HP
{
    public class HealthView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _healthText;

        public void SetText(int value)
        {
            _healthText.text = "" + value;
        }
    }
}
