using System.Numerics;
using Content.Shared.Damage;
using Content.Shared.FixedPoint;
using Robust.Shared.Audio;

namespace Content.Shared.Weapons.Melee.Backstab;

[RegisterComponent]

public sealed partial class BackstabDamageMultipilierComponent : Component
{
    [DataField]
    public bool IgnoreResists = false;
    [DataField]
    public DamageSpecifier BonusDamage = new();
}
