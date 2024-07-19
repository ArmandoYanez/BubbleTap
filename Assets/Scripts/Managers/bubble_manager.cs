using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// clase Ronda
[System.Serializable]
public class Rounds
{
    public int numberOfBubbles; // Número de burbujas en cada ronda
    
    //Crear contadores de tiempo 
    private float timeP1 = 0;
    private float timeP2 = 0;
}

public class bubble_manager : MonoBehaviour
{
    // Variable publicas
    public int bubbleCount = 0;
    public Camera mainCamera;
    public GameObject bubble;
    public int round = 1;
    public List<Rounds> rounds; // Lista para manejar las rondas
    public int bubblesRemaining; // Burbujas restantes en la ronda actual
    public TextMeshProUGUI timetext;
    public float time = 0;
    
    //Objetos paneles
    [SerializeField] public GameObject Panel; //Panel para desactivar y activar
    [SerializeField] public GameObject Panel2; //Panel para desactivar y activar
    
    //Variables privadas
    private bool estado = false;
    private int currentRound = 0; // Ronda actual
    private bool turnoPlayer1 = true;
    public List<GameObject> BubblesArray;  //Bubble array
    
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
            BubblesArray = new List<GameObject>(); // Inicializar lista
            // Inicializar la primera ronda
            StartRound(currentRound);
            
    }

    // Update is called once per frame
    void Update()
    {
        if (turnoPlayer1)
        {
            CheckBubbles();
            time += Time.deltaTime;
            timetext.text = time.ToString("F3");
        }
        else
        {
            ActivarBorbujasP2(currentRound);
        }
       Debug.Log("Turno player 1: " + turnoPlayer1);
       Debug.Log("Tap to start:" + estado);
    }

    // Generador de burbujas bonitas
    void bubbleGenerator()
    {
        /*
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
        
        //Para audio manager
        //audioSource.enabled = false;
        */
        Vector3 randomPosition = new Vector3(10, 10, 10);
    
             // Calcular los límites visibles de la cámara
            float cameraHeight = 2f * mainCamera.orthographicSize;
            float cameraWidth = cameraHeight * mainCamera.aspect;
            float left = mainCamera.transform.position.x - (cameraWidth / 2) + 1;
            float right = mainCamera.transform.position.x + (cameraWidth / 2) - 1;
            float top = mainCamera.transform.position.y + (cameraHeight / 2) - 1;
            float bottom = mainCamera.transform.position.y - (cameraHeight / 2) + 1;

            bool validPositionFound = false;
            Vector3 worldPosition = Vector3.zero;

            while (!validPositionFound)
            {
                // Generar una posición aleatoria dentro de los límites de la cámara
                float randomX = Random.Range(left, right);
                float randomY = Random.Range(bottom, top);
        
                worldPosition = new Vector3(randomX, randomY, 10);  // Convertir a coordenadas del mundo

                // Verificar si la posición está libre de colisiones
                Collider2D collider = Physics2D.OverlapCircle(worldPosition, bubble.GetComponent<CircleCollider2D>().radius);
                if (collider == null)
                {
                    validPositionFound = true;
                }
            }

            GameObject newBubble =Instantiate(bubble, worldPosition, Quaternion.identity);  // Instanciar el prefab en la posición del mundo
            BubblesArray.Add(newBubble);
            
            AudioSource audioSource = bubble.GetComponent<AudioSource>();
    
            //Para audio manager
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

        void CheckBubbles() {
            // Si no quedan burbujas, pasamos a la siguiente ronda
            if (bubblesRemaining == 0)
            {
                turnoPlayer1 = false;
                if (turnoPlayer1)
                {
                    TapToStart();
                    currentRound++;

                    if (currentRound < rounds.Count) {
                        StartRound(currentRound);
                    }
                    else
                    {
                        Debug.Log("¡Todas las rondas completadas!");
                        //Corutina animacion panel
                    }
                }
            }
         }
        
        // Generar borbujas player 2
        public void ActivarBorbujasP2(int _round)
        {
            TapToStart();
            estado = true;
            bubblesRemaining = rounds[_round].numberOfBubbles;
            
            for (int i = 0; i < bubblesRemaining; i++)
            {
                foreach (GameObject bubbleNew in BubblesArray)
                {
                    Vector3 bubblePosition = bubbleNew.transform.position;
                    Instantiate(bubble, bubblePosition, Quaternion.identity);
                }
            }
            turnoPlayer1 = true;
        }

        // Funciones para boton
        
        // Funcion para cambiar estado del tap to start
        public void TapToStart()
        {
            //Activar panel
            if (!estado)
            {
                //Meter corrutina con animacion de entrada
                Panel.SetActive(true);
            }
            else
            {
                estado = true; //Le dejamos acceder a la funcion generar
            }
        } 
        
        public void TapToStartP2()
        {
            //Activar panel
            if (!estado)
            {
                //Meter corrutina con animacion de entrada
                Panel2.SetActive(true);
            }
            else
            {
                estado = true; //Le dejamos acceder a la funcion generar
            }
        }   
        
        //Iniciar tiempo en 0
        public void TiempoEnCero()
        {
            time = 0;
        }
}
