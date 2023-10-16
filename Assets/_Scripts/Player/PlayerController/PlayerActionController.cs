using _Scripts.NetworkContainter;
using Unity.Collections;
using Unity.Netcode;

public class PlayerActionController : PlayerControllerDependency
{
    public NativeList<CardContainer> DeckCards = new ();
    public NativeList<CardContainer> HandCards = new ();
    public NativeList<CardContainer> DiscardCards = new ();
    
    [ClientRpc]
    public void AddCardToHandClientRPC(CardContainer cardID)
    {
        DeckCards.Add(cardID);
    }

    [ServerRpc]
    public void DrawCardAtTopServerRPC()
    {
        DrawCardAtTopClientRPC();
    }

    [ClientRpc]
    public void DrawCardAtTopClientRPC()
    {
        var topCard = DeckCards.ElementAt(0);
        DeckCards.RemoveAt(0);
        HandCards.Add(topCard);
    }

    [ClientRpc]
    public void DiscardCardClientRPC(CardContainer cardID)
    {
        DiscardCards.Add(cardID);
    }
    
    [ClientRpc]
    public void RemoveCardFromHandClientRPC(CardContainer cardID)
    {
        
    }
}
