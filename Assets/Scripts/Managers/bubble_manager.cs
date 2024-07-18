using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bubble_manager : MonoBehaviour
{
    // Variable publicas
    public int bubbleCount = 0;
    public Camera mainCamera;
    public GameObject bubble;
    public int round = 1;
    
    //Singleton
    public static bubble_manager Instance { get; private set; }

    private void Awake()
    {
        //Verificacion del singleton
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bubbleGenerator();
    }

    // Generador de burbujas bonitas
    void bubbleGenerator()
    {
        Vector3 randomPosition = new Vector3(10, 10, 10);
       
        // Calcular los límites visibles de la cámara
        float cameraHeight = 2f * mainCamera.orthographicSize;
        float cameraWidth = cameraHeight * mainCamera.aspect;
        
        float left = mainCamera.transform.position.x - (cameraWidth / 2) + 1;
        float right = mainCamera.transform.position.x + (cameraWidth / 2) - 1;
        float top = mainCamera.transform.position.y + (cameraHeight / 2) - 1;
        float bottom = mainCamera.transform.position.y - (cameraHeight / 2) + 1;
        
        // Generar una posición aleatoria dentro de los límites de la cámara
        float randomX = Random.Range(left, right);
        float randomY = Random.Range(bottom, top);
        
        
        Vector3 worldPosition = new Vector3(randomX, randomY, 10);  // Convertir a coordenadas del mundo
        
        Instantiate(bubble, worldPosition, Quaternion.identity);  // Instanciar el prefab en la posición del mundo
        
        AudioSource audioSource = bubble.GetComponent<AudioSource>();
        //audioSource.enabled = false;
    }
    
}
