using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

[Serializable]
public class Protocolo
{
	/*Argumentos para llamada a bomberos*/
	public bool callfirefighters { get; set; }
	public bool flagFireFighters { get; set; }
	public bool flagAreaBusqueda { get; set; }
	public bool houseLocation { get; set; }
	public bool fireLocation { get; set; }
	public bool nearLocation { get; set; }
	public (float, float, float) posicionAreaBusqueda { get; set; }
	public int numAcciones { get; set; }
	public bool house1 { get; set; }
	public bool house2 { get; set; }
	public bool house3 { get; set; }
	public bool house4 { get; set; }
	public bool house5 { get; set; }
	public bool house6 { get; set; }
	public bool house7 { get; set; }
	public bool finish { get; set; }
	public bool fire { get; set; }
	public (float, float, float) posicionIncendio { get; set; }

	//public List<Vector3> houseList { get; set; }

	//public (float, float, float) infIzq { get; set; }
	//public (float, float, float) supDcha { get; set; }
	//public (float, float, float) infDcha { get; set; }
}

