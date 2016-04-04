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
		DURATION = NewDuration;
		POWER = NewPower;
		CATEGORY = NewType;
	}	


 	public void ActivateDebuff (Debuff D)		
 	{		
 				
 	}		


 	public void Tick ()		
 	{		
 		DURATION--;		
 	}		
 		
 	public int Duration		
 	{		
 		get
        {
            return DURATION;
        }
 	}		


 	public float Power		
 	{		
 		get
        {
            return POWER;
        }
 	}		


 	public int Cetegory
 	{		
 		get
        {
            return CATEGORY;
        }
	}
}

