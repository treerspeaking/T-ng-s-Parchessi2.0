using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterManger : MonoBehaviour
{
   public static CharacterManger instance;

   public Character[] characters;
   public Character currentCharacter;

   public void Awake()
   {
      if (instance == null)
      {
         instance = this;
      }
      else
      {
         Destroy(gameObject);
      }
      DontDestroyOnLoad(gameObject);
   }

   private void Start()
   {
      if (characters.Length > 0 && currentCharacter == null)
      {
         currentCharacter = characters[0];
      }  
      
   }

   public void SetCharacter(Character character)
   {
      currentCharacter = character; 
   }  
}
