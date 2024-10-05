using UnityEngine;

public class RotateGlobe : MonoBehaviour
{
    public float rotationSpeed = 100f; // Velocidad de rotación ajustable
    private bool isDragging = false; // Indica si el usuario está arrastrando con el ratón

    void Update()
    {
        // Detectar cuando el botón izquierdo del ratón se presiona
        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
        }

        // Detectar cuando el botón izquierdo del ratón se suelta
        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }

        // Si el usuario está arrastrando
        if (isDragging)
        {
            // Obtener el movimiento del ratón en el eje X
            float mouseX = Input.GetAxis("Mouse X");

            // Rotar la esfera alrededor del eje Y basado en el movimiento del ratón
            transform.Rotate(Vector3.up, -mouseX * rotationSpeed * Time.deltaTime, Space.World);
        }
    }
}
