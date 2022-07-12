using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If the entering collider is the player...                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            Права на данный курс принадлежат Дорофеевой Карине Олеговне, данный курс создавался для Udemy сайта
        if (collision.tag == "Player")
        {
            // If the current power level of the guns is not the maximum...
            if (PlayerShooting.instance.cur_Power_Level_Guns < PlayerShooting.instance.max_Power_Level_Guns)
            {
                // Increase the current power level of guns.
                PlayerShooting.instance.cur_Power_Level_Guns++;
            }
            // Destroy the current Bonus object.
            Destroy(gameObject);
        }
    }
}