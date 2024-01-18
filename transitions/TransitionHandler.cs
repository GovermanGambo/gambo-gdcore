using System;
using System.Threading.Tasks;
using Gambo.GDCore.Diagnostics;
using Godot;

namespace Gambo.GDCore.Transitions;

public partial class TransitionHandler : Control
{
    public bool HasUnfinishedTransition => CurrentTransition is not null;
    public ITransition CurrentTransition { get; private set; }

    public async Task PerformTransition<TTransition>(TTransition transition, Action onTransition = null)
        where TTransition : Node, ITransition
    {
        await transition.StartTransition();
        
        onTransition?.Invoke();
        
        await EndTransition();
    }
    
    public async Task PerformTransition<TTransition>(TTransition transition, Func<Task> onTransition = null)
        where TTransition : Node, ITransition
    {
        await transition.StartTransition();

        if (onTransition is not null)
        {
            await onTransition.Invoke();
        }
        
        await EndTransition();
    }
    
    public async Task BeginTransition<TTransition>(TTransition transition) 
        where TTransition : Node, ITransition
    {
        AddChild(transition);
        CurrentTransition = transition;
        await transition.StartTransition();
    }

    public async Task EndTransition()
    {
        if (CurrentTransition is null)
        {
            Log.Error("Cannot end transition as no transition has been started!");
            return;
        }

        await CurrentTransition.EndTransition();
        ((Node)CurrentTransition).QueueFree();
        CurrentTransition = null;
    }
}