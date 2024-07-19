using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class bubble : MonoBehaviour
{
    // Variables publicas
  
    
    //Variables privadas 
    private Collider2D collider;
    private Animator animator;
    private AudioSource audio;
    
    private void Start()
    {
        collider = GetComponent<Collider2D>(); // Accedemos al componente collider
        animator = GetComponent<Animator>(); // Accedemos al componente animator
        audio = GetComponent<AudioSource>(); // Accedemos al componente audiosource
    }

    private void Update()
    {
        // Verificar si hay almenos un toque en la pantalla
        if (Input.touchCount > 0)
        {
            // recorrer en todos los toques detectados
            for (int i = 0; i < Input.touchCount; i++)
            {
                // obtener el toque
                Touch touch = Input.GetTouch(i);

                // verificamos si el toque fue comnezado (TouchPhase.Began)
                if (touch.phase == TouchPhase.Began)
                {
                    // Obtener la posición del toque en la pantalla
                    Vector2 touchPos = touch.position;

                    // Convertir la posición del toque a coordenadas del mundo
                    Vector3 worldTouchPos = Camera.main.ScreenToWorldPoint(new Vector3(touchPos.x, touchPos.y, 10f));
                    
                    if (collider != null && collider.OverlapPoint(worldTouchPos))
                    {
                        StartCoroutine(AnimacionExplosion());
                    }
                }
            }
        }
    }

    private IEnumerator AnimacionExplosion()
    {
        // Iniciamos la animacion 
        animator.Play("explosion");
        
        // Desactivamos collider
        collider.enabled = false;
        
        // Reproduciomos su audiosoruce
        audio.pitch = (Random.Range(0.6f, 1.5f));
        audio.Play();
        
        //Sumamos la burbuja al manager
        bubble_manager.Instance.bubbleCount++;
        bubble_manager.Instance.bubblesRemaining--;
        
        // Esperamos hasta que la animacion termine para continuar 
        yield return new WaitForSeconds(animator.GetCurrentAnimatorClipInfo(0).Length + 0.05f);
        
        //Desactivamos el objeto
        gameObject.SetActive(false);
    }
}
