using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeshController : MonoBehaviour
{




    public void IncreasePlayerSize()
    {
        // transform.parent.localScale += new Vector3(0.1f,0.1f,0.1f);
        // transform.parent.localScale = transform.parent.localScale + new Vector3(0.05f, 0.05f, 0.05f);
        if (transform.parent.localScale.x <= 3)
        {
       //     transform.parent.DOScale(transform.parent.localScale + Vector3.one * 0.2f, 1f); // BUN ACCAKSIN DOTEEN KURUP 
            // transform.parent.localScale = Vector3.one*2;
        }
    }



}
