using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Medic : Unit
{
    /*==================================
			   Ability Indexes
	===================================*/
    private readonly int ADRENALINE     = 0;
    private readonly int BULWARK        = 1;
    private readonly int WAVE           = 2;
    private readonly int PISTOL 		= 3;
	private readonly int TASER	 		= 4;

	private readonly int WAVE_HEAL		= 3;

	/*==================================
			Character stat values
	===================================*/
	private readonly int[,] LVL_DMG = new int[,] { {4, 9}, {5, 11}, {6, 13}, {6, 14}, {7, 16}};
	private readonly int[] LVL_HEALTH = new int[] {24, 29, 34, 39, 44};
	private readonly int[] LVL_DODGE = new int[] {0, 5, 10, 15, 20};
	private readonly int[] LVL_SPEED = new int[] {4, 4, 5, 5, 6};
	private readonly int[] LVL_CRIT = new int[] {2, 3, 3, 4, 4};

	public Medic () : base ()
	{
		int NewLevel = 0;
		BaseHealth = LVL_HEALTH[NewLevel];
		BaseSpeed = LVL_SPEED[NewLevel];
		BaseDodge = LVL_DODGE[NewLevel];
		BaseCrit = LVL_CRIT[NewLevel];
		BaseDmg = new int[] {LVL_DMG[NewLevel, 0], LVL_DMG[NewLevel, 1]};
		BaseArmor = 0;

		CritMods = new int[] {5, 0, 0, 0, 0};
		DmgMods = new float[] {0f, 0f, 0f, 0f, -0.5f};
		AccMods = new int[] {85, 0, 0, 0, 85};
		DebuffMods = new float[] {0f, 0f, 0.25f, 0.15f, -0.15f};
		ValidRanks = new bool[][] {
			new bool [] { true, true, true, true, false, false, false },	// Pistol 		1-4
			new bool [] { false, false, false, false, false, true, false },	// Healing Wave	Allies
			new bool [] { false, false, false, false, true, false, false },	// Bulwark		Self	
			new bool [] { false, false, false, false, false, true, false },	// Adrenaline	one ally
			new bool [] { true, true, false, false, true, false, false }	// Taser		1-2
		};
		Debuffs = new List<Debuff>();

		CurrHealth = BaseHealth;
		Level = 1;
		Rank = THREE;
		Category = "Medic";
		IsMech = false;
		IsFriendly = true;
		XP = 0;
		HasPlayed = false;
	}

	public override void SetStats (int NewLevel, int NewRank, int NewHealth)
	{
		NewLevel--;
		this.BaseHealth = this.LVL_HEALTH[NewLevel];
		this.BaseSpeed = this.LVL_SPEED[NewLevel];
		this.BaseDodge = this.LVL_DODGE[NewLevel];
		this.BaseCrit = this.LVL_CRIT[NewLevel];
		this.BaseDmg = new int[] {this.LVL_DMG[NewLevel, 0], this.LVL_DMG[NewLevel, 1]};
		this.BaseArmor = 0;

		this.CurrHealth = NewHealth;
		this.Level = NewLevel;
		this.Rank = NewRank;
	}

	public override bool MakeMove (int MoveID, Unit[] Allies, Unit[] Enemies, Unit Target)
	{	
		if (MoveID == PISTOL || MoveID == TASER) 
		{
			if (!this.CheckHit(MoveID, Target)) 
			{
				return false;
			}
		}

		if (MoveID == PISTOL)
		{
			Target.RemoveHealth (RollDamage (MoveID, this.BaseDmg, Target));
			return SUCCESS;
		}
		else if (MoveID == WAVE)
		{
			foreach (Unit U in Allies)
			{
				U.AddHealth (WAVE_HEAL);
			}
			return SUCCESS;
		}
		else if (MoveID == BULWARK)
		{
//			Debuff D1 = new Debuff (DEBUFF_DUR, DebuffMods[BULWARK], ARMOR);
//			this.AddDebuff (D1);
			return SUCCESS;
		}
		else if (MoveID == ADRENALINE)
		{
//			Debuff D2 = new Debuff (DEBUFF_DUR, DebuffMods [ADRENALINE], SPEED);
//			Target.AddDebuff (D2);
			return SUCCESS;
		}
		else if (MoveID == TASER)
		{
			//Debuff stun
			return SUCCESS;
		}

		return FAILURE;
	}
}