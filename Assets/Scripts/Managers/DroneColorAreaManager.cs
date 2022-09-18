using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneColorAreaManager : MonoBehaviour
{
    #region Self Variables

    #region Public Variables

    public ColorTypes CurrentColorType;
    public MatchType matchType = MatchType.UnMatched;

    #endregion
    #region Serialized Variables

    [SerializeField] bool openDroneAreaExit;
    [SerializeField] private DroneAreaMeshController droneAreaMeshController;

    #endregion
    #region Private Variables
    private List<GameObject> _tempList = new List<GameObject>();
    private MeshRenderer _meshRenderer;
    #endregion

    #endregion

   

    private void Awake()
    {
        GetReferences();
    }

    private void GetReferences()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    private void OnEnable()
    {
        SubscribeEvents();

    }
    private void OnDisable()
    {
        UnSubscribeEvents();

    }

    void SubscribeEvents()
    {

        CoreGameSignals.Instance.onGameInit += OnSetColor;
    }

    private void OnSetColor()
    {
        //renk gelecek _meshRenderer.material.color = Resources.Load<Material>("/Data/").color;
        droneAreaMeshController.ChangeAreaColor(CurrentColorType);
    }

    void UnSubscribeEvents()
    {
        CoreGameSignals.Instance.onGameInit -= OnSetColor;
    }
}