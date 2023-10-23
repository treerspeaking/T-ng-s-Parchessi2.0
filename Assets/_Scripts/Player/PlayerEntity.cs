using System;
using _Scripts.Simulation;
using UnityEngine;

public class PlayerEntity : MonoBehaviour, ITargetee
{
    
    [SerializeField] private TargetType _targeteeType;
    protected ulong InternalOwnerClientID;
    protected int InternalContainerIndex;

    public int ContainerIndex
    { 
        get => InternalContainerIndex;
        set { }
    }
    public ulong OwnerClientID 
    { 
        get => InternalOwnerClientID;
        set { }
    }
    
    public TargetType TargetType
    {
        get => _targeteeType;
        set {}
    }

    protected virtual void Initialize(int containerIndex, ulong ownerClientID)
    {
        InternalContainerIndex = containerIndex;
        InternalOwnerClientID = ownerClientID;
    }

    protected virtual void Awake()
    {
        ActionManager.Instance.AddTargetEntity(this);
    }

    public virtual SimulationPackage ExecuteTargetee<TTargeter>(TTargeter targeter) where TTargeter : ITargeter
    {
        return null;
    }

    public virtual void StartHighlight()
    {
        transform.localScale = transform.localScale * 1.2f;
    }

    public virtual void EndHighlight()
    {
        transform.localScale = transform.localScale / 1.2f;
    }

    protected virtual void OnDestroy()
    {
        ActionManager.Instance.RemoveTargetEntity(this);
    }
}