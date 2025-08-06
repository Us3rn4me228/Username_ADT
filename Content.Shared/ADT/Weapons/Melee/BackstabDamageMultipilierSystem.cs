using Content.Shared.Damage;
using Content.Shared.Damage.Components;
using Content.Shared.Mobs;
using Content.Shared.Mobs.Components;
using Content.Shared.Weapons.Melee.Events;

namespace Content.Shared.Weapons.Melee.Backstab;

public sealed class BackstabDamageMultipilierSystem : EntitySystem
{
    [Dependency] protected readonly DamageableSystem _damageable = default!;
    public override void Initialize()
    {
        SubscribeLocalEvent<BackstabDamageMultipilierComponent, MeleeHitEvent>(OnMeleeHit);
    }

    private void OnMeleeHit(Entity<BackstabDamageMultipilierComponent> ent, ref MeleeHitEvent args)
    {
        foreach (var damaged in args.HitEntities)
        {
            if (damaged == args.User) continue; // проверка на пользователя оружия
            if (!TryComp(damaged, out DamageableComponent? dmgcmp))
                return;
            if (!TryComp(damaged, out MobStateComponent? crit))
                return;
            var degrees = Transform(damaged).LocalRotation.Degrees - Transform(args.User).LocalRotation.Degrees;
            if (dmgcmp.DamageContainerID == "Biological" && crit.CurrentState == MobState.Alive)  // Чтоб не бэкстабать мехов и боргов. Ну и в принципе всё что угодно в игре
            {
                if (degrees >= 300 || degrees <= 60 && degrees >= -30) // проверка  на градус, работает криво
                {
                    _damageable.TryChangeDamage(damaged, ent.Comp.BonusDamage, ent.Comp.IgnoreResists, origin: args.User);
                }
            }
        }
    }
}
