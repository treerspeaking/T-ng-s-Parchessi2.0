using System;
using _Scripts.Scriptable_Objects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(HandCard))]
public class HandCardVisual : MonoBehaviour
{
    private HandCard _handCard;
    private CardDescription _cardDescription;

    [SerializeField] private TMP_Text _cardName;
    [SerializeField] private TMP_Text _cardEffectDescription;
    [SerializeField] private SpriteRenderer _cardImage;
    [SerializeField] private TMP_Text _cardCost;

    [SerializeField] private SpriteRenderer _backgroundImage;
    [SerializeField] private SpriteRenderer _descriptionBackgroundImage;
    [SerializeField] private SpriteRenderer _bannerImage;

    
    private void Start()
    {
        _handCard = GetComponent<HandCard>();
        _cardDescription = _handCard.CardDescription;
        LoadCard();
    }

    private void LoadCard()
    {
        if(_cardDescription == null) return;
    
        _cardName.text = _cardDescription.CardName;
        _cardEffectDescription.text = _cardDescription.CardEffectDescription;
        _cardImage.sprite = _cardDescription.CardSprite;
        _cardCost.text = _cardDescription.CardCost.ToString();
    }
}
