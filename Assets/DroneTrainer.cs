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

    public float mutationAmount=0.1f;

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
            ob.GetComponent<drone>().kiAngular += Random.Range(-mutationAmount, mutationAmount);
            ob.GetComponent<drone>().kpAngular += Random.Range(-mutationAmount, mutationAmount);
            ob.GetComponent<drone>().kdLinear += Random.Range(-mutationAmount, mutationAmount);

            population.Add(ob);
            
        }
        winner = population[0];
    }


    void Update()
    {
        time += Time.deltaTime;
        
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

            foreach (GameObject drone in population)
            {
                if (drone != winner)
                {
                    Destroy(drone);
                }
            }
            //time = 0f;
        }
    }

}
