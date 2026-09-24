using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x0200066C RID: 1644
	public class wild_boss : SprotoTypeBase
	{
		// Token: 0x06002F91 RID: 12177 RVA: 0x000BC254 File Offset: 0x000BA454
		public wild_boss() : base(wild_boss.max_field_count)
		{
		}

		// Token: 0x06002F92 RID: 12178 RVA: 0x000BC264 File Offset: 0x000BA464
		public wild_boss(byte[] buffer) : base(wild_boss.max_field_count, buffer)
		{
			this.decode();
		}

		// Token: 0x17000E11 RID: 3601
		// (get) Token: 0x06002F94 RID: 12180 RVA: 0x000BC284 File Offset: 0x000BA484
		// (set) Token: 0x06002F95 RID: 12181 RVA: 0x000BC28C File Offset: 0x000BA48C
		public long bossId
		{
			get
			{
				return this._bossId;
			}
			set
			{
				this.has_field.set_field(0, true);
				this._bossId = value;
			}
		}

		// Token: 0x17000E12 RID: 3602
		// (get) Token: 0x06002F96 RID: 12182 RVA: 0x000BC2A4 File Offset: 0x000BA4A4
		public bool HasBossId
		{
			get
			{
				return this.has_field.has_field(0);
			}
		}

		// Token: 0x17000E13 RID: 3603
		// (get) Token: 0x06002F97 RID: 12183 RVA: 0x000BC2B4 File Offset: 0x000BA4B4
		// (set) Token: 0x06002F98 RID: 12184 RVA: 0x000BC2BC File Offset: 0x000BA4BC
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

		// Token: 0x17000E14 RID: 3604
		// (get) Token: 0x06002F99 RID: 12185 RVA: 0x000BC2D4 File Offset: 0x000BA4D4
		public bool HasState
		{
			get
			{
				return this.has_field.has_field(1);
			}
		}

		// Token: 0x17000E15 RID: 3605
		// (get) Token: 0x06002F9A RID: 12186 RVA: 0x000BC2E4 File Offset: 0x000BA4E4
		// (set) Token: 0x06002F9B RID: 12187 RVA: 0x000BC2EC File Offset: 0x000BA4EC
		public long refreshTime
		{
			get
			{
				return this._refreshTime;
			}
			set
			{
				this.has_field.set_field(2, true);
				this._refreshTime = value;
			}
		}

		// Token: 0x17000E16 RID: 3606
		// (get) Token: 0x06002F9C RID: 12188 RVA: 0x000BC304 File Offset: 0x000BA504
		public bool HasRefreshTime
		{
			get
			{
				return this.has_field.has_field(2);
			}
		}

		// Token: 0x17000E17 RID: 3607
		// (get) Token: 0x06002F9D RID: 12189 RVA: 0x000BC314 File Offset: 0x000BA514
		// (set) Token: 0x06002F9E RID: 12190 RVA: 0x000BC31C File Offset: 0x000BA51C
		public long killId
		{
			get
			{
				return this._killId;
			}
			set
			{
				this.has_field.set_field(3, true);
				this._killId = value;
			}
		}

		// Token: 0x17000E18 RID: 3608
		// (get) Token: 0x06002F9F RID: 12191 RVA: 0x000BC334 File Offset: 0x000BA534
		public bool HasKillId
		{
			get
			{
				return this.has_field.has_field(3);
			}
		}

		// Token: 0x17000E19 RID: 3609
		// (get) Token: 0x06002FA0 RID: 12192 RVA: 0x000BC344 File Offset: 0x000BA544
		// (set) Token: 0x06002FA1 RID: 12193 RVA: 0x000BC34C File Offset: 0x000BA54C
		public string killName
		{
			get
			{
				return this._killName;
			}
			set
			{
				this.has_field.set_field(4, true);
				this._killName = value;
			}
		}

		// Token: 0x17000E1A RID: 3610
		// (get) Token: 0x06002FA2 RID: 12194 RVA: 0x000BC364 File Offset: 0x000BA564
		public bool HasKillName
		{
			get
			{
				return this.has_field.has_field(4);
			}
		}

		// Token: 0x17000E1B RID: 3611
		// (get) Token: 0x06002FA3 RID: 12195 RVA: 0x000BC374 File Offset: 0x000BA574
		// (set) Token: 0x06002FA4 RID: 12196 RVA: 0x000BC37C File Offset: 0x000BA57C
		public string mapInfoId
		{
			get
			{
				return this._mapInfoId;
			}
			set
			{
				this.has_field.set_field(5, true);
				this._mapInfoId = value;
			}
		}

		// Token: 0x17000E1C RID: 3612
		// (get) Token: 0x06002FA5 RID: 12197 RVA: 0x000BC394 File Offset: 0x000BA594
		public bool HasMapInfoId
		{
			get
			{
				return this.has_field.has_field(5);
			}
		}

		// Token: 0x17000E1D RID: 3613
		// (get) Token: 0x06002FA6 RID: 12198 RVA: 0x000BC3A4 File Offset: 0x000BA5A4
		// (set) Token: 0x06002FA7 RID: 12199 RVA: 0x000BC3AC File Offset: 0x000BA5AC
		public long lineIndex
		{
			get
			{
				return this._lineIndex;
			}
			set
			{
				this.has_field.set_field(6, true);
				this._lineIndex = value;
			}
		}

		// Token: 0x17000E1E RID: 3614
		// (get) Token: 0x06002FA8 RID: 12200 RVA: 0x000BC3C4 File Offset: 0x000BA5C4
		public bool HasLineIndex
		{
			get
			{
				return this.has_field.has_field(6);
			}
		}

		// Token: 0x17000E1F RID: 3615
		// (get) Token: 0x06002FA9 RID: 12201 RVA: 0x000BC3D4 File Offset: 0x000BA5D4
		// (set) Token: 0x06002FAA RID: 12202 RVA: 0x000BC3DC File Offset: 0x000BA5DC
		public long posx
		{
			get
			{
				return this._posx;
			}
			set
			{
				this.has_field.set_field(7, true);
				this._posx = value;
			}
		}

		// Token: 0x17000E20 RID: 3616
		// (get) Token: 0x06002FAB RID: 12203 RVA: 0x000BC3F4 File Offset: 0x000BA5F4
		public bool HasPosx
		{
			get
			{
				return this.has_field.has_field(7);
			}
		}

		// Token: 0x17000E21 RID: 3617
		// (get) Token: 0x06002FAC RID: 12204 RVA: 0x000BC404 File Offset: 0x000BA604
		// (set) Token: 0x06002FAD RID: 12205 RVA: 0x000BC40C File Offset: 0x000BA60C
		public long posz
		{
			get
			{
				return this._posz;
			}
			set
			{
				this.has_field.set_field(8, true);
				this._posz = value;
			}
		}

		// Token: 0x17000E22 RID: 3618
		// (get) Token: 0x06002FAE RID: 12206 RVA: 0x000BC424 File Offset: 0x000BA624
		public bool HasPosz
		{
			get
			{
				return this.has_field.has_field(8);
			}
		}

		// Token: 0x06002FAF RID: 12207 RVA: 0x000BC434 File Offset: 0x000BA634
		protected override void decode()
		{
			int num;
			while ((num = this.deserialize.read_tag()) != -1)
			{
				switch (num)
				{
				case 0:
					this.bossId = this.deserialize.read_integer();
					break;
				case 1:
					this.state = this.deserialize.read_integer();
					break;
				case 2:
					this.refreshTime = this.deserialize.read_integer();
					break;
				case 3:
					this.killId = this.deserialize.read_integer();
					break;
				case 4:
					this.killName = this.deserialize.read_string();
					break;
				case 5:
					this.mapInfoId = this.deserialize.read_string();
					break;
				case 6:
					this.lineIndex = this.deserialize.read_integer();
					break;
				case 7:
					this.posx = this.deserialize.read_integer();
					break;
				case 8:
					this.posz = this.deserialize.read_integer();
					break;
				default:
					this.deserialize.read_unknow_data();
					break;
				}
			}
		}

		// Token: 0x06002FB0 RID: 12208 RVA: 0x000BC564 File Offset: 0x000BA764
		public override int encode(SprotoStream stream)
		{
			this.serialize.open(stream);
			if (this.has_field.has_field(0))
			{
				this.serialize.write_integer(this.bossId, 0);
			}
			if (this.has_field.has_field(1))
			{
				this.serialize.write_integer(this.state, 1);
			}
			if (this.has_field.has_field(2))
			{
				this.serialize.write_integer(this.refreshTime, 2);
			}
			if (this.has_field.has_field(3))
			{
				this.serialize.write_integer(this.killId, 3);
			}
			if (this.has_field.has_field(4))
			{
				this.serialize.write_string(this.killName, 4);
			}
			if (this.has_field.has_field(5))
			{
				this.serialize.write_string(this.mapInfoId, 5);
			}
			if (this.has_field.has_field(6))
			{
				this.serialize.write_integer(this.lineIndex, 6);
			}
			if (this.has_field.has_field(7))
			{
				this.serialize.write_integer(this.posx, 7);
			}
			if (this.has_field.has_field(8))
			{
				this.serialize.write_integer(this.posz, 8);
			}
			return this.serialize.close();
		}

		// Token: 0x04001F74 RID: 8052
		private static int max_field_count = 9;

		// Token: 0x04001F75 RID: 8053
		private long _bossId;

		// Token: 0x04001F76 RID: 8054
		private long _state;

		// Token: 0x04001F77 RID: 8055
		private long _refreshTime;

		// Token: 0x04001F78 RID: 8056
		private long _killId;

		// Token: 0x04001F79 RID: 8057
		private string _killName;

		// Token: 0x04001F7A RID: 8058
		private string _mapInfoId;

		// Token: 0x04001F7B RID: 8059
		private long _lineIndex;

		// Token: 0x04001F7C RID: 8060
		private long _posx;

		// Token: 0x04001F7D RID: 8061
		private long _posz;
	}
}
