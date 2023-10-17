using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.Scriptable_Objects;
using UnityEditor;
using UnityEngine;
using UnityUtilities;
using Object = UnityEngine.Object;

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
        // Use the AssetDatabase to load all assets at the specified folder path.
        string[] assetPaths = AssetDatabase.FindAssets("", new[] { _cardDescriptionAssetPath });

        foreach (string assetPath in assetPaths)
        {
            Object loadedAsset =
                AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(assetPath), (typeof(CardDescription)));
            if (loadedAsset == null) continue;

            var cardDescription = (CardDescription)loadedAsset;
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
        // Use the AssetDatabase to load all assets at the specified folder path.
        string[] assetPaths = AssetDatabase.FindAssets("", new[] { _diceDescriptionAssetPath });

        foreach (string assetPath in assetPaths)
        {
            Object loadedAsset =
                AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(assetPath), (typeof(DiceDescription)));
            if (loadedAsset == null) continue;

            var diceDescription = (DiceDescription)loadedAsset;
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
        // Use the AssetDatabase to load all assets at the specified folder path.
        string[] assetPaths = AssetDatabase.FindAssets("", new[] { _pawnDescriptionAssetPath });

        foreach (string assetPath in assetPaths)
        {
            Object loadedAsset =
                AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(assetPath), (typeof(PawnDescription)));
            if (loadedAsset == null) continue;

            var pawnDescription = (PawnDescription)loadedAsset;
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
