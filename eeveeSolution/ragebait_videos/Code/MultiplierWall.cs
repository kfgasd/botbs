using Sandbox;
using System;

public sealed class MultiplierWall : Component
{
	[Property, Description( "Maximum health of the wall" )]
	public float MaxHealth { get; set; } = 5f;

	[Property, Description( "Multiplier value given to the player when broken" )]
	public int MultiplierValue { get; set; } = 2;

	private float currentHealth;

	protected override void OnAwake()
	{
		// health when the object is created in the scene
		currentHealth = MaxHealth;
	}

	// This method will be called by the projectile when it hits the wall
	public void TakeDamage( float amount )
	{
		currentHealth -= amount;

		// Check if the wall has run out of health
		if ( currentHealth <= 0 )
		{
			BreakWall();
		}
	}

	private void BreakWall()
	{
		// For now, we just print this to the console. 
		// Later we will link this to the player's actual multiplier stat
		Log.Info( $"Wall broken! Player gets a x{MultiplierValue} multiplier." );

		// Destroy the wall game object from the scene
		GameObject.Destroy();
	}
}
