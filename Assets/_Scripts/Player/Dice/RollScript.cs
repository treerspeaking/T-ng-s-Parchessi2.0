using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollScript : MonoBehaviour
{
    // Start is called before the first frame update
    private int RollDice()
    {
        return Random.Range(1, 6);
    }

    public void OnButtonClick()
    {
        int res = RollDice();
        Debug.Log(res);
    }
}
