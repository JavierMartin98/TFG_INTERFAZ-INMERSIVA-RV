using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using Random = UnityEngine.Random;


public class CarController : MonoBehaviour
{   
    public Transform target;   
    public float speed;
    public GameObject fireFighter;
    public bool finish = false;


    void Start()
    {
        if (GameObject.Find("VillageTerrain").GetComponent<Server>().callfirefighters)
        {
            transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
           
        }
    }
    void Update()
    {
        //Debug.Log(GameObject.Find("VillageTerrain").GetComponentInChildren<Server>().callfirefighters);
        
        if (GameObject.Find("VillageTerrain").GetComponent<Server>().callfirefighters)
        {
            if (finish)
            {
                transform.Translate(new Vector3(0, 0, 0));
                Protocolo p = new Protocolo { finish = true };
                byte[] message = ObjectToByteArray(p);
                GameObject.Find("VillageTerrain").GetComponent<Server>().clientSocket.Send(message);
                GameObject.Find("VillageTerrain").GetComponent<Server>().callfirefighters = false;
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
    
    /// <summary>
    /// Funcion que permite encontrar el siguiente waypoint por donde tiene que avanzar el camion de bomberos
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
       
        // other.gameObject.GetComponent<Collider>().enabled = !other.gameObject.GetComponent<Collider>().enabled;
        if (other.tag=="Waypoint")
        {
            //Debug.Log(other.gameObject.GetComponent<WaypointFire>().name);
            //Debug.Log(GetObjectMinDistance().name);
            
            //Si el Waypoint con el que el camion ha chocado es distinto del ultimo Waypoint, continua.
            if (other.gameObject.GetComponent<WaypointFire>().name!= GetObjectMinDistance().name)
            {
                //Si el Ultimo Waypoint no contiene "A" y El siguiente Waypoint al que estoy tiene un nexpoint diferente de null significa que tengo que coger el camino "B"
                if (!GetObjectMinDistance().name.Contains("A") && other.gameObject.GetComponent<WaypointFire>().netxpoint2!=null)
                {
                    //Debug.Log("ENTRA");
                    var target = other.gameObject.GetComponent<WaypointFire>().netxpoint2;
                    transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
                }
                //Si no se dan las condiciones anteriores, tengo que continuar por el camino "A"
                else
                {
                    var target = other.gameObject.GetComponent<WaypointFire>().netxpoint1;
                    transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
                }
            }
            //Si no, es que ya he llegado al final del camino.
            else
            {
                finish = true;
                SpawnFirefighters();
            }
        }
    }
    /// <summary>
    /// Funcion que permite spamear bomberos alrededor del fuego que se ha originado
    /// </summary>
    private void SpawnFirefighters()
    {
        var fire = GameObject.Find("FireGrid").transform.position;
        
        for (int i = 0; i < 7; i++)
        {
            float randX = UnityEngine.Random.Range(fire.x + 5, fire.x + 20);
            float randZ = UnityEngine.Random.Range(fire.z + 5, fire.z + 20);
            float yVal = Terrain.activeTerrain.SampleHeight(new Vector3(randX, 0, randZ));
            GameObject objInstance = (GameObject)Instantiate(fireFighter, new Vector3(randX, yVal, randZ), Quaternion.identity);          
        }
        Destroy(GameObject.Find("FireGrid"), 60);

    }

    /// <summary>
    /// Funcion que permite obtener la distancia mínima entre el Waypoint mas cercano al fuego que se origina
    /// </summary>
    /// <returns></returns>
    private GameObject GetObjectMinDistance()
    {
        var fire = GameObject.Find("FireGrid").transform.position;
        var distances = new Dictionary<GameObject,float>();
        foreach (var item in GameObject.FindGameObjectsWithTag("Waypoint"))
        {
            distances.Add(item,Vector3.Distance(item.transform.position, fire));
        }
        var min = distances.Aggregate((l, r) => l.Value < r.Value ? l : r).Key;
        //Debug.Log(min.name);
        return min;
    }

    public byte[] ObjectToByteArray(object obj)
    {
        if (obj == null)
        {
            return null;
        }
        var bf = new BinaryFormatter();
        using (var ms = new MemoryStream())
        {
            bf.Serialize(ms, obj);
            return ms.ToArray();
        }
    }


}