using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DamageText_Controller : MonoBehaviour
{
    public static DamageText_Controller damageText_Controller_Instance;
    [SerializeField] private GameObject damageTextPrefab;

    private void Awake() 
    {
        damageText_Controller_Instance = this;
    }

    public Damage_Text InstantiateText(Transform position = null)
    {
        GameObject currentObject = Instantiate(damageTextPrefab,Camera.main.WorldToScreenPoint(position.position),Quaternion.identity);
        currentObject.transform.localScale =Vector2.zero;
        currentObject.transform.SetParent(transform);

        return currentObject.GetComponent<Damage_Text>();
    }
}
