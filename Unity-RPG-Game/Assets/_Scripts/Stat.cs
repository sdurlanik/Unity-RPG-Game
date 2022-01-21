using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

public class Stat : MonoBehaviour
{
   private Image content;
   [SerializeField] private float lerpSpeed;
   private float currentFill;
   public float MyMaxValue { get; set; }
   private float currentValue;

   [SerializeField]
   private Text statValue;

   public float MyCurrentValue
   {
      get
      {
         return currentValue;
      }
      set
      {
         // Can ve mana değerlerini tanımlar (100den büyük 0dan küçük olamaz)
         if (value > MyMaxValue)
         {
            currentValue = MyMaxValue;
         }
         else if (value < MyMaxValue)
         {
            currentValue = 0;
         }
         else
         {
            currentValue = value;
         }

         currentFill = currentValue / MyMaxValue;

         if (statValue != null)
         {
            statValue.text = currentValue + "/" + MyMaxValue;
         }
         
      }
   }

   private void Start()
   {
      content = GetComponent<Image>();
   }

   private void Update()
   {
 
      
   }


   public void Initialize(float currentValue, float maxValue)
   {
      MyMaxValue = maxValue;
      MyCurrentValue = currentValue;
   }
   
}
