using Game.Scripts.MVC.Gold;
using UnityEngine;

namespace Game.Scripts.Managers
{
    public class UIManager : MonoBehaviour
    {

        public GoldController GoldController;
        public void Initialize()
        {
            GoldController.Initialize();
        }
    }
}
