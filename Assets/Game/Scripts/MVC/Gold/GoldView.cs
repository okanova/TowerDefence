using TMPro;
using UnityEngine;

namespace Game.Scripts.MVC.Gold
{
   public class GoldView : MonoBehaviour
   {
      [SerializeField] private TextMeshProUGUI _goldText;

      public void SetText(int value)
      {
         _goldText.text = "" + value;
      }
   }
}
