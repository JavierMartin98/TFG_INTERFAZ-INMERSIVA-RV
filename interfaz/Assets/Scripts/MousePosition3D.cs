using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class MousePosition3D : MonoBehaviour
{
    private InputDevice _targetDevice;
    [SerializeField] private Camera mainCamera;
    //[SerializeField] private GameObject cube;
    public (float, float, float) posicionAreaBusqueda;
    public bool flag1 = true;
    public XRRayInteractor leftInteractorRay;
    public XRRayInteractor RightInteractorRay;
    public GameObject obj;


    void Start()
    {
        TryInitialize();
    }

    void TryInitialize()
    {
        var inputDevices = new List<InputDevice>();
        InputDeviceCharacteristics rightControllerCharacteristics =
            InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;
        InputDevices.GetDevicesWithCharacteristics(rightControllerCharacteristics, inputDevices);

        if (inputDevices.Count == 0)
        {
            return;
        }

        _targetDevice = inputDevices[0];
    }

    void Update()
    {
        if (!_targetDevice.isValid)
        {
            TryInitialize();
        }
        else
        {
            //if (flag1)
            //{
                //Cuando se pulsa el boton
                if (_targetDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryButtonValue) && primaryButtonValue)
                {

                    obj = GameObject.Find("Cylinder(Clone)");
                    posicionAreaBusqueda = (obj.transform.position.x, 100f, obj.transform.position.z);
                    //flag1 = false;
                    GameObject.Find("RightHand Controller").GetComponent<XRInteractorReticleVisual>().enabled = false;
                    //mainCamera.ScreenPointToRay(new Vector3(posicionAreaBusqueda.Item1, posicionAreaBusqueda.Item2, posicionAreaBusqueda.Item3));
                   // Debug.Log("Pressing Primary button");
                }

            //}

        }

    }
}
