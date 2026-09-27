using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020003A1 RID: 929
	public class equip_fashion_item
	{
		// Token: 0x020003A2 RID: 930
		public class request : SprotoTypeBase
		{
			// Token: 0x06001BCC RID: 7116 RVA: 0x00094DA0 File Offset: 0x00092FA0
			public request() : base(equip_fashion_item.request.max_field_count)
			{
			}

			// Token: 0x06001BCD RID: 7117 RVA: 0x00094DB0 File Offset: 0x00092FB0
			public request(byte[] buffer) : base(equip_fashion_item.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x170006E9 RID: 1769
			// (get) Token: 0x06001BCF RID: 7119 RVA: 0x00094DCC File Offset: 0x00092FCC
			// (set) Token: 0x06001BD0 RID: 7120 RVA: 0x00094DD4 File Offset: 0x00092FD4
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

			// Token: 0x170006EA RID: 1770
			// (get) Token: 0x06001BD1 RID: 7121 RVA: 0x00094DEC File Offset: 0x00092FEC
			public bool HasIndexId
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x06001BD2 RID: 7122 RVA: 0x00094DFC File Offset: 0x00092FFC
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

			// Token: 0x06001BD3 RID: 7123 RVA: 0x00094E58 File Offset: 0x00093058
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.indexId, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x04001A27 RID: 6695
			private static int max_field_count = 1;

			// Token: 0x04001A28 RID: 6696
			private long _indexId;
		}
	}
}
