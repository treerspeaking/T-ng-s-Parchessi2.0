using System;
using _Scripts.NetworkContainter;
using _Scripts.Player;
using Shun_Unity_Editor;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerResourceController : NetworkBehaviour
{
    const int DICE_HAND_SIZE = 3;
    const int CARD_HAND_SIZE = 5;
    private static readonly CardContainer EmptyCardContainer = new CardContainer{CardID = -1};
    private static readonly DiceContainer EmptyDiceContainer = new DiceContainer{DiceID = -1};

    public NetworkList<CardContainer> DeckCards;
    public NetworkList<CardContainer> HandCards; // This Must work as array
    public NetworkList<CardContainer> DiscardCards; 
    
    public NetworkList<DiceContainer> IncomeDices;
    public NetworkList<DiceContainer> CurrentTurnDices; // This Must work as array

    private PlayerDiceHand _playerDiceHand;
    private PlayerCardHand _playerCardHand;

    private void Awake()
    {
        DeckCards = new();        
        HandCards = new(new CardContainer[CARD_HAND_SIZE]);        
        DiscardCards = new();     
                          
        IncomeDices = new();      
        CurrentTurnDices = new(new DiceContainer[DICE_HAND_SIZE]);

    }

    private void Start()
    {
        for (var index = 0; index < HandCards.Count; index++)
        {
            HandCards[index] = EmptyCardContainer;
        }
        
        for (var index = 0; index < CurrentTurnDices.Count; index++)
        {
            CurrentTurnDices[index] = EmptyDiceContainer;
        }
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
    public void AddCardToDeckServerRPC(CardContainer cardContainer)
    {
        DeckCards.Add(cardContainer);
    }
    
    [ServerRpc(RequireOwnership = false)]
    public void GainIncomeServerRPC()
    {
        DiceContainer[] addDiceContainers = new DiceContainer[IncomeDices.Count]; // RPC came before NetworkList 
        CurrentTurnDices.Clear();
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
    public void AddCardToHandServerRPC()
    {
        int index = Random.Range(0, DeckCards.Count);
        var card = DeckCards[index];
        DeckCards.RemoveAt(index);

        int handCardContainerIndex = -1;
        for (var i = 0; i < HandCards.Count; i++)
        {
            var cardContainer = HandCards[i];
            if (cardContainer.Equals(EmptyCardContainer))
            {
                handCardContainerIndex = i;
                HandCards[i] = card;
                break;
            }
        }

        AddCardToHandClientRPC(card, handCardContainerIndex);
    }
    
    [ClientRpc]
    public void AddCardToHandClientRPC(CardContainer cardContainer, int containerIndex)
    {
        _playerCardHand.AddCardToHand(cardContainer, containerIndex);    
    }
    
    [ServerRpc]
    public void RemoveDiceServerRPC(int index)
    {
        //CurrentTurnDices.RemoveAt(index);
        CurrentTurnDices[index] = EmptyDiceContainer;
    }
    
    [ServerRpc]
    public void RemoveCardFromHandServerRPC(int handCardContainerIndex)
    {
        DiscardCards.Add(HandCards[handCardContainerIndex]);
        //HandCards.RemoveAt(handCardContainerIndex);
        HandCards[handCardContainerIndex] = EmptyCardContainer;
    }

    
    
}