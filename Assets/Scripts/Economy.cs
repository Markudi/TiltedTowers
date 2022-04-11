using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Economy : MonoBehaviour
{

    public TextMeshProUGUI coinsText;
    public TextMeshProUGUI livesText;

    public int startPlayerCoins = 20;
    public int startPlayerLives = 5;
    
    public static int playerCoins = 20;
    public static int playerLives = 5;
    
    // Start is called before the first frame update
    void Start()
    {
        playerCoins = startPlayerCoins;
        playerLives = startPlayerLives;
            
        coinsText.text = $"Coins:{playerCoins}";
        livesText.text = $"Lives:{playerLives}";
    }

    // Update is called once per frame
    void Update()
    {
        coinsText.text = $"Coins:{playerCoins}";
        livesText.text = $"Lives:{playerLives}";
    }
}
