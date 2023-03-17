using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public Dictionary<string,int> Cards = new();
    public int maxStorage = 15;
    public int storage = 0;

    public static PlayerInventory instance;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        Cards.Clear();
        Cards["speed"] = 0;
        Cards["jump"] = 0;
        Cards["hp"] = 0;
        Cards["fly"] = 0;
        Cards["dash"] = 0;
        Cards["shield"] = 0;

    }
    private void Update()
    {
        storage = Cards["speed"] + Cards["jump"] + Cards["hp"] + Cards["fly"] + Cards["dash"] + Cards["shield"];
    }

    public void GetCard(int random)
    {
        if(storage < maxStorage)
        {
            switch(random)
            {
                case 1:
                    Cards["speed"]++;
                    Debug.Log("Get Speed Card");
                    break;
                case 2:
                    Cards["jump"]++;
                    Debug.Log("Get Jump Card");
                    break; 
                case 3:
                    Cards["hp"]++;
                    Debug.Log("Get Hp Card");
                    break;
                case 4:
                    Cards["fly"]++;
                    Debug.Log("Get Fly Card");
                    break;
                case 5:
                    Cards["dash"]++;
                    Debug.Log("Get Dash Card");
                    break;
                case 6:
                    Cards["shield"]++;
                    Debug.Log("Get Sheild Card");
                    break;
                default:
                    Debug.Log("Get NO Card");
                    break;
            }
        }
        else
        {
            Debug.Log("Max");
        }
        
        storage = 0;
        foreach (var card in Cards)
        {
            storage += card.Value;
        }
       
    }

    public int GetCardAmount(string cardName)
    {
        return Cards[cardName];
    }

    public void UseCard(string cardName)
    {
        Cards[cardName]--;
        //storage--;
    }
}
