using System.Collections;
using System.Collections.Generic;
using _Scripts.Player;
using UnityEngine;
using UnityUtilities;

public class GameResourceManager : SingletonMonoBehaviour<GameResourceManager>
{
    public PlayerTurnController PlayerTurnControllerPrefab;
    public PlayerActionController PlayerActionControllerPrefab;
}
