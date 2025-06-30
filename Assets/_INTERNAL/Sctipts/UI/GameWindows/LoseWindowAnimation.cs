using UnityEngine;

namespace UI.GameWindows
{
    public class LoseWindowAnimation : MonoBehaviour
    {
        [SerializeField] private string _openTrigger;

        private Animator _animator;

        public void GetAnimator()
        {
            _animator = GetComponent<Animator>();
            _animator.updateMode = AnimatorUpdateMode.UnscaledTime;
        }

        public void OpenWindow()
        {
            _animator.SetTrigger(_openTrigger);
            _animator.ResetTrigger(_openTrigger);
        }
    }
}