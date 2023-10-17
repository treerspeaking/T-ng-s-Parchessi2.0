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
        for (int i = 0; i < CurrentTurnDices.Count; i++)
        {
            
            _playerDiceHand.AddDiceToHand(CurrentTurnDices[i], i);
        }
        
    }
    
    [ServerRpc]
    public void RemoveDiceServerRPC(int index)
    {
        CurrentTurnDices.RemoveAt(index);
    }


}