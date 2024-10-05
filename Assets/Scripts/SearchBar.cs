using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using System.Text;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

public class SearchBar : MonoBehaviour
{
    public InputField searchInputField; // Assign the InputField from the inspector
    public Button searchButton; // Assign the Button from the inspector
    public Text feedbackText; // Text component to display feedback messages (optional)
    private string apiUrl = "http://172.20.10.2:8000/users/login/"; // Replace with your backend URL

    private void Start()
    {
        // Add a listener to the button to call the Search function when clicked
        searchButton.onClick.AddListener(Search);
    }

    private void Search()
    {
        // Check if the input field text is null or empty/whitespace
        if (string.IsNullOrWhiteSpace(searchInputField.text))
        {
            if (feedbackText != null)
            {
                feedbackText.gameObject.SetActive(true);
                feedbackText.text = "You have to introduce some name.";
            }
            return; // Exit the method if the input is invalid
        }

        // Get the text from the InputField
        string searchText = searchInputField.text;

        // Call the function to perform the search
        SearchInBackend(searchText);
    }

    private async void SearchInBackend(string searchText)
    {
        // Optional: Disable the button and show a loading message
        searchButton.interactable = false;
        if (feedbackText != null)
        {
            feedbackText.gameObject.SetActive(true);
            feedbackText.text = "Searching...";
        }

        // Create the JSON data to send
        string jsonData = JsonUtility.ToJson(new { name = searchText });

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

                // Asegúrate de que la respuesta fue exitosa
                response.EnsureSuccessStatusCode();

                // Leer el contenido de la respuesta
                var responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Respuesta de la API: " + responseBody);
                feedbackText.gameObject.SetActive(true);
                feedbackText.text = "Data: " + responseBody;
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("Error en la solicitud: " + e.Message);
                feedbackText.gameObject.SetActive(true);
                feedbackText.text = "Error: " + e.Message;
            }
        }

        // Re-enable the button after the request
        searchButton.interactable = true;
    }
}
