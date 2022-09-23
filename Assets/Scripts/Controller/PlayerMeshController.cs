using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerMeshController : MonoBehaviour
{
    [SerializeField]private SkinnedMeshRenderer meshRenderer;
    
    
    public void IncreasePlayerSize()
    {
       
        if (transform.parent.localScale.x <= 3)
        {
            transform.parent.DOScale(transform.parent.localScale + Vector3.one * 0.2f, 1f); 
           
        }
    }
    public void ActiveMesh()
    {
        meshRenderer.enabled = true;
    }


}
