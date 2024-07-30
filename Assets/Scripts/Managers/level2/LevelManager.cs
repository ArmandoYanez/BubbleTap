using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class LevelManager : MonoBehaviour
{
    public float tiempop1 = 0;
    public float tiempop2 = 0;


    public GameObject ObjetoManager;

    public Animator animacionBorbuja;
    public Animator animacionBorbuja2;
    
    public GameObject Bubble;
    public GameObject Bubble2;
    
    //Numero de ronda
    private int ronda = 0;
    
    //Para tiempo
    public TextMeshProUGUI timetext;
    public float time = 0;

    //Para la barra de reventamiento
    public shakeManager manager;
    public Image Barra;
    public Image Barra2;
    
    //Boton 2 p2 
    public GameObject panel2;
    
    
    //ParaAnimacion
    public AnimationController animatorSc;
    private bool turno = true;
    
    
    //Sounds
    public AudioSource nice;
    private bool unaSolaVez = true;


    public winnerManager winerSc;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (ronda == 1)
        {
            if (Barra.fillAmount < 1)
            {
                //Actualizamos el tiempo
                time += Time.deltaTime;
                timetext.text = time.ToString("F3") + ("s");
                
                Barra.fillAmount = (manager.vecesSacundido * 0.01f);
            }else if (Barra.fillAmount == 1)
            {
                timetext.color = new Color32(119,221,119, 255);
                if (unaSolaVez)
                {
                    nice.Play();
                    unaSolaVez = false;
                    
                    tiempop1 = time;
                }
                

                StartCoroutine(popDeBorbuja());
                StartCoroutine(TiempoDeEspera());
                
            }
        }else if (ronda == 2)
        {
            if (Barra2.fillAmount < 1)
            {
                //Actualizamos el tiempo
                time += Time.deltaTime;
                timetext.text = time.ToString("F3") + ("s");
                
                Barra2.fillAmount = (manager.vecesSacundido * 0.01f);
            }else if (Barra2.fillAmount == 1)
            {
                timetext.color = new Color32(119,221,119, 255);
                if (unaSolaVez)
                {
                    nice.Play();
                    unaSolaVez = false;
                }
                tiempop2 = time;

                StartCoroutine(popDeBorbuja2());
                StartCoroutine(Esperar());
            }
        }
    }

    public void ShakeRoundP1()
    {
        //Esto se hara despues de que la pantalla de p1 de tap to strat sea precionada
        ObjetoManager.SetActive(true);
        ronda = 1;
    }
    
    public void ShakeRoundP2()
    {
        //Esto se hara despues de que la pantalla de p1 de tap to strat sea precionada
        ObjetoManager.SetActive(true);
        Bubble2.SetActive(true);
        unaSolaVez = true;
        Barra2.enabled = true;
        ronda = 2;
    }
    
    private IEnumerator popDeBorbuja()
    {
        animacionBorbuja.Play("explosion");
        yield return new WaitForSeconds(animacionBorbuja.GetCurrentAnimatorClipInfo(0).Length + 0.05f);
        Bubble.SetActive(false);
        Destroy(Barra);
       
        manager.vecesSacundido = 0;
    }

    private IEnumerator popDeBorbuja2()
    {
        animacionBorbuja2.Play("explosion");
        yield return new WaitForSeconds(animacionBorbuja2.GetCurrentAnimatorClipInfo(0).Length + 0.05f);
        Bubble2.SetActive(false);
    }
    
    private IEnumerator Esperar()
    {
        yield return new WaitForSeconds(1f);
        winerSc.tp1 = tiempop1;
        winerSc.tp2 = tiempop2;
        winerSc.MostrarWinnerPanel();
    }
    
    public IEnumerator TiempoDeEspera()
    {
        animacionBorbuja.Play("explosion");
        yield return new WaitForSeconds(0.5f);
        if (turno)
        {
            StartCoroutine(AnimacionOlas());
            ObjetoManager.SetActive(false);
            time = 0;
        }
      
    }
    
    public IEnumerator AnimacionOlas()
    {
        animatorSc.playAnimation();
        yield return new WaitForSeconds(0.35f);
        panel2.SetActive(true);
        turno = false;
    }
}


