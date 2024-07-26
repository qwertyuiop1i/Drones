using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneTrainer : MonoBehaviour
{
    public int droneAmount;
    public GameObject drone;
    public List<GameObject> population;
    public float waitTime=2.5f;
    private float time = 0f;

    public GameObject winner;

    float ScorePID(float timeToTarget, float overshoot, int oscillations)
    {
        float weightTimeToTarget = 1.0f;
        float weightOvershoot = 0.5f;
        float weightOscillations = 0.3f;

        return weightOvershoot * 1 / (overshoot);
    }
    void Start()
    {
        for(int i = 0; i < droneAmount; i++)
        {
            GameObject ob = Instantiate(drone);
            population.Add(ob);
        }
    }


    void Update()
    {
        time += Time.deltaTime;
        winner = population[0];
        if (time >= waitTime)
        {
            Debug.Log("scoring");
            foreach(GameObject g in population)
            {
                if (ScorePID(0f, g.GetComponent<drone>().maxedOvershoot, 0)>ScorePID(0f,winner.GetComponent<drone>().maxedOvershoot,0))
                {
                    winner = g;
                }
            }
            //time = 0f;
        }
    }

}
