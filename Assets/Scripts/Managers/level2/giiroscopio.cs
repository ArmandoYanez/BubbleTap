using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class giiroscopio : MonoBehaviour
{
    private Gyroscope gyro;
    private Quaternion rotCorrection;
    private Vector3 rotation;
    
    void Start()
    {
        // Verifica si el dispositivo soporta giroscopio
        if (SystemInfo.supportsGyroscope)
        {
            gyro = Input.gyro;
            gyro.enabled = true;

            // Ajusta la rotación para alinear el giroscopio con la cámara
            rotCorrection = new Quaternion(0, 0, 1, 0); // Ajusta según sea necesario
        }
        else
        {
            Debug.LogError("El giroscopio no está soportado en este dispositivo.");
        }
    }

    void Update()
    {
        if (gyro != null)
        {
            // Obtén la rotación del giroscopio y ajusta la corrección
            Quaternion gyroRotation = gyro.attitude * rotCorrection;

            // Conviértelo en ángulos de Euler
            rotation = new Vector3(
                Mathf.Clamp(gyroRotation.eulerAngles.x, -90, 90),
                Mathf.Clamp(gyroRotation.eulerAngles.y, -90, 90),
                Mathf.Clamp(gyroRotation.eulerAngles.z, -90, 90)
            );

            // Mueve la burbuja en función de la rotación
            MoveBubble();
        }
    }

    void MoveBubble()
    {
        // Asume que la burbuja está en 2D y se mueve en el plano X-Y
        Vector2 newPosition = new Vector2(rotation.x, rotation.y) * 0.1f;
        transform.position = newPosition;
    }
}
