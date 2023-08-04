using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InputController : MonoBehaviour
{
   [SerializeField]private TMP_InputField inputField;
   [SerializeField]private Button setButton;
   [SerializeField]private GridGenerator gridGenerator;
   private int input = 0;
   private void Start()
   {
      inputField.onValueChanged.AddListener(OnValueChanged);
      setButton.onClick.AddListener(OnClickSet);
   }

   public void OnClickSet()
   {
      if(input == 0)
         return;
      
      gridGenerator.GenerateGridCells(input);
   }
   public void OnValueChanged(string value)
   {
      input = Int32.Parse(value);
   }
}
