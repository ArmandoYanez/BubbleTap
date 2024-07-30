using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(EdgeCollider2D))]
public class colliders : MonoBehaviour
{
    [SerializeField] private float _edgeRadius = 0.1f;
    
   //Componentes de la camara
   public Camera cam;
   private EdgeCollider2D _edgeCollider2D;
   
   //Parametros
   private Vector2 leftTopCorner;
   private Vector2 rightTopCorner;
   private Vector2 leftBottomCorner;
   private Vector2 rightButtonCorner;
   
    // Update is called once per frame
    private void Awake()
    {
     init();   
    }

    private void Start()
    {
        ArrayAdapt();
    }

    private void init()
    {
        cam = GetComponent<Camera>();
        _edgeCollider2D = GetComponent<EdgeCollider2D>();

        cam.orthographic = true;
    }
    
    //Metodo para establecer lmiites
    private void ObtenerTamañoDeLaCamara()
    {
        leftTopCorner = (Vector2)cam.ScreenToWorldPoint(new Vector3(0, cam.pixelHeight, cam.nearClipPlane));
        leftBottomCorner= (Vector2)cam.ScreenToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));
        rightTopCorner = (Vector2)cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth, cam.pixelHeight, cam.nearClipPlane));
        rightButtonCorner = (Vector2)cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth, 0, cam.nearClipPlane));
    }

    private void ArrayAdapt()
    {
        ObtenerTamañoDeLaCamara();

        Vector2[] corners = new Vector2[] { leftTopCorner, rightTopCorner, rightButtonCorner, leftBottomCorner, leftTopCorner };

        _edgeCollider2D.points = corners;

        _edgeCollider2D.edgeRadius = _edgeRadius;
    }
}
