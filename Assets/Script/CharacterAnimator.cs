using UnityEngine;

namespace Script
{
    public class CharacterAnimator : MonoBehaviour
    {
        private Animator _animator;
        public Animator Animator => _animator ? _animator : GetComponentInChildren<Animator>();

        public void SetFloat(string id, float value)
        {
            Animator.SetFloat(id, value);
        }

        public void SetTrigger(string id)
        {
            Animator.SetTrigger(id);
        }
    }
}