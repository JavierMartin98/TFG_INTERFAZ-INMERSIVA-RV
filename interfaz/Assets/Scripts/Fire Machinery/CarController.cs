using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;


public class CarController : MonoBehaviour
{
   
    public Transform target;
    
    public float speed;
    public GameObject fireFighter;


    public bool finish = false;
    public bool start = false;

    void Start()
    {
        if (start)
        {
            transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
        }
       
    }
    void Update()
    {
        if (start)
        {
            if (finish)
            {
                transform.Translate(new Vector3(0, 0, 0));
            }
            else
            {
                transform.Translate(new Vector3(0, 0, Time.deltaTime * speed));
            }
        }
        
        /*if (GameObject.Find("FireGrid") == true)
        {
            var item=GetMinDistance();

            
            var fire = GameObject.Find("FireGrid").transform.position;
            List<float> distances = new List<float>();
           
            foreach (var item in GameObject.FindGameObjectsWithTag("Waypoint"))
            {
                Debug.Log(item.name);
                distances.Add(Vector3.Distance(item.transform.position, fire));
            }

    }*/

    }
    

    private void OnTriggerEnter(Collider other)
    {
       
        // other.gameObject.GetComponent<Collider>().enabled = !other.gameObject.GetComponent<Collider>().enabled;
        if (other.tag=="Waypoint")
        {
            //Debug.Log(other.gameObject.GetComponent<Waypoint>().name);
            Debug.Log(GetObjectMinDistance().name);
            if (other.gameObject.GetComponent<WaypointFire>().name!= GetObjectMinDistance().name)
            {
                if (!GetObjectMinDistance().name.Contains("A") && other.gameObject.GetComponent<WaypointFire>().netxpoint2!=null)
                {
                    //Debug.Log("ENTRA");
                    var target = other.gameObject.GetComponent<WaypointFire>().netxpoint2;
                    transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
                }
                else
                {
                    var target = other.gameObject.GetComponent<WaypointFire>().netxpoint1;
                    transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
                }            
            }
            else
            {
                finish = true;
            }
        }
    }

    public GameObject GetObjectMinDistance()
    {
        var posIncendio = GameObject.Find("InterfaceController").GetComponent<Interface>().posIncendio;
        var fire = new Vector3(posIncendio.Item1, posIncendio.Item2, posIncendio.Item3);
        var distances = new Dictionary<GameObject,float>();
        
        foreach (var item in GameObject.FindGameObjectsWithTag("Waypoint"))
        {
            distances.Add(item,Vector3.Distance(item.transform.position, fire));
        }
        /*foreach (var item in distances)
        {
            Debug.Log(item.Value);
        }*/
        var min = distances.Aggregate((l, r) => l.Value < r.Value ? l : r).Key;
        //Debug.Log(min.name);
        return min;
        
    }

   
}