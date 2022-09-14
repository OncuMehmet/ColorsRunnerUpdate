using System.Collections;
using System.Collections.Generic;
using UnityEngine;





public class CollectableManager : MonoBehaviour
{
    #region Self Variables

    #region Public Variables

    public ColorTypes CurrentColorType;
    public MatchType MatchType;


    #endregion

    #region Serialized Variables

    //[SerializeField]
    //private CollectableMovementCommand movementCommand;
    [SerializeField]
    private CollectableMeshController collectableMeshController;
    //[SerializeField]
    //private CollectablePhysicsController collectablePhysicsController;
    //[SerializeField]
    //private CollectableAnimationController collectableAnimationController;
    [SerializeField]
    private CapsuleCollider collider;
    #endregion


    #endregion



    public void IncreaseStack()
    {
        StackSignals.Instance.onIncreaseStack?.Invoke(gameObject);
        ChangeAnimationOnController(CollectableAnimationTypes.Run);
    }

    public void ChangeAnimationOnController(CollectableAnimationTypes _currentAnimation)
    {
       //collectableAnimationController.ChangeAnimation(_currentAnimation);
    }
    public void DeListStack()
    {
        StackSignals.Instance.onDroneArea?.Invoke(transform.GetSiblingIndex());
    }
    public void DecreaseStackOnIdle()
    {
        
        StackSignals.Instance.onDecreaseStackRoullette?.Invoke(transform.GetSiblingIndex());
        gameObject.transform.parent = null;
        DelayedDeath(false);

        PlayerSignals.Instance.onIncreaseScale?.Invoke();
    }

    public void DelayedDeath(bool _isDelayed)
    {
        ParticleSignals.Instance.onPlayerDeath?.Invoke(transform.position);
        if (_isDelayed)
        {
            collider.enabled = false;
            ChangeAnimationOnController(CollectableAnimationTypes.Death);
            ChangeOutlineState(true);
            Destroy(gameObject, 2f);
        }
        else
        {
            ChangeAnimationOnController(CollectableAnimationTypes.Death);
            collider.enabled = false;
            Destroy(gameObject, .1f);
        }
    }

    public void DecreaseStack()
    {
        ParticleSignals.Instance.onPlayerDeath?.Invoke(transform.position);
        StackSignals.Instance.onDecreaseStack?.Invoke(transform.GetSiblingIndex());
        gameObject.transform.parent = null;
        DelayedDeath(false);

    }
    public void ChangeColor(ColorTypes colorType)
    {
        CurrentColorType = colorType;
        collectableMeshController.ChangeCollectableMaterial(CurrentColorType);
    }

    public void ChangeOutlineState(bool _state)
    {
       // collectableMeshController.ActivateOutline(_state);
    }
}
