using TopDown.Core;

namespace TopDown.Health
{
    public class PlayerHealthComponent : HealthComponent
    {
        //Pass health component to game manager
        private void Start()
        {
            GameManager.Instance?.SubscribeToPlayer(this);
        }
    }
}
