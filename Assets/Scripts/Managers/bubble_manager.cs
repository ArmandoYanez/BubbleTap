using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// clase Ronda
[System.Serializable]
public class Rounds
{
    
    public int numberOfBubbles; // Número de burbujas en cada ronda
}

public class bubble_manager : MonoBehaviour
{
    // Variable publicas
    public int bubbleCount = 0;
    public Camera mainCamera;
    public GameObject bubble;
    public int round = 1;
    public List<Rounds> rounds; // Lista para manejar las rondas
    private int currentRound = 0; // Ronda actual
    public int bubblesRemaining; // Burbujas restantes en la ronda actual
    
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
        // Inicializar la primera ronda
        StartRound(currentRound);
    }

    // Update is called once per frame
    void Update()
    {
        CheckBubbles();
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

        void StartRound(int _round)
    {
        if (_round < rounds.Count)
        {
            bubblesRemaining = rounds[_round].numberOfBubbles;
            // Generamos las burbujas segun la ronda
            for (int i = 0; i < bubblesRemaining; i++){
                bubbleGenerator();
            }
            Debug.Log("Ronda " + (_round + 1) + " con " + bubblesRemaining + " burbujas.");
        }
        else
        {
            Debug.Log("¡Rondas completadas!");
        }
    }

        void CheckBubbles()
    {
            // Si no quedan burbujas, pasamos a la siguiente ronda
            if (bubblesRemaining == 0)
            {
                currentRound++;
                if (currentRound < rounds.Count)
                {
                    StartRound(currentRound);
                }
                else
                {
                    Debug.Log("¡Todas las rondas completadas!");
                }
            }
    }
    
}
