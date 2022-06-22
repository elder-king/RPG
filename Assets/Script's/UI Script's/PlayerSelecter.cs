
using UnityEngine;

public class PlayerSelecter : MonoBehaviour
{
    public GameObject PlayerPrefap;

    [System.Obsolete]
    public void PlayerSelect()
    {
        GameObject.Find("Battel System").GetComponent<BattelSystem>().Input1(PlayerPrefap);
    }
}