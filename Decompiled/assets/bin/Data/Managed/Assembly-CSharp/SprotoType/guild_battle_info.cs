using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020003CE RID: 974
	public class guild_battle_info : SprotoTypeBase
	{
		// Token: 0x06001D9F RID: 7583 RVA: 0x00098AA4 File Offset: 0x00096CA4
		public guild_battle_info() : base(guild_battle_info.max_field_count)
		{
		}

		// Token: 0x06001DA0 RID: 7584 RVA: 0x00098AB4 File Offset: 0x00096CB4
		public guild_battle_info(byte[] buffer) : base(guild_battle_info.max_field_count, buffer)
		{
			this.decode();
		}

		// Token: 0x170007B9 RID: 1977
		// (get) Token: 0x06001DA2 RID: 7586 RVA: 0x00098AD0 File Offset: 0x00096CD0
		// (set) Token: 0x06001DA3 RID: 7587 RVA: 0x00098AD8 File Offset: 0x00096CD8
		public long state
		{
			get
			{
				return this._state;
			}
			set
			{
				this.has_field.set_field(0, true);
				this._state = value;
			}
		}

		// Token: 0x170007BA RID: 1978
		// (get) Token: 0x06001DA4 RID: 7588 RVA: 0x00098AF0 File Offset: 0x00096CF0
		public bool HasState
		{
			get
			{
				return this.has_field.has_field(0);
			}
		}

		// Token: 0x170007BB RID: 1979
		// (get) Token: 0x06001DA5 RID: 7589 RVA: 0x00098B00 File Offset: 0x00096D00
		// (set) Token: 0x06001DA6 RID: 7590 RVA: 0x00098B08 File Offset: 0x00096D08
		public guild_battle_round battle_round_1
		{
			get
			{
				return this._battle_round_1;
			}
			set
			{
				this.has_field.set_field(1, true);
				this._battle_round_1 = value;
			}
		}

		// Token: 0x170007BC RID: 1980
		// (get) Token: 0x06001DA7 RID: 7591 RVA: 0x00098B20 File Offset: 0x00096D20
		public bool HasBattle_round_1
		{
			get
			{
				return this.has_field.has_field(1);
			}
		}

		// Token: 0x170007BD RID: 1981
		// (get) Token: 0x06001DA8 RID: 7592 RVA: 0x00098B30 File Offset: 0x00096D30
		// (set) Token: 0x06001DA9 RID: 7593 RVA: 0x00098B38 File Offset: 0x00096D38
		public guild_battle_round battle_round_2
		{
			get
			{
				return this._battle_round_2;
			}
			set
			{
				this.has_field.set_field(2, true);
				this._battle_round_2 = value;
			}
		}

		// Token: 0x170007BE RID: 1982
		// (get) Token: 0x06001DAA RID: 7594 RVA: 0x00098B50 File Offset: 0x00096D50
		public bool HasBattle_round_2
		{
			get
			{
				return this.has_field.has_field(2);
			}
		}

		// Token: 0x170007BF RID: 1983
		// (get) Token: 0x06001DAB RID: 7595 RVA: 0x00098B60 File Offset: 0x00096D60
		// (set) Token: 0x06001DAC RID: 7596 RVA: 0x00098B68 File Offset: 0x00096D68
		public guild_battle_round battle_round_3
		{
			get
			{
				return this._battle_round_3;
			}
			set
			{
				this.has_field.set_field(3, true);
				this._battle_round_3 = value;
			}
		}

		// Token: 0x170007C0 RID: 1984
		// (get) Token: 0x06001DAD RID: 7597 RVA: 0x00098B80 File Offset: 0x00096D80
		public bool HasBattle_round_3
		{
			get
			{
				return this.has_field.has_field(3);
			}
		}

		// Token: 0x170007C1 RID: 1985
		// (get) Token: 0x06001DAE RID: 7598 RVA: 0x00098B90 File Offset: 0x00096D90
		// (set) Token: 0x06001DAF RID: 7599 RVA: 0x00098B98 File Offset: 0x00096D98
		public long time
		{
			get
			{
				return this._time;
			}
			set
			{
				this.has_field.set_field(4, true);
				this._time = value;
			}
		}

		// Token: 0x170007C2 RID: 1986
		// (get) Token: 0x06001DB0 RID: 7600 RVA: 0x00098BB0 File Offset: 0x00096DB0
		public bool HasTime
		{
			get
			{
				return this.has_field.has_field(4);
			}
		}

		// Token: 0x170007C3 RID: 1987
		// (get) Token: 0x06001DB1 RID: 7601 RVA: 0x00098BC0 File Offset: 0x00096DC0
		// (set) Token: 0x06001DB2 RID: 7602 RVA: 0x00098BC8 File Offset: 0x00096DC8
		public string ID
		{
			get
			{
				return this._ID;
			}
			set
			{
				this.has_field.set_field(5, true);
				this._ID = value;
			}
		}

		// Token: 0x170007C4 RID: 1988
		// (get) Token: 0x06001DB3 RID: 7603 RVA: 0x00098BE0 File Offset: 0x00096DE0
		public bool HasID
		{
			get
			{
				return this.has_field.has_field(5);
			}
		}

		// Token: 0x170007C5 RID: 1989
		// (get) Token: 0x06001DB4 RID: 7604 RVA: 0x00098BF0 File Offset: 0x00096DF0
		// (set) Token: 0x06001DB5 RID: 7605 RVA: 0x00098BF8 File Offset: 0x00096DF8
		public long guildId
		{
			get
			{
				return this._guildId;
			}
			set
			{
				this.has_field.set_field(6, true);
				this._guildId = value;
			}
		}

		// Token: 0x170007C6 RID: 1990
		// (get) Token: 0x06001DB6 RID: 7606 RVA: 0x00098C10 File Offset: 0x00096E10
		public bool HasGuildId
		{
			get
			{
				return this.has_field.has_field(6);
			}
		}

		// Token: 0x170007C7 RID: 1991
		// (get) Token: 0x06001DB7 RID: 7607 RVA: 0x00098C20 File Offset: 0x00096E20
		// (set) Token: 0x06001DB8 RID: 7608 RVA: 0x00098C28 File Offset: 0x00096E28
		public string championName
		{
			get
			{
				return this._championName;
			}
			set
			{
				this.has_field.set_field(7, true);
				this._championName = value;
			}
		}

		// Token: 0x170007C8 RID: 1992
		// (get) Token: 0x06001DB9 RID: 7609 RVA: 0x00098C40 File Offset: 0x00096E40
		public bool HasChampionName
		{
			get
			{
				return this.has_field.has_field(7);
			}
		}

		// Token: 0x06001DBA RID: 7610 RVA: 0x00098C50 File Offset: 0x00096E50
		protected override void decode()
		{
			int num;
			while ((num = this.deserialize.read_tag()) != -1)
			{
				switch (num)
				{
				case 0:
					this.state = this.deserialize.read_integer();
					break;
				case 1:
					this.battle_round_1 = this.deserialize.read_obj<guild_battle_round>();
					break;
				case 2:
					this.battle_round_2 = this.deserialize.read_obj<guild_battle_round>();
					break;
				case 3:
					this.battle_round_3 = this.deserialize.read_obj<guild_battle_round>();
					break;
				case 4:
					this.time = this.deserialize.read_integer();
					break;
				case 5:
					this.ID = this.deserialize.read_string();
					break;
				case 6:
					this.guildId = this.deserialize.read_integer();
					break;
				case 7:
					this.championName = this.deserialize.read_string();
					break;
				default:
					this.deserialize.read_unknow_data();
					break;
				}
			}
		}

		// Token: 0x06001DBB RID: 7611 RVA: 0x00098D64 File Offset: 0x00096F64
		public override int encode(SprotoStream stream)
		{
			this.serialize.open(stream);
			if (this.has_field.has_field(0))
			{
				this.serialize.write_integer(this.state, 0);
			}
			if (this.has_field.has_field(1))
			{
				this.serialize.write_obj(this.battle_round_1, 1);
			}
			if (this.has_field.has_field(2))
			{
				this.serialize.write_obj(this.battle_round_2, 2);
			}
			if (this.has_field.has_field(3))
			{
				this.serialize.write_obj(this.battle_round_3, 3);
			}
			if (this.has_field.has_field(4))
			{
				this.serialize.write_integer(this.time, 4);
			}
			if (this.has_field.has_field(5))
			{
				this.serialize.write_string(this.ID, 5);
			}
			if (this.has_field.has_field(6))
			{
				this.serialize.write_integer(this.guildId, 6);
			}
			if (this.has_field.has_field(7))
			{
				this.serialize.write_string(this.championName, 7);
			}
			return this.serialize.close();
		}

		// Token: 0x04001AAD RID: 6829
		private static int max_field_count = 8;

		// Token: 0x04001AAE RID: 6830
		private long _state;

		// Token: 0x04001AAF RID: 6831
		private guild_battle_round _battle_round_1;

		// Token: 0x04001AB0 RID: 6832
		private guild_battle_round _battle_round_2;

		// Token: 0x04001AB1 RID: 6833
		private guild_battle_round _battle_round_3;

		// Token: 0x04001AB2 RID: 6834
		private long _time;

		// Token: 0x04001AB3 RID: 6835
		private string _ID;

		// Token: 0x04001AB4 RID: 6836
		private long _guildId;

		// Token: 0x04001AB5 RID: 6837
		private string _championName;
	}
}
