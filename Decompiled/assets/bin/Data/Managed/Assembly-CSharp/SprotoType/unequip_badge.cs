using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x0200062B RID: 1579
	public class unequip_badge
	{
		// Token: 0x0200062C RID: 1580
		public class request : SprotoTypeBase
		{
			// Token: 0x06002DFC RID: 11772 RVA: 0x000B90D8 File Offset: 0x000B72D8
			public request() : base(unequip_badge.request.max_field_count)
			{
			}

			// Token: 0x06002DFD RID: 11773 RVA: 0x000B90E8 File Offset: 0x000B72E8
			public request(byte[] buffer) : base(unequip_badge.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000D8B RID: 3467
			// (get) Token: 0x06002DFF RID: 11775 RVA: 0x000B9104 File Offset: 0x000B7304
			// (set) Token: 0x06002E00 RID: 11776 RVA: 0x000B910C File Offset: 0x000B730C
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

			// Token: 0x17000D8C RID: 3468
			// (get) Token: 0x06002E01 RID: 11777 RVA: 0x000B9124 File Offset: 0x000B7324
			public bool HasIndexId
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x06002E02 RID: 11778 RVA: 0x000B9134 File Offset: 0x000B7334
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
						this.indexId = this.deserialize.read_integer();
					}
				}
			}

			// Token: 0x06002E03 RID: 11779 RVA: 0x000B9190 File Offset: 0x000B7390
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.indexId, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x04001F0E RID: 7950
			private static int max_field_count = 1;

			// Token: 0x04001F0F RID: 7951
			private long _indexId;
		}
	}
}
