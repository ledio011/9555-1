using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x0200030A RID: 778
	public class ask_pickup_item
	{
		// Token: 0x0200030B RID: 779
		public class request : SprotoTypeBase
		{
			// Token: 0x060015B5 RID: 5557 RVA: 0x00088208 File Offset: 0x00086408
			public request() : base(ask_pickup_item.request.max_field_count)
			{
			}

			// Token: 0x060015B6 RID: 5558 RVA: 0x00088218 File Offset: 0x00086418
			public request(byte[] buffer) : base(ask_pickup_item.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x1700043B RID: 1083
			// (get) Token: 0x060015B8 RID: 5560 RVA: 0x00088234 File Offset: 0x00086434
			// (set) Token: 0x060015B9 RID: 5561 RVA: 0x0008823C File Offset: 0x0008643C
			public long serverId
			{
				get
				{
					return this._serverId;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._serverId = value;
				}
			}

			// Token: 0x1700043C RID: 1084
			// (get) Token: 0x060015BA RID: 5562 RVA: 0x00088254 File Offset: 0x00086454
			public bool HasServerId
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x060015BB RID: 5563 RVA: 0x00088264 File Offset: 0x00086464
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
						this.serverId = this.deserialize.read_integer();
					}
				}
			}

			// Token: 0x060015BC RID: 5564 RVA: 0x000882C0 File Offset: 0x000864C0
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.serverId, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x04001869 RID: 6249
			private static int max_field_count = 1;

			// Token: 0x0400186A RID: 6250
			private long _serverId;
		}
	}
}
