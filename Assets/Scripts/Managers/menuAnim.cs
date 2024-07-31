using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class menuAnim : MonoBehaviour
{

    public GameObject panelGameModes;
    public GameObject mainMenu;
    
    public GameObject bidof;
    public GameObject tupper;
    
  
    // Start is called before the first frame update
    void Start()
    {
        
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
    
    public void CloseGameModes()
    {
        LeanTween.scale(panelGameModes.GetComponent<RectTransform>(), new Vector3(0, 0, 0), 0.5f);
        LeanTween.scale(mainMenu.GetComponent<RectTransform>(), new Vector3(1, 1, 1), 0.5f);
    }
    
    public void LlevarAClasic()
    {
        SceneManager.LoadScene(1);
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
