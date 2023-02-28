using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class EggBehaviour : MonoBehaviour
{
    [SerializeField] private float ExplosionRadius;
    [SerializeField] private float ExplosionForce;
    // Start is called before the first frame update
    private void Start()
    {
        transform.DOScaleY(0,0);
        transform.DOScaleY(0.8f,0.4f).OnComplete(()=>Destroy(this.gameObject,0.4f));
    }

    private void OnDestroy() 
    {
        CameraUtililty.ShakeCamera();    
        Collider[] affectedPlayers = Physics.OverlapSphere(transform.position,ExplosionRadius);
        foreach(var player in affectedPlayers)
        {
            if(player.TryGetComponent<PlayerController>(out PlayerController extractedPlayer))
            {
                if(!player.transform.CompareTag("Penguin"))
                {
                    extractedPlayer.GetComponent<Rigidbody>().AddExplosionForce(ExplosionForce,transform.position,ExplosionRadius,1,ForceMode.Impulse);
                }
            }
        }
    }
}
