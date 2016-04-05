using UnityEngine;
using System.Collections;

public class Rifleman : Unit
{
    /*==================================
			   Ability Indexes
	===================================*/
    private readonly int BAYONET        = 0;
    private readonly int NET_GUN        = 1;
    private readonly int RELOAD         = 2;
    private readonly int RIFLE 			= 3;
	private readonly int SHOTGUN		= 4;

	/*==================================
			Character stat values
	===================================*/
	private readonly int[,] LVL_DMG = new int[,] { {5, 10}, {6, 12}, {7, 13}, {8, 15}, {9, 16}};
	private readonly int[] LVL_HEALTH = new int[] {23, 28, 33, 38, 43};
	private readonly int[] LVL_DODGE = new int[] {10, 15, 20, 25, 30};
	private readonly int[] LVL_SPEED = new int[] {5, 5, 6, 6, 7};
	private readonly int[] LVL_CRIT = new int[] {5, 5, 6, 6, 7};

	public Rifleman () : base ()
	{
		int NewLevel = 0;
		BASE_HEALTH = LVL_HEALTH[NewLevel];
		BASE_SPEED = LVL_SPEED[NewLevel];
		BASE_DODGE = LVL_DODGE[NewLevel];
		BASE_CRIT = LVL_CRIT[NewLevel];
		BASE_DMG = new int[] {LVL_DMG[NewLevel, 0], LVL_DMG[NewLevel, 1]};
		BASE_ARMOR = 0;

		CRIT_MODS = new int[] {10, 0, 0, 0, 0};
		DMG_MODS = new float[] {0f, 0f, -0.4f, -0.8f, 0f};
		ACC_MODS = new int[] {85, 85, 85, 85, 0};
		DEBUFF_MODS = new float[] {0f, 0f, 0f, -0.15f, 0.10f};
//		VALID_RANKS = new bool[][] {
//			new bool [] { TWO, FOUR },
//			new bool [] { ONE, TWO },
//			new bool [] { ONE, THREE },
//			new bool [] { ONE, FOUR },
//			new bool [] { SELF }
//		};
		VALID_RANKS = new bool[][] {
			new bool [] { true, true, false, false, false, false, false },	// Heavy Swing
			new bool [] { true, true, true, false, false, false, false },	// Slice
			new bool [] { true, true, false, false, false, false, false },	// Kick
			new bool [] { false, false, false, false, true, false, false },		// Steroids
			new bool [] { false, false, false, false, false, true, false }		// War Cry
		};
		IS_MULTI_HIT = new bool[] { false, false, true, false, false };

		CurrHealth = BASE_HEALTH;
		Level = 1;
		Rank = TWO;
		CAT = RIFLEMAN;
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

	public override bool MakeMove (int MoveID, Unit[] Allies, Unit[] Enemies, Unit Target)
	{	
		if (MoveID == BAYONET)
		{
			MoveUnit (Allies, this, 1);
		}

		if (MoveID == RIFLE || MoveID == BAYONET || MoveID == NET_GUN) 
		{
			if (!this.CheckHit(MoveID, Target)) 
			{
				return false;
			}
		}

		if (MoveID == RIFLE)
		{
			Target.RemoveHealth (RollDamage (MoveID, this.BASE_DMG, Target));
			return SUCCESS;
		}
		else if (MoveID == BAYONET)
		{
			Target.RemoveHealth (RollDamage (MoveID, this.BASE_DMG, Target));
			return SUCCESS;
		}
		else if (MoveID == NET_GUN)
		{
			Debuff D1 = new Debuff (DEBUFF_DUR, DEBUFF_MODS [NET_GUN], SPEED);
			Debuff D2 = new Debuff (DEBUFF_DUR, DEBUFF_MODS [NET_GUN], DODGE);
			Target.AddDebuff (D1);
			Target.AddDebuff (D2);
			return SUCCESS;
		}
		else if (MoveID == RELOAD)
		{
			Debuff D3 = new Debuff (DEBUFF_DUR, DEBUFF_MODS [RELOAD], SPEED);
			Debuff D4 = new Debuff (DEBUFF_DUR, DEBUFF_MODS [RELOAD], DAMAGE);
			Debuff D5 = new Debuff (DEBUFF_DUR, DEBUFF_MODS [RELOAD] * -1, ARMOR);
			this.AddDebuff (D3);
			this.AddDebuff (D4);
			this.AddDebuff (D5);
			return SUCCESS;
		}

		return FAILURE;
	}


	public override bool[] MakeMove (int MoveID, Unit[] Allies, Unit[] Enemies, Unit[] Targets)
	{
		if (MoveID == SHOTGUN)
		{
			bool[] Hit = new bool[Targets.Length];
			for (int i = 0; i < Targets.Length; i++)
			{
				if (this.CheckHit(MoveID, Targets[i]))
				{
					Targets[i].RemoveHealth (RollDamage (MoveID, this.BASE_DMG, Targets[i]));
					Hit [i] = SUCCESS;
					continue;
				}
				Hit [i] = FAILURE;
			}
			return Hit;
		}

		bool[] Default = new bool[Targets.Length];
		for (int i = 0; i < Targets.Length; i++) 
		{
			Default [i] = FAILURE;
		}
		return Default;
	}
}