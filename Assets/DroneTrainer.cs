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
            GameObject ob = Instantiate(drone, new Vector3(0, 0, 0),Quaternion.identity); ;
            ob.GetComponent<drone>().kiAngular += Random.Range(-mutationAmount, mutationAmount);
            ob.GetComponent<drone>().kpAngular += Random.Range(-mutationAmount, mutationAmount);
            ob.GetComponent<drone>().kdAngular += Random.Range(-mutationAmount, mutationAmount);

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
 
                    Destroy(drone);
     
            }


            population.Clear();
            for (int i = 0; i < droneAmount-1; i++)
            {
                GameObject ob = Instantiate(winner,new Vector3(0f,0f,0f),Quaternion.Euler(0f,0f,0f));
                ob.GetComponent<drone>().kiAngular += Random.Range(-mutationAmount, mutationAmount);
                ob.GetComponent<drone>().kpAngular += Random.Range(-mutationAmount, mutationAmount);
                ob.GetComponent<drone>().kdAngular += Random.Range(-mutationAmount, mutationAmount);

                population.Add(ob);

            }
            winner = population[0];
            time = 0f;


        }
    }

}
