
using System;
using _Scripts.Scriptable_Objects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HandCard : MonoBehaviour
{
    private PlayerCardHand _playerCardHand;
    
    public CardDescription CardDescription;
    
    [SerializeField] private TMP_Text _cardName;
    [SerializeField] private TMP_Text _cardDescription;
    [SerializeField] private Image _cardImage;
    [SerializeField] private TMP_Text _cardCost;
    
    [SerializeField] private Image _backgroundImage;
    [SerializeField] private Image _descriptionBackgroundImage;
    [SerializeField] private Image _bannerImage;
    
    private void Start()
    {
        LoadCard();
    }

    private void LoadCard()
    {
        if(CardDescription == null) return;
        
        _cardName.text = CardDescription.CardName;
        _cardDescription.text = CardDescription.CardDescriptionText;
        _cardImage.sprite = CardDescription.CardSprite;
        _cardCost.text = CardDescription.CardCost.ToString();
    }
    
    
    
}
