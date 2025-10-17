using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SC_minion : MonoBehaviour
{
    public int health;
    public int attack;
    public float attack_speed;
    public int speed;

    public GameObject[] enemies_positions;
    private SC_enemy enemy;
    private Rigidbody rb;
    public GameObject damage_popup;

    private Transform aim;
    public float max_distance_from_aim;
    private float timer;

    public GameObject death_fx;

    private bool attacking;

    void Start()
    {
        aim = GameObject.Find("Player").transform;

        rb = GetComponent<Rigidbody>();
        enemies_positions = GameObject.FindGameObjectsWithTag("Enemy");
        InvokeRepeating("checkenemies", 0, 1);
        checkenemies();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1)) 
        {
            attacking = true;
            checkenemies();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (Vector3.Distance(aim.position, transform.position) > max_distance_from_aim)
        {
            Vector3 direction = (aim.transform.position - rb.position).normalized;
            rb.linearVelocity = direction * speed;
        }
        else
        {
            timer += Time.deltaTime;

            if (timer >= attack_speed)
            {
                Attack();
                timer = 0f;
            }
        }
        transform.LookAt(aim.transform);

    }


    void checkenemies()
    {
        if (attacking)
        {
            enemies_positions = enemies_positions.Where(obj => obj).ToArray();

            Transform bestTarget = null;
            float closestDistanceSqr = Mathf.Infinity;
            Vector3 currentPosition = Vector3.zero;

            foreach (GameObject obj in enemies_positions)
            {

                Vector3 directionToTarget = obj.transform.position - currentPosition;
                float dSqrToTarget = directionToTarget.sqrMagnitude;

                if (dSqrToTarget < closestDistanceSqr)
                {
                    closestDistanceSqr = dSqrToTarget;
                    bestTarget = obj.transform;
                }
            }

            aim = bestTarget;
            enemy = aim.GetComponent<SC_enemy>();
        }

    }

    void Attack()
    {
        enemy.takedamage(attack);
    }

    public void takedamage(int dmg)
    {
        SC_damage_popup dmg_ = Instantiate(damage_popup, transform.position, Quaternion.identity).GetComponent<SC_damage_popup>();
        dmg_.text.text = dmg.ToString();
        health = health - dmg;
        if (health> 0)
        {

        }
        else
        {
            die();
        }
    }

    void die()
    {
        Instantiate(death_fx, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
