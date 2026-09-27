using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020003A6 RID: 934
	public class equip_inlay
	{
		// Token: 0x020003A7 RID: 935
		public class request : SprotoTypeBase
		{
			// Token: 0x06001BFB RID: 7163 RVA: 0x000953B0 File Offset: 0x000935B0
			public request() : base(equip_inlay.request.max_field_count)
			{
			}

			// Token: 0x06001BFC RID: 7164 RVA: 0x000953C0 File Offset: 0x000935C0
			public request(byte[] buffer) : base(equip_inlay.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x170006FD RID: 1789
			// (get) Token: 0x06001BFE RID: 7166 RVA: 0x000953DC File Offset: 0x000935DC
			// (set) Token: 0x06001BFF RID: 7167 RVA: 0x000953E4 File Offset: 0x000935E4
			public long index1
			{
				get
				{
					return this._index1;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._index1 = value;
				}
			}

			// Token: 0x170006FE RID: 1790
			// (get) Token: 0x06001C00 RID: 7168 RVA: 0x000953FC File Offset: 0x000935FC
			public bool HasIndex1
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x170006FF RID: 1791
			// (get) Token: 0x06001C01 RID: 7169 RVA: 0x0009540C File Offset: 0x0009360C
			// (set) Token: 0x06001C02 RID: 7170 RVA: 0x00095414 File Offset: 0x00093614
			public long index2
			{
				get
				{
					return this._index2;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._index2 = value;
				}
			}

			// Token: 0x17000700 RID: 1792
			// (get) Token: 0x06001C03 RID: 7171 RVA: 0x0009542C File Offset: 0x0009362C
			public bool HasIndex2
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x17000701 RID: 1793
			// (get) Token: 0x06001C04 RID: 7172 RVA: 0x0009543C File Offset: 0x0009363C
			// (set) Token: 0x06001C05 RID: 7173 RVA: 0x00095444 File Offset: 0x00093644
			public long diamond_index1
			{
				get
				{
					return this._diamond_index1;
				}
				set
				{
					this.has_field.set_field(2, true);
					this._diamond_index1 = value;
				}
			}

			// Token: 0x17000702 RID: 1794
			// (get) Token: 0x06001C06 RID: 7174 RVA: 0x0009545C File Offset: 0x0009365C
			public bool HasDiamond_index1
			{
				get
				{
					return this.has_field.has_field(2);
				}
			}

			// Token: 0x17000703 RID: 1795
			// (get) Token: 0x06001C07 RID: 7175 RVA: 0x0009546C File Offset: 0x0009366C
			// (set) Token: 0x06001C08 RID: 7176 RVA: 0x00095474 File Offset: 0x00093674
			public long diamond_index2
			{
				get
				{
					return this._diamond_index2;
				}
				set
				{
					this.has_field.set_field(3, true);
					this._diamond_index2 = value;
				}
			}

			// Token: 0x17000704 RID: 1796
			// (get) Token: 0x06001C09 RID: 7177 RVA: 0x0009548C File Offset: 0x0009368C
			public bool HasDiamond_index2
			{
				get
				{
					return this.has_field.has_field(3);
				}
			}

			// Token: 0x06001C0A RID: 7178 RVA: 0x0009549C File Offset: 0x0009369C
			protected override void decode()
			{
				int num;
				while ((num = this.deserialize.read_tag()) != -1)
				{
					switch (num)
					{
					case 0:
						this.index1 = this.deserialize.read_integer();
						break;
					case 1:
						this.index2 = this.deserialize.read_integer();
						break;
					case 2:
						this.diamond_index1 = this.deserialize.read_integer();
						break;
					case 3:
						this.diamond_index2 = this.deserialize.read_integer();
						break;
					default:
						this.deserialize.read_unknow_data();
						break;
					}
				}
			}

			// Token: 0x06001C0B RID: 7179 RVA: 0x00095548 File Offset: 0x00093748
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.index1, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_integer(this.index2, 1);
				}
				if (this.has_field.has_field(2))
				{
					this.serialize.write_integer(this.diamond_index1, 2);
				}
				if (this.has_field.has_field(3))
				{
					this.serialize.write_integer(this.diamond_index2, 3);
				}
				return this.serialize.close();
			}

			// Token: 0x04001A34 RID: 6708
			private static int max_field_count = 4;

			// Token: 0x04001A35 RID: 6709
			private long _index1;

			// Token: 0x04001A36 RID: 6710
			private long _index2;

			// Token: 0x04001A37 RID: 6711
			private long _diamond_index1;

			// Token: 0x04001A38 RID: 6712
			private long _diamond_index2;
		}
	}
}
