using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public float speed = 1.0f;       // Velocidad de movimiento vertical
    public float height = 0.5f;      // Altura m�xima del movimiento vertical
    public float rotationSpeed = 50.0f;  // Velocidad de rotaci�n

    private Vector3 startPosition;

    void Start()
    {
        // Guardar la posici�n inicial del objeto
        startPosition = transform.position;
    }

    void Update()
    {
        // Movimiento de subida y bajada usando una funci�n seno
        float newY = Mathf.Sin(Time.time * speed) * height;
        transform.position = new Vector3(startPosition.x, startPosition.y + newY, startPosition.z);

        // Rotaci�n sobre el eje local Y (ajuste para el objeto "acostado")
        transform.Rotate(new Vector3(rotationSpeed * Time.deltaTime, 0,0));
    }
}
