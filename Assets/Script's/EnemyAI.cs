using System.Collections;
using UnityEngine;
using UnityEngine.UI;
[System.Obsolete]
public class EnemyAI : MonoBehaviour
{
    public enum characterState { Idle, ready, NotReady, ChoseAnAct, fainted, attaking, Attacked, Selected, Retering }

    characterStats Enemy;
    public BattelSystem BS;
    public characterState state;
    public GameObject turnIndecatre;
    public GameObject Button;
    public Slider HealthBar;
    Vector3 startPosetion;
    GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        Enemy = GetComponent<characterStats>();
        Enemy.Hp = Enemy.MaxHp;
        turnIndecatre.gameObject.SetActive(false);
        HealthBar.maxValue = Enemy.MaxHp;
        HealthBar.value = Enemy.Hp;
        startPosetion = transform.position;
    }
    public void Update()
    {
        switch (state)
        {
            case (characterState.attaking):
                if (target != null)
                {

                    if (Vector3.Distance(getPos(), target.transform.position) > 1f)
                    {
                        transform.position -= (transform.position - target.transform.position) * 4 * Time.deltaTime;
                    }
                    else
                    {
                        DoDamage(target);
                        state = characterState.Retering;
                    }
                }
                else
                {
                    state = characterState.Idle;
                }
                break;
            case (characterState.Retering):
                if (Vector3.Distance(getPos(), startPosetion) > 0.2f)
                {
                    transform.position -= (transform.position - startPosetion) * 4 * Time.deltaTime;
                }
                else
                {
                    BS.EnemyNextTurn();
                    transform.position = startPosetion;
                    state = characterState.Idle;
                    turnIndecatre.gameObject.SetActive(false);
                }
                break;
        }
    }

    public void ActionSTate()
    {
        if (BS.PlayerTeamInBattel.Count > 0)
            target = BS.PlayerTeamInBattel[Random.Range(0, BS.PlayerTeamInBattel.Count)];
        turnIndecatre.gameObject.SetActive(true);
        state = characterState.attaking;
    }

    public IEnumerator OnHeal()
    {
        if (state == characterState.ready)
        {
            Debug.Log(gameObject.name + " is Healing!!");
            yield return new WaitForSeconds(2f);
            Enemy.Hp += Enemy.HealAmount;
            Debug.Log(gameObject.name + " just healed  ");
            state = characterState.Idle;
            GameEvent.current.EnemyNexTturnSwitch();
            turnIndecatre.gameObject.SetActive(false);
            HealthBar.value = Enemy.Hp;
        }
    }

    public void GetDamage(float damage)
    {
        Enemy.Hp -= damage;
        Debug.Log(gameObject.name + " Gat Attact");
        if (Enemy.Hp <= 0)
        {
            Destroy(Button);
            Destroy(gameObject);
        }
        HealthBar.value = Enemy.Hp;
    }
    void DoDamage(GameObject target)
    {
        BaisecAttack atk;
        float damage;
        if (Enemy.AttacksList.Count > 0) { atk = Enemy.AttacksList[Random.Range(0, Enemy.AttacksList.Count)]; }
        else
        {
            atk = null;
        }
        if (atk != null)
        {
            damage = Enemy.Damage + atk.AttackDamage;
        }
        else
        {
            damage = Enemy.Damage;
        }
        target.GetComponent<inBattleScript>().GetDamage(damage);
    }
    public Vector3 getPos()
    {
        return transform.position;
    }
    private void OnDestroy()
    {
        BS.EnemyTeamInBattel.Remove(gameObject);
    }
}
