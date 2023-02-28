using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Selector : MonoBehaviour
{
    private PlayerController currentController;
    private Camera mainCamera;

    private void Awake() 
    {
        mainCamera = Camera.main;    
    }
    private void Update() 
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray,out hit))
        {
            if(hit.collider.TryGetComponent<PlayerController>(out PlayerController selectedAnimal))
            {
                if(Input.GetMouseButtonDown(0))
                {
                    PlayerController.OnNewCharacterSelected?.Invoke(selectedAnimal);
                }
            }
        }
    }
}
