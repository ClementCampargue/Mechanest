using TMPro;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using System.Collections.Generic;
public class SC_game_master : MonoBehaviour
{
    public bool debug;
    public int money;
    public TextMeshProUGUI current_minions_text;
    public bool fighting;

    public  List<GameObject> minions = new List<GameObject>();
    public float clickCooldown = 0.25f;
    public float clickCooldown_mult = 1;
    private float lastClickTime = 0f;
    public GameObject xp;
    void Start()
    {
        if (debug)
        {
            Debug.unityLogger.logEnabled = true;
        }
        else
        {
            Debug.unityLogger.logEnabled = false; 
        }
    }

    void Update()
    {
        current_minions_text.text = minions.Count.ToString();
        if (Input.GetMouseButton(0))
        {
            if(minions.Count > 50)
            {
                clickCooldown_mult =1.5f;
            }
            if(minions.Count > 100)
            {
                clickCooldown_mult =2;
            }
            if(minions.Count > 200)
            {
                clickCooldown_mult =4;
            }
            if(minions.Count > 300)
            {
                clickCooldown_mult =6;
            }
            if(minions.Count > 400)
            {
                clickCooldown_mult =8;
            }
            if(minions.Count > 500)
            {
                clickCooldown_mult =10;
            }

            if (Time.time - lastClickTime >= clickCooldown/ clickCooldown_mult)
            {
                int i = 1;
                foreach (GameObject gb in minions)
                {
                    SC_minion min = gb.GetComponent<SC_minion>();
                    if (!min.searching && !min.fighting && i != 0)
                    {
                        i--;
                        min.go_to_mouse();
                    }
                }
                lastClickTime = Time.time; 
            }


        }
        if (Input.GetMouseButton(2))
        {
            foreach (GameObject gb in minions)
            {
                SC_minion min = gb.GetComponent<SC_minion>();
                min.go_to_mouse();
            }
        }
    }


    public void begin_wave()
    {
        fighting = true;
    }
    public void end_wave()
    {
        fighting = false;
        foreach (GameObject gb in minions)
        {
            Destroy(gb);
            Instantiate(xp, gb.transform.position, Quaternion.identity);
            money++;
        }
    }
}
