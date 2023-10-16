using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.Scriptable_Objects;
using UnityEngine;
using UnityUtilities;

public class GameResourceManager : SingletonMonoBehaviour<GameResourceManager>
{
    public HandCard HandCard;
    public HandDice HandDice;
    
    private readonly Dictionary<int, CardDescription> _cardDescriptionsDictionary = new();
    private readonly Dictionary<int, DiceDescription> _diceDescriptionsDictionary = new();
    private readonly Dictionary<int, PawnDescription> _pawnDescriptionsDictionary = new();

    [SerializeField] private string _cardDescriptionAssetPath;
    [SerializeField] private string _diceDescriptionAssetPath;
    [SerializeField] private string _pawnDescriptionAssetPath;
    
    private void Awake()
    {
        InitializeCardDescriptionDictionary();
        InitializePawnDescriptionDictionary();
        InitializeDiceDescriptionDictionary();
    }

    private void InitializeCardDescriptionDictionary()
    {
        CardDescription[] cardDescriptions = Resources.LoadAll<CardDescription>(_cardDescriptionAssetPath); // Provide the folder path if needed
        foreach (CardDescription cardDescription in cardDescriptions)
        {
            _cardDescriptionsDictionary[cardDescription.CardID] = cardDescription;
        }
    }

    public CardDescription GetCardDescription(int cardID)
    {
        if (_cardDescriptionsDictionary.TryGetValue(cardID, out CardDescription cardDescription))
        {
            return cardDescription;
        }

        Debug.LogWarning("CardDescription not found for CardID: " + cardID);
        return null;
    }

    private void InitializeDiceDescriptionDictionary()
    {
        DiceDescription[] diceDescriptions = Resources.LoadAll<DiceDescription>(_diceDescriptionAssetPath); // Provide the folder path if needed
        foreach (DiceDescription diceDescription in diceDescriptions)
        {
            _diceDescriptionsDictionary[diceDescription.DiceID] = diceDescription;
        }
    }

    public DiceDescription GetDiceDescription(int diceID)
    {
        if (_diceDescriptionsDictionary.TryGetValue(diceID, out DiceDescription diceDescription))
        {
            return diceDescription;
        }

        Debug.LogWarning("DiceDescription not found for DiceID: " + diceID);
        return null;
    }
    
    private void InitializePawnDescriptionDictionary()
    {
        PawnDescription[] pawnDescriptions = Resources.LoadAll<PawnDescription>(_pawnDescriptionAssetPath); // Provide the folder path if needed
        foreach (PawnDescription pawnDescription in pawnDescriptions)
        {
            _pawnDescriptionsDictionary[pawnDescription.PawnID] = pawnDescription;
        }
    }

    public PawnDescription GetPawnDescription(int pawnID)
    {
        if (_pawnDescriptionsDictionary.TryGetValue(pawnID, out PawnDescription pawnDescription))
        {
            return pawnDescription;
        }

        Debug.LogWarning("PawnDescription not found for PawnID: " + pawnID);
        return null;
    }
    
}
