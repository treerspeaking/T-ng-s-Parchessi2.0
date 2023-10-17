using System;
using _Scripts.NetworkContainter;
using _Scripts.Player;
using Shun_Unity_Editor;
using Unity.Netcode;

public class PlayerResourceController : NetworkBehaviour
{
    
    public NetworkList<CardContainer> DeckCards = new();
    public NetworkList<CardContainer> HandCards = new();
    public NetworkList<CardContainer> DiscardCards = new();
    
    public NetworkList<DiceContainer> IncomeDices = new();
    public NetworkList<DiceContainer> CurrentTurnDices = new();

    private PlayerDiceHand _playerDiceHand;
    private PlayerCardHand _playerCardHand;

    public override void OnNetworkSpawn()
    {
        if (!IsOwner) return;
        
        _playerDiceHand = FindObjectOfType<PlayerDiceHand>();
        _playerCardHand = FindObjectOfType<PlayerCardHand>();

    }

    [ServerRpc(RequireOwnership = false)]
    public void AddIncomeServerRPC(DiceContainer diceContainer)
    {
        IncomeDices.Add(diceContainer);
    }
    
    [ServerRpc]
    public void GainIncomeServerRPC()
    {
        foreach(var diceContainer in IncomeDices)
        {
            CurrentTurnDices.Add(diceContainer);
        }
        
        GainIncomeClientRPC();
    }

    [ClientRpc]
    private void GainIncomeClientRPC()
    {
        foreach (DiceContainer diceContainer in CurrentTurnDices)
        {
            _playerDiceHand.AddDiceToHand(diceContainer);
        }
    }


    [ServerRpc]
    public void AddDiceServerRPC(DiceContainer diceContainer)
    {
        CurrentTurnDices.Add(diceContainer);
    }

    [ServerRpc]
    public void RemoveBackDiceServerRPC()
    {
        CurrentTurnDices.RemoveAt(CurrentTurnDices.Count - 1);    
    }


}