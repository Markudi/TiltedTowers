using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Economy : MonoBehaviour
{

    public TextMeshProUGUI coinsText;

    public static int playerCoins = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        coinsText.text = $"Coins:{playerCoins}";
    }

    // Update is called once per frame
    void Update()
    {
        coinsText.text = $"Coins:{playerCoins}";
    }
}
