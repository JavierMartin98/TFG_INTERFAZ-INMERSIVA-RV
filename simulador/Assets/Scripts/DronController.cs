using PathCreation;
using PathCreation.Examples;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DronController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("VillageTerrain").GetComponentInChildren<Server>().flagAreaBusqueda)
        {
            activaDrones();
            GameObject.Find("VillageTerrain").GetComponentInChildren<Server>().flagAreaBusqueda = false;
        }

    }

    //Asigna a los drones la ruta correspondiente
    public void activaDrones()
    {
        if (GameObject.Find("DronesA").GetComponent<PathFollower>() != null && GameObject.Find("DronesB").GetComponent<PathFollower>() != null)
        {
            Destroy(GameObject.Find("DronesA").GetComponent<PathFollower>());
            Destroy(GameObject.Find("DronesB").GetComponent<PathFollower>());
        }
        var dronA = GameObject.Find("DronesA").AddComponent<PathFollower>();
        var dronB = GameObject.Find("DronesB").AddComponent<PathFollower>();
        dronA.speed = 0.05f;
        dronB.speed = 0.05f;
        dronA.endOfPathInstruction = EndOfPathInstruction.Stop;
        dronB.endOfPathInstruction = EndOfPathInstruction.Stop;

        var pos = GameObject.Find("VillageTerrain").GetComponent<Server>().posicionAreaBusqueda;
        if (GameObject.Find("VillageTerrain").GetComponentInChildren<Server>().numAcciones == 1)
        {
            GameObject.Find("PathDrones3").transform.position = new Vector3(pos.Item1, pos.Item2, pos.Item3);
            dronA.pathCreator = GameObject.Find("PathDrones3").GetComponent<PathCreator>();
            dronB.pathCreator = GameObject.Find("PathDrones3").GetComponent<PathCreator>();
            dronB.GetComponent<CollisionCast>().enabled = false;

        }
        if (GameObject.Find("VillageTerrain").GetComponentInChildren<Server>().numAcciones == 2)
        {
            GameObject.Find("PathDrones4").transform.position = new Vector3(pos.Item1 - 50, pos.Item2, pos.Item3);
            GameObject.Find("PathDrones5").transform.position = new Vector3(pos.Item1 + 50, pos.Item2, pos.Item3);
            dronA.pathCreator = GameObject.Find("PathDrones4").GetComponent<PathCreator>();
            dronB.pathCreator = GameObject.Find("PathDrones5").GetComponent<PathCreator>();
        }

    }
   
}
