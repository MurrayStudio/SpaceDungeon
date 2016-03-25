using UnityEngine;
using System.Collections;

public class Engineer : Unit
{
	/*==================================
			   Ability Indexes
	===================================*/
	private readonly int RATCHET		= 0;
	private readonly int ION		 	= 1;
	private readonly int FLASHBANG		= 2;
	private readonly int SNARE	 		= 3;
	private readonly int LIGHT_WALL		= 4;

	/*==================================
			Character stat values
	===================================*/
	private readonly int[,] LVL_DMG = new int[,] { {4, 7}, {5, 8}, {6, 10}, {6, 11}, {7, 13} };
	private readonly int[] LVL_HEALTH = new int[] {19, 23, 27, 31, 35};
	private readonly int[] LVL_DODGE = new int[] {10, 15, 20, 25, 30};
	private readonly int[] LVL_SPEED = new int[] {6, 6, 7, 7, 8};
	private readonly int[] LVL_CRIT = new int[] {7, 8, 8, 9, 9};

	public Engineer () : base ()
	{
		int NewLevel = 0;
		BASE_HEALTH = LVL_HEALTH[NewLevel];
		BASE_SPEED = LVL_SPEED[NewLevel];
		BASE_DODGE = LVL_DODGE[NewLevel];
		BASE_CRIT = LVL_CRIT[NewLevel];
		BASE_DMG = new int[] {LVL_DMG[NewLevel, 0], LVL_DMG[NewLevel, 1]};
		BASE_ARMOR = 0;

		CRIT_MODS = new int[] {3, 0, 0, 0, 0};
		DMG_MODS = new float[] {0f, -0.30f, -0.80f, -0.50f};
		ACC_MODS = new int[] {85, 85, 85, 85, 0};

		CurrHealth = BASE_HEALTH;
		Level = 1;
		Rank = FOUR;
		CAT = ENGINEER;
		IS_MECH = false;
		IS_FRIENDLY = true;
		XP = 0;
		HasPlayed = false;
	}

	public override void SetStats (int NewLevel, int NewRank, int NewHealth)
	{
		NewLevel--;
		this.BASE_HEALTH = this.LVL_HEALTH[NewLevel];
		this.BASE_SPEED = this.LVL_SPEED[NewLevel];
		this.BASE_DODGE = this.LVL_DODGE[NewLevel];
		this.BASE_CRIT = this.LVL_CRIT[NewLevel];
		this.BASE_DMG = new int[] {this.LVL_DMG[NewLevel, 0], this.LVL_DMG[NewLevel, 1]};
		this.BASE_ARMOR = 0;

		this.CurrHealth = NewHealth;
		this.Level = NewLevel;
		this.Rank = NewRank;
	}

	public bool Ratchet (Unit Enemy)
	{
		return true;
	}

	public bool Ion (Unit Enemy)
	{
		return true;
	}

	public bool Flashbang (Unit Enemy)
	{
		return true;
	}

	public bool Snare (Unit Enemy)
	{
		return true;
	}

	public void LightWall ()
	{

	}

	public override bool MakeMove (int MoveID, Unit[] Allies, Unit[] Enemies, Unit Target) {return false;}
}