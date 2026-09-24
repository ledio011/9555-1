using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000373 RID: 883
	public class dict_hash : SprotoTypeBase
	{
		// Token: 0x06001A91 RID: 6801 RVA: 0x000926A0 File Offset: 0x000908A0
		public dict_hash() : base(dict_hash.max_field_count)
		{
		}

		// Token: 0x06001A92 RID: 6802 RVA: 0x000926B0 File Offset: 0x000908B0
		public dict_hash(byte[] buffer) : base(dict_hash.max_field_count, buffer)
		{
			this.decode();
		}

		// Token: 0x17000679 RID: 1657
		// (get) Token: 0x06001A94 RID: 6804 RVA: 0x000926CC File Offset: 0x000908CC
		// (set) Token: 0x06001A95 RID: 6805 RVA: 0x000926D4 File Offset: 0x000908D4
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

		// Token: 0x1700067A RID: 1658
		// (get) Token: 0x06001A96 RID: 6806 RVA: 0x000926EC File Offset: 0x000908EC
		public bool HasId
		{
			get
			{
				return this.has_field.has_field(0);
			}
		}

		// Token: 0x1700067B RID: 1659
		// (get) Token: 0x06001A97 RID: 6807 RVA: 0x000926FC File Offset: 0x000908FC
		// (set) Token: 0x06001A98 RID: 6808 RVA: 0x00092704 File Offset: 0x00090904
		public long parm
		{
			get
			{
				return this._parm;
			}
			set
			{
				this.has_field.set_field(1, true);
				this._parm = value;
			}
		}

		// Token: 0x1700067C RID: 1660
		// (get) Token: 0x06001A99 RID: 6809 RVA: 0x0009271C File Offset: 0x0009091C
		public bool HasParm
		{
			get
			{
				return this.has_field.has_field(1);
			}
		}

		// Token: 0x06001A9A RID: 6810 RVA: 0x0009272C File Offset: 0x0009092C
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
						this.parm = this.deserialize.read_integer();
					}
				}
				else
				{
					this.id = this.deserialize.read_string();
				}
			}
		}

		// Token: 0x06001A9B RID: 6811 RVA: 0x000927A4 File Offset: 0x000909A4
		public override int encode(SprotoStream stream)
		{
			this.serialize.open(stream);
			if (this.has_field.has_field(0))
			{
				this.serialize.write_string(this.id, 0);
			}
			if (this.has_field.has_field(1))
			{
				this.serialize.write_integer(this.parm, 1);
			}
			return this.serialize.close();
		}

		// Token: 0x040019D6 RID: 6614
		private static int max_field_count = 2;

		// Token: 0x040019D7 RID: 6615
		private string _id;

		// Token: 0x040019D8 RID: 6616
		private long _parm;
	}
}
