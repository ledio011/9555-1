using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x0200039D RID: 925
	public class equip_badge
	{
		// Token: 0x0200039E RID: 926
		public class request : SprotoTypeBase
		{
			// Token: 0x06001BB4 RID: 7092 RVA: 0x00094AB0 File Offset: 0x00092CB0
			public request() : base(equip_badge.request.max_field_count)
			{
			}

			// Token: 0x06001BB5 RID: 7093 RVA: 0x00094AC0 File Offset: 0x00092CC0
			public request(byte[] buffer) : base(equip_badge.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x170006E1 RID: 1761
			// (get) Token: 0x06001BB7 RID: 7095 RVA: 0x00094ADC File Offset: 0x00092CDC
			// (set) Token: 0x06001BB8 RID: 7096 RVA: 0x00094AE4 File Offset: 0x00092CE4
			public long indexId
			{
				get
				{
					return this._indexId;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._indexId = value;
				}
			}

			// Token: 0x170006E2 RID: 1762
			// (get) Token: 0x06001BB9 RID: 7097 RVA: 0x00094AFC File Offset: 0x00092CFC
			public bool HasIndexId
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x170006E3 RID: 1763
			// (get) Token: 0x06001BBA RID: 7098 RVA: 0x00094B0C File Offset: 0x00092D0C
			// (set) Token: 0x06001BBB RID: 7099 RVA: 0x00094B14 File Offset: 0x00092D14
			public long pos
			{
				get
				{
					return this._pos;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._pos = value;
				}
			}

			// Token: 0x170006E4 RID: 1764
			// (get) Token: 0x06001BBC RID: 7100 RVA: 0x00094B2C File Offset: 0x00092D2C
			public bool HasPos
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x06001BBD RID: 7101 RVA: 0x00094B3C File Offset: 0x00092D3C
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
							this.pos = this.deserialize.read_integer();
						}
					}
					else
					{
						this.indexId = this.deserialize.read_integer();
					}
				}
			}

			// Token: 0x06001BBE RID: 7102 RVA: 0x00094BB4 File Offset: 0x00092DB4
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.indexId, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_integer(this.pos, 1);
				}
				return this.serialize.close();
			}

			// Token: 0x04001A21 RID: 6689
			private static int max_field_count = 2;

			// Token: 0x04001A22 RID: 6690
			private long _indexId;

			// Token: 0x04001A23 RID: 6691
			private long _pos;
		}
	}
}
