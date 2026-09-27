using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x0200058B RID: 1419
	public class ret_require_vip_reward
	{
		// Token: 0x0200058C RID: 1420
		public class request : SprotoTypeBase
		{
			// Token: 0x0600291B RID: 10523 RVA: 0x000AF2D8 File Offset: 0x000AD4D8
			public request() : base(ret_require_vip_reward.request.max_field_count)
			{
			}

			// Token: 0x0600291C RID: 10524 RVA: 0x000AF2E8 File Offset: 0x000AD4E8
			public request(byte[] buffer) : base(ret_require_vip_reward.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000BAB RID: 2987
			// (get) Token: 0x0600291E RID: 10526 RVA: 0x000AF304 File Offset: 0x000AD504
			// (set) Token: 0x0600291F RID: 10527 RVA: 0x000AF30C File Offset: 0x000AD50C
			public vip vip
			{
				get
				{
					return this._vip;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._vip = value;
				}
			}

			// Token: 0x17000BAC RID: 2988
			// (get) Token: 0x06002920 RID: 10528 RVA: 0x000AF324 File Offset: 0x000AD524
			public bool HasVip
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x06002921 RID: 10529 RVA: 0x000AF334 File Offset: 0x000AD534
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
						this.vip = this.deserialize.read_obj<vip>();
					}
				}
			}

			// Token: 0x06002922 RID: 10530 RVA: 0x000AF390 File Offset: 0x000AD590
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_obj(this.vip, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x04001DB5 RID: 7605
			private static int max_field_count = 1;

			// Token: 0x04001DB6 RID: 7606
			private vip _vip;
		}
	}
}
