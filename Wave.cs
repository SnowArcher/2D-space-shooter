using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ShootingSettings
{
    //Chance of a shot enemy in the form of a slider
    [Range(0, 100)]
    public int shot_Chance;
    //time interval within which the shot occurs.
    public float shot_Time_Min, shot_Time_Max;
}

public class Wave : MonoBehaviour
{
    // Reference to the ShootingSettings.
    public ShootingSettings shooting_Settings;
    [Space]
    // The enemy prefab to be spawned.
    public GameObject obj_Enemy;
    //Amount of enemies in one wave.
    public int count_in_Wave;
    // The speed at which the enemy moves.
    public float speed_Enemy;
    //time between spawn enemy in wave
    public float time_Spawn;
    //An array of waypoints along which the enemy moves in a wave.
    public Transform[] path_Points;
    //Destroy the surviving enemies at the end of the path or send them to the beginning of the path.
    public bool is_return;

    //Test Wave
    //an infinite wave appears every 5 seconds to debug the wave
    [Header("Test wave!")]
    public bool is_Test_Wave;

    private FollowThePath follow_Component;
    private Enemy enemy_Component_Script;

    private void Start()
    {
        // Start function CreateEnemyWave as a coroutine.
        StartCoroutine(CreateEnemyWave());
    }

    IEnumerator CreateEnemyWave()
    {
        //Ceate enemies...
        for (int i = 0; i < count_in_Wave; i++)
        {
            // Create an instance of the prefab obj_Enemy in the obj_Enemy position and without rotation.
            GameObject new_enemy = Instantiate(obj_Enemy, obj_Enemy.transform.position, Quaternion.identity);

            // Try and find an FollowThePath script on the gameobject new_enemy.
            follow_Component = new_enemy.GetComponent<FollowThePath>();
            // Specify the path that will move the new_enemy
            follow_Component.path_Points = path_Points;
            // Specify the speed with which the new enemy will move
            follow_Component.speed_Enemy = speed_Enemy;
            //Destroy the surviving enemies at the end of the path or send them to the beginning of the path.
            follow_Component.is_return = is_return;

            // Try and find an Enemy script on the gameobject new_enemy.
            enemy_Component_Script = new_enemy.GetComponent<Enemy>();
            // Specify shot chance a new enemy.
            enemy_Component_Script.shot_Chance = shooting_Settings.shot_Chance;
            // Specify time interval within which the shot occurs.
            enemy_Component_Script.shot_Time_Min = shooting_Settings.shot_Time_Min;
            enemy_Component_Script.shot_Time_Max = shooting_Settings.shot_Time_Max;

            new_enemy.SetActive(true);
            // Every time_Spawn seconds
            yield return new WaitForSeconds(time_Spawn);
        }
        // If wave test
        if (is_Test_Wave)
        {
            //Infinitely generate the current wave every 5 seconds
            yield return new WaitForSeconds(5f);
            StartCoroutine(CreateEnemyWave());
        }

        // If is_return = false destroy the enemy at the end of the path
        if (!is_return)
            Destroy(gameObject);
    }
    // To make it easier to set up enemy waypoints, connect them with a line
    void OnDrawGizmos()
    {
        NewPositionByPath(path_Points);
    }
    void NewPositionByPath(Transform[] path)
    {
        Vector3[] path_Positions = new Vector3[path.Length];
        for (int i = 0; i < path.Length; i++)
        {
            path_Positions[i] = path[i].position;
        }
        for (int i = 0; i < 3; i++)
        {
            path_Positions = Smoothing(path_Positions);
        }
        for (int i = 0; i < path_Positions.Length - 1; i++)
        {
            Gizmos.DrawLine(path_Positions[i], path_Positions[i + 1]);
        }
    }
    Vector3[] Smoothing(Vector3[] path_Positions)
    {
        Vector3[] new_Path_Positions = new Vector3[(path_Positions.Length - 2) * 2 + 2];
        new_Path_Positions[0] = path_Positions[0];
        new_Path_Positions[new_Path_Positions.Length - 1] = path_Positions[path_Positions.Length - 1];

        int j = 1;
        for (int i = 0; i < path_Positions.Length - 2; i++)
        {
            new_Path_Positions[j] = path_Positions[i] + (path_Positions[i + 1] - path_Positions[i]) * 0.75f;
            new_Path_Positions[j + 1] = path_Positions[i + 1] + (path_Positions[i + 2] - path_Positions[i + 1]) * 0.25f;
            j += 2;
        }
        return new_Path_Positions;
    }
}
