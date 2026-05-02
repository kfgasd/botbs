using Sandbox;
using System;

public sealed class Projectile : Component
{
    [Property, Description("Ammuksen lentonopeus")]
    public float Speed { get; set; } = 800f;

    [Property, Description("Kuinka kauan ammus on elossa, jos se ei osu mihinkään (sekuntia)")]
    public float LifeTime { get; set; } = 3f;

    // Bullet's existance crisis
    private TimeSince timeAlive = 0f;

    protected override void OnUpdate()
    {
        // 1. Destroy bullet if it has lived too long
        if (timeAlive >= LifeTime)
        {
            GameObject.Destroy();
            return; // Stop
        }

        MoveAndCheckCollisions();
    }

    private void MoveAndCheckCollisions()
    {
        // 2. Calcing the direction and distance
        // X axeli like player
        Vector3 moveDirection = Vector3.Forward; 
        float moveDistance = Speed * Time.Delta;
        
        // Calc how bullet should be at the end of frame
        Vector3 nextPosition = Transform.Position + moveDirection * moveDistance;

        // 3. Raycast for current position to new position
        // Ignore the bullet hitbox so it doesnt shoot itself (kuopio yliopistollinen sairaala)
        var trace = Scene.Trace.Ray(Transform.Position, nextPosition)
            .IgnoreGameObjectHierarchy(GameObject.Root) 
            .Run();

        // 4. hit?
        if (trace.Hit)
        {
            HandleHit(trace.GameObject);
            
            // Destroy bullet when hit smthg
            GameObject.Destroy();
        }
        else
        {
            // 5. If no hit we continue
            Transform.Position = nextPosition;
        }
    }

    private void HandleHit(GameObject hitObject)
    {
        // Try to find the MultiplierWall component from the object we hit
        var wall = hitObject.Components.Get<MultiplierWall>();
        
        // If the component exists, it means we hit a wall
        if (wall != null)
        {
            // Deal 1 damage to the wall
            wall.TakeDamage(1f);
        }

        // debugg yes
        Log.Info($"Projectile hit: {hitObject.Name}");
    }
}
