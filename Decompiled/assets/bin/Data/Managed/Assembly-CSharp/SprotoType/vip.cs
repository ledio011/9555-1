using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000664 RID: 1636
	public class vip : SprotoTypeBase
	{
		// Token: 0x06002F54 RID: 12116 RVA: 0x000BBAB4 File Offset: 0x000B9CB4
		public vip() : base(vip.max_field_count)
		{
		}

		// Token: 0x06002F55 RID: 12117 RVA: 0x000BBAC4 File Offset: 0x000B9CC4
		public vip(byte[] buffer) : base(vip.max_field_count, buffer)
		{
			this.decode();
		}

		// Token: 0x17000DFB RID: 3579
		// (get) Token: 0x06002F57 RID: 12119 RVA: 0x000BBAE0 File Offset: 0x000B9CE0
		// (set) Token: 0x06002F58 RID: 12120 RVA: 0x000BBAE8 File Offset: 0x000B9CE8
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

		// Token: 0x17000DFC RID: 3580
		// (get) Token: 0x06002F59 RID: 12121 RVA: 0x000BBB00 File Offset: 0x000B9D00
		public bool HasId
		{
			get
			{
				return this.has_field.has_field(0);
			}
		}

		// Token: 0x17000DFD RID: 3581
		// (get) Token: 0x06002F5A RID: 12122 RVA: 0x000BBB10 File Offset: 0x000B9D10
		// (set) Token: 0x06002F5B RID: 12123 RVA: 0x000BBB18 File Offset: 0x000B9D18
		public long count
		{
			get
			{
				return this._count;
			}
			set
			{
				this.has_field.set_field(1, true);
				this._count = value;
			}
		}

		// Token: 0x17000DFE RID: 3582
		// (get) Token: 0x06002F5C RID: 12124 RVA: 0x000BBB30 File Offset: 0x000B9D30
		public bool HasCount
		{
			get
			{
				return this.has_field.has_field(1);
			}
		}

		// Token: 0x17000DFF RID: 3583
		// (get) Token: 0x06002F5D RID: 12125 RVA: 0x000BBB40 File Offset: 0x000B9D40
		// (set) Token: 0x06002F5E RID: 12126 RVA: 0x000BBB48 File Offset: 0x000B9D48
		public long state
		{
			get
			{
				return this._state;
			}
			set
			{
				this.has_field.set_field(2, true);
				this._state = value;
			}
		}

		// Token: 0x17000E00 RID: 3584
		// (get) Token: 0x06002F5F RID: 12127 RVA: 0x000BBB60 File Offset: 0x000B9D60
		public bool HasState
		{
			get
			{
				return this.has_field.has_field(2);
			}
		}

		// Token: 0x06002F60 RID: 12128 RVA: 0x000BBB70 File Offset: 0x000B9D70
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
					this.count = this.deserialize.read_integer();
					break;
				case 2:
					this.state = this.deserialize.read_integer();
					break;
				default:
					this.deserialize.read_unknow_data();
					break;
				}
			}
		}

		// Token: 0x06002F61 RID: 12129 RVA: 0x000BBC04 File Offset: 0x000B9E04
		public override int encode(SprotoStream stream)
		{
			this.serialize.open(stream);
			if (this.has_field.has_field(0))
			{
				this.serialize.write_string(this.id, 0);
			}
			if (this.has_field.has_field(1))
			{
				this.serialize.write_integer(this.count, 1);
			}
			if (this.has_field.has_field(2))
			{
				this.serialize.write_integer(this.state, 2);
			}
			return this.serialize.close();
		}

		// Token: 0x04001F64 RID: 8036
		private static int max_field_count = 3;

		// Token: 0x04001F65 RID: 8037
		private string _id;

		// Token: 0x04001F66 RID: 8038
		private long _count;

		// Token: 0x04001F67 RID: 8039
		private long _state;
	}
}
