using System;
using System.Collections.Generic;

public class Debuff
{
	/*==================================
				  CONSTANTS
	==================================*/
	private int DURATION;
	private float POWER;
	private int CATEGORY;

	// Debuff Types
	public readonly int STUN 		= 0; //TODO Not this.
	public readonly int BLEED 		= 1;
	public readonly int ACID 		= 2;
	public readonly int ARMOR 		= 3;
	public readonly int SPEED 		= 4;
	public readonly int DODGE 		= 5;
	public readonly int DAMAGE 		= 6;
	public readonly int WALL 		= 7;


	public Debuff ()
	{
		DURATION = 0;
		POWER = 0;
		CATEGORY = 0;
	}


	public Debuff (int NewDuration, float NewPower, int NewType)
	{
		this.DURATION = NewDuration;
		this.POWER = NewPower;
		this.CATEGORY = NewType;
	}	


 	public void ActivateDebuff (Debuff D)		
 	{		
 				
 	}		


 	public void Tick ()		
 	{		
 		this.DURATION--;		
 	}		
 		
 	public int GetDuration ()		
 	{		
 		return this.DURATION;		
 	}		


 	public float GetPower ()		
 	{		
 		return this.POWER;		
 	}		


 	public int GetCetegory ()		
 	{		
 		return this.CATEGORY;		
	}
}

