using UnityEngine;

public class RotateGlobe : MonoBehaviour
{
    public float rotationSpeed = 100f; // Velocidad de rotaci�n ajustable
    private bool isDragging = false; // Indica si el usuario est� arrastrando con el rat�n

    void Update()
    {
        // Detectar cuando el bot�n izquierdo del rat�n se presiona
        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
        }

        // Detectar cuando el bot�n izquierdo del rat�n se suelta
        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }

        // Si el usuario est� arrastrando
        if (isDragging)
        {
            // Obtener el movimiento del rat�n en el eje X
            float mouseX = Input.GetAxis("Mouse X");

            // Rotar la esfera alrededor del eje Y basado en el movimiento del rat�n
            transform.Rotate(Vector3.up, -mouseX * rotationSpeed * Time.deltaTime, Space.World);
        }
    }
}
