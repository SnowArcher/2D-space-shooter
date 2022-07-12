using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // The damage inflicted by each bullet.
    public int damage;
    // Is the bullet of an enemy or player.
    public bool is_Enemy_Bullet;

    // Method destruction bullet.
    private void Destruction()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        // If the bullet belongs to an enemy and collides with a player...
        if (is_Enemy_Bullet && coll.tag == "Player")
        {
            // Call the Player for the method of taking damage and deal damage to him.                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                  //Права на данный курс принадлежат Дорофеевой Карине Олеговне, данный курс создавался для Udemy сайта
            Player.instance.GetDamage(damage);
            // Destruction bullet.
            Destruction();
        }
        // If the bullet belongs to the player and collides with the enemy ...
        else if (!is_Enemy_Bullet && coll.tag == "Enemy")
        {
            // At the collider we find the Enemy component and call the method for taking damage
            coll.GetComponent<Enemy>().GetDamage(damage);
            // Destruction bullet.
            Destruction();
        }
        // If the bullet belongs to the enemy and collides with the player's shield ...
        else if (is_Enemy_Bullet && coll.tag == "Shield")
        {
            // Call the Player for the method of taking damage shield and deal damage to him.
            Player.instance.GetDamageShield(damage);
            // Destruction bullet.
            Destruction();
        }
    }
}
