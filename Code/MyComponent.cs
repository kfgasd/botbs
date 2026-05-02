using Sandbox;
using System;

public sealed class PlayerMovement : Component
{
    [Property, Description("Speed of moving vertically")] 
    public float MoveSpeed { get; set; } = 300f;
    
    [Property, Description("Automatic walk speed")] 
    public float ForwardSpeed { get; set; } = 150f;
    
    [Property, Description("The most left allowed place")] 
    public float MinY { get; set; } = -150f;
    
    [Property, Description("Same as above but right")] 
    public float MaxY { get; set; } = 150f;

    [Property, Description("Bullet onject (Prefab)")] 
    public GameObject ProjectilePrefab { get; set; }
    
    [Property, Description("How often shoot (seconds)")] 
    public float FireRate { get; set; } = 0.5f;

    // TimeSince is s&box very cool to follow time	
    private TimeSince timeSinceLastFire = 0f;

    protected override void OnUpdate()
    {
        HandleMovement();
        HandleShooting();
    }

    private void HandleMovement()
    {
  
        float sideMovement = Input.AnalogMove.y; 
        
        Vector3 currentPos = Transform.Position;
        

        currentPos.y -= sideMovement * MoveSpeed * Time.Delta;
        

        currentPos.y = MathX.Clamp(currentPos.y, MinY, MaxY);
        

        currentPos.x += ForwardSpeed * Time.Delta;

        Transform.Position = currentPos;
    }

    private void HandleShooting()
    {
 
        if (timeSinceLastFire >= FireRate)
        {
            Fire();
            timeSinceLastFire = 0f; 
        }
    }

    private void Fire()
    {
      
        if (ProjectilePrefab == null) return;


        var projectile = ProjectilePrefab.Clone(Transform.Position);
        
    
    }
}
