using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class EggBehaviour : MonoBehaviour
{
    [Header("Explosion Settings")]
    [SerializeField] private float ExplosionRadius;
    [SerializeField] private float ExplosionForce;

    [Header("Damage Settings")]
    [SerializeField] private float eggDamage;
    // Start is called before the first frame update
    private void Start()
    {
        transform.DOScaleY(0.8f,0.4f).OnComplete(()=>Destroy(this.gameObject,0.4f));
    }

    private void OnDestroy() 
    {
        CameraUtililty.ShakeCamera();    
        Collider[] affectedPlayers = Physics.OverlapSphere(transform.position,ExplosionRadius);
        foreach(var player in affectedPlayers)
        {
            if(player.TryGetComponent<AnimalRoot>(out AnimalRoot extractedPlayer))
            {
                if(!player.transform.CompareTag("Penguin"))
                {
                    Damage_Text textObj = DamageText_Controller.damageText_Controller_Instance.InstantiateText(extractedPlayer.transform);
                    float damageByDistance = Vector3.Distance(transform.position,extractedPlayer.transform.position); 
                    float realDamage = Mathf.Abs(damageByDistance - eggDamage);
                    extractedPlayer.TakeDamage(realDamage);
                    textObj.SetDamageText(realDamage);
                    extractedPlayer.GetRigidBody().AddExplosionForce(ExplosionForce,transform.position,ExplosionRadius,1,ForceMode.Impulse);
                }
            }
        }
    }
}
