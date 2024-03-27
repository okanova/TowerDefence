using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.Views.UIViews
{
   public class ButtonView : BaseView
   {
      protected Button _button;
      
      public override void Initialize()
      {
         _button = GetComponent<Button>();
         _button.onClick.AddListener(ButtonClick);
      }

      protected virtual void ButtonClick()
      {
        
      }
   }
}
