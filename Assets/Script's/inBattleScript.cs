using UnityEngine;
using UnityEngine.UI;


public class inBattleScript : MonoBehaviour
{
    public enum characterState { Idle, ready, NotReady, ChoseAnAct, fainted, attaking, Attacked, Retering }
    public characterState state;

    Vector3 startPosetion;
    public BattelSystem BS;
    public GameObject Button, turnIndecatre;

    BaisecAttack chosingAttack;
    characterStats player;
    public Slider HealthBar;
    void Start()
    {
        BS = GameObject.Find("Battel System").GetComponent<BattelSystem>();
        startPosetion = transform.position;
        turnIndecatre.GetComponent<SpriteRenderer>().enabled = false;
        player = GetComponent<characterStats>();
        player.Hp = player.MaxHp;
    }

    public void Update()
    {
        HealthBar.value = player.Hp;
        switch (state)
        {
            case (characterState.ready):
                Button.GetComponent<Button>().interactable = true;
                break;

            case (characterState.Idle):
                transform.position = startPosetion;
                Button.GetComponent<Button>().interactable = false;
                break;

            case (characterState.attaking):
                Button.GetComponent<Button>().interactable = false;
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
                    BS.ActCount();
                    state = characterState.Idle;
                }
                break;
        }
    }
    public void ActionSTate()
    {
        state = characterState.ready;
        Debug.Log(gameObject.name + "  Turn!!");
        turnIndecatre.GetComponent<SpriteRenderer>().enabled = true;
    }
    public void onAttackBotten(BaisecAttack atk)
    {
        state = characterState.attaking;
        chosingAttack = atk;
    }
    public void OnHealButton()
    {
        //StartCoroutine(PlayerHeal());
    }
    public void DoDamage()
    {
        float damageOutput = player.Damage + chosingAttack.AttackDamage;
        BS.targetCharacter.GetComponent<EnemyAI>().GetDamage(damageOutput);
    }
    public void GetDamage(float Damage)
    {
        player.Hp -= Damage;
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
}
