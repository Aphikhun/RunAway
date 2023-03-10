using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public Dictionary<string,int> Cards = new();
    private int maxStorage = 5;
    private int storage = 0;

    public static PlayerInventory instance;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        //Cards.Clear();
        Cards["speed"] = 0;
        Cards["jump"] = 0;
        Cards["hp"] = 0;
        Cards["fly"] = 1;
        Cards["dash"] = 0;
        Cards["shield"] = 0;

        storage = 0;
    }

    public void GetCard(int random)
    {
        if(storage < maxStorage)
        {
            switch(random)
            {
                case 1:
                    Cards["speed"] += 1;
                    Debug.Log("Get Speed Card");
                    break;
                case 2:
                    Cards["jump"] += 1;
                    Debug.Log("Get Jump Card");
                    break; 
                case 3:
                    Cards["hp"] += 1;
                    Debug.Log("Get Hp Card");
                    break;
                case 4:
                    Cards["fly"] += 1;
                    Debug.Log("Get Fly Card");
                    break;
                case 5:
                    Cards["dash"] += 1;
                    Debug.Log("Get Dash Card");
                    break;
                case 6:
                    Cards["shield"] += 1;
                    Debug.Log("Get Sheild Card");
                    break;
                default:
                    Debug.Log("Get NO Card");
                    break;
            }
        }
        /*
        storage = 0;
        foreach (var card in Cards)
        {
            storage += card.Value;
        }
        */
        storage++;
    }

    public int GetCardAmount(string cardName)
    {
        return Cards[cardName];
    }

    public void UseCard(string cardName)
    {
        Cards[cardName] -= 1;
        storage--;
    }
}
