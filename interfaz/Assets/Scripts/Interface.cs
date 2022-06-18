using PathCreation;
using PathCreation.Examples;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class Interface : MonoBehaviour
{
    
    public GameObject FireImagePrefab;
    public GameObject SphereFireArea;
    public GameObject SphereHousesArea;
    public GameObject WaypointLocation;
    public GameObject WindDirection;
    public GameObject CallFirefighters;
    [SerializeField] private GameObject Canvas1;
    [SerializeField] private GameObject Canvas2;
    [SerializeField] private GameObject Canvas3;
    [SerializeField] private GameObject cube;
    public Canvas myCanvas;
    private bool flag1 = false;
    private bool flag2 = false;
    private bool flag3 = false;
    private bool flag4 = false;
    private bool flag5 = false;
    private bool exist = false;
    private bool house1 = true;
    private bool house2 = true;
    private bool house3 = true;
    private bool house4 = true;
    private bool house5 = true;
    private bool house6 = true;
    private bool house7 = true;
    private bool fire = true;
    private bool nearLocation = true;
    private int numCliente = 0;
    private string ultimoCliente = "";
    private int numAcciones;
    public (float, float, float) posIncendio;

    // Start is called before the first frame update
    void Start()
    {
        //Temperatura aire
       Canvas1.GetComponent<TMP_Text>().text = GameObject.Find("FireManager").GetComponent<FireManager>().airTemperature.ToString();

        //Velocidad aire
        var speed = GameObject.Find("FireManager").GetComponent<FireManager>().windzone.windMain * GameObject.Find("FireManager").GetComponent<FireManager>().propagationBias * 100;
        GameObject.Find("AirVel").GetComponent<TMP_Text>().text = speed.ToString();
        //StartCoroutine(ExampleCoroutine());

    }

    // Update is called once per frame
    void Update()
    {

        if (GameObject.Find(ultimoCliente))
        {
            if (GameObject.Find(ultimoCliente).GetComponent<Client>().finish)
            {   
                activaInterfazCasa(ultimoCliente);
                activarInterfazFuego(ultimoCliente);
                activarAccesoCercano(ultimoCliente);
                Canvas1.SetActive(true);
                Canvas3.SetActive(false);
                GameObject.Find(ultimoCliente).GetComponent<Client>().finish = false;
                Destroy(GameObject.Find(ultimoCliente));
                Debug.Log("PAsas una vez");
            }
        }

      
    }

    /*public void ActivateFireFocus()
    {   
        if (flag1)
        {
            SphereFireArea.SetActive(false);
            FireImagePrefab.SetActive(false);
        }
        else
        {
            
            var fire = GameObject.Find("FireGrid").transform;
            SphereFireArea.SetActive(true);
            SphereFireArea.transform.position = new Vector3(fire.position.x, fire.position.y, fire.position.z);
            SphereFireArea.transform.localScale = new Vector3(100f, 100f, 100f);
            FireImagePrefab.transform.position = new Vector3(fire.position.x, fire.position.y + 50, fire.position.z);
            FireImagePrefab.SetActive(true);
        }
        flag1 = !flag1;
        
    }

    public void ActiveNearLocation()
    {
        if (flag2)
        {
            WaypointLocation.SetActive(false);
        }
        else
        {
            CarController a = new CarController();
            var waypoint = a.GetObjectMinDistance().transform;
            WaypointLocation.SetActive(true);
            WaypointLocation.transform.localScale = new Vector3(2f, 2f, 2f);
            WaypointLocation.transform.position = new Vector3(waypoint.position.x, waypoint.position.y + 10, waypoint.position.z);

        }
        flag2 = !flag2;
       
    }

    public void ActiveHouseLocation()
    {
        if (flag3)
        {
            foreach (var item in GameObject.FindGameObjectsWithTag("HouseLocation"))
            {
                item.GetComponent<MeshRenderer>().enabled = false;
            }

        }
        else
        {
            if (!exist)
            {
                foreach (var item in GameObject.FindGameObjectsWithTag("House"))
                {
                    GameObject a = Instantiate(SphereHousesArea, new Vector3(item.transform.position.x, item.transform.position.y + 4, item.transform.position.z), Quaternion.identity);
                    a.transform.localScale = new Vector3(40f, 40f, 40f);
                    a.SetActive(true);
                    a.tag = "HouseLocation";
                    exist = true;
                }

            }
            else
            {
                foreach (var item in GameObject.FindGameObjectsWithTag("HouseLocation"))
                {
                    item.GetComponent<MeshRenderer>().enabled = true;
                }

            }
   
        }
        flag3 = !flag3;
    }*/


    public void ActivateFireFocus()
    {
        if (flag1)
        {
            GameObject.Find("ButtonFireLocation").GetComponent<Image>().color = Color.white;
        }
        else
        {
            GameObject.Find("ButtonFireLocation").GetComponent<Image>().color = Color.green;         
        }
        flag1 = !flag1;
    }

    public void ActiveNearLocation()
    {
        if (flag2)
        {
            GameObject.Find("ButtonNearLocation").GetComponent<Image>().color = Color.white;
        }
        else
        {
            GameObject.Find("ButtonNearLocation").GetComponent<Image>().color = Color.green;
        }
        flag2 = !flag2;
    }

    public void ActiveHouseLocation()
    {
        if (flag3)
        {
            GameObject.Find("ButtonHouseLocation").GetComponent<Image>().color = Color.white;
        }
        else
        {
            GameObject.Find("ButtonHouseLocation").GetComponent<Image>().color = Color.green;
        }
        flag3 = !flag3;
    }

    public void ActiveWindDirection()
    {
        if (flag4)
        {
            WindDirection.SetActive(false);
        }
        else
        {
            WindDirection.transform.position = new Vector3(GameObject.Find("FireManager").GetComponent<FireManager>().windzone.transform.position.x,30, GameObject.Find("FireManager").GetComponent<FireManager>().windzone.transform.position.z);
            WindDirection.transform.eulerAngles = new Vector3(90f, 90f, 0f);
            WindDirection.transform.localScale = new Vector3(5f, 5f, 5f);
            WindDirection.SetActive(true);

        }
        flag4 = !flag4;  
    }
    public void ActiveCallFirefighters()
    {
        GameObject.Find("Fire_Truck").GetComponent<CarController>().start = true;
        GameObject gameObject = new GameObject("C");
        Client cliente = gameObject.AddComponent<Client>();
        //Client cliente = new Client();
        Protocolo p = new Protocolo() { callfirefighters = true, flagFireFighters = true };
        cliente.ConnectToTcpServer(p);
        //GameObject.Find("ButtonCallFirefighters").GetComponent<Button>().interactable = false;      
    }
    
    public void DefineArea()
    {
        Canvas1.SetActive(false);
        Canvas2.SetActive(true);
        Canvas3.SetActive(false);
        cube.SetActive(true);
        GameObject.Find("RightHand Controller").GetComponent<XRInteractorReticleVisual>().enabled = true;
        //Temperatura aire
        GameObject.Find("AirTemp").GetComponent<TMP_Text>().text = GameObject.Find("FireManager").GetComponent<FireManager>().airTemperature.ToString();

        //Velocidad aire
        var speed = GameObject.Find("FireManager").GetComponent<FireManager>().windzone.windMain * GameObject.Find("FireManager").GetComponent<FireManager>().propagationBias * 100;
        GameObject.Find("AirVel").GetComponent<TMP_Text>().text = speed.ToString();
    }
    public void Atras()
    {
        flag1 = false;
        GameObject.Find("ButtonFireLocation").GetComponent<Image>().color = Color.white;
        flag2 = false;
        GameObject.Find("ButtonNearLocation").GetComponent<Image>().color = Color.white;
        flag3 = false;
        GameObject.Find("ButtonHouseLocation").GetComponent<Image>().color = Color.white;
        Canvas2.SetActive(false);
        Canvas1.SetActive(true);
        //cube.GetComponent<MousePosition3D>().flag1 = true;
        GameObject.Find("RightHand Controller").GetComponent<XRInteractorReticleVisual>().enabled = false;
        cube.SetActive(false);
    }

    public void Empezar()
    {
        //Obtenemos el area de busqueda, y ajustamos a los bordes del mapa si es necesario
        var posicionAreaBusqueda = GameObject.Find("AreaBusqueda").GetComponent<MousePosition3D>().posicionAreaBusqueda;
        if (posicionAreaBusqueda.Item1 < 100)
            posicionAreaBusqueda.Item1 = 100f;

        if (posicionAreaBusqueda.Item1 > 400)
            posicionAreaBusqueda.Item1 = 400f;

        if (posicionAreaBusqueda.Item3 < 100)
            posicionAreaBusqueda.Item3 = 100f;

        if (posicionAreaBusqueda.Item3 > 400)
            posicionAreaBusqueda.Item3 = 400f;

        posicionAreaBusqueda.Item2 = 100f;
        
        //cube.GetComponent<MousePosition3D>().flag1 = true;
        cube.SetActive(false);
        GameObject.Find("RightHand Controller").GetComponent<XRInteractorReticleVisual>().enabled = false;

        //Client cliente = new Client();
        GameObject cl = new GameObject("Cliente" + numCliente.ToString());
        Client cliente = cl.AddComponent<Client>();
        ultimoCliente = "Cliente" + numCliente.ToString();
        numCliente++;

        if (flag1 && flag3)
        {
            Protocolo p0 = new Protocolo()
            {
                posicionAreaBusqueda = (posicionAreaBusqueda.Item1, posicionAreaBusqueda.Item2, posicionAreaBusqueda.Item3),
                flagAreaBusqueda = true,
                numAcciones = 2,
                fireLocation = true,
                callfirefighters = false,
                houseLocation = true
            };
            cliente.ConnectToTcpServer(p0);
            numAcciones = 2;
        }
        else
        {
            if (flag1)
            {
                /*Localización incendio*/
                Protocolo p1 = new Protocolo()
                {
                    posicionAreaBusqueda = (posicionAreaBusqueda.Item1, posicionAreaBusqueda.Item2, posicionAreaBusqueda.Item3),
                    flagAreaBusqueda = true,
                    numAcciones = 1,
                    fireLocation = true,
                    callfirefighters = false,
                    houseLocation = false
                };
                cliente.ConnectToTcpServer(p1);
            }
            if (flag2)
            {
                /*Localización acceso más cercano */
                Protocolo p2 = new Protocolo()
                {
                    posicionAreaBusqueda = (posicionAreaBusqueda.Item1, posicionAreaBusqueda.Item2, posicionAreaBusqueda.Item3),
                    flagAreaBusqueda = true,
                    numAcciones = 1,
                    fireLocation = false,
                    callfirefighters = false,
                    nearLocation = true,
                    houseLocation = false
                };
                cliente.ConnectToTcpServer(p2);
            }

            if (flag3)
            {
                /*Localización de casas*/
                Protocolo p3 = new Protocolo()
                {
                    posicionAreaBusqueda = (posicionAreaBusqueda.Item1, posicionAreaBusqueda.Item2, posicionAreaBusqueda.Item3),
                    flagAreaBusqueda = true,
                    numAcciones = 1,
                    fireLocation = false,
                    callfirefighters = false,
                    houseLocation = true
                };

                cliente.ConnectToTcpServer(p3);
            }

            numAcciones = 1;
        }

        activaDrones(numAcciones, posicionAreaBusqueda);
        flag1 = false;
        GameObject.Find("ButtonFireLocation").GetComponent<Image>().color = Color.white;
        flag2 = false;
        GameObject.Find("ButtonNearLocation").GetComponent<Image>().color = Color.white;
        flag3 = false;
        GameObject.Find("ButtonHouseLocation").GetComponent<Image>().color = Color.white;
        Canvas1.SetActive(false);
        Canvas2.SetActive(false);
        Canvas3.SetActive(true);
    }

    public void activaInterfazCasa(string ultimoCliente)
    {
        Debug.Log(ultimoCliente);
        if (GameObject.Find(ultimoCliente).GetComponent<Client>().house1 && house1)
        {
            var item = GameObject.Find("House1").transform;
            GameObject a = Instantiate(SphereHousesArea, new Vector3(item.position.x, item.transform.position.y + 4, item.transform.position.z), Quaternion.identity);
            a.transform.localScale = new Vector3(40f, 40f, 40f);
            a.SetActive(true);
            house1 = false;
            GameObject.Find(ultimoCliente).GetComponent<Client>().house1 = false;
        }

        if (GameObject.Find(ultimoCliente).GetComponent<Client>().house2 && house2)
        {
            var item = GameObject.Find("House2").transform;
            GameObject a = Instantiate(SphereHousesArea, new Vector3(item.position.x, item.transform.position.y + 4, item.transform.position.z), Quaternion.identity);
            a.transform.localScale = new Vector3(40f, 40f, 40f);
            a.SetActive(true);
            house2 = false;
            GameObject.Find(ultimoCliente).GetComponent<Client>().house2 = false;
        }
        if (GameObject.Find(ultimoCliente).GetComponent<Client>().house3 && house3)
        {
            var item = GameObject.Find("House3").transform;
            GameObject a = Instantiate(SphereHousesArea, new Vector3(item.position.x, item.transform.position.y + 4, item.transform.position.z), Quaternion.identity);
            a.transform.localScale = new Vector3(40f, 40f, 40f);
            a.SetActive(true);
            house3 = false;
            GameObject.Find(ultimoCliente).GetComponent<Client>().house3 = false;
        }
        if (GameObject.Find(ultimoCliente).GetComponent<Client>().house4 && house4)
        {
            var item = GameObject.Find("House4").transform;
            GameObject a = Instantiate(SphereHousesArea, new Vector3(item.position.x, item.transform.position.y + 4, item.transform.position.z), Quaternion.identity);
            a.transform.localScale = new Vector3(40f, 40f, 40f);
            a.SetActive(true);
            house4 = false;
            GameObject.Find(ultimoCliente).GetComponent<Client>().house4 = false;
        }
        if (GameObject.Find(ultimoCliente).GetComponent<Client>().house5 && house5)
        {
            var item = GameObject.Find("House5").transform;
            GameObject a = Instantiate(SphereHousesArea, new Vector3(item.position.x, item.transform.position.y + 4, item.transform.position.z), Quaternion.identity);
            a.transform.localScale = new Vector3(40f, 40f, 40f);
            a.SetActive(true);
            house5 = false;
            GameObject.Find(ultimoCliente).GetComponent<Client>().house5 = false;
        }
        if (GameObject.Find(ultimoCliente).GetComponent<Client>().house6 && house6)
        {
            var item = GameObject.Find("House6").transform;
            GameObject a = Instantiate(SphereHousesArea, new Vector3(item.position.x, item.transform.position.y + 4, item.transform.position.z), Quaternion.identity);
            a.transform.localScale = new Vector3(40f, 40f, 40f);
            a.SetActive(true);
            house6 = false;
            GameObject.Find(ultimoCliente).GetComponent<Client>().house6 = false;
        }
        if (GameObject.Find(ultimoCliente).GetComponent<Client>().house7 && house7)
        {
            var item = GameObject.Find("House7").transform;
            GameObject a = Instantiate(SphereHousesArea, new Vector3(item.position.x, item.transform.position.y + 4, item.transform.position.z), Quaternion.identity);
            a.transform.localScale = new Vector3(40f, 40f, 40f);
            a.SetActive(true);
            house7 = false;
            GameObject.Find(ultimoCliente).GetComponent<Client>().house7 = false;
        }
    }

    public void activarInterfazFuego(string ultimoCliente)
    {

        if (GameObject.Find(ultimoCliente).GetComponent<Client>().fire && fire)
        {
            Debug.Log("Localiza el incendio");
            posIncendio = GameObject.Find(ultimoCliente).GetComponent<Client>().posicionIncendio;         
            //var item = new Vector3(134.600006f, 7.8499999f, 345.109985f);
            //var item = GameObject.Find("FireGrid").transform;
            SphereFireArea.SetActive(true);
            SphereFireArea.transform.position = new Vector3(posIncendio.Item1, posIncendio.Item2, posIncendio.Item3);
            SphereFireArea.transform.localScale = new Vector3(100f, 100f, 100f);
            FireImagePrefab.transform.position = new Vector3(posIncendio.Item1, posIncendio.Item2 + 50, posIncendio.Item3);
            FireImagePrefab.SetActive(true);
            fire = false;
            GameObject.Find(ultimoCliente).GetComponent<Client>().fire = false;
            GameObject.Find("ButtonCallFirefighters").GetComponent<Button>().interactable = true;
        }
    }

    public void activarAccesoCercano(string ultimoCliente)
    {
        if (GameObject.Find(ultimoCliente).GetComponent<Client>().nearLocation && nearLocation)
        {
            CarController a = new CarController();
            var waypoint = a.GetObjectMinDistance().transform;
            WaypointLocation.SetActive(true);
            WaypointLocation.transform.localScale = new Vector3(2f, 2f, 2f);
            WaypointLocation.transform.position = new Vector3(waypoint.position.x, waypoint.position.y + 10, waypoint.position.z);
            nearLocation = false;
        }
        
    }

    public void activaDrones( int numAcciones, (float, float, float) pos)
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
     
        if (numAcciones == 1)
        {
            GameObject.Find("PathDrones3").transform.position = new Vector3(pos.Item1, pos.Item2, pos.Item3);
            dronA.pathCreator = GameObject.Find("PathDrones3").GetComponent<PathCreator>();
            dronB.pathCreator = GameObject.Find("PathDrones3").GetComponent<PathCreator>();

        }
      
        if (numAcciones == 2)
        {
            GameObject.Find("PathDrones4").transform.position = new Vector3(pos.Item1 - 50, pos.Item2, pos.Item3);
            GameObject.Find("PathDrones5").transform.position = new Vector3(pos.Item1 + 50, pos.Item2, pos.Item3);
            dronA.pathCreator = GameObject.Find("PathDrones4").GetComponent<PathCreator>();
            dronB.pathCreator = GameObject.Find("PathDrones5").GetComponent<PathCreator>();
        }

    }

}
