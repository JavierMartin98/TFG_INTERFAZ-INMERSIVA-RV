using System.Collections;
using System.Collections.Generic;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.CompilerServices;
using UnityEngine;
using System.Threading;
using PathCreation.Examples;


//using Ceras;

public class Server: MonoBehaviour
{
    public Thread tcpListenerThread;
	public bool callfirefighters = false;
	public bool flagAreaBusqueda = false;
	public bool houseLocation = false;
	public bool fireLocation = false;
	public bool nearLocation = false;
	public (float, float, float) posicionAreaBusqueda = (0,0,0);
	public int numAcciones = 0;
	public Socket clientSocket;

	// A C# Program for Server
	// Main Method
	void Start()
	{
		tcpListenerThread = new Thread(new ThreadStart(ExecuteServer));
		tcpListenerThread.IsBackground = true;
		tcpListenerThread.Start();
	}

	public void ExecuteServer()
	{

		// Establish the local endpoint
		// for the socket. Dns.GetHostName
		// returns the name of the host
		// running the application.
		IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
		IPAddress ipAddr = ipHost.AddressList[0];
		IPEndPoint localEndPoint = new IPEndPoint(ipAddr, 11111);
		

		// Creation TCP/IP Socket using
		// Socket Class Constructor
		Socket listener = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

		try
		{
			// Using Bind() method we associate a
			// network address to the Server Socket
			// All client that will connect to this
			// Server Socket must know this network
			// Address
			listener.Bind(localEndPoint);

			// Using Listen() method we create
			// the Client list that will want
			// to connect to Server
			listener.Listen(10);

			while (true)
			{

				Debug.Log("Waiting connection ... ");

				// Suspend while waiting for
				// incoming connection Using
				// Accept() method the server
				// will accept connection of client
				clientSocket = listener.Accept();

				// Data buffer
				byte[] bytes = new Byte[4096];
				string data = null;
				int numByte;

				clientSocket.Receive(bytes);
				

				/*while (true)
				{

					numByte = clientSocket.Receive(bytes);

					data += Encoding.ASCII.GetString(bytes, 0, numByte);

					if (data.IndexOf("<EOF>") > -1)
						break;
				}
				Debug.Log(data);*/
				/*var memStream = new MemoryStream();
				var binForm = new BinaryFormatter();
				memStream.Write(bytes, 0, numByte);
				memStream.Seek(0, SeekOrigin.Begin);
				var obj = binForm.Deserialize(memStream);*/

				/*ClassPrueba o = new ClassPrueba();
				o = (ClassPrueba) ByteArrayToObject2(bytes);*/

				//var ceras = new CerasSerializer();
				//var clone1 = ceras.Deserialize<CarController>(bytes);
				
				Protocolo p = ByteArrayToObject<Protocolo>(bytes);
				posicionAreaBusqueda = p.posicionAreaBusqueda;//(150f, 100f, 150f);
				flagAreaBusqueda = p.flagAreaBusqueda;
				callfirefighters = p.callfirefighters;
				numAcciones = p.numAcciones;
				houseLocation = p.houseLocation;
				fireLocation = p.fireLocation;
				nearLocation = p.nearLocation;
				//Debug.Log(GameObject.Find("FireMachinery").name);
				//Debug.Log(p.houseLocation);
				//Debug.Log(p.fireLocation);

				//byte[] message = Encoding.ASCII.GetBytes("Test Server");

				// Send a message to Client
				// using Send() method
				//clientSocket.Send(message);

				// Close client Socket using the
				// Close() method. After closing,
				// we can use the closed Socket
				// for a new Client Connection
				//clientSocket.Shutdown(SocketShutdown.Both);
				//clientSocket.Close();
			}
		}

		catch (Exception e)
		{
			Console.WriteLine(e.ToString());
		}
	}
	/*public static object ByteArrayToObject(byte[] arrBytes)
	{
		using (var memStream = new MemoryStream())
		{
			var binForm = new BinaryFormatter();
			memStream.Write(arrBytes, 0, arrBytes.Length);
			memStream.Seek(0, SeekOrigin.Begin);
			var obj = binForm.Deserialize(memStream);
			return obj;
		}

	}
	public static T FromByteArray<T>(byte[] data)
	{
		if (data == null)
			return default(T);
		BinaryFormatter bf = new BinaryFormatter();
		using (MemoryStream ms = new MemoryStream(data))
		{
			object obj = bf.Deserialize(ms);
			return (T)obj;
		}
	}*/
	public  T ByteArrayToObject<T>(byte[] byteArray) where T : class
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

}