using UnityEngine;

namespace RPG.Core
{
    public class HealthComponent : MonoBehaviour
    {
        [SerializeField] private float health = 100.0f;
        [SerializeField] private Animator animator;

        private bool isAlive = true;

        public bool IsAlive()
        {
            return isAlive;
        }

        public void AdjustHealth(float amount)
        {
            if (Mathf.Abs(amount) > 0.0f)
            {
                health += amount;
                health = Mathf.Clamp(health, 0, 100);
                print(health);

                if (health <= 0.0f && isAlive)
                {
                    Die();
                }

            }
        }

        private void Die()
        {
            isAlive = false;
            if (GetComponent<CapsuleCollider>() != null)
            {
                GetComponent<CapsuleCollider>() .enabled = false;
            }
            GetComponent<ActionScheduler>().CancelCurrentAction();
            animator.SetTrigger("Death");
        }
    }
}