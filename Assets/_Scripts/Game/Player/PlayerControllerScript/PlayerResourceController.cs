using System;
using System.Collections.Generic;
using System.Linq;
using _Scripts.NetworkContainter;
using _Scripts.Player;
using _Scripts.Simulation;
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
        HandCards = new(Enumerable.Repeat(EmptyCardContainer, CARD_HAND_SIZE).ToArray());        
        DiscardCards = new();     
        
        IncomeDices = new();      
        CurrentTurnDices = new(Enumerable.Repeat(EmptyDiceContainer, DICE_HAND_SIZE).ToArray());

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
        for (int i = 0; i < addDiceContainers.Length; i++)
        {
            _playerDiceHand.AddDiceToHand(addDiceContainers[i], i);
        }
    }

    [ServerRpc]
    public void AddCardToHandServerRPC()
    {
        CardContainer card ;
        if (DeckCards.Count == 0)
        {
            ShuffleDiscardIntoDeck();
            card = DeckCards[0];
        }
        else
        {
            card = DeckCards[0];
            DeckCards.RemoveAt(0);
        }

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


        if (handCardContainerIndex != -1)
        {
            AddCardToHandClientRPC(card, handCardContainerIndex);
        }
        else
        {
            DiscardCards.Add(card);
            FailAddCardToHandClientRPC(card);
        }
    }
    
    [ClientRpc]
    private void FailAddCardToHandClientRPC(CardContainer cardContainer)
    {
        _playerCardHand.FailAddCardToHand(cardContainer);
        
    }
    
    // Shuffle the discard pile into the deck
    public void ShuffleDiscardIntoDeck()
    {
        // Filter out empty cards from the discard pile
        List<CardContainer> nonEmptyDiscard = new List<CardContainer>();
        for (var index = 0; index < DiscardCards.Count; index++)
        {
            nonEmptyDiscard.Add( DiscardCards[index] );
        }
        
        DiscardCards.Clear();
        DeckCards.Clear();
        
        // Shuffle the non-empty discard pile
        var shuffleList = Shun_Utility.SetOperations.ShuffleList(nonEmptyDiscard);

        for (int index = 0; index < shuffleList.Count; index++)
        {
            DeckCards.Add(shuffleList[index]);
        }
    }

    
    [ClientRpc]
    public void AddCardToHandClientRPC(CardContainer cardContainer, int containerIndex)
    {
        _playerCardHand.AddCardToHand(cardContainer, containerIndex);    
    }
    
    [ServerRpc]
    public void RemoveDiceServerRPC(int index)
    {
        CurrentTurnDices[index] = EmptyDiceContainer;
    }
    
    [ServerRpc]
    public void RemoveCardFromHandServerRPC(int handCardContainerIndex)
    {
        if (handCardContainerIndex < 0) return;
        
        DiscardCards.Add(HandCards[handCardContainerIndex]);
        HandCards[handCardContainerIndex] = EmptyCardContainer;
    }
    
    public bool CheckEndRollPhaseTurn()
    {
        foreach (var currentDiceContainer in CurrentTurnDices)
        {
            if (!currentDiceContainer.Equals(EmptyDiceContainer))
            {
                return false;
            }    
        }

        return true;
    }
    
    
    
}