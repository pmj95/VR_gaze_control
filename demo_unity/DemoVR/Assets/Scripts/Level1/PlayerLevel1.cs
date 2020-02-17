using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLevel1 : BasePlayer
{
    private GameLevel1 game;

    protected override void DoAwake()
    {
        base.DoAwake();
        this.game = GameLevel1.getInstance();
        this.game.startGame();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
