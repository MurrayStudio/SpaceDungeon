using System;
using System.Collections.Generic;

public class Debuff
{
	/*==================================
				  CONSTANTS
	==================================*/
	private int DURATION;
	private float POWER;
	private String CATEGORY;

	// Debuff Types
	public readonly String STUN 		= "Stun";
	public readonly String BLEED 		= "Bleed";
	public readonly String ACID 		= "Acid";
	public readonly String ARMOR 		= "Armor";
	public readonly String SPEED 		= "Speed";
	public readonly String DODGE 		= "Dodge";
	public readonly String DAMAGE 		= "Damage";
	public readonly String WALL 		= "Light Wall";


	public Debuff ()
	{
		DURATION = 0;
		POWER = 0;
		CATEGORY = "";
	}


	public Debuff (int NewDuration, float NewPower, String NewType)
	{
		this.DURATION = NewDuration;
		this.POWER = NewPower;
		this.CATEGORY = NewType;
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


 	public String GetCategory ()		
 	{		
 		return this.CATEGORY;		
	}
}

