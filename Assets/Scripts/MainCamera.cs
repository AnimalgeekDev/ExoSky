using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    [Header("Movement Settings")]
    public float mainSpeed = 100.0f;  // Velocidad regular
    public float shiftAdd = 250.0f;   // Aumenta al mantener Shift
    public float maxShift = 1000.0f;  // Velocidad m�xima con Shift
    public float camSens = 0.25f;     // Sensibilidad del rat�n
    /*
    [Header("Zoom Settings")]
    public float zoomSpeed = 10f;     // Velocidad del zoom
    public float minZoom = 20f;       // Valor m�nimo del campo de visi�n (Zoom in)
    public float maxZoom = 60f;       // Valor m�ximo del campo de visi�n (Zoom out)
    */
    private Vector3 lastMouse = new Vector3(255, 255, 255); // Posici�n inicial del rat�n
    private float totalRun = 1.0f;
    private Camera cam;               // Referencia a la c�mara

    void Start()
    {
        cam = GetComponent<Camera>(); // Obtener la referencia a la c�mara
    }

    void Update()
    {
        // Controlar la rotaci�n con el rat�n
        lastMouse = Input.mousePosition - lastMouse;
        lastMouse = new Vector3(-lastMouse.y * camSens, lastMouse.x * camSens, 0);
        lastMouse = new Vector3(transform.eulerAngles.x + lastMouse.x, transform.eulerAngles.y + lastMouse.y, 0);
        transform.eulerAngles = lastMouse;
        lastMouse = Input.mousePosition;

        // Controlar el movimiento con las teclas
        float f = 0.0f;
        Vector3 p = GetBaseInput();
        if (p.sqrMagnitude > 0)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                totalRun += Time.deltaTime;
                p = p * totalRun * shiftAdd;
                p.x = Mathf.Clamp(p.x, -maxShift, maxShift);
                p.y = Mathf.Clamp(p.y, -maxShift, maxShift);
                p.z = Mathf.Clamp(p.z, -maxShift, maxShift);
            }
            else
            {
                totalRun = Mathf.Clamp(totalRun * 0.5f, 1f, 1000f);
                p = p * mainSpeed;
            }

            p = p * Time.deltaTime;
            Vector3 newPosition = transform.position;
            if (Input.GetKey(KeyCode.Space))
            {
                // Moverse solo en los ejes X y Z
                transform.Translate(p);
                newPosition.x = transform.position.x;
                newPosition.z = transform.position.z;
                transform.position = newPosition;
            }
            else
            {
                transform.Translate(p);
            }
        }

        // Controlar el zoom con la rueda del rat�n
        //HandleZoom();
    }

    private Vector3 GetBaseInput()
    {
        Vector3 p_Velocity = new Vector3();
        if (Input.GetKey(KeyCode.W))
        {
            p_Velocity += new Vector3(0, 0, 1);
        }
        if (Input.GetKey(KeyCode.S))
        {
            p_Velocity += new Vector3(0, 0, -1);
        }
        if (Input.GetKey(KeyCode.A))
        {
            p_Velocity += new Vector3(-1, 0, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            p_Velocity += new Vector3(1, 0, 0);
        }
        return p_Velocity;
    }
    /*
    private void HandleZoom()
    {
        // Detectar la rueda del rat�n para ajustar el campo de visi�n (FOV) de la c�mara
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0.0f)
        {
            cam.fieldOfView -= scroll * zoomSpeed;
            cam.fieldOfView = Mathf.Clamp(cam.fieldOfView, minZoom, maxZoom); // Limitar el FOV entre minZoom y maxZoom
        }
    }
    */
}
