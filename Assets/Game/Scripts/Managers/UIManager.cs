using Game.Scripts.MVC.Gold;
using Game.Scripts.MVC.HP;
using UnityEngine;

namespace Game.Scripts.Managers
{
    public class UIManager : MonoBehaviour
    {

        public GoldController GoldController;

        public HealthController HealthController;
        
        public void Initialize()
        {
            GoldController.Initialize();
            HealthController.Initialize();
        }
    }
}
