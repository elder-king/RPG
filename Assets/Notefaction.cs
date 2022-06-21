using System;
using UnityEngine;
using UnityEngine.UI;

//Notefaction.Instance.Notefy("doset work?", () => { ; Notefaction.Instance.Hide();}, () => { Notefaction.Instance.Hide();});
public class Notefaction : MonoBehaviour
{
    public Text text;
    public Button yesButten;
    public Button NoButten;
    public GameObject bacround;

    public static Notefaction Instance { get; private set; }
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
        gameObject.SetActive(true);
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            gameObject.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            gameObject.SetActive(false);
        }
    }
    public void Notefy(string note, Action YesBTN, Action NoBTN)
    {
        bacround.SetActive(true);
        text.text = note;
        yesButten.onClick.AddListener(new UnityEngine.Events.UnityAction(YesBTN));
        NoButten.onClick.AddListener(new UnityEngine.Events.UnityAction(NoBTN));
        gameObject.SetActive(true);

    }
    public void Hide() => bacround.SetActive(false);
}
