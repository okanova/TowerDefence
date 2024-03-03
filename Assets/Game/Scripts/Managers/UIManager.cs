using System;
using Game.Scripts.MVC.Gold;
using Game.Scripts.MVC.HP;
using UnityEngine;

namespace Game.Scripts.Managers
{
    public class UIManager : MonoBehaviour
    {

        public GoldController GoldController;

        public HealthController HealthController;

        public GameObject GamePanel;
        public GameObject LosePanel;
        
        public void Initialize()
        {
            CloseAllPanels();
            OpenSelectPanel(GamePanel);
            
            GoldController.Initialize();
            HealthController.Initialize();
            
            GameManager.Instance.EventManager.GameLose += LoseGame;
        }

        private void CloseAllPanels()
        {
            GamePanel.SetActive(false);
            LosePanel.SetActive(false);
        }

        private void OpenSelectPanel(GameObject obj)
        {
            obj.SetActive(true);
        }
        
        private void LoseGame(object sender, EventArgs args)
        {
            CloseAllPanels();
            OpenSelectPanel(LosePanel);
        }
    }
}
