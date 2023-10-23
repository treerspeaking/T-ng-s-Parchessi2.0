using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.NetworkContainter;
using _Scripts.Player.Card;
using UnityEngine;

public class PlayerCardHand : PlayerControllerCompositionDependency
{
    [SerializeField] private int _maxCards = 5;
    private readonly Dictionary<int, HandCard> _containerIndexToHandCardDictionary = new Dictionary<int, HandCard>();
    private HandCardRegion _handCardRegion;

    private void Awake()
    {
        _handCardRegion = gameObject.GetComponent<HandCardRegion>();
    }

    public HandCard GetHandCard(int cardContainerIndex)
    {
        _containerIndexToHandCardDictionary.TryGetValue(cardContainerIndex, out var handCard);
        return handCard;
    }

    public void AddCardToHand(CardContainer cardContainer, int cardContainerIndex)
    {
        if (_maxCards <= _containerIndexToHandCardDictionary.Count) return;
        
        var handCard = CreateCardHand(cardContainer, cardContainerIndex);
        _containerIndexToHandCardDictionary.Add(cardContainerIndex, handCard);
        _handCardRegion.TryAddCard(handCard.GetComponent<HandCardDragAndTargeter>());
    }


    public HandCard CreateCardHand(CardContainer cardContainer, int cardContainerIndex)
    {
        var cardDescription = GameResourceManager.Instance.GetCardDescription(cardContainer.CardID);
        var handCard = Instantiate(GameResourceManager.Instance.HandCardPrefab);
        handCard.Initialize(this, cardDescription, cardContainerIndex, PlayerController.OwnerClientId);
        return handCard;
    }

    public void PlayCard(HandCard handCard)
    {
        if (IsOwner)
        {
            PlayerController.PlayerResourceController.RemoveCardFromHandServerRPC(handCard.ContainerIndex);
            _containerIndexToHandCardDictionary.Remove(handCard.ContainerIndex);

        }
        else
        {
            _handCardRegion.RemoveCard(handCard.GetComponent<HandCardDragAndTargeter>());
            _containerIndexToHandCardDictionary.Remove(handCard.ContainerIndex);

        }
    }
}