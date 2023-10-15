using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;

public class PlayerTurnController : NetworkBehaviour
{
    public enum PlayerPhase
    {
        Wait,
        Preparation,
        Roll,
        Subsequence
    }
    
    public PlayerPhase CurrentPlayerPhase { get; private set; }
    
    public NativeList<int> deckCards;
    public NativeList<int> handCards;
    public NativeList<int> discardCards;
    
    private void Start()
    {
        CurrentPlayerPhase = PlayerPhase.Wait;
        Debug.Log("Start Player Turn Controller");
    }
    
    private void StartPreparationPhase()
    {
        CurrentPlayerPhase = PlayerPhase.Preparation;
        Debug.Log("Player Start Preparation");
        
    }

    private void StartRollPhase()
    {
        CurrentPlayerPhase = PlayerPhase.Roll;
    }
    
    private void StartSubsequencePhase()
    {
        CurrentPlayerPhase = PlayerPhase.Subsequence;
    }
    
    private void EndTurn()
    {
        CurrentPlayerPhase = PlayerPhase.Wait;
    }
    
    
    
}
