using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000389 RID: 905
	public class enter_map
	{
		// Token: 0x0200038A RID: 906
		public class request : SprotoTypeBase
		{
			// Token: 0x06001B3C RID: 6972 RVA: 0x00093C08 File Offset: 0x00091E08
			public request() : base(enter_map.request.max_field_count)
			{
			}

			// Token: 0x06001B3D RID: 6973 RVA: 0x00093C18 File Offset: 0x00091E18
			public request(byte[] buffer) : base(enter_map.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x170006B9 RID: 1721
			// (get) Token: 0x06001B3F RID: 6975 RVA: 0x00093C34 File Offset: 0x00091E34
			// (set) Token: 0x06001B40 RID: 6976 RVA: 0x00093C3C File Offset: 0x00091E3C
			public string mapInfoId
			{
				get
				{
					return this._mapInfoId;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._mapInfoId = value;
				}
			}

			// Token: 0x170006BA RID: 1722
			// (get) Token: 0x06001B41 RID: 6977 RVA: 0x00093C54 File Offset: 0x00091E54
			public bool HasMapInfoId
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x170006BB RID: 1723
			// (get) Token: 0x06001B42 RID: 6978 RVA: 0x00093C64 File Offset: 0x00091E64
			// (set) Token: 0x06001B43 RID: 6979 RVA: 0x00093C6C File Offset: 0x00091E6C
			public long line_index
			{
				get
				{
					return this._line_index;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._line_index = value;
				}
			}

			// Token: 0x170006BC RID: 1724
			// (get) Token: 0x06001B44 RID: 6980 RVA: 0x00093C84 File Offset: 0x00091E84
			public bool HasLine_index
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x170006BD RID: 1725
			// (get) Token: 0x06001B45 RID: 6981 RVA: 0x00093C94 File Offset: 0x00091E94
			// (set) Token: 0x06001B46 RID: 6982 RVA: 0x00093C9C File Offset: 0x00091E9C
			public long line_count
			{
				get
				{
					return this._line_count;
				}
				set
				{
					this.has_field.set_field(2, true);
					this._line_count = value;
				}
			}

			// Token: 0x170006BE RID: 1726
			// (get) Token: 0x06001B47 RID: 6983 RVA: 0x00093CB4 File Offset: 0x00091EB4
			public bool HasLine_count
			{
				get
				{
					return this.has_field.has_field(2);
				}
			}

			// Token: 0x06001B48 RID: 6984 RVA: 0x00093CC4 File Offset: 0x00091EC4
			protected override void decode()
			{
				int num;
				while ((num = this.deserialize.read_tag()) != -1)
				{
					switch (num)
					{
					case 0:
						this.mapInfoId = this.deserialize.read_string();
						break;
					case 1:
						this.line_index = this.deserialize.read_integer();
						break;
					case 2:
						this.line_count = this.deserialize.read_integer();
						break;
					default:
						this.deserialize.read_unknow_data();
						break;
					}
				}
			}

			// Token: 0x06001B49 RID: 6985 RVA: 0x00093D58 File Offset: 0x00091F58
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_string(this.mapInfoId, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_integer(this.line_index, 1);
				}
				if (this.has_field.has_field(2))
				{
					this.serialize.write_integer(this.line_count, 2);
				}
				return this.serialize.close();
			}

			// Token: 0x04001A03 RID: 6659
			private static int max_field_count = 3;

			// Token: 0x04001A04 RID: 6660
			private string _mapInfoId;

			// Token: 0x04001A05 RID: 6661
			private long _line_index;

			// Token: 0x04001A06 RID: 6662
			private long _line_count;
		}
	}
}
