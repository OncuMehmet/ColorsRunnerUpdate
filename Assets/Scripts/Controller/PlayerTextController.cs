using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class PlayerTextController : MonoBehaviour
{
    #region Self Variables
    #region Serialized Variables

    [SerializeField] private TextMeshPro playerScoreText;
    [SerializeField] private PlayerManager playerManager;


    #endregion
    #endregion

    public void UpdatePlayerScore(float totalScore)
    {
        playerScoreText.text = totalScore.ToString();
    }

    public void UpdateScoreText(bool _isClosed)
    {
        if (_isClosed)
        {
            transform.DOScale(Vector3.zero, 0.1f);

        }
        else
        {
            transform.DOScale(Vector3.one, 0.1f);
        }
    }
    private void Update()
    {
        transform.rotation = Quaternion.Euler(0,-playerManager.transform.rotation.y, 0);
    }
}
