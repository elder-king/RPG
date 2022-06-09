using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;

public class characterStats : MonoBehaviour
{
    public string unitName;

    public float Hp, MaxHp = 100, HealAmount = 20, Level = 0, currentXp = 0, Damage = 30, Defince = 30;

    public Slider HPSlider;

    public List<BaisecAttack> AttacksList = new List<BaisecAttack>();

    public enum characterTeam {playerTeam ,EnemyTeam}

   
    public characterTeam Team;
    public bool OnTheBattel;

    void Start()
    { 
        Hp = MaxHp;
    }
    
    public void TakeDamage(float damage)
    {
        Hp -= Damage;
        if( HPSlider != null) HPSlider.value = Hp;
    }
    public void Heal(int amount)
    {
        Hp += amount;
        if (Hp > MaxHp)
            Hp = MaxHp;
    }
}
