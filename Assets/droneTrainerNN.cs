using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class droneTrainerNN : MonoBehaviour
{
   
    public int droneAmount;
    public float mutationAmount;
    public GameObject drone;
    public GameObject winner;
    public List<GameObject> population;

    public bool isTraining = true;

    public float waitTime = 1f;
    private float time = 0f;
    public float timeScale = 1f;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < droneAmount; i++)
        {
            GameObject ob = Instantiate(drone, new Vector3(0, 0, 0), Quaternion.identity); ;

            population.Add(ob);

        }
        winner = population[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (isTraining)
        {

            Time.timeScale = timeScale;
            time += Time.deltaTime;

            if (time >= waitTime)
            {
                foreach (GameObject g in population)
                {
                    if (scoring(g.GetComponent<droneNN>().distance) > scoring(winner.GetComponent<droneNN>().distance))
                    {
                        winner = g;
                    }
                }
                Debug.Log("The lowest distance is " + winner.GetComponent<droneNN>().distance);
                foreach (GameObject drone in population)
                {

                    Destroy(drone);

                }


                population.Clear();
                for (int i = 0; i < droneAmount - 2; i++)
                {
                    GameObject ob = Instantiate(winner, new Vector3(0f, 0f, 0f), Quaternion.Euler(0f, 0f, 0f));
                    ob.GetComponent<droneNN>().mutationAm = 6f;
                    ob.gameObject.name = "bigChange";
                    ob.GetComponent<droneNN>().enabled = true;
                    population.Add(ob);

                }
                for (int i = 0; i < 15; i++)
                {
                    GameObject ob = Instantiate(winner, new Vector3(0f, 0f, 0f), Quaternion.Euler(0f, 0f, 0f));
                    ob.GetComponent<droneNN>().mutationAm = 2f;
                    ob.gameObject.name = "mediumChange";
                    ob.GetComponent<droneNN>().enabled = true;
                    population.Add(ob);

                }

                for (int i = 0; i < 7; i++)
                {
                    GameObject ob = Instantiate(winner, new Vector3(0f, 0f, 0f), Quaternion.Euler(0f, 0f, 0f));
                    ob.GetComponent<droneNN>().mutationAm = 1f;
                    ob.gameObject.name = "smallChange";
                    ob.GetComponent<droneNN>().enabled = true;
                    population.Add(ob);

                }


                for (int i = 0; i < 3; i++)
                {
                    GameObject bb = Instantiate(winner, new Vector3(0f, 0f, 0f), Quaternion.Euler(0f, 0f, 0f));
                    bb.GetComponent<droneNN>().enabled = true;
                    population.Add(bb);
                    bb.name = "WINNER";
                }


                winner = population[0];



                time = 0f;
            }
        }
    }

    public float scoring(float dist)
    {
        return 1 / (1 + dist);
    }
}
