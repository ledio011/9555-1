using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020003CF RID: 975
	public class guild_battle_item_info : SprotoTypeBase
	{
		// Token: 0x06001DBC RID: 7612 RVA: 0x00098EA0 File Offset: 0x000970A0
		public guild_battle_item_info() : base(guild_battle_item_info.max_field_count)
		{
		}

		// Token: 0x06001DBD RID: 7613 RVA: 0x00098EB0 File Offset: 0x000970B0
		public guild_battle_item_info(byte[] buffer) : base(guild_battle_item_info.max_field_count, buffer)
		{
			this.decode();
		}

		// Token: 0x170007C9 RID: 1993
		// (get) Token: 0x06001DBF RID: 7615 RVA: 0x00098ECC File Offset: 0x000970CC
		// (set) Token: 0x06001DC0 RID: 7616 RVA: 0x00098ED4 File Offset: 0x000970D4
		public long id
		{
			get
			{
				return this._id;
			}
			set
			{
				this.has_field.set_field(0, true);
				this._id = value;
			}
		}

		// Token: 0x170007CA RID: 1994
		// (get) Token: 0x06001DC1 RID: 7617 RVA: 0x00098EEC File Offset: 0x000970EC
		public bool HasId
		{
			get
			{
				return this.has_field.has_field(0);
			}
		}

		// Token: 0x170007CB RID: 1995
		// (get) Token: 0x06001DC2 RID: 7618 RVA: 0x00098EFC File Offset: 0x000970FC
		// (set) Token: 0x06001DC3 RID: 7619 RVA: 0x00098F04 File Offset: 0x00097104
		public string name
		{
			get
			{
				return this._name;
			}
			set
			{
				this.has_field.set_field(1, true);
				this._name = value;
			}
		}

		// Token: 0x170007CC RID: 1996
		// (get) Token: 0x06001DC4 RID: 7620 RVA: 0x00098F1C File Offset: 0x0009711C
		public bool HasName
		{
			get
			{
				return this.has_field.has_field(1);
			}
		}

		// Token: 0x170007CD RID: 1997
		// (get) Token: 0x06001DC5 RID: 7621 RVA: 0x00098F2C File Offset: 0x0009712C
		// (set) Token: 0x06001DC6 RID: 7622 RVA: 0x00098F34 File Offset: 0x00097134
		public long guildId
		{
			get
			{
				return this._guildId;
			}
			set
			{
				this.has_field.set_field(2, true);
				this._guildId = value;
			}
		}

		// Token: 0x170007CE RID: 1998
		// (get) Token: 0x06001DC7 RID: 7623 RVA: 0x00098F4C File Offset: 0x0009714C
		public bool HasGuildId
		{
			get
			{
				return this.has_field.has_field(2);
			}
		}

		// Token: 0x170007CF RID: 1999
		// (get) Token: 0x06001DC8 RID: 7624 RVA: 0x00098F5C File Offset: 0x0009715C
		// (set) Token: 0x06001DC9 RID: 7625 RVA: 0x00098F64 File Offset: 0x00097164
		public string guildName
		{
			get
			{
				return this._guildName;
			}
			set
			{
				this.has_field.set_field(3, true);
				this._guildName = value;
			}
		}

		// Token: 0x170007D0 RID: 2000
		// (get) Token: 0x06001DCA RID: 7626 RVA: 0x00098F7C File Offset: 0x0009717C
		public bool HasGuildName
		{
			get
			{
				return this.has_field.has_field(3);
			}
		}

		// Token: 0x170007D1 RID: 2001
		// (get) Token: 0x06001DCB RID: 7627 RVA: 0x00098F8C File Offset: 0x0009718C
		// (set) Token: 0x06001DCC RID: 7628 RVA: 0x00098F94 File Offset: 0x00097194
		public long killNum
		{
			get
			{
				return this._killNum;
			}
			set
			{
				this.has_field.set_field(4, true);
				this._killNum = value;
			}
		}

		// Token: 0x170007D2 RID: 2002
		// (get) Token: 0x06001DCD RID: 7629 RVA: 0x00098FAC File Offset: 0x000971AC
		public bool HasKillNum
		{
			get
			{
				return this.has_field.has_field(4);
			}
		}

		// Token: 0x170007D3 RID: 2003
		// (get) Token: 0x06001DCE RID: 7630 RVA: 0x00098FBC File Offset: 0x000971BC
		// (set) Token: 0x06001DCF RID: 7631 RVA: 0x00098FC4 File Offset: 0x000971C4
		public long continueKill
		{
			get
			{
				return this._continueKill;
			}
			set
			{
				this.has_field.set_field(5, true);
				this._continueKill = value;
			}
		}

		// Token: 0x170007D4 RID: 2004
		// (get) Token: 0x06001DD0 RID: 7632 RVA: 0x00098FDC File Offset: 0x000971DC
		public bool HasContinueKill
		{
			get
			{
				return this.has_field.has_field(5);
			}
		}

		// Token: 0x170007D5 RID: 2005
		// (get) Token: 0x06001DD1 RID: 7633 RVA: 0x00098FEC File Offset: 0x000971EC
		// (set) Token: 0x06001DD2 RID: 7634 RVA: 0x00098FF4 File Offset: 0x000971F4
		public long score
		{
			get
			{
				return this._score;
			}
			set
			{
				this.has_field.set_field(6, true);
				this._score = value;
			}
		}

		// Token: 0x170007D6 RID: 2006
		// (get) Token: 0x06001DD3 RID: 7635 RVA: 0x0009900C File Offset: 0x0009720C
		public bool HasScore
		{
			get
			{
				return this.has_field.has_field(6);
			}
		}

		// Token: 0x06001DD4 RID: 7636 RVA: 0x0009901C File Offset: 0x0009721C
		protected override void decode()
		{
			int num;
			while ((num = this.deserialize.read_tag()) != -1)
			{
				switch (num)
				{
				case 0:
					this.id = this.deserialize.read_integer();
					break;
				case 1:
					this.name = this.deserialize.read_string();
					break;
				case 2:
					this.guildId = this.deserialize.read_integer();
					break;
				case 3:
					this.guildName = this.deserialize.read_string();
					break;
				case 4:
					this.killNum = this.deserialize.read_integer();
					break;
				case 5:
					this.continueKill = this.deserialize.read_integer();
					break;
				case 6:
					this.score = this.deserialize.read_integer();
					break;
				default:
					this.deserialize.read_unknow_data();
					break;
				}
			}
		}

		// Token: 0x06001DD5 RID: 7637 RVA: 0x00099118 File Offset: 0x00097318
		public override int encode(SprotoStream stream)
		{
			this.serialize.open(stream);
			if (this.has_field.has_field(0))
			{
				this.serialize.write_integer(this.id, 0);
			}
			if (this.has_field.has_field(1))
			{
				this.serialize.write_string(this.name, 1);
			}
			if (this.has_field.has_field(2))
			{
				this.serialize.write_integer(this.guildId, 2);
			}
			if (this.has_field.has_field(3))
			{
				this.serialize.write_string(this.guildName, 3);
			}
			if (this.has_field.has_field(4))
			{
				this.serialize.write_integer(this.killNum, 4);
			}
			if (this.has_field.has_field(5))
			{
				this.serialize.write_integer(this.continueKill, 5);
			}
			if (this.has_field.has_field(6))
			{
				this.serialize.write_integer(this.score, 6);
			}
			return this.serialize.close();
		}

		// Token: 0x04001AB6 RID: 6838
		private static int max_field_count = 7;

		// Token: 0x04001AB7 RID: 6839
		private long _id;

		// Token: 0x04001AB8 RID: 6840
		private string _name;

		// Token: 0x04001AB9 RID: 6841
		private long _guildId;

		// Token: 0x04001ABA RID: 6842
		private string _guildName;

		// Token: 0x04001ABB RID: 6843
		private long _killNum;

		// Token: 0x04001ABC RID: 6844
		private long _continueKill;

		// Token: 0x04001ABD RID: 6845
		private long _score;
	}
}
