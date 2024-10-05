using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class TextClickHandler : MonoBehaviour, IPointerClickHandler
{
    // The number to pass to the next scene
    public int elementNumber; // Set this in the Inspector for each text object

    // This method is called when the text is clicked
    public void OnPointerClick(PointerEventData eventData)
    {
        // Call the method to load the next scene and pass the number
        LoadNextScene(elementNumber);
    }

    private void LoadNextScene(int number)
    {
        // Store the number in PlayerPrefs or a static variable to access in the next scene
        Console.WriteLine(number);
        PlayerPrefs.SetInt("PassedNumber", number);
        PlayerPrefs.Save(); // Save PlayerPrefs

        // Load the next scene (replace "NextSceneName" with the actual name of your scene)
        SceneManager.LoadScene("NextSceneName");
    }
}
