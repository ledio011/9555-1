using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020005AB RID: 1451
	public class ret_use_item
	{
		// Token: 0x020005AC RID: 1452
		public class request : SprotoTypeBase
		{
			// Token: 0x060029E3 RID: 10723 RVA: 0x000B0B68 File Offset: 0x000AED68
			public request() : base(ret_use_item.request.max_field_count)
			{
			}

			// Token: 0x060029E4 RID: 10724 RVA: 0x000B0B78 File Offset: 0x000AED78
			public request(byte[] buffer) : base(ret_use_item.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000BED RID: 3053
			// (get) Token: 0x060029E6 RID: 10726 RVA: 0x000B0B94 File Offset: 0x000AED94
			// (set) Token: 0x060029E7 RID: 10727 RVA: 0x000B0B9C File Offset: 0x000AED9C
			public long success
			{
				get
				{
					return this._success;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._success = value;
				}
			}

			// Token: 0x17000BEE RID: 3054
			// (get) Token: 0x060029E8 RID: 10728 RVA: 0x000B0BB4 File Offset: 0x000AEDB4
			public bool HasSuccess
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x17000BEF RID: 3055
			// (get) Token: 0x060029E9 RID: 10729 RVA: 0x000B0BC4 File Offset: 0x000AEDC4
			// (set) Token: 0x060029EA RID: 10730 RVA: 0x000B0BCC File Offset: 0x000AEDCC
			public long indexId
			{
				get
				{
					return this._indexId;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._indexId = value;
				}
			}

			// Token: 0x17000BF0 RID: 3056
			// (get) Token: 0x060029EB RID: 10731 RVA: 0x000B0BE4 File Offset: 0x000AEDE4
			public bool HasIndexId
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x060029EC RID: 10732 RVA: 0x000B0BF4 File Offset: 0x000AEDF4
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
							this.indexId = this.deserialize.read_integer();
						}
					}
					else
					{
						this.success = this.deserialize.read_integer();
					}
				}
			}

			// Token: 0x060029ED RID: 10733 RVA: 0x000B0C6C File Offset: 0x000AEE6C
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.success, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_integer(this.indexId, 1);
				}
				return this.serialize.close();
			}

			// Token: 0x04001DEB RID: 7659
			private static int max_field_count = 2;

			// Token: 0x04001DEC RID: 7660
			private long _success;

			// Token: 0x04001DED RID: 7661
			private long _indexId;
		}
	}
}
