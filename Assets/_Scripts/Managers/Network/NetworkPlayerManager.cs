using _Scripts.NetworkContainter;
using Unity.Netcode;

namespace _Scripts.Managers.Network
{
    public class NetworkPlayerManager : PersistentSingletonNetworkBehavior<NetworkPlayerManager>
    {
        NetworkList<PlayerContainer> _playerDataList = new();
        
        
        protected override void Awake()
        {
            base.Awake();
            _playerDataList.OnListChanged += OnPlayerDataListChanged;
            
        }

        private void OnPlayerDataListChanged(NetworkListEvent<PlayerContainer> changeEvent)
        {
            
        }

        public void AddPlayer()
        {
            
        }
        
        
    }
}