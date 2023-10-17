using System;
using _Scripts.NetworkContainter;
using _Scripts.Player;
using Shun_Unity_Editor;
using Unity.Netcode;
using UnityEngine;

public class PlayerResourceController : NetworkBehaviour
{

    public NetworkList<CardContainer> DeckCards;
    public NetworkList<CardContainer> HandCards;
    public NetworkList<CardContainer> DiscardCards;
    
    public NetworkList<DiceContainer> IncomeDices;
    public NetworkList<DiceContainer> CurrentTurnDices;

    private PlayerDiceHand _playerDiceHand;
    private PlayerCardHand _playerCardHand;

    private void Awake()
    {
        DeckCards = new();        
        HandCards = new();        
        DiscardCards = new();     
                          
        IncomeDices = new();      
        CurrentTurnDices = new(); 
    }

    public override void OnNetworkSpawn()
    {
        if (!IsOwner) return;
        
        _playerDiceHand = FindObjectOfType<PlayerDiceHand>();
        _playerCardHand = FindObjectOfType<PlayerCardHand>();

    }

    [ServerRpc(RequireOwnership = false)]
    public void AddIncomeServerRPC(DiceContainer diceContainer)
    {
        IncomeDices.Add(diceContainer);
    }
    
    [ServerRpc(RequireOwnership = false)]
    public void GainIncomeServerRPC()
    {
        foreach(var diceContainer in IncomeDices)
        {
            CurrentTurnDices.Add(diceContainer);
        }
        
        GainIncomeClientRPC();
    }

    [ClientRpc]
    private void GainIncomeClientRPC()
    {
        if (IsOwner)
        {

            for (int i = 0; i < CurrentTurnDices.Count; i++)
            {
                _playerDiceHand.AddDiceToHand(CurrentTurnDices[i], i);
            }
        }
        else
        {
            Debug.Log($"Not Owner Gain Income {OwnerClientId}, {NetworkManager.LocalClientId}");
        }
    }
    
    [ServerRpc]
    public void RemoveDiceServerRPC(int index)
    {
        CurrentTurnDices.RemoveAt(index);
    }


}