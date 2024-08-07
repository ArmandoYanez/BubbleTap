using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class winne_manager : MonoBehaviour
{
    // Variables publicas
    public bubble_manager BubbleManager;

    
    public TextMeshProUGUI[]  P1list;
    public TextMeshProUGUI[]  P2list;

    //Para acceder a los audios source
    public AudioSource musicbg;
    public AudioSource winnerSoundEfect;
    
    //Paneles de winner
    public GameObject winnerPanel1;
    public GameObject winnerPanel2;
    
    // Variables privadas
    public float[] player1;
    public float[] player2;
    int flag = 0;

    private int puntosp1 = 0;
    private int puntosp2 = 0;
    
    public void rondasterminadas()
    {
        if (puntosp1 > puntosp2)
        {
            musicbg.Stop();
            winnerPanel1.SetActive(true);
            //transition.Play();
            LeanTween.scale(winnerPanel1.GetComponent<RectTransform>(), new Vector3(1, 1, 1), 0.5f);
            winnerSoundEfect.Play();
            //winnerPanel1.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        }else if (puntosp2 > puntosp1)
        {
            musicbg.Stop();
            winnerPanel2.SetActive(true);
            //transition.Play();
            LeanTween.scale(winnerPanel2.GetComponent<RectTransform>(), new Vector3(1, 1, 1), 0.5f);
            winnerSoundEfect.Play();
            //winnerPanel2.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        // Inicializamos los arrays con el tamaño necesario
        player1 = new float[P1list.Length];
        player2 = new float[P2list.Length];
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var newText in P1list)
        {
            player1[flag] = BubbleManager.rounds[flag].timeP1;
            player2[flag] =  BubbleManager.rounds[flag].timeP2;
            flag++;
        }
        flag = 0; //Reinciamos bandera
        
        for (int i = 0; i <= 2; i++)
        {
            if (player1[i] < player2[i] && player2[i] > 0)
            {
                P1list[i].color = new Color32(119,221,119, 255);
                P2list[i].color = Color.white;
                puntosp1++;
            }
            else if (player1[i] > player2[i])
            {
                P2list[i].color = new Color32(119,221,119, 255);
                P1list[i].color = Color.white;
                puntosp2++;
            }
            
            P1list[i].text = player1[i].ToString("F3");
            P2list[i].text = player2[i].ToString("F3");
        }
    }

    public void menuPrincipal()
    {
        SceneManager.LoadScene(0);
    }
}
