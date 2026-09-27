using System;
using System.Collections.Generic;
using Sproto;

namespace SprotoType
{
	// Token: 0x020003D0 RID: 976
	public class guild_battle_round : SprotoTypeBase
	{
		// Token: 0x06001DD6 RID: 7638 RVA: 0x00099234 File Offset: 0x00097434
		public guild_battle_round() : base(guild_battle_round.max_field_count)
		{
		}

		// Token: 0x06001DD7 RID: 7639 RVA: 0x00099244 File Offset: 0x00097444
		public guild_battle_round(byte[] buffer) : base(guild_battle_round.max_field_count, buffer)
		{
			this.decode();
		}

		// Token: 0x170007D7 RID: 2007
		// (get) Token: 0x06001DD9 RID: 7641 RVA: 0x00099260 File Offset: 0x00097460
		// (set) Token: 0x06001DDA RID: 7642 RVA: 0x00099268 File Offset: 0x00097468
		public Dictionary<long, guild_battle_team> battle_team
		{
			get
			{
				return this._battle_team;
			}
			set
			{
				this.has_field.set_field(0, true);
				this._battle_team = value;
			}
		}

		// Token: 0x170007D8 RID: 2008
		// (get) Token: 0x06001DDB RID: 7643 RVA: 0x00099280 File Offset: 0x00097480
		public bool HasBattle_team
		{
			get
			{
				return this.has_field.has_field(0);
			}
		}

		// Token: 0x170007D9 RID: 2009
		// (get) Token: 0x06001DDC RID: 7644 RVA: 0x00099290 File Offset: 0x00097490
		// (set) Token: 0x06001DDD RID: 7645 RVA: 0x00099298 File Offset: 0x00097498
		public long state
		{
			get
			{
				return this._state;
			}
			set
			{
				this.has_field.set_field(1, true);
				this._state = value;
			}
		}

		// Token: 0x170007DA RID: 2010
		// (get) Token: 0x06001DDE RID: 7646 RVA: 0x000992B0 File Offset: 0x000974B0
		public bool HasState
		{
			get
			{
				return this.has_field.has_field(1);
			}
		}

		// Token: 0x06001DDF RID: 7647 RVA: 0x000992C0 File Offset: 0x000974C0
		protected override void decode()
		{
			int num;
			while ((num = this.deserialize.read_tag()) != -1)
			{
				int num2 = num;
				if (num2 != 0)
				{
					if (num2 != 1)
					{
						this.deserialize.read_unknow_data();
					}
					else
					{
						this.state = this.deserialize.read_integer();
					}
				}
				else
				{
					this.battle_team = this.deserialize.read_map<long, guild_battle_team>((guild_battle_team v) => v.index);
				}
			}
		}

		// Token: 0x06001DE0 RID: 7648 RVA: 0x00099354 File Offset: 0x00097554
		public override int encode(SprotoStream stream)
		{
			this.serialize.open(stream);
			if (this.has_field.has_field(0))
			{
				this.serialize.write_obj<long, guild_battle_team>(this.battle_team, 0);
			}
			if (this.has_field.has_field(1))
			{
				this.serialize.write_integer(this.state, 1);
			}
			return this.serialize.close();
		}

		// Token: 0x04001ABE RID: 6846
		private static int max_field_count = 2;

		// Token: 0x04001ABF RID: 6847
		private Dictionary<long, guild_battle_team> _battle_team;

		// Token: 0x04001AC0 RID: 6848
		private long _state;
	}
}
