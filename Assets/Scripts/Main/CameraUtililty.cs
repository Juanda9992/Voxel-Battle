using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class CameraUtililty : MonoBehaviour
{
    private static Camera mainCamera;
    private void Awake() 
    {
        mainCamera = Camera.main;    
    }
    public static void ShakeCamera()
    {
        mainCamera.transform.DOShakePosition(0.5f);
    }
}
