using System.Collections;
using System.Collections.Generic;
using _Scripts.NetworkContainter;
using UnityEngine;

public class PlayerCardHand : PlayerControllerCompositionDependency
{
    private readonly Dictionary<int, HandCard> _containerIndexToHandCardDictionary = new Dictionary<int, HandCard>();

    public HandCard GetHandCard(int cardContainerIndex)
    {
        _containerIndexToHandCardDictionary.TryGetValue(cardContainerIndex, out var handCard);
        return handCard;
    }

    public void AddCardToHand(CardContainer cardContainer, int cardContainerIndex)
    {
        var handCard = CreateCardHand(cardContainer, cardContainerIndex);
        _containerIndexToHandCardDictionary.Add(cardContainerIndex, handCard);
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
        PlayerController.PlayerResourceController.RemoveCardServerRPC(handCard.ContainerIndex);

    }
}