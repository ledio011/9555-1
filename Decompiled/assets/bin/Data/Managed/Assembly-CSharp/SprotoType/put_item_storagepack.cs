using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000447 RID: 1095
	public class put_item_storagepack
	{
		// Token: 0x02000448 RID: 1096
		public class request : SprotoTypeBase
		{
			// Token: 0x06002231 RID: 8753 RVA: 0x000A2204 File Offset: 0x000A0404
			public request() : base(put_item_storagepack.request.max_field_count)
			{
			}

			// Token: 0x06002232 RID: 8754 RVA: 0x000A2214 File Offset: 0x000A0414
			public request(byte[] buffer) : base(put_item_storagepack.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x170009AF RID: 2479
			// (get) Token: 0x06002234 RID: 8756 RVA: 0x000A2230 File Offset: 0x000A0430
			// (set) Token: 0x06002235 RID: 8757 RVA: 0x000A2238 File Offset: 0x000A0438
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

			// Token: 0x170009B0 RID: 2480
			// (get) Token: 0x06002236 RID: 8758 RVA: 0x000A2250 File Offset: 0x000A0450
			public bool HasIndexId
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x06002237 RID: 8759 RVA: 0x000A2260 File Offset: 0x000A0460
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

			// Token: 0x06002238 RID: 8760 RVA: 0x000A22BC File Offset: 0x000A04BC
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.indexId, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x04001BF4 RID: 7156
			private static int max_field_count = 1;

			// Token: 0x04001BF5 RID: 7157
			private long _indexId;
		}
	}
}
