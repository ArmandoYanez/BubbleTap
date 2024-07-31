using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine;

public class winnerManager : MonoBehaviour
{
    public GameObject panelWinner;

    public GameObject panel1;
    public GameObject panel2;

    public TextMeshProUGUI textp1;
    public TextMeshProUGUI textp2;

    public float tp1;
    public float tp2;

    public AudioSource music;
    public AudioSource win;


    public bool oneTime = true;
    
    public void MostrarWinnerPanel()
    {
        LeanTween.scale(panelWinner.GetComponent<RectTransform>(), new Vector3(1, 1, 1), 0.5f);

        if (tp1 > tp2)
        {
            textp1.text = tp1.ToString("F3") + ("s");
            
            textp2.text = tp2.ToString("F3") + ("s");
            textp2.color = new Color32(119,221,119, 255);
            StartCoroutine(mostrarGanadorP2());
        }else if (tp2 > tp1)
        {
            textp2.text = tp2.ToString("F3") + ("s");
            textp1.text = tp1.ToString("F3") + ("s");
            textp1.color = new Color32(119,221,119, 255);
            StartCoroutine(mostrarGanadorP1());
        }
    }

    public IEnumerator mostrarGanadorP1()
    { 
        yield return new WaitForSeconds(2f);
        music.Stop();
        if (oneTime)
        {
            win.Play();
            oneTime = false;
        }
        LeanTween.scale(panel1.GetComponent<RectTransform>(), new Vector3(1, 1, 1), 0.5f);
    }
    
    public IEnumerator mostrarGanadorP2()
    { 
        yield return new WaitForSeconds(2f);
        music.Stop();
        if (oneTime)
        {
            win.Play();
            oneTime = false;
        }
        LeanTween.scale(panel2.GetComponent<RectTransform>(), new Vector3(1, 1, 1), 0.5f);
    }
    
    
    public void menuPrincipal()
    {
        SceneManager.LoadScene(0);
    }
}
