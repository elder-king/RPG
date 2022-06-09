using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BattelSystem : MonoBehaviour
{
    //state's of the battle
    public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }
    public enum TeamsTurn { playerTurn, EnemyTurn }
    public TeamsTurn currentTurn = TeamsTurn.playerTurn;
    public enum PlayerGUI { Activat, Waiting, Input1, Input2, Done }
    PlayerGUI PlayerInput;
    public BattleState state;

    //The List Of Players And Enemy's And Items
    public List<GameObject> PlayerTeamInBattel = new List<GameObject>();
    public int PlyerTurnID = 0;

    public List<GameObject> EnemyTeamInBattel = new List<GameObject>();
    public int EnemyTurnID = 0;

    public List<GameObject> ATB = new List<GameObject>();

    public Transform playerBattleStation, enemyBattleStation;
    public GameObject PTM, ETM;
    bool teamPlayerTurn = false;

    // UI stuff!
    public Text dialogueText;
    public Transform EnemySpaicer, PlayerSpaicer, AttackListSpacer, ItemSpacer;
    public GameObject ActionPanel, EnemyButton, PlayerButton, ItemButton, AttackButton, EnemyHealthBar, EndOfBattel, ChoseActionPanel, ItemPanel;
    public static BattelSystem Instance;

    //current action data
    [HideInInspector]
    public Inventory inventory;

    [HideInInspector]
    public GameObject CharacterInTurn;

    [HideInInspector]
    public GameObject targetCharacter;

    [HideInInspector]
    public Item ChosinItem;

    [HideInInspector]
    public BaisecAttack AttackType;

    [HideInInspector]
    public BaisecAttack ChoseenAttack;

    private void Awake()
    {
        Instance = this;
        Inventory.Instance.call();
        PlayerTeamInBattel.AddRange(GameObject.FindGameObjectsWithTag("Player"));
        EnemyTeamInBattel.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));

        //GameEvent.current.switchTurn += NextTurn;
        EnemySelectBotten();
        PlayerSelectButton();
        ItemSelectButton();
        SwitchTurns();
    }
    public void SwitchTurns()
    {
        teamPlayerTurn = !teamPlayerTurn;
        if (teamPlayerTurn == true)
        {
            currentTurn = TeamsTurn.playerTurn;
            foreach (GameObject player in PlayerTeamInBattel)
            {
                player.GetComponent<inBattleScript>().ActionSTate();
            }
        }
        if (teamPlayerTurn == false)
        {
            EnemyNextTurn();
            currentTurn = TeamsTurn.EnemyTurn;
        }
    }
    public void ActCount()
    {
        PlyerTurnID += 1;

        if (PlyerTurnID == PlayerTeamInBattel.Count)
        {
            SwitchTurns();
            PlyerTurnID = 0;
        }
    }
    public void EnemyNextTurn()
    {
        if (EnemyTurnID < EnemyTeamInBattel.Count)
        {
            EnemyTeamInBattel[EnemyTurnID].GetComponent<EnemyAI>().ActionSTate();
            EnemyTurnID++;
        }
        else
        {
            EnemyTurnID = 0;
            SwitchTurns();
        }
    }
    void EnemySelectBotten()
    {
        foreach (GameObject Enemy in EnemyTeamInBattel)
        {
            GameObject newButton = Instantiate(EnemyButton) as GameObject;
            GameObject NewHealthBar = Instantiate(EnemyHealthBar) as GameObject;

            newButton.transform.SetParent(EnemySpaicer, false);
            NewHealthBar.transform.SetParent(Enemy.transform, false);

            NewHealthBar.transform.position = new Vector2(NewHealthBar.transform.position.x, NewHealthBar.transform.position.y + 1);
            Slider Bar = NewHealthBar.transform.Find("Slider").GetComponent<Slider>();

            Enemy.GetComponent<EnemyAI>().HealthBar = Bar;

            Enemy.GetComponent<EnemyAI>().Button = newButton;
            EnemySelectButton butten = newButton.GetComponent<EnemySelectButton>();
            characterStats CurrentEnemy = Enemy.GetComponent<characterStats>();

            Text buttonText = newButton.transform.Find("Text").gameObject.GetComponent<Text>();
            buttonText.text = CurrentEnemy.unitName;

            butten.EnemyPrefap = Enemy;
            newButton.transform.SetParent(EnemySpaicer, false);
        }
    }
    void PlayerSelectButton()
    {
        foreach (GameObject Character in PlayerTeamInBattel)
        {
            GameObject newButton = Instantiate(PlayerButton) as GameObject;
            PlayerSelecter butten = newButton.GetComponent<PlayerSelecter>();
            characterStats CurrentPlayer = Character.GetComponent<characterStats>();
            Character.GetComponent<inBattleScript>().Button = newButton;
            Text buttonText = newButton.transform.Find("Text").gameObject.GetComponent<Text>();
            buttonText.text = CurrentPlayer.unitName;
            Slider Health = newButton.transform.Find("health bar").gameObject.GetComponent<Slider>();
            Character.GetComponent<inBattleScript>().HealthBar = Health;

            butten.PlayerPrefap = Character;
            newButton.transform.SetParent(PlayerSpaicer, false);
        }
    }
    public void ItemSelectButton()
    {
        foreach (Transform child in ItemSpacer)
        {
            Destroy(child.gameObject);
        }

        foreach (Item Chosinitem in inventory.itemList)
        {
            GameObject Button = Instantiate(ItemButton) as GameObject;
            Button.GetComponent<ItemButtonScript>().item = Chosinitem;
            Text text = Button.transform.Find("Text").GetComponent<Text>();
            text.text = Chosinitem.ItemName;
            Button.transform.SetParent(ItemSpacer, false);
        }
    }
    public void Input1(GameObject Character)
    {
        CharacterInTurn = Character;
        ChoseActionPanel.gameObject.SetActive(true);
    }
    public void Input2(GameObject Target)
    {
        targetCharacter = Target;

        Vector2 CharacterInTurn = new Vector2(Target.transform.position.x - 1.5f, Target.transform.position.y);
        Target = targetCharacter;
        Input3();
        AttackListSpacer.parent.gameObject.SetActive(true);

    }
    public void Input3()
    {
        foreach (BaisecAttack attack in CharacterInTurn.GetComponent<characterStats>().AttacksList)
        {
            GameObject newButton = Instantiate(AttackButton) as GameObject;

            Text attacktext = newButton.transform.Find("Text").gameObject.GetComponent<Text>();
            attacktext.text = attack.AttackName + " ATK" + attack.AttackDamage;
            AttackButton butten = newButton.GetComponent<AttackButton>();
            butten.Attack = attack;
            newButton.transform.SetParent(AttackListSpacer, false);
            ATB.Add(newButton);
        }
    }

    public void Input4(Item item)
    {
        ChosinItem = item;
        CharacterInTurn.GetComponent<inBattleScript>().state = inBattleScript.characterState.UsingItam;
        ItemPanel.SetActive(false);
    }
    public void Input5(BaisecAttack MoveType)
    {
        ChoseenAttack = MoveType;
        CharacterInTurn.GetComponent<inBattleScript>().onAttackBotten(ChoseenAttack);
        execute();
    }
    void execute()
    {
        foreach (GameObject Button in ATB) { Destroy(Button); }
        ATB.Clear();
        EnemySpaicer.transform.parent.gameObject.SetActive(false);
        AttackListSpacer.parent.gameObject.SetActive(false);
    }
    private void Update()
    {
        if (PlayerTeamInBattel.Count == 0)
        {
            state = BattleState.LOST;
        }
        if (EnemyTeamInBattel.Count == 0)
        {
            state = BattleState.WON;
        }
        switch (state)
        {
            case (BattleState.WON):
                Text text = EndOfBattel.transform.Find("text").GetComponent<Text>();
                EndOfBattel.SetActive(true);
                if (text != null) text.text = "YOU WON!";
                break;
            case (BattleState.LOST):
                Text LOSEtext = EndOfBattel.transform.Find("text").GetComponent<Text>();
                EndOfBattel.SetActive(true);
                if (LOSEtext != null) LOSEtext.text = "YOU LOST";
                break;
        }
    }
}
