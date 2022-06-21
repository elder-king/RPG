using UnityEngine;
using UnityEngine.UI;

public class Desctription : MonoBehaviour
{
    public GameObject teaxt;

    public static Desctription Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    public void OnEnter(string des)
    {
        Debug.Log(des);
        teaxt.GetComponent<Text>().text = des;
    }

    public void OnExit()
    {
        teaxt.GetComponent<Text>().text = "";
    }
}
