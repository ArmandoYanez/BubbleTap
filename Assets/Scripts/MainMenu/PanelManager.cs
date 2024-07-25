using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleAndDisappear2D : MonoBehaviour
{
    public GameObject panel; // El panel que queremos escalar y hacer desaparecer
    public float scaleIncrement = 0.1f; // Cantidad en que incrementará la escala
    public float maxScale = 5f; // Escala máxima antes de desaparecer el panel
    public float scaleSpeed = 2f; // Velocidad de escalado

    private Vector3 originalScale; // Para guardar la escala original del panel
    private bool isScaling = false; // Para controlar si estamos escalando

    void Start()
    {
        // Guardamos la escala original del panel
        originalScale = panel.transform.localScale;
    }

    void Update()
    {
        // Si estamos escalando, incrementamos la escala del panel
        if (isScaling)
        {
            panel.transform.localScale += Vector3.one * scaleIncrement * Time.deltaTime * scaleSpeed;

            // Comprobamos si la escala del panel supera la escala máxima
            if (panel.transform.localScale.x > maxScale || panel.transform.localScale.y > maxScale)
            {
                panel.SetActive(false); // Desactivamos el panel si la escala es muy grande
                isScaling = false; // Detenemos el escalado
            }
        }
    }

    // Método que se llamará al pulsar el botón
    public void OnButtonPress()
    {
        isScaling = true; // Activamos el escalado
        panel.SetActive(true); // Aseguramos que el panel esté activo
    }

    // Método opcional para resetear la escala y posición del panel
    public void ResetPanel()
    {
        panel.transform.localScale = originalScale;
        panel.SetActive(true);
        isScaling = false;
    }
}
