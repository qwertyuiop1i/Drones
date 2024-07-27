using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingPoint : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
     
            if (Input.GetMouseButtonDown(0)) { 
   
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePosition.z = 0;
           

                transform.position = mousePosition;
            }
        
    }
}
