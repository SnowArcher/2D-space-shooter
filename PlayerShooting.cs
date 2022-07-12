using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Guns
{
    public GameObject obj_Central_Gun, obj_Right_Gun, obj_Left_Gun;
    public ParticleSystem ps_Central_Gun, ps_Left_Gun, ps_Right_Gun;
}

public class PlayerShooting : MonoBehaviour
{
    // Static reference to the PlayerShooting (can be used in other scripts).
    public static PlayerShooting instance;
    // Reference to the Guns.
    public Guns guns;
    //Maximum power level of guns
    [HideInInspector]
    public int max_Power_Level_Guns = 5;
    // The Bullet prefab to be spawned.
    public GameObject obj_Bullet;
    // How long between each bullet spawn.
    public float time_Bullet_Spawn = 0.3f;
    // Timer to determine when to shoot.
    [HideInInspector]
    public float timer_Shot;
    //Current level of weapon strength through the slider
    [Range(1, 5)]
    public int cur_Power_Level_Guns = 1;

    private void Awake()
    {
        // Setting up the references.
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        // Try and find an ParticleSystem Component on the gameobject gun central.
        guns.ps_Central_Gun = guns.obj_Central_Gun.GetComponent<ParticleSystem>();
        // Try and find an ParticleSystem Component on the gameobject gun left.
        guns.ps_Left_Gun = guns.obj_Left_Gun.GetComponent<ParticleSystem>();
        // Try and find an ParticleSystem Component on the gameobject gun right.
        guns.ps_Right_Gun = guns.obj_Right_Gun.GetComponent<ParticleSystem>();
    }
    //Права на данный курс принадлежат Дорофеевой Карине Олеговне, данный курс создавался для Udemy сайта
    private void Update()
    {
        //every timer_Shot seconds we call the method MakeAShot
        if (Time.time > timer_Shot)
        {
            timer_Shot = Time.time + time_Bullet_Spawn;
            //Call the method MakeAShot
            MakeAShot();
        }
    }
    // Method CreateBullet. This method rotates the bullets in the z-axis
    private void CreateBullet(GameObject bullet, Vector3 position_Bullet, Vector3 rotation_Bullet)
    {
        Instantiate(bullet, position_Bullet, Quaternion.Euler(rotation_Bullet));
    }
    // Method MakeAShot 
    // This method, depending on the cur_Power_Level_Guns, changes the player’s weapon shooting.
    private void MakeAShot()
    {
        switch (cur_Power_Level_Guns)
        {
            //One gun shoot
            case 1:
                CreateBullet(obj_Bullet, guns.obj_Central_Gun.transform.position, Vector3.zero);
                guns.ps_Central_Gun.Play();
                break;
            //two guns shoots
            case 2:
                CreateBullet(obj_Bullet, guns.obj_Right_Gun.transform.position, Vector3.zero);
                CreateBullet(obj_Bullet, guns.obj_Left_Gun.transform.position, Vector3.zero);
                guns.ps_Right_Gun.Play();
                guns.ps_Left_Gun.Play();
                break;
            //three guns shoots
            case 3:
                CreateBullet(obj_Bullet, guns.obj_Central_Gun.transform.position, Vector3.zero);
                CreateBullet(obj_Bullet, guns.obj_Right_Gun.transform.position, new Vector3(0, 0, -5));
                CreateBullet(obj_Bullet, guns.obj_Left_Gun.transform.position, new Vector3(0, 0, 5));
                guns.ps_Right_Gun.Play();
                guns.ps_Left_Gun.Play();
                guns.ps_Central_Gun.Play();
                break;
            //five guns shoots
            case 4:
                CreateBullet(obj_Bullet, guns.obj_Central_Gun.transform.position, Vector3.zero);
                CreateBullet(obj_Bullet, guns.obj_Right_Gun.transform.position, new Vector3(0, 0, 0));
                CreateBullet(obj_Bullet, guns.obj_Right_Gun.transform.position, new Vector3(0, 0, 5));
                CreateBullet(obj_Bullet, guns.obj_Left_Gun.transform.position, new Vector3(0, 0, 0));
                CreateBullet(obj_Bullet, guns.obj_Left_Gun.transform.position, new Vector3(0, 0, -5));
                guns.ps_Right_Gun.Play();
                guns.ps_Left_Gun.Play();
                guns.ps_Central_Gun.Play();
                break;
            //five guns shoots. Another shot mode.
            case 5:
                CreateBullet(obj_Bullet, guns.obj_Central_Gun.transform.position, Vector3.zero);
                CreateBullet(obj_Bullet, guns.obj_Right_Gun.transform.position, new Vector3(0, 0, -5));
                CreateBullet(obj_Bullet, guns.obj_Right_Gun.transform.position, new Vector3(0, 0, -15));
                CreateBullet(obj_Bullet, guns.obj_Left_Gun.transform.position, new Vector3(0, 0, 5));
                CreateBullet(obj_Bullet, guns.obj_Left_Gun.transform.position, new Vector3(0, 0, 15));
                guns.ps_Right_Gun.Play();
                guns.ps_Left_Gun.Play();
                guns.ps_Central_Gun.Play();
                break;
        }
    }
}
