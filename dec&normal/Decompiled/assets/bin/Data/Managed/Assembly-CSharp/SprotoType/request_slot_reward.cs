using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020004CF RID: 1231
	public class request_slot_reward
	{
		// Token: 0x020004D0 RID: 1232
		public class request : SprotoTypeBase
		{
			// Token: 0x06002478 RID: 9336 RVA: 0x000A616C File Offset: 0x000A436C
			public request() : base(request_slot_reward.request.max_field_count)
			{
			}

			// Token: 0x06002479 RID: 9337 RVA: 0x000A617C File Offset: 0x000A437C
			public request(byte[] buffer) : base(request_slot_reward.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000A21 RID: 2593
			// (get) Token: 0x0600247B RID: 9339 RVA: 0x000A6198 File Offset: 0x000A4398
			// (set) Token: 0x0600247C RID: 9340 RVA: 0x000A61A0 File Offset: 0x000A43A0
			public string uuid
			{
				get
				{
					return this._uuid;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._uuid = value;
				}
			}

			// Token: 0x17000A22 RID: 2594
			// (get) Token: 0x0600247D RID: 9341 RVA: 0x000A61B8 File Offset: 0x000A43B8
			public bool HasUuid
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x0600247E RID: 9342 RVA: 0x000A61C8 File Offset: 0x000A43C8
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
						this.uuid = this.deserialize.read_string();
					}
				}
			}

			// Token: 0x0600247F RID: 9343 RVA: 0x000A6224 File Offset: 0x000A4424
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_string(this.uuid, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x04001C72 RID: 7282
			private static int max_field_count = 1;

			// Token: 0x04001C73 RID: 7283
			private string _uuid;
		}
	}
}
