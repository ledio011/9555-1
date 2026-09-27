using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x0200039B RID: 923
	public class equip_appraise
	{
		// Token: 0x0200039C RID: 924
		public class request : SprotoTypeBase
		{
			// Token: 0x06001BAB RID: 7083 RVA: 0x000949A8 File Offset: 0x00092BA8
			public request() : base(equip_appraise.request.max_field_count)
			{
			}

			// Token: 0x06001BAC RID: 7084 RVA: 0x000949B8 File Offset: 0x00092BB8
			public request(byte[] buffer) : base(equip_appraise.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x170006DF RID: 1759
			// (get) Token: 0x06001BAE RID: 7086 RVA: 0x000949D4 File Offset: 0x00092BD4
			// (set) Token: 0x06001BAF RID: 7087 RVA: 0x000949DC File Offset: 0x00092BDC
			public long index
			{
				get
				{
					return this._index;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._index = value;
				}
			}

			// Token: 0x170006E0 RID: 1760
			// (get) Token: 0x06001BB0 RID: 7088 RVA: 0x000949F4 File Offset: 0x00092BF4
			public bool HasIndex
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x06001BB1 RID: 7089 RVA: 0x00094A04 File Offset: 0x00092C04
			protected override void decode()
			{
				int num;
				while ((num = this.deserialize.read_tag()) != -1)
				{
					int num2 = num;
					if (num2 != 0)
					{
						this.deserialize.read_unknow_data();
					}
					else
					{
						this.index = this.deserialize.read_integer();
					}
				}
			}

			// Token: 0x06001BB2 RID: 7090 RVA: 0x00094A60 File Offset: 0x00092C60
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.index, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x04001A1F RID: 6687
			private static int max_field_count = 1;

			// Token: 0x04001A20 RID: 6688
			private long _index;
		}
	}
}
