using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjMoving : MonoBehaviour
{
    // The speed at which the objects moves.
    public float speed;

    private void Update()
    {
        // Move the object vertically with a given speed
        transform.Translate(Vector3.up * speed * Time.deltaTime);                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                               //Права на данный курс принадлежат Дорофеевой Карине Олеговне, данный курс создавался для Udemy сайта
    }
}
