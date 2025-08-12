using System.Collections;
using UnityEngine;

namespace UI.MainMenu.Leaderboard
{
    public class LeaderboardPanel : MonoBehaviour
    {
        [Header("Triggers")]
        [SerializeField] private string _openTrigger;
        [SerializeField] private string _closeTrigger;

        [Space(10), Header("Animator")]
        [SerializeField] private Animator _animator;

        [Space(10), Header("Other varilables")]
        [SerializeField] private float _delayBeforeClosing;

        public void SetActive(bool value)
        {
            if (value == true)
            {
                gameObject.SetActive(true);
                _animator.SetTrigger(_openTrigger);
                _animator.ResetTrigger(_openTrigger);
            }
            else if (value == false) 
            {
                _animator.SetTrigger(_closeTrigger);
                StartCoroutine(ClosingDelay());
            }
        }

        private IEnumerator ClosingDelay()
        {
            yield return new WaitForSeconds(_delayBeforeClosing);

            gameObject.SetActive(false);
            StopCoroutine(ClosingDelay());
            Debug.Log("Coroutine is stopped!");
        }
    }
}