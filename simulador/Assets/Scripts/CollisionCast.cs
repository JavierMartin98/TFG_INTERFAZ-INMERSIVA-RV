using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using UnityEngine;
using System.Linq;
using PathCreation.Examples;
using PathCreation;

public class CollisionCast : MonoBehaviour
{

    // public HashSet<Vector3> houseList;
    bool house1 = false;
    bool house2 = false;
    bool house3 = false;
    bool house4 = false;
    bool house5 = false;
    bool house6 = false;
    bool house7 = false;
    bool fire = false;
    (float, float, float) posIncendio = (0, 0, 0);
    bool nearLocation = false;
    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        float maxDistance = 110f;
        RaycastHit hit;
        //var houseList = new HashSet<Vector3>(); transform.lossyScale /2
        if( Physics.BoxCast(transform.position, new Vector3(15, 5, 15) / 2, Vector3.down, out hit, transform.rotation, maxDistance))
        {
            if (GameObject.Find("VillageTerrain").GetComponent<Server>().houseLocation)
            {
                Debug.Log("Entra en Localizacion casa");

                if (hit.collider.name == "House1" && !house1)
                    house1 = !house1;

                if (hit.collider.name == "House2" && !house2)
                    house2 = !house2;

                if (hit.collider.name == "House3" && !house3)
                    house3 = !house3;

                if (hit.collider.name == "House4" && !house4)
                    house4 = !house4;

                if (hit.collider.name == "House5" && !house5)
                    house5 = !house5;

                if (hit.collider.name == "House6" && !house6)
                    house6 = !house6;

                if (hit.collider.name == "House7" && !house7)
                    house7 = !house7;
            }
            if (GameObject.Find("VillageTerrain").GetComponent<Server>().fireLocation)
            {
                Debug.Log("Entra en Localizacion incendio");
                if (hit.collider.name.Contains("Fire"))
                {
                    Debug.Log(hit.collider.name);
                }
                if (hit.collider.name.Contains("Fire") && !fire)
                {
                    fire = !fire;
                    posIncendio = (hit.collider.transform.position.x, hit.collider.transform.position.y, hit.collider.transform.position.z);
                }
            }
            if (GameObject.Find("VillageTerrain").GetComponent<Server>().nearLocation)
            {
                nearLocation = true;
            }

            if (GameObject.Find("DronesA").GetComponent<PathFollower>() != null && GameObject.Find("DronesB").GetComponent<PathFollower>() != null)
            {
                /*Los drones han llegado al final de su recorrido y envían mensaje al cliente de lo que han encontrado*/
                if (GameObject.Find("DronesA").GetComponent<PathFollower>().pathCreator.path.GetPointAtTime(1, EndOfPathInstruction.Stop) == GameObject.Find("DronesA").transform.position &&
                    GameObject.Find("DronesB").GetComponent<PathFollower>().pathCreator.path.GetPointAtTime(1, EndOfPathInstruction.Stop) == GameObject.Find("DronesB").transform.position)
                {
                    if (this.gameObject.name=="DronesA")
                    {
                        Destroy(GameObject.Find("DronesA").GetComponent<PathFollower>());
                        Destroy(GameObject.Find("DronesB").GetComponent<PathFollower>());
                        construirProtocolo(house1, house2, house3, house4, house5, house6, house7, fire, nearLocation, posIncendio);
                        GameObject.Find("VillageTerrain").GetComponentInChildren<Server>().houseLocation = false;
                        GameObject.Find("VillageTerrain").GetComponentInChildren<Server>().fireLocation = false;
                        GameObject.Find("VillageTerrain").GetComponentInChildren<Server>().nearLocation = false;
                    }

                }
            }
        }
}
    void construirProtocolo(bool h1, bool h2, bool h3, bool h4, bool h5, bool h6, bool h7, bool fire, bool nearLocation, (float,float,float) posIncendio)
    {
        Debug.Log("envio");
        Protocolo p = new Protocolo { house1 = h1, house2 = h2, house3 = h3, house4 = h4, house5 = h5, house6 = h6, house7 = h7, finish = true, fire = fire, nearLocation = nearLocation, posicionIncendio = posIncendio};
        byte[] message = ObjectToByteArray(p);
        GameObject.Find("VillageTerrain").GetComponent<Server>().clientSocket.Send(message);
        
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
