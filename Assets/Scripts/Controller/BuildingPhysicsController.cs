using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Controller
{
    public class BuildingPhysicsController: MonoBehaviour
    {
        #region Variables

        #region Serialized

       // [SerializeField] private ParticleSystem particleSystem;
        [SerializeField] private BuildingManager manager;

        #endregion

        #region Private

        private float timer;
        private float delay = .1f;

        #endregion

        #endregion

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
               // particleSystem.Play();
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (CompareTag("Market"))
            {
                Debug.Log("ONTRİGGER ENTER");
                timer += Time.deltaTime;
                if (timer >= delay && ScoreSignals.Instance.onGetScore(ScoreVariableType.TotalScore) >= 0) //buraya bakarsın bidaha
                {
                    manager.OnPlayerEnter();
                    timer = 0;
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (CompareTag("Market"))
            {
                // particleSystem.Stop();
                Debug.Log("Exit");
            }
        }

    }
}