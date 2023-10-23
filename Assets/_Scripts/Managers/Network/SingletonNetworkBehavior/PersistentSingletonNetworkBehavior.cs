using Unity.Netcode;
using UnityUtilities;

namespace _Scripts.Managers.Network
{
    public class PersistentSingletonNetworkBehavior<TSubclass> : NetworkBehaviour where TSubclass : PersistentSingletonNetworkBehavior<TSubclass>
    {
        static TSubclass instance;

        /// <summary>
        /// Returns the singleton instance.
        /// </summary>
        public static TSubclass Instance => instance;

        /// <summary>
        /// Returns true if a singleton instance is registered.
        /// </summary>
        protected bool InstanceExists => instance != null;

        /// <summary>
        /// If this is the first instance of this type:
        /// 1. Register it as the instance.
        /// 2. Mark it as <see cref="Object.DontDestroyOnLoad"/>.
        /// 3. Call <see cref="OnPersistentSingletonAwake"/>.
        /// 4. Call <see cref="OnAwakeOrSwitch"/>.
        /// 
        /// If an instance of this already exists, destroy this instance.
        /// </summary>
        protected virtual void Awake()
        {
            // If an instance already exists, destroy this instance.
            if (InstanceExists)
            {
                Destroy(gameObject);
                return;
            }

            // Register, make persistent and call event methods
            instance = (TSubclass) this;
            DontDestroyOnLoad(gameObject);
            OnPersistentSingletonAwake();
            OnAwakeOrSwitch();

        }

        /// <summary>
        /// If this is the persistent instance and it was destroyed (manually),
        /// remove the instance registration.
        /// 
        /// Note: This also means that you need to use
        /// 
        ///     protected override void Destroy()
        ///     {
        ///         base.Destroy();
        ///         // [Your code]
        ///     }
        /// 
        /// in subclasses.
        /// </summary>
        public override void OnDestroy()
        {
            base.OnDestroy();
            if (instance == this)
            {
                instance = null;
                OnPersistentSingletonDestroyed();
            }
        }


        /// <summary>
        /// This method is called when the Awake() method of the first instance of the persistent
        /// singleton is done. This is not called if this is a second instance (which is destroyed
        /// automatically immediately).
        /// </summary>
        protected virtual void OnPersistentSingletonAwake()
        {
        }

        /// <summary>
        /// This method is called when the registered instance of the persistent singleton is either
        /// destroyed manually by calling Destroy() or the application is closed. This is not called
        /// if this is a second instance (which is destroyed automatically immediately).
        /// </summary>
        protected virtual void OnPersistentSingletonDestroyed()
        {
        }

        /// <summary>
        /// This method is called after switching to a new scene.
        /// </summary>
        protected virtual void OnSceneSwitched()
        {
        }

        /// <summary>
        /// This method is called immediately after <see cref="OnPersistentSingletonAwake"/>
        /// or <see cref="OnSceneSwitched"/>.
        /// </summary>
        protected virtual void OnAwakeOrSwitch()
        {
        }
    }
}