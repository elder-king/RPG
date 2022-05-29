using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class EnemySelectButton : MonoBehaviour
{
    public GameObject EnemyPrefap;

    public bool Selecter ;

    public void EnemySelect()
    {
        GameObject.Find("Battel System").GetComponent<BattelSystem>().Input2(EnemyPrefap);
        
    }
    [System.Obsolete]
    public void OnMouseHuvir()
    {
        if (Selecter == false)
        {
            EnemyPrefap.transform.FindChild("turn indcuter").gameObject.SetActive(true);
            Selecter = !Selecter;
        }
    }
    [System.Obsolete]
    public void OnMouseExit()
    {
        if (Selecter == true)
        {
            EnemyPrefap.transform.FindChild("turn indcuter").gameObject.SetActive(false);
            Selecter = !Selecter;
        }
    }

}
