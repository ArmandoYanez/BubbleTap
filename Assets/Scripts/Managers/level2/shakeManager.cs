using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shakeManager : MonoBehaviour
{
    public float shakeThreshold = 2.0f; // Umbral de aceleración para detectar sacudidas
    public float shakeDuration = 0.5f; // Duración mínima entre sacudidas para contar

    public AudioSource sacudir; // Fuente de audio para reproducir sonido al sacudir

    private float lastShakeTime; // Tiempo del último registro de sacudida
    private Vector3 lastAcceleration; // Última aceleración registrada

    public int vecesSacudido = 0; // Conteo de sacudidas

    void Update()
    {
        Vector3 acceleration = Input.acceleration;
        
        // Calcula el cambio en la aceleración
        float deltaAcceleration = (acceleration - lastAcceleration).magnitude;

        if (deltaAcceleration > shakeThreshold)
        {
            if (Time.time - lastShakeTime > shakeDuration)
            {
                OnShakeDetected(deltaAcceleration); // Pasa el deltaAcceleration a OnShakeDetected
                lastShakeTime = Time.time;
            }
        }

        lastAcceleration = acceleration;
    }

    public void OnShakeDetected(float deltaAcceleration)
    {
        // Incrementa puntos basados en la magnitud de la aceleración
        // Puedes ajustar este valor o la fórmula según tus necesidades
        vecesSacudido += Mathf.RoundToInt(deltaAcceleration); // Incrementa puntos proporcionalmente a la magnitud de la aceleración
        
        sacudir.Play(); // Reproduce sonido de sacudida
        Vibrate(); // Activa vibración si es soportada
    }
    
    public void Vibrate()
    {
        // Verifica si el dispositivo soporta vibración
        if (SystemInfo.supportsVibration)
        {
            // Activa la vibración
            Handheld.Vibrate();
        }
        else
        {
            Debug.LogWarning("El dispositivo no soporta vibración.");
        }
    }
}
