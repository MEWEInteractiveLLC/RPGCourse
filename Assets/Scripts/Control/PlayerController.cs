using RPG.Movement;
using RPG.Combat;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Mover mover;
        [SerializeField] private Fighter fighter;

        
        private Camera mainCamera;

        // Start is called before the first frame update
        void Start()
        {
            mainCamera = Camera.main;
        }

        // Update is called once per frame
        void Update()
        {
            if (InteractWithCombat())
            {
                return;
            }

            if ( InteractWithMovement())
            {
                return;
            }
          
            
            

           
            
        }

        private bool InteractWithCombat()
        {

            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
        
            foreach (var hit in hits)
            {
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();
                if (!target)
                {
                    continue;
                }

                if (Mouse.current.leftButton.wasPressedThisFrame)
                {
                    fighter.Attack(target.gameObject);
                    
                }
                return true;
                
            }

            return false;

        }

        private bool InteractWithMovement()
        {
            RaycastHit hit;
            bool hasHit = Physics.Raycast(GetMouseRay(), out hit);
            
            if (hasHit )
            {

                if (Mouse.current.leftButton.isPressed)
                {
                    mover.StartMoveAction(hit.point);
                }
            }

            return hasHit;
        }

        private Ray GetMouseRay()
        {
            return mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        }
    }
}
