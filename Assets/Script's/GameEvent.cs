using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameEvent : MonoBehaviour
{
    public static GameEvent current;

    private void Awake()
    {
        current = this;
    }

    public event Action switchTurn;
    public void PlayerNexTturnSwitch()
    {
        if (switchTurn != null)
        {
            switchTurn();
        }
    }

    public event Action EnemyswitchTurn;
    public void EnemyNexTturnSwitch()
    {
        if (EnemyswitchTurn != null)
        {
            EnemyswitchTurn();
        }
    }
}
