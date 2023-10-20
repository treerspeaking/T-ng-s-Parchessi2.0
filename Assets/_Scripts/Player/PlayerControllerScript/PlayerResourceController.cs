using System;
using _Scripts.NetworkContainter;
using _Scripts.Player;
using Shun_Unity_Editor;
using Unity.Collections;
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
    }

    public void InitializeHand(PlayerDiceHand playerDiceHand, PlayerCardHand playerCardHand)
    {
        _playerDiceHand = playerDiceHand;
        _playerCardHand = playerCardHand;
    }

    [ServerRpc(RequireOwnership = false)]
    public void AddIncomeServerRPC(DiceContainer diceContainer)
    {
        IncomeDices.Add(diceContainer);
    }
    
    [ServerRpc(RequireOwnership = false)]
    public void GainIncomeServerRPC()
    {
        DiceContainer[] addDiceContainers = new DiceContainer[IncomeDices.Count]; // RPC came before NetworkList 
        for (var index = 0; index < IncomeDices.Count; index++)
        {
            var diceContainer = IncomeDices[index];
            addDiceContainers[index] = diceContainer;
            CurrentTurnDices.Add(diceContainer);
        }

        GainIncomeClientRPC(addDiceContainers);
    }

    [ClientRpc]
    private void GainIncomeClientRPC(DiceContainer[] addDiceContainers = default)
    {
        if (IsOwner)
        {

            for (int i = 0; i < addDiceContainers.Length; i++)
            {
                _playerDiceHand.AddDiceToHand(addDiceContainers[i], i);
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
    
    [ServerRpc]
    public void RemoveCardServerRPC(int handCardContainerIndex)
    {
        DiscardCards.Add(HandCards[handCardContainerIndex]);
        HandCards.RemoveAt(handCardContainerIndex);
    }
}