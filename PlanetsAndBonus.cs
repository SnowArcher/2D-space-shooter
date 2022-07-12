using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetsAndBonus : MonoBehaviour
{
    // The Bonus prefab to be spawned.
    public GameObject obj_Bonus;
    // How long between each Bonus spawn.
    public float time_Bonus_Spawn;
    // An array the planets prefab to be spawned.
    public GameObject[] obj_Planets;
    // How long between each planets spawn.
    public float time_Planet_Spawn;
    // The speed at which the Planets moves.
    public float speed_Planets;
    //Planets list
    // we will use this list so that the planets do not repeat.
    List<GameObject> planetsList = new List<GameObject>();

    private void Start()
    {
        // Start function PowerupBonusCreation as a coroutine.
        StartCoroutine(BonusCreation());
        // Start function PlanetsCreation as a coroutine.
        StartCoroutine(PlanetsCreation());
    }
    IEnumerator BonusCreation()
    {
        //Create bonus...
        while (true)
        {
            // Every time_Bonus_Spawn seconds
            yield return new WaitForSeconds(time_Bonus_Spawn);
            // Create an instance of the Prefab Bonus, taking into account the limits of the player’s movement width
            // The bonus will be created above the camera's visibility.
            Instantiate(obj_Bonus, new Vector2(Random.Range(PlayerMoving.instance.borders.minX, PlayerMoving.instance.borders.maxX),
                PlayerMoving.instance.borders.maxY * 1.5f), Quaternion.identity);
        }
    }


    IEnumerator PlanetsCreation()
    {
        // Fill the list with planets
        for (int i = 0; i < obj_Planets.Length; i++)
        {
            planetsList.Add(obj_Planets[i]);
        }
        // wait 7 seconds after the game started...
        yield return new WaitForSeconds(7);
        //Create planets...
        while (true)
        {
            // Select a random planet from the list.
            int randomIndex = Random.Range(0, planetsList.Count);
            // Create an instance of the planet, taking into account the limits of the player’s movement width
            // The planet will be created above the camera's visibility
            // The planets will move at an angle in the range of -25 to 25. 
            GameObject newPlanet = Instantiate(planetsList[randomIndex],
                new Vector2(Random.Range(PlayerMoving.instance.borders.minX, PlayerMoving.instance.borders.maxX),
                PlayerMoving.instance.borders.maxY * 1.7f),
                Quaternion.Euler(0, 0, Random.Range(-25, 25)));

            //Remove the selected planet from the list
            planetsList.RemoveAt(randomIndex);
            // if the list is empty, fill it again
            if (planetsList.Count == 0)
            {
                for (int i = 0; i < obj_Planets.Length; i++)
                {
                    planetsList.Add(obj_Planets[i]);
                }
            }
            // On the created planet we find the component MovingObjects and set the speed of movement
            newPlanet.GetComponent<ObjMoving>().speed = speed_Planets;
            // Every time_Planet_Spawn seconds
            yield return new WaitForSeconds(time_Planet_Spawn);
        }
    }
}