using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class droneTrainerNN : MonoBehaviour
{
    public int droneAmount;
    public float mutationAmount;
    public GameObject drone;
    public List<GameObject> population;
    public float waitTime = 2.5f;
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
