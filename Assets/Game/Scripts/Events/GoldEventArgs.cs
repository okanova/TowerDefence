using System;

namespace Game.Scripts.Events
{
    public class GoldEventArgs : EventArgs
    {
        public int Amount { get; }

        public GoldEventArgs(int amount)
        {
            Amount = amount;
        }
    }
}
