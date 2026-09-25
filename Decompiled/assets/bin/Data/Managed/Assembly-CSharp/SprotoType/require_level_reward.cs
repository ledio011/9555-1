using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020004EB RID: 1259
	public class require_level_reward
	{
		// Token: 0x020004EC RID: 1260
		public class request : SprotoTypeBase
		{
			// Token: 0x060024ED RID: 9453 RVA: 0x000A6E2C File Offset: 0x000A502C
			public request() : base(require_level_reward.request.max_field_count)
			{
			}

			// Token: 0x060024EE RID: 9454 RVA: 0x000A6E3C File Offset: 0x000A503C
			public request(byte[] buffer) : base(require_level_reward.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000A37 RID: 2615
			// (get) Token: 0x060024F0 RID: 9456 RVA: 0x000A6E58 File Offset: 0x000A5058
			// (set) Token: 0x060024F1 RID: 9457 RVA: 0x000A6E60 File Offset: 0x000A5060
			public string ID
			{
				get
				{
					return this._ID;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._ID = value;
				}
			}

			// Token: 0x17000A38 RID: 2616
			// (get) Token: 0x060024F2 RID: 9458 RVA: 0x000A6E78 File Offset: 0x000A5078
			public bool HasID
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x060024F3 RID: 9459 RVA: 0x000A6E88 File Offset: 0x000A5088
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
						this.ID = this.deserialize.read_string();
					}
				}
			}

			// Token: 0x060024F4 RID: 9460 RVA: 0x000A6EE4 File Offset: 0x000A50E4
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_string(this.ID, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x04001C8B RID: 7307
			private static int max_field_count = 1;

			// Token: 0x04001C8C RID: 7308
			private string _ID;
		}
	}
}
