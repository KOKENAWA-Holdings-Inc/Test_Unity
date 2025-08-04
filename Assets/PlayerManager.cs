using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public Player player;
    public ResultUI ResultUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player.PlayerHP <= 0)
        {
            // playerのExperienceTotalを引数として渡す
            ResultUI.resultUIonlyPlayer(player.ExperienceTotal);
        }
    }
}
