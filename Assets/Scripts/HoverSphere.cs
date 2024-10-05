using UnityEngine;
using UnityEngine.EventSystems;

public class HoverTextLabel : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // Reference to the panel that will be shown when hovering
    public GameObject panel;

    // Method called when the mouse enters the text label
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (panel != null)
        {
            panel.SetActive(true); // Show the panel when mouse hovers over the text label
        }
    }

    // Method called when the mouse exits the text label
    public void OnPointerExit(PointerEventData eventData)
    {
        if (panel != null)
        {
            panel.SetActive(false); // Hide the panel when mouse leaves the text label
        }
    }
}
