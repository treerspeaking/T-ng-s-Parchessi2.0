using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.Player.Pawn;
using _Scripts.Scriptable_Objects;
using UnityEngine;
using UnityUtilities;


public class GameResourceManager : SingletonMonoBehaviour<GameResourceManager>
{
    public HandCard HandCard;
    public HandDice HandDice;
    public MapPawn MapPawn;
    
    private readonly Dictionary<int, CardDescription> _cardDescriptionsDictionary = new();
    private readonly Dictionary<int, DiceDescription> _diceDescriptionsDictionary = new();
    private readonly Dictionary<int, PawnDescription> _pawnDescriptionsDictionary = new();

    const string CARD_DESCRIPTIONS_PATH = "CardDescriptions";
    const string DICE_DESCRIPTIONS_PATH = "DiceDescriptions";
    const string PAWN_DESCRIPTIONS_PATH = "PawnDescriptions";
    
    private void Awake()
    {
        LoadCardDescriptions();
        LoadDiceDescriptions();
        LoadPawnDescriptions();
    }

    private void LoadCardDescriptions()
    {
        CardDescription[] cardDescriptions = Resources.LoadAll<CardDescription>(CARD_DESCRIPTIONS_PATH);
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

    private void LoadDiceDescriptions()
    {
        DiceDescription[] diceDescriptions = Resources.LoadAll<DiceDescription>(DICE_DESCRIPTIONS_PATH);
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

    private void LoadPawnDescriptions()
    {
        PawnDescription[] pawnDescriptions = Resources.LoadAll<PawnDescription>(PAWN_DESCRIPTIONS_PATH);
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
