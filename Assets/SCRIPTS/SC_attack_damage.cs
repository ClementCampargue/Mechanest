using UnityEngine;

public class SC_attack_damage : MonoBehaviour
{
    public bool player_attack;
    public bool enemy_attack;
    public int damage;


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy" && player_attack)
        {
            other.GetComponent<SC_enemy>().takedamage(damage);
        }
        if (other.tag == "Loot" && player_attack)
        {
            other.GetComponent<SC_chest>().takedamage(damage);
        }
        if (other.tag == "Minion" && enemy_attack)
        {
            other.GetComponent<SC_minion>().takedamage(damage);
        }
        
    }
}
