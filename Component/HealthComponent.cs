using System;
using Godot;

namespace FantasticSpaceVoyage.Component;

public partial class HealthComponent : Node
{
    [Signal]
    public delegate void DiedEventHandler();

    [Signal]
    public delegate void HealthChangedEventHandler();

    [Export] public double MaxHealth { get; private set; } = 10;

    public double CurrentHealth { get; private set; }
    public double HealthPercent => MaxHealth == 0.0 ? 0.0 : Math.Clamp(CurrentHealth / MaxHealth, 0.0, 1.0);

    public override void _Ready()
    {
        CurrentHealth = MaxHealth;
    }

    public void Damage(double amount)
    {
        CurrentHealth = Math.Max(CurrentHealth - amount, 0);
        EmitSignal(nameof(HealthChanged));

        Callable.From(CheckDeath).CallDeferred();

        void CheckDeath()
        {
            if (!(CurrentHealth <= 0.0)) return;
            EmitSignal(nameof(Died));
            Owner.QueueFree();
        }
    }
}