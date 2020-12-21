using UnityEngine;

namespace RPG.Combat
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
                    isAlive = false;
                    GetComponent<CapsuleCollider>().enabled = false;
                    animator.SetTrigger("Death");
                }

            }
        }

    }
}