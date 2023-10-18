using System;
using _Scripts.Scriptable_Objects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(HandCard))]
public class HandCardRectVisual : MonoBehaviour
{
    private HandCard _handCard;
    private CardDescription _cardDescription;

    [SerializeField] private TMP_Text _cardName;
    [SerializeField] private TMP_Text _cardEffectDescription;
    [SerializeField] private Image _cardImage;
    [SerializeField] private TMP_Text _cardCost;

    [SerializeField] private Image _backgroundImage;
    [SerializeField] private Image _descriptionBackgroundImage;
    [SerializeField] private Image _bannerImage;

    
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
