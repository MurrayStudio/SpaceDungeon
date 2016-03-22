using UnityEngine;
using System.Collections;

public class Enforcer : Friendly
{
	/*==================================
			   Ability Indexes
	===================================*/
	private readonly int HEAVY_SWING 	= 0;
	private readonly int SLICE 			= 1;
	private readonly int KICK			= 2;
	private readonly int STEROIDS 		= 3;
	private readonly int WAR_CRY 		= 4;

	/*==================================
			Character stat values
	===================================*/
	private readonly int[,] LVL_DMG = new int[,] { {6, 12}, {7, 14}, {8, 16}, {9, 17}, {10, 18} };
	private readonly int[] LVL_HEALTH = new int[] {33, 40, 47, 54, 61};
	private readonly int[] LVL_DODGE = new int[] {5, 10, 15, 20, 25};
	private readonly int[] LVL_SPEED = new int[] {1, 1, 2, 2, 3};
	private readonly int[] LVL_CRIT = new int[] {5, 5, 6, 6, 7};


	public Enforcer () : base ()
	{
		int NewLevel = 0;
		MAX_HEALTH = LVL_HEALTH[NewLevel];
		SPEED = LVL_SPEED[NewLevel];
		DODGE = LVL_DODGE[NewLevel];
		BASE_CRIT = LVL_CRIT[NewLevel];
		BASE_DMG = new int[] {LVL_DMG[NewLevel, 0], LVL_DMG[NewLevel, 1]};
		ARMOR = 0;
		IS_STUNNED = false;

		CRIT_MODS= new int[] {5, 0, 0, 0, 0};
		DMG_MODS = new float[] {0.15f, -0.15f, -0.60f, 0f, 0f};
		ACC_MODS = new int[] {85, 85, 85, 0, 0};

		CurrHealth = MAX_HEALTH;
		Level = 1;
		Rank = 0;
		CAT = ENFORCER;
		IS_MECH = false;
		XP = 0;
	}

	public void SetStats (int NewLevel, int NewRank, int NewHealth)
	{
		NewLevel--;
		MAX_HEALTH = LVL_HEALTH[NewLevel];
		SPEED = LVL_SPEED[NewLevel];
		DODGE = LVL_DODGE[NewLevel];
		BASE_CRIT = LVL_CRIT[NewLevel];
		BASE_DMG = new int[] {LVL_DMG[NewLevel, 0], LVL_DMG[NewLevel, 1]};
		ARMOR = 0;
		IS_STUNNED = false;

		CRIT_MODS= new int[] {5, 0, 0, 0};
		DMG_MODS = new float[] {-0.60f, 0.15f, -0.15f, 0f};
		ACC_MODS = new int[] {90, 85, 85, 100};

		CurrHealth = NewHealth;
		Level = NewLevel;
		Rank = NewRank;
	}

	public bool HeavySwing (Unit Enemy) 	// Stats from smite
	{
		if (!CheckHit (HEAVY_SWING, this, Enemy)) 
		{
			return false;
		}

		Enemy.DecreaseHealth (RollDamage (BASE_DMG[0], BASE_DMG[1], CheckCrit (HEAVY_SWING, this)));
		return true;
	}

	public bool Slice (Unit Enemy)		// Stats from open vein
	{
		if (!CheckHit (SLICE, this, Enemy)) 
		{
			return false;
		}

		Enemy.DecreaseHealth (RollDamage (BASE_DMG[0], BASE_DMG[1], CheckCrit (SLICE, this)));
		return true;
	}

	public bool Kick (Unit Enemy) 		// Stats from rampart
	{
		if (!CheckHit (KICK, this, Enemy)) 
		{
			return false;
		}

		Enemy.DecreaseHealth (RollDamage (BASE_DMG[0], BASE_DMG[1], CheckCrit (KICK, this)));
		return true;
	}

	public void Steroids ()
	{

	}

	public void WarCry ()				// Kinda bulwark of faith?
	{
			
	}
}