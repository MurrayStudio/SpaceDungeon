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
	public readonly int STUN 		= "Stun"; //TODO Not this.
	public readonly int BLEED 		= "Bleed";
	public readonly int ACID 		= "Acid";
	public readonly int ARMOR 		= "Armor";
	public readonly int SPEED 		= "Speed";
	public readonly int DODGE 		= "Dodge";
	public readonly int DAMAGE 		= "Damage";
	public readonly int WALL 		= "Light Wall";


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
		if (this.CATEGORY == BLEED || this.CATEGORY == ACID)
		{
			
		}		
 	}		
 		
 	public int GetDuration ()		
 	{		
 		return this.DURATION;		
 	}		


 	public float GetPower ()		
 	{		
 		return this.POWER;		
 	}		


 	public String GetCategory ()		
 	{		
 		return this.CATEGORY;		
	}
}

