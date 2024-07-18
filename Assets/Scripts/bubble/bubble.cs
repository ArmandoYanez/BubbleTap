using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class bubble : MonoBehaviour
{
    
    //Variables privadas 
    private Collider2D collider;
    private Animator animator;
    
    private void Start()
    {
        collider = GetComponent<Collider2D>(); // Accedemos al componente collider
        animator = GetComponent<Animator>(); // Accedemos al componente animator
    }

    private void Update()
    {
        // Verificar si hay almenos un toque en la pantalla
        if (Input.touchCount > 0)
        {
            // recorrer en todos los toques detectados
            for (int i = 0; i < Input.touchCount; i++)
            {
                // pbtener el toque
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
        
        // Esperamos hasta que la animacion termine para continuar 
        yield return new WaitForSeconds(animator.GetCurrentAnimatorClipInfo(0).Length);
        
        //Desactivamos el objeto
        gameObject.SetActive(false);
    }
}
