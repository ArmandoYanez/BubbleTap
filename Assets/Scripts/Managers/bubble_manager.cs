using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// clase Ronda
[System.Serializable] public class Rounds
{
    public int numberOfBubbles; // Número de burbujas en cada ronda
    
    //Crear contadores de tiempo
    public float timeP1 = 0;
    public float timeP2 = 0;
}

public class bubble_manager : MonoBehaviour
{
    // Variable publicas
    public int bubbleCount;
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
    public int currentRound = 0; // Ronda actual
    private bool turnoPlayer1 = true;
    public List<GameObject> BubblesArray;  //Bubble array
    
    bool tiempoDeEspera = false;
    bool tiempoDeEspera2 = false;
    
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
            
            //Activar panel
            Panel.SetActive(true);
            tiempoDeEspera2 = false;
            
    }

    // Verifica que jugador le toca
    public void CheckPlayer()
    {
        if (turnoPlayer1)
        {
            RondaPlayer1();
        }
        
        else if (!turnoPlayer1)
        {
            RondaPlayer2();
        }
    }

    //Iniciar ronda del player 1
    public void RondaPlayer1()
    {
        CheckBubbles1();
        
    }

    //Iniciar ronda del player 2
    public void RondaPlayer2()
    {
        CheckBubbles2();
        
    }
    
    // Update is called once per frame
    void Update()
    {
        
       if (!(currentRound > 2))
       {            
            CheckPlayer();

            if (!(bubblesRemaining == 0))
            {
                time += Time.deltaTime;
                timetext.text = time.ToString("F3");
            }
            

            Debug.Log("Turno player 1: " + turnoPlayer1);
            Debug.Log("Tap to start:" + estado);
            Debug.Log("Ronda:" + currentRound);
            Debug.Log("Burbujas Restantes:" + bubblesRemaining);
       }     
      
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
            
            newBubble.SetActive(true);
            
            
            // Obtener el material del prefab original
            Renderer renderer = bubble.GetComponent<Renderer>();
            Material originalMaterial = renderer.sharedMaterial;

            // Asignar el mismo material al nuevo objeto instanciado
            Renderer newRenderer = newBubble.GetComponent<Renderer>();
            newRenderer.sharedMaterial = originalMaterial;
            
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
            
            for (int i = 0; i < bubblesRemaining; i++){
                GameObject bubbleNew = BubblesArray[i];
                bubbleNew.GetComponent<CircleCollider2D>().enabled = false;
            }
            
            Debug.Log("Ronda " + (_round + 1) + " con " + bubblesRemaining + " burbujas.");

        }
        else
        {
            Debug.Log("¡Rondas completadas!");
            
        }
    }

        void CheckBubbles1() {
            // Si no quedan burbujas, pasamos a la siguiente ronda
            if (bubblesRemaining == 0)
            {
                StartCoroutine(EsperamosTiempoTap2());
                rounds[currentRound].timeP1 = time;
                
                //Paramos el texto y lo cambiamos de color
                timetext.text = rounds[currentRound].timeP1.ToString("F3");
                timetext.color = new Color32(119,221,119, 255);

                    if (tiempoDeEspera2)
                    {
                        TapToStartP2();
                        turnoPlayer1 = false;
                        timetext.color = Color.white;
                        
                        if (currentRound <= rounds.Count) {
                            ActivarBorbujasP2(currentRound);
                        }
                        else
                        {
                            Debug.Log("¡Todas las rondas P1 completadas!");
                            //Corutina animacion panel
                        }
                        
                    }
            }
         }

        void CheckBubbles2() {
            // Si no quedan burbujas, pasamos a la siguiente ronda
            if (bubblesRemaining == 0)
            {
                StartCoroutine(EsperamosTiempoTap1());
                rounds[currentRound].timeP2 = time;
                
                //Paramos el texto y lo cambiamos de color
                timetext.text = rounds[currentRound].timeP2.ToString("F3");
                timetext.color = new Color32(119,221,119, 255);
                
                
                if (tiempoDeEspera)
                {
                    TapToStartP1();
                    currentRound++;
                    turnoPlayer1 = true;
                    timetext.color = Color.white;
                    
                    BubblesArray.Clear();

                    if (currentRound <= rounds.Count) {
                        StartRound(currentRound);
                    }
                    else
                    {
                        Debug.Log("¡Todas las rondas P2 completadas!");
                        //Corutina animacion panel
                    }
                  
                }
            }
         }
        
        /*// Generar borbujas player 2
        public void ActivarBorbujasP2(int _round)
        {
            bubblesRemaining = rounds[_round].numberOfBubbles;
            
                foreach (GameObject bubbleNew in BubblesArray)
                {
                    Vector3 bubblePosition = bubbleNew.transform.position;
                    BubblesArray.Add(bubbleNew);
                    Instantiate(bubble, bubblePosition, Quaternion.identity);
                }
        }
        */

        public void ActivarBorbujasP2(int _round)
        {
            bubblesRemaining = rounds[_round].numberOfBubbles;

            int cantidad = BubblesArray.Count;
            
                for (int i = 0; i < cantidad; i++)
                {
                GameObject bubbleNew = BubblesArray[i];
                
                Vector3 bubblePosition = bubbleNew.transform.position;
                GameObject newBubble = Instantiate(bubble, bubblePosition, Quaternion.identity);
                BubblesArray.Add(newBubble);
                    
                BubblesArray[i+rounds[_round].numberOfBubbles].SetActive(true);
            
                // Obtener el material del prefab original
                Renderer renderer = bubble.GetComponent<Renderer>();
                Material originalMaterial = renderer.sharedMaterial;

                // Asignar el mismo material al nuevo objeto instanciado
                Renderer newRenderer = bubbleNew.GetComponent<Renderer>();
                newRenderer.sharedMaterial = originalMaterial;
            
                AudioSource audioSource = bubble.GetComponent<AudioSource>();
                }
                
                for (int i = 0; i < cantidad * 2; i++)
                {
                    GameObject bubbleNew = BubblesArray[i];
                    bubbleNew.GetComponent<CircleCollider2D>().enabled = false;
                }
                
        }

        IEnumerator EsperamosTiempoTap1()
        {
            // Esperar 0.5 segundos
            yield return new WaitForSeconds(1f);
            tiempoDeEspera = true;
            
        }
        
        IEnumerator EsperamosTiempoTap2()
        {
            // Esperar 0.5 segundos
            yield return new WaitForSeconds(1f);
            tiempoDeEspera2 = true;

        }
        
        // Funciones para boton
        
        // Funcion para cambiar estado del tap to start
    
        public void TapToStartP1()
        {
            foreach (var bubbleCollider in BubblesArray)
            {
                bubbleCollider.GetComponent<CircleCollider2D>().enabled = true;
            }
            
            //Activar panel
                Panel.SetActive(true);
                tiempoDeEspera2 = false;
        } 
        
        public void TapToStartP2()
        {
            foreach (var bubbleCollider in BubblesArray)
            {
                bubbleCollider.GetComponent<CircleCollider2D>().enabled = true;
            }
            
            //Activar panel
                Panel2.SetActive(true);
                tiempoDeEspera = false;
        }   
        
        //Iniciar tiempo en 0
        public void TiempoEnCero()
        {
            time = 0;
        }
}
