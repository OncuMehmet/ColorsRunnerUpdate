using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class CollectableMovementCommand : MonoBehaviour
{
    public void MoveToGround(Transform groundtransform)
    {
        float zValue = Random.Range(-(groundtransform.localScale.z / 3), (groundtransform.localScale.z / 3));
        transform.DOMove(new Vector3(groundtransform.position.x,
            transform.position.y, groundtransform.position.z + zValue), 2f).OnComplete(
                () =>
                {
                    transform.GetComponent<CollectableManager>().ChangeAnimationOnController(CollectableAnimationTypes.Crouch);



                }
           );

    }
}
