using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class DisplayUsername : MonoBehaviour
{
    public Text usernameText;
    public Text planet_1;
    public Text planet_2;
    public Text planet_3;
    public Text planet_4;
    public Text planet_5;
    public Text panel_1;
    public Text panel_2;
    public Text panel_3;
    public Text panel_4;
    public Text panel_5;
    private string url = "http://172.20.10.2:8000/exoplanets/getrandomfrombd/";

    private void Start()
    {
        // Obtener el username de PlayerPrefs y mostrarlo
        usernameText.text = PlayerPrefs.GetString("Username", "Guest"); // Default to "Guest" si no se encuentra un username
        LoadData();
    }

    public void LoadData()
    {
        StartCoroutine(LoadExoplanets());
    }

    private IEnumerator LoadExoplanets()
    {
        // Crear el objeto JSON usando un Dictionary
        var jsonData = new Dictionary<string, int>
        {
            { "limit", 5 }
        };
        string json = JsonUtility.ToJson(jsonData);

        // Crear la solicitud POST
        using (UnityWebRequest request = new UnityWebRequest(url, "POST"))
        {
            byte[] bodyRaw = new System.Text.UTF8Encoding().GetBytes(json);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            // Enviar la solicitud y esperar la respuesta
            yield return request.SendWebRequest();

            // Manejar errores
            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error: " + request.error);
                Debug.LogError("Response: " + request.downloadHandler.text);
            }
            else
            {
                // Deserializar el JSON a una lista de objetos Exoplanet
                Exoplanet[] exoplanets = JsonHelper.FromJson<Exoplanet>(request.downloadHandler.text);

                // Asegurarnos de que tengamos al menos 5 planetas
                if (exoplanets.Length >= 5)
                {
                    Debug.Log(exoplanets[0].pl_name);
                    planet_1.text = exoplanets[0].pl_name;
                    planet_2.text = exoplanets[1].pl_name;
                    planet_3.text = exoplanets[2].pl_name;
                    planet_4.text = exoplanets[3].pl_name;
                    planet_5.text = exoplanets[4].pl_name;

                    panel_1.text = "Disc. Year: " + exoplanets[0].disc_year + "\n" +
                                    "Disc. Method: " + exoplanets[0].discoverymethod + "\n" +
                                     "D. F.: " + exoplanets[0].disc_facility + "\n" +
                                    "ra: " + exoplanets[0].ra + "\n" +
                                    "dec" + exoplanets[0].dec + "\n";

                    panel_2.text = "Disc. Year: " + exoplanets[1].disc_year + "\n" +
                                    "Disc. Method: " + exoplanets[1].discoverymethod + "\n" +
                                     "D. F.: " + exoplanets[1].disc_facility + "\n" +
                                    "ra: " + exoplanets[1].ra + "\n" +
                                    "dec" + exoplanets[1].dec + "\n";

                    panel_3.text =  "Disc. Year: " + exoplanets[2].disc_year + "\n" +
                                    "Disc. Method: " + exoplanets[2].discoverymethod + "\n" +
                                     "D. F.: " + exoplanets[2].disc_facility + "\n" +
                                    "RA: " + exoplanets[2].ra + "\n" +
                                    "DEC: " + exoplanets[2].dec + "\n";

                    panel_4.text = "Disc. Year: " + exoplanets[3].disc_year + "\n" +
                                    "Disc. Method: " + exoplanets[3].discoverymethod + "\n" +
                                     "D. F.: " + exoplanets[3].disc_facility + "\n" +
                                    "ra: " + exoplanets[3].ra + "\n" +
                                    "dec" + exoplanets[3].dec + "\n";

                    panel_5.text = "Disc. Year: " + exoplanets[4].disc_year + "\n" +
                                    "Disc. Method: " + exoplanets[4].discoverymethod + "\n" +
                                    "D. F.: " + exoplanets[4].disc_facility + "\n" +
                                    "ra: " + exoplanets[4].ra + "\n" +
                                    "dec" + exoplanets[4].dec + "\n";
                }
                else
                {
                    Debug.LogError("No se recibieron suficientes planetas en la respuesta.");
                }

                Debug.Log("Respuesta exoplantes: " + request.downloadHandler.text);
            }
        }
    }
}
