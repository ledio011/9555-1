using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020005A5 RID: 1445
	public class ret_tower_reset
	{
		// Token: 0x020005A6 RID: 1446
		public class request : SprotoTypeBase
		{
			// Token: 0x060029C7 RID: 10695 RVA: 0x000B082C File Offset: 0x000AEA2C
			public request() : base(ret_tower_reset.request.max_field_count)
			{
			}

			// Token: 0x060029C8 RID: 10696 RVA: 0x000B083C File Offset: 0x000AEA3C
			public request(byte[] buffer) : base(ret_tower_reset.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000BE7 RID: 3047
			// (get) Token: 0x060029CA RID: 10698 RVA: 0x000B0858 File Offset: 0x000AEA58
			// (set) Token: 0x060029CB RID: 10699 RVA: 0x000B0860 File Offset: 0x000AEA60
			public bool state
			{
				get
				{
					return this._state;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._state = value;
				}
			}

			// Token: 0x17000BE8 RID: 3048
			// (get) Token: 0x060029CC RID: 10700 RVA: 0x000B0878 File Offset: 0x000AEA78
			public bool HasState
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x060029CD RID: 10701 RVA: 0x000B0888 File Offset: 0x000AEA88
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
						this.state = this.deserialize.read_boolean();
					}
				}
			}

			// Token: 0x060029CE RID: 10702 RVA: 0x000B08E4 File Offset: 0x000AEAE4
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_boolean(this.state, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x04001DE4 RID: 7652
			private static int max_field_count = 1;

			// Token: 0x04001DE5 RID: 7653
			private bool _state;
		}
	}
}
