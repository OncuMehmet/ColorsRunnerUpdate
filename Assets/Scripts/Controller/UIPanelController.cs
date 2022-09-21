using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPanelController : MonoBehaviour
{
    #region Self Variables
    #region Serialized Variables
    [SerializeField] private List<GameObject> panels;

    #endregion
    #endregion



    public void OpenPanel(UIPanelTypes panelParam)
    {
        panels[(int)panelParam].SetActive(true);
    }

    public void ClosePanel(UIPanelTypes panelParam)
    {
        panels[(int)panelParam].SetActive(false);
    }
}

