using System;
using System.Collections.Generic;
using Game.Scripts.MVC.Gold;
using Game.Scripts.MVC.HP;
using Game.Scripts.Views.UIViews;
using UnityEngine;

namespace Game.Scripts.Managers
{
    public class UIManager : MonoBehaviour
    {
        public GoldController GoldController;

        public HealthController HealthController;

        public GameObject GamePanel;
        public GameObject LosePanel;
        [SerializeField] private GameObject _buyTowerPanel;

        public ButtonView[] ButtonViews;
        
       

        public void Initialize()
        {
            CloseAllPanels();
            OpenSelectPanel(GamePanel);
            
            GoldController.Initialize();
            HealthController.Initialize();
            
            GameManager.Instance.EventManager.GameLose += LoseGame;

            ButtonViews = GetComponentsInChildren<ButtonView>();
            
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
        
        private void LoseGame(object sender, EventArgs args)
        {
            CloseAllPanels();
            OpenSelectPanel(LosePanel);
        }

        public void OpenBuyTowerPanel()
        {
            _buyTowerPanel.SetActive(true);
        }
    }
}
