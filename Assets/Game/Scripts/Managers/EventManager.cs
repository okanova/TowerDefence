using System;
using Game.Scripts.Events;
using UnityEngine;

namespace Game.Scripts.Managers
{
    public class EventManager : MonoBehaviour
    {
        public event EventHandler<PathFinderEventArgs> OnChangePath;
        public event EventHandler<GoldEventArgs> OnGoldChanger;
        public event EventHandler<HPEventArgs> OnTakeDamage;
        public event EventHandler<GameLoseEventArgs> OnGameLose;

        public void TriggerPathFindEvent()
        {
            OnChangePath?.Invoke(this, new PathFinderEventArgs());
        }
        
        public void GoldEvent(int amount)
        {
            OnGoldChanger?.Invoke(this, new GoldEventArgs(amount));
        }

        public void HPEvent()
        {
            OnTakeDamage?.Invoke(this, new HPEventArgs());
        }
        
        public void GameLoseEvent()
        {
            OnGameLose?.Invoke(this, new GameLoseEventArgs());
        }
       
    }
}
