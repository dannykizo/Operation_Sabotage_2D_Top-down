using UnityEngine;

namespace TopDown.Functionality
{
    public class EntityController : MonoBehaviour
    {
        public void DisableEntity()
        {
            //Get all components on entity and disable them 
            var scripts = GetComponents<MonoBehaviour>();
            foreach (var script in scripts)
                script.enabled = false;

            //Get rigidbody and stop any movement
            Rigidbody2D body;
            if (TryGetComponent(out body))
                body.Sleep();


        }
    }
}