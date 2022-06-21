using UnityEngine;
using UnityEngine.UI;


public class inBattleScript : MonoBehaviour
{
    public enum characterState { Idle, ready, NotReady, ChoseAnAct, fainted, attaking, Attacked, Retering, UsingItam }
    public characterState state;

    Vector3 startPosetion;
    public BattelSystem BS;
    public IUseItem UseItem;
    public GameObject Button, turnIndecatre;

    BaisecAttack chosingAttack;
    characterStats player;
    public Slider HealthBar;

    private void Awake()
    {
        gameObject.AddComponent<IUseItem>();
        UseItem = GetComponent<IUseItem>();
    }
    void Start()
    {
        BS = BattelSystem.Instance;
        startPosetion = transform.position;
        turnIndecatre.GetComponent<SpriteRenderer>().enabled = false;
        player = GetComponent<characterStats>();
        player.Hp = player.MaxHp;
    }

    public void Update()
    {
        // state machine 
        HealthBar.value = player.Hp;
        switch (state)
        {
            case (characterState.ready):
                Button.GetComponent<Button>().interactable = true;
                break;

            case (characterState.Idle):
                transform.position = startPosetion;
                break;

            case (characterState.attaking):
                if (BS.targetCharacter != null)
                {
                    GameObject Enemy = BS.targetCharacter;
                    if (Vector3.Distance(getPos(), Enemy.transform.position) > 1f)
                    {
                        transform.position -= (transform.position - Enemy.transform.position) * 4 * Time.deltaTime;
                    }
                    else
                    {
                        DoDamage();
                        state = characterState.Retering;
                    }

                }
                break;
            case (characterState.Retering):
                if (Vector3.Distance(getPos(), startPosetion) > 0.2f)
                {
                    transform.position -= (transform.position - startPosetion) * 4 * Time.deltaTime;
                }
                else
                {
                    state = characterState.Idle;
                    BattelSystem.Instance.ActCount();
                }
                break;
            case (characterState.UsingItam):
                UseItem.Use();
                state = characterState.Idle;
                break;
        }
    }
    public void ActionSTate()
    {
        state = characterState.ready;
        turnIndecatre.GetComponent<SpriteRenderer>().enabled = true;
    }
    public void onAttackBotten(BaisecAttack atk)
    {
        state = characterState.attaking;
        chosingAttack = atk;
    }
    public void OnHealButton(float amount)
    {
        player.Hp += amount;
        Debug.Log(player.unitName + " Hp Incresed by " + amount);
    }
    public void DoDamage()
    {
        float damageOutput = player.Damage + chosingAttack.AttackDamage;
        BS.targetCharacter.GetComponent<EnemyAI>().GetDamage(damageOutput);
    }
    public void GetDamage(float Damage)
    {
        float DamageOutbot = Damage - player.Defince / 3;
        player.Hp -= DamageOutbot;
        Debug.Log(DamageOutbot);
        if (player.Hp <= 0)
        {
            BS.PlayerTeamInBattel.Remove(gameObject);
            Destroy(Button);
            Destroy(gameObject);
        }
    }
    public Vector3 getPos()
    {
        return transform.position;
    }
    public void EndTurn() => Button.GetComponent<Button>().interactable = false;
}
