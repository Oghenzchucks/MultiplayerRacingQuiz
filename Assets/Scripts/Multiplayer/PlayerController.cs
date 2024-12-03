using Fusion;
using MenuNavigation;

namespace Multiplayer
{
    public class PlayerController : NetworkBehaviour
    {
        public override void Spawned()
        {
            if (HasInputAuthority)
            {
                MenuManager.OnLoadMenu?.Invoke(MenuEnums.HUD_VIEW, true);
            }
        }
    }
}