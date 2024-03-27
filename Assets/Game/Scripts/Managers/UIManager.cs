using System;
using Game.Scripts.MVC.Gold;
using Game.Scripts.MVC.HP;
using Game.Scripts.Views.UIViews;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

namespace Game.Scripts.Managers
{
    public class UIManager : MonoBehaviour
    {
        public GoldController GoldController;

        public HealthController HealthController;

        public GameObject GamePanel;
        public GameObject LosePanel;

        public ButtonView[] ButtonViews;

        public TowerBuyPanelView TowerBuyPanelView;
        
        public void Initialize()
        {
            CloseAllPanels();
            OpenSelectPanel(GamePanel);
            
            GoldController.Initialize();
            HealthController.Initialize();
            TowerBuyPanelView.Initialize();
            
            GameManager.Instance.EventManager.OnGameLose += LoseOnGame;

            ButtonViews = FindObjectsOfType<ButtonView>(true);
            
            for (int i = 0; i < ButtonViews.Length; i++)
            {
                ButtonViews[i].Initialize();
            }
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
        
        private void LoseOnGame(object sender, EventArgs args)
        {
            CloseAllPanels();
            OpenSelectPanel(LosePanel);
        }
    }
}