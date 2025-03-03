using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class menuAnim : MonoBehaviour
{

    public GameObject panelGameModes;
    public GameObject mainMenu;
    public GameObject panelCredits;
    
    public GameObject bidof;
    public GameObject tupper;

    // Dicionario para juardar las burbujas por ronda
    private Dictionary<int, (int round1, int round2, int round3)> Difficulties;

    // Start is called before the first frame update
    void Start()
    {
        // Definir las burbujas de cada ronda
        Difficulties = new Dictionary<int, (int, int, int)>
        {
            { 1, (1, 2, 4) },  // Facil 1
            { 2, (3, 5, 7) },  // Medio 2
            { 3, (6, 8, 10) }, // Dificil 3
        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenGameModes()
    {
        LeanTween.scale(panelGameModes.GetComponent<RectTransform>(), new Vector3(1, 1, 1), 0.5f);
        LeanTween.scale(mainMenu.GetComponent<RectTransform>(), new Vector3(0, 0, 0), 0.5f);
    }
    
    public void OpenCredits()
    {
        LeanTween.scale(panelCredits.GetComponent<RectTransform>(), new Vector3(1, 1, 1), 0.5f);
        LeanTween.scale(mainMenu.GetComponent<RectTransform>(), new Vector3(0, 0, 0), 0.5f);
    }
    
    public void CloseGameModes()
    {
        LeanTween.scale(panelGameModes.GetComponent<RectTransform>(), new Vector3(0, 0, 0), 0.5f);
        LeanTween.scale(mainMenu.GetComponent<RectTransform>(), new Vector3(1, 1, 1), 0.5f);
    }
    
    public void CloseCredits()
    {
        LeanTween.scale(panelCredits.GetComponent<RectTransform>(), new Vector3(0, 0, 0), 0.5f);
        LeanTween.scale(mainMenu.GetComponent<RectTransform>(), new Vector3(1, 1, 1), 0.5f);
    }

    public void LlevarAClasic(int difficulty)
    {

        if (Difficulties.ContainsKey(difficulty))
        {
            var data = Difficulties[difficulty];
            // Guardar los valores en PlayerPrefs
            PlayerPrefs.SetInt("Round1", data.round1);
            PlayerPrefs.SetInt("Round2", data.round2);
            PlayerPrefs.SetInt("Round3", data.round3);
            PlayerPrefs.Save(); // Guardar cambios
            SceneManager.LoadScene(1);
        }
        else
        {
            Debug.LogError($"El nivel {difficulty} no existe en el diccionario.");
        }
    }
    
    public void LlevarAShaking()
    {
        SceneManager.LoadScene(2);
    }
    
    public void bidfoegg()
    {
        StartCoroutine(bidoffuc());
    }
    
    public void tupperegg()
    {
        StartCoroutine(tupperfuc());
    }

    IEnumerator bidoffuc()
    {
        bidof.SetActive(true);
        yield return new WaitForSeconds(3f);
        bidof.SetActive(false);
    }
    
    IEnumerator tupperfuc()
    {
        tupper.SetActive(true);
        yield return new WaitForSeconds(3f);
        tupper.SetActive(false);
    }
}
