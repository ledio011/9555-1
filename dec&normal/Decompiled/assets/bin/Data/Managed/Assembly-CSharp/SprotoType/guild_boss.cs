using System;
using System.Collections.Generic;
using Sproto;

namespace SprotoType
{
	// Token: 0x020003D5 RID: 981
	public class guild_boss : SprotoTypeBase
	{
		// Token: 0x06001E1C RID: 7708 RVA: 0x00099B74 File Offset: 0x00097D74
		public guild_boss() : base(guild_boss.max_field_count)
		{
		}

		// Token: 0x06001E1D RID: 7709 RVA: 0x00099B84 File Offset: 0x00097D84
		public guild_boss(byte[] buffer) : base(guild_boss.max_field_count, buffer)
		{
			this.decode();
		}

		// Token: 0x170007F7 RID: 2039
		// (get) Token: 0x06001E1F RID: 7711 RVA: 0x00099BA0 File Offset: 0x00097DA0
		// (set) Token: 0x06001E20 RID: 7712 RVA: 0x00099BA8 File Offset: 0x00097DA8
		public string id
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

		// Token: 0x170007F8 RID: 2040
		// (get) Token: 0x06001E21 RID: 7713 RVA: 0x00099BC0 File Offset: 0x00097DC0
		public bool HasId
		{
			get
			{
				return this.has_field.has_field(0);
			}
		}

		// Token: 0x170007F9 RID: 2041
		// (get) Token: 0x06001E22 RID: 7714 RVA: 0x00099BD0 File Offset: 0x00097DD0
		// (set) Token: 0x06001E23 RID: 7715 RVA: 0x00099BD8 File Offset: 0x00097DD8
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

		// Token: 0x170007FA RID: 2042
		// (get) Token: 0x06001E24 RID: 7716 RVA: 0x00099BF0 File Offset: 0x00097DF0
		public bool HasState
		{
			get
			{
				return this.has_field.has_field(1);
			}
		}

		// Token: 0x170007FB RID: 2043
		// (get) Token: 0x06001E25 RID: 7717 RVA: 0x00099C00 File Offset: 0x00097E00
		// (set) Token: 0x06001E26 RID: 7718 RVA: 0x00099C08 File Offset: 0x00097E08
		public long time
		{
			get
			{
				return this._time;
			}
			set
			{
				this.has_field.set_field(2, true);
				this._time = value;
			}
		}

		// Token: 0x170007FC RID: 2044
		// (get) Token: 0x06001E27 RID: 7719 RVA: 0x00099C20 File Offset: 0x00097E20
		public bool HasTime
		{
			get
			{
				return this.has_field.has_field(2);
			}
		}

		// Token: 0x170007FD RID: 2045
		// (get) Token: 0x06001E28 RID: 7720 RVA: 0x00099C30 File Offset: 0x00097E30
		// (set) Token: 0x06001E29 RID: 7721 RVA: 0x00099C38 File Offset: 0x00097E38
		public long curNum
		{
			get
			{
				return this._curNum;
			}
			set
			{
				this.has_field.set_field(3, true);
				this._curNum = value;
			}
		}

		// Token: 0x170007FE RID: 2046
		// (get) Token: 0x06001E2A RID: 7722 RVA: 0x00099C50 File Offset: 0x00097E50
		public bool HasCurNum
		{
			get
			{
				return this.has_field.has_field(3);
			}
		}

		// Token: 0x170007FF RID: 2047
		// (get) Token: 0x06001E2B RID: 7723 RVA: 0x00099C60 File Offset: 0x00097E60
		// (set) Token: 0x06001E2C RID: 7724 RVA: 0x00099C68 File Offset: 0x00097E68
		public List<sort_item> sort_item
		{
			get
			{
				return this._sort_item;
			}
			set
			{
				this.has_field.set_field(4, true);
				this._sort_item = value;
			}
		}

		// Token: 0x17000800 RID: 2048
		// (get) Token: 0x06001E2D RID: 7725 RVA: 0x00099C80 File Offset: 0x00097E80
		public bool HasSort_item
		{
			get
			{
				return this.has_field.has_field(4);
			}
		}

		// Token: 0x06001E2E RID: 7726 RVA: 0x00099C90 File Offset: 0x00097E90
		protected override void decode()
		{
			int num;
			while ((num = this.deserialize.read_tag()) != -1)
			{
				switch (num)
				{
				case 0:
					this.id = this.deserialize.read_string();
					break;
				case 1:
					this.state = this.deserialize.read_integer();
					break;
				case 2:
					this.time = this.deserialize.read_integer();
					break;
				case 3:
					this.curNum = this.deserialize.read_integer();
					break;
				case 4:
					this.sort_item = this.deserialize.read_obj_list<sort_item>();
					break;
				default:
					this.deserialize.read_unknow_data();
					break;
				}
			}
		}

		// Token: 0x06001E2F RID: 7727 RVA: 0x00099D58 File Offset: 0x00097F58
		public override int encode(SprotoStream stream)
		{
			this.serialize.open(stream);
			if (this.has_field.has_field(0))
			{
				this.serialize.write_string(this.id, 0);
			}
			if (this.has_field.has_field(1))
			{
				this.serialize.write_integer(this.state, 1);
			}
			if (this.has_field.has_field(2))
			{
				this.serialize.write_integer(this.time, 2);
			}
			if (this.has_field.has_field(3))
			{
				this.serialize.write_integer(this.curNum, 3);
			}
			if (this.has_field.has_field(4))
			{
				this.serialize.write_obj<sort_item>(this.sort_item, 4);
			}
			return this.serialize.close();
		}

		// Token: 0x04001AD3 RID: 6867
		private static int max_field_count = 5;

		// Token: 0x04001AD4 RID: 6868
		private string _id;

		// Token: 0x04001AD5 RID: 6869
		private long _state;

		// Token: 0x04001AD6 RID: 6870
		private long _time;

		// Token: 0x04001AD7 RID: 6871
		private long _curNum;

		// Token: 0x04001AD8 RID: 6872
		private List<sort_item> _sort_item;
	}
}
