using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000589 RID: 1417
	public class ret_require_vip_info
	{
		// Token: 0x0200058A RID: 1418
		public class request : SprotoTypeBase
		{
			// Token: 0x06002912 RID: 10514 RVA: 0x000AF1D0 File Offset: 0x000AD3D0
			public request() : base(ret_require_vip_info.request.max_field_count)
			{
			}

			// Token: 0x06002913 RID: 10515 RVA: 0x000AF1E0 File Offset: 0x000AD3E0
			public request(byte[] buffer) : base(ret_require_vip_info.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000BA9 RID: 2985
			// (get) Token: 0x06002915 RID: 10517 RVA: 0x000AF1FC File Offset: 0x000AD3FC
			// (set) Token: 0x06002916 RID: 10518 RVA: 0x000AF204 File Offset: 0x000AD404
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

			// Token: 0x17000BAA RID: 2986
			// (get) Token: 0x06002917 RID: 10519 RVA: 0x000AF21C File Offset: 0x000AD41C
			public bool HasVip
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x06002918 RID: 10520 RVA: 0x000AF22C File Offset: 0x000AD42C
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

			// Token: 0x06002919 RID: 10521 RVA: 0x000AF288 File Offset: 0x000AD488
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_obj(this.vip, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x04001DB3 RID: 7603
			private static int max_field_count = 1;

			// Token: 0x04001DB4 RID: 7604
			private vip _vip;
		}
	}
}
