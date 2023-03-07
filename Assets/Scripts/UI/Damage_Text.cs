using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
public class Damage_Text : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI damageText;
    [SerializeField] private float enterTime;
    [SerializeField] private Ease enterEase;
    private void Awake() 
    {
        transform.DOScale(1,enterTime).SetEase(enterEase).OnComplete(()=>transform.DOScale(0,enterTime/2).SetDelay(0.3f).SetEase(enterEase).OnComplete(()=>Destroy(this.gameObject)));
    }
    public void SetDamageText(float amount)
    {
        damageText.text = amount.ToString("F0");
    }
}
