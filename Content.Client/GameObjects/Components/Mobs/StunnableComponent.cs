#nullable enable
using Content.Shared.Audio;
using Content.Shared.GameObjects.Components.Mobs;
using Content.Shared.GameObjects.Components.Movement;
using Robust.Client.GameObjects.EntitySystems;
using Robust.Shared.GameObjects;
using Robust.Shared.GameObjects.Systems;

namespace Content.Client.GameObjects.Components.Mobs
{
    [RegisterComponent]
    [ComponentReference(typeof(SharedStunnableComponent))]
    public class StunnableComponent : SharedStunnableComponent
    {
        protected override void OnInteractHand()
        {
            EntitySystem.Get<AudioSystem>()
                .Play("/Audio/Effects/thudswoosh.ogg", Owner, AudioHelpers.WithVariation(0.25f));
        }

        public override void HandleComponentState(ComponentState? curState, ComponentState? nextState)
        {
            base.HandleComponentState(curState, nextState);

            if (!(curState is StunnableComponentState state))
            {
                return;
            }

            StunnedTimer = state.StunnedTimer;
            KnockdownTimer = state.KnockdownTimer;
            SlowdownTimer = state.SlowdownTimer;

            WalkModifierOverride = state.WalkModifierOverride;
            RunModifierOverride = state.RunModifierOverride;

            if (Owner.TryGetComponent(out MovementSpeedModifierComponent? movement))
            {
                movement.RefreshMovementSpeedModifiers();
            }
        }
    }
}
