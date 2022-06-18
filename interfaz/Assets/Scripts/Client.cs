using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
using System.Threading;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class Client : MonoBehaviour
{
	// Main Method
	public Thread tcpListenerThread;
	public Protocolo prot;
	public bool house1 = false;
	public bool house2 = false;
	public bool house3 = false;
	public bool house4 = false;
	public bool house5 = false;
	public bool house6 = false;
	public bool house7 = false;
	public bool finish = false;
	public bool fire = false;
	public bool nearLocation = false;
	public (float, float, float) posicionIncendio = (0,0,0);



	// ExecuteClient() Method
	/*void Start()
	{
		ConnectToTcpServer();
	}*/
	public void ConnectToTcpServer(Protocolo p)
	{
		try
		{
			tcpListenerThread = new Thread(() => ExecuteClient(p));
			//myNewThread.Start();
			//tcpListenerThread = new Thread(new ThreadStart(ExecuteClient());
			tcpListenerThread.IsBackground = true;
			tcpListenerThread.Start();
		}
		catch (Exception e)
		{
			Debug.Log("On client connect exception " + e);
		}
	}

    /*void Update()
    {
		if (Input.GetKeyDown(KeyCode.Space))
		{
			ExecuteClient();
		}
	}*/

    public void ExecuteClient(Protocolo p)
	{
		// Establish the remote endpoint
		// for the socket. This example
		// uses port 11111 on the local
		// computer.
		IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
		IPAddress ipAddr = ipHost.AddressList[0];
		IPEndPoint localEndPoint = new IPEndPoint(ipAddr, 11111);

		// Creation TCP/IP Socket using
		// Socket Class Constructor
		Socket sender = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
		try
		{
			// Connect Socket to the remote
			// endpoint using method Connect()
			sender.Connect(localEndPoint);

			// We print EndPoint information
			// that we are connected
			Debug.Log($"Socket connected to -> {sender.RemoteEndPoint.ToString()} " );
			//var ceras = new CerasSerializer();
			//var bytes = ceras.Serialize(a);

			//byte[] messageSent = bytes;
			// Call Serialize/Deserialize, that's all
			byte[] messageSent = ObjectToByteArray(p);


			int byteSent = sender.Send(messageSent);

			// Data buffer
			byte[] messageReceived = new Byte[4096];
			int byteRecv = sender.Receive(messageReceived);
			Protocolo prot = ByteArrayToObject<Protocolo>(messageReceived);
			house1 = prot.house1;
			house2 = prot.house2;
			house3 = prot.house3;
			house4 = prot.house4;
			house5 = prot.house5;
			house6 = prot.house6;
			house7 = prot.house7;
			finish = prot.finish;
			fire = prot.fire;
			nearLocation = prot.nearLocation;
			posicionIncendio = prot.posicionIncendio;
			//procesaProtocolo(prot.house1);



			//GameObject.Find("InterfaceController").GetComponent<Interface>().house1 = prot.house1;
			//GameObject.Find("InterfaceController").GetComponent<Interface>().house2 = prot.house2;
			//GameObject.Find("InterfaceController").GetComponent<Interface>().house3 = prot.house3;

			// We receive the message using
			// the method Receive(). This
			// method returns number of bytes
			// received, that we'll use to
			// convert them to string

			// Close Socket using
			// the method Close()
			sender.Shutdown(SocketShutdown.Both);
			sender.Close();
		}
		// Manage of Socket's Exceptions
		catch (ArgumentNullException ane)
		{
			Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
		}

		catch (SocketException se)
		{
			Console.WriteLine("SocketException : {0}", se.ToString());
		}

		catch (Exception e)
		{
			Console.WriteLine("Unexpected exception : {0}", e.ToString());
		}
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
	public T ByteArrayToObject<T>(byte[] byteArray) where T : class
	{
		if (byteArray == null)
		{
			return null;
		}
		using (var memStream = new MemoryStream())
		{
			var binForm = new BinaryFormatter();
			memStream.Write(byteArray, 0, byteArray.Length);
			memStream.Seek(0, SeekOrigin.Begin);
			var obj = (T)binForm.Deserialize(memStream);
			return obj;
		}
	}

	void procesaProtocolo(bool house1)
    {
		if (house1)
		{
			Debug.Log("0");
			GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
			Debug.Log("1");
			sphere.GetComponent<Renderer>().material =  (Material)Resources.Load("SphereHousesArea", typeof(Material));
			Debug.Log("2");
			GameObject a = Instantiate(sphere, new Vector3(0, 0, 0), Quaternion.identity);
			Debug.Log("3");
			a.transform.localScale = new Vector3(40f, 40f, 40f);
			Debug.Log("4");
			a.SetActive(true);
			Debug.Log("Despues de llamar a función");
			
		}
	}


}

