using Godot;

namespace FantasticSpaceVoyage.Component;

public partial class HurtboxComponent : Area2D
{
    [Export] private HealthComponent _healthComponent;

    public override void _Ready()
    {
        AreaEntered += otherArea =>
        {
            if (otherArea is HitboxComponent hitboxComponent) _healthComponent.Damage(hitboxComponent.HitAmount);
        };
    }
}