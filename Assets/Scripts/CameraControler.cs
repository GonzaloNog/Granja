using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControler : MonoBehaviour
{
    public float cameraSpeed = 10f; // Velocidad de movimiento de la cámara
    public float edgeSize = 10f;    // Tamaño del borde en píxeles donde se detecta el mouse
    public float maxX = 50f;        // Límite en el eje X de la cámara
    public float maxY = 50f;        // Límite en el eje Y de la cámara
    public float minX = -50f;       // Límite en el eje X en sentido negativo
    public float minY = -50f;       // Límite en el eje Y en sentido negativo

    public float zoomSpeed = 10f;   // Velocidad de zoom
    public float maxZoom = 20f;     // Zoom máximo
    public float minZoom = 5f;      // Zoom mínimo

    void Update()
    {
        if (LevelManager.instance.editorMode)
        {
            Vector3 pos = transform.position;

            // Obtiene la posición del mouse en la pantalla
            Vector3 mousePos = Input.mousePosition;

            // Movimiento horizontal
            if (mousePos.x <= edgeSize)
            {
                pos.x -= cameraSpeed * Time.deltaTime;
            }
            if (mousePos.x >= Screen.width - edgeSize)
            {
                pos.x += cameraSpeed * Time.deltaTime;
            }

            // Movimiento vertical
            if (mousePos.y <= edgeSize)
            {
                pos.y -= cameraSpeed * Time.deltaTime;
            }
            if (mousePos.y >= Screen.height - edgeSize)
            {
                pos.y += cameraSpeed * Time.deltaTime;
            }

            // Limita el movimiento de la cámara dentro de los valores máximos y mínimos
            pos.x = Mathf.Clamp(pos.x, minX, maxX);
            pos.y = Mathf.Clamp(pos.y, minY, maxY);

            // Actualiza la posición de la cámara
            transform.position = pos;

            // Zoom con la ruedita del mouse
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            if (scroll != 0f)
            {
                Camera.main.orthographicSize -= scroll * zoomSpeed;
                Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, minZoom, maxZoom);
            }
        }
    }
}
