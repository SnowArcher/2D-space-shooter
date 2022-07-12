using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public static Explosion instance;
    // The Explosion prefab to be spawned.
    public GameObject obj_Explosion;
    void Start()
    {
        if (GameObject.FindGameObjectsWithTag("Explosion").Length > 0)
        {
            // Destroy Explosion after 1 seconds.
            Destroy(obj_Explosion, 1f);
        }
    }

}
