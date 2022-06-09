using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class AttackButton : MonoBehaviour
{
    public BaisecAttack Attack;

    public void OnClick()
    {
        GameObject.Find("Battel System").GetComponent<BattelSystem>().Input5(Attack);
    }
}
