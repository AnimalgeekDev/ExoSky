using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Text;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

[System.Serializable]
public class CloudNearby
{
    public double ra;
    public double dec;
    public double parsecs;
    public double visible_distance;
    public double n_stars;
    public double phot_g_mean_mag;
}

// Clase para representar los datos de XYZ
[System.Serializable]
public class StarPosition
{
    public string[] dist_central;
    public string[] DESIGNATION;
    public string[] ra;
    public string[] dec;
    public string[] distance_gspphot;
    public string[] parallax;
    public string[] bp_rp;
    public string[] X;
    public string[] Y;
    public string[] Z;
    public float[] X_sphere;
    public float[] Y_sphere;
    public float[] Z_sphere;
    public float[] radius_sphere;
    public float[] color_r;
    public float[] color_g;
    public float[] color_b;
}

public class GetNearbyStars : MonoBehaviour
{    
    // Referencia al objeto "CenterStars" en la escena
    public Transform centerStars;

    private string apiUrl = "http://127.0.0.1:8000/gaia/nearbystars/"; // Replace with your backend URL

    // Start is called before the first frame update
    void Start()
    {
        Console.WriteLine("Llamando el api");
        GetStarsCloud();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private async void GetStarsCloud()
    {
        // Create the JSON data to send
        string jsonData = JsonUtility.ToJson(new CloudNearby
        { 
            ra = 181.31578544866565,
            dec = 76.90533593519827,
            parsecs = 100.86132547317047,
            visible_distance = 100,
            n_stars = 5000
        });

        // Create a new request to send the JSON to the backend
        using (var client = new HttpClient())
        {
            // Configurar el cliente
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // Crear el contenido de la solicitud
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            try
            {
                // Enviar la solicitud POST
                var response = await client.PostAsync(apiUrl, content);

                // Asegurarse de que la respuesta fue exitosa
                response.EnsureSuccessStatusCode();

                // Leer el contenido de la respuesta
                var responseBody = await response.Content.ReadAsStringAsync();
                Debug.Log("Respuesta de la API: " + responseBody);

                // Parsear el array de posiciones desde el JSON de la respuesta
                StarPosition starPositions = JsonUtility.FromJson<StarPosition>(responseBody);

                int numStars = starPositions.X_sphere.Length;

                Debug.Log("numStars: " + numStars);

                // Crear esferas en las posiciones XYZ
                for (int i = 0; i < numStars; i++)
                {
                    Vector3 position = new Vector3(starPositions.X_sphere[i], starPositions.Y_sphere[i], starPositions.Z_sphere[i]);

                    GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);

                    // Asignar el nombre usando DESIGNATION
                    sphere.name = starPositions.DESIGNATION[i];

                    // Crear una esfera en la posición especificada
                    sphere.transform.position = position;

                    // Ajustar la escala de la esfera usando radius_sphere
                    float radius = starPositions.radius_sphere[i];
                    sphere.transform.localScale = new Vector3(radius, radius, radius);

                    // Cambiar el color de la esfera usando los valores de color_r, color_g, color_b
                    Renderer sphereRenderer = sphere.GetComponent<Renderer>();
                    float r = starPositions.color_r[i] / 255f;  // Normalizar el valor a un rango entre 0 y 1
                    float g = starPositions.color_g[i] / 255f;
                    float b = starPositions.color_b[i] / 255f;
                    sphereRenderer.material.color = new Color(r, g, b);

                    // Hacer de "CenterStars" el padre de la esfera
                    sphere.transform.parent = centerStars;
                }
            }
            catch (HttpRequestException e)
            {
                Debug.Log("Error en la solicitud: " + e.Message);
            }
        }
    }
}