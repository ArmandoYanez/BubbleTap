using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shakeManager : MonoBehaviour
{
    public float shakeThreshold = 2.0f; 
    public float shakeDuration = 0.5f;


    public AudioSource sacudir;
    
    private float lastShakeTime;
    private Vector3 lastAcceleration;

    public int vecesSacundido = 0;
    
    void Update()
    {
        Vector3 acceleration = Input.acceleration;
        
        // Calcula el cambio en la aceleración
        float deltaAcceleration = (acceleration - lastAcceleration).magnitude;

   
        if (deltaAcceleration > shakeThreshold)
        {
            if (Time.time - lastShakeTime > shakeDuration)
            {
                OnShakeDetected();
                lastShakeTime = Time.time;
            }
        }

        lastAcceleration = acceleration;
    }

    public void OnShakeDetected()
    {
        vecesSacundido+= 2;
        sacudir.Play();
        Vibrate();
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
