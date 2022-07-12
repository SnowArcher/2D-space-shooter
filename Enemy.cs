using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Enemy health.
    public int enemy_Health;
    // The amount added to the player's score when the enemy dies.
    public int score_Value;

    [Space]
    // The Bullet prefab to be spawned.
    public GameObject obj_Bullet;
    //time interval within which the shot occurs.
    public float shot_Time_Min, shot_Time_Max;
    //the probability of the enemy's shot
    public int shot_Chance;
    // The Explosion prefab to be spawned.
    public GameObject obj_Explosion;

    [Header("BOSS")]
    // The current object is the boss.
    public bool is_Boss;
    //The bullet prefab extra boss shot
    public GameObject obj_Bullet_Boss;
    // How long between each bullet Boss spawn.
    public float time_Bullet_Boss_Spawn;
    // Timer to determine when to shoot.
    private float _timer_Shot_Boss;
    //the probability of the Boss shot
    public int shot_Chance_Boss;
    //Права на данный курс принадлежат Дорофеевой Карине Олеговне, данный курс создавался для Udemy сайта
    private void Start()
    {
        // If the current object is not the boss, make only one shot
        if (!is_Boss)
        {
            // Call the OpenFire in the time interval between shot_Time_Min and shot_Time_Max
            Invoke("OpenFire", Random.Range(shot_Time_Min, shot_Time_Max));
        }
    }

    private void Update()
    {
        // If the current object is the boss
        if (is_Boss)
        {
            //every timer_Shot_Boss seconds we call the method:
            //OpenFire;
            //OpenFireBoss.
            if (Time.time > _timer_Shot_Boss)
            {
                _timer_Shot_Boss = Time.time + time_Bullet_Boss_Spawn;
                OpenFire();
                OpenFireBoss();
            }
        }
    }
    //Method Open fire Boss
    private void OpenFireBoss()
    {
        //if random value less than shot chance, making a extra boss shot
        if (Random.value < (float)shot_Chance_Boss / 100)
        {
            // Shoot a few bullets like a fan
            for (int zZz = -40; zZz < 40; zZz += 10)
            {
                // Create an instance of the prefab obj_Bullet_Boss in the boss position and 
                // rotates zZz degrees around the z axis
                Instantiate(obj_Bullet_Boss, transform.position, Quaternion.Euler(0, 0, zZz));
            }
        }
    }
    //Method Open fire
    private void OpenFire()
    {
        //if random value less than shot chance, making a shot
        if (Random.value < (float)shot_Chance / 100)
        {
            // Create an instance of the prefab obj_Bullet in the enemy position and without rotation.
            Instantiate(obj_Bullet, transform.position, Quaternion.identity);
        }
    }

    // Method of taking damage by the enemy
    public void GetDamage(int damage)
    {
        // Reduce the health by the damage amount.
        enemy_Health -= damage;
        // If the enemy does not have a health...
        if (enemy_Health <= 0)
        {
            // Call the enemy destruction method
            Destruction();
        }
    }
    // Method destruction enemy.
    private void Destruction()
    {
        LevelController.instance.ScoreInGame(score_Value);
        // Destroy the current player object.
        Destroy(gameObject);
        // Create an instance of the prefab obj_Explosion in the enemy position and without rotation.
        Instantiate(obj_Explosion, transform.position, Quaternion.identity);
    }
    //if enemy collides player, Player gets the damage. 
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Player")
        {
            GetDamage(1);
            Player.instance.GetDamage(1);
        }
        if (coll.tag == "Shield")
        {
            GetDamage(1);
            Player.instance.GetDamageShield(1);
        }
    }
}
