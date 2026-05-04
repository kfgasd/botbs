using Sandbox;

public sealed class PlayerConstraint2D : Component
{
    [Property] public float MinZ { get; set; } = -50f; // Back border
    [Property] public float MaxZ { get; set; } = 50f;  // Front border
    [Property] public GameObject CharacterBody { get; set; }

    protected override void OnUpdate()
    {
        // 1. Get current position
        var pos = WorldPosition;

        // 2. Clamp the Z axis so the player can't walk past the borders
        // This allows movement between MinZ and MaxZ but stops them at the edges
        pos.z = pos.z.Clamp(MinZ, MaxZ);

        WorldPosition = pos;

        // 3. Keep the "Flipping" logic for left/right visuals
        //if (CharacterBody.IsValid())
        //{
            //float moveX = Input.AnalogMove.x;

            //if (moveX > 0)
                //CharacterBody.WorldRotation = Rotation.FromYaw(0);
            //else if (moveX < 0)
                //CharacterBody.WorldRotation = Rotation.FromYaw(180);
        }
    }
}