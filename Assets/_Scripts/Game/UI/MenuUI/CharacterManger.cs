using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityUtilities;


public class CharacterManger : SingletonMonoBehaviour<CharacterManger>
{
   public Character[] characters;
   public Character currentCharacter;

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
