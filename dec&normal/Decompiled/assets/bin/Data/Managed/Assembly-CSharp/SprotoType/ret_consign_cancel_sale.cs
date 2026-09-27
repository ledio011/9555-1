using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000511 RID: 1297
	public class ret_consign_cancel_sale
	{
		// Token: 0x02000512 RID: 1298
		public class request : SprotoTypeBase
		{
			// Token: 0x060025F8 RID: 9720 RVA: 0x000A8F64 File Offset: 0x000A7164
			public request() : base(ret_consign_cancel_sale.request.max_field_count)
			{
			}

			// Token: 0x060025F9 RID: 9721 RVA: 0x000A8F74 File Offset: 0x000A7174
			public request(byte[] buffer) : base(ret_consign_cancel_sale.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000A9B RID: 2715
			// (get) Token: 0x060025FB RID: 9723 RVA: 0x000A8F90 File Offset: 0x000A7190
			// (set) Token: 0x060025FC RID: 9724 RVA: 0x000A8F98 File Offset: 0x000A7198
			public long id
			{
				get
				{
					return this._id;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._id = value;
				}
			}

			// Token: 0x17000A9C RID: 2716
			// (get) Token: 0x060025FD RID: 9725 RVA: 0x000A8FB0 File Offset: 0x000A71B0
			public bool HasId
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x17000A9D RID: 2717
			// (get) Token: 0x060025FE RID: 9726 RVA: 0x000A8FC0 File Offset: 0x000A71C0
			// (set) Token: 0x060025FF RID: 9727 RVA: 0x000A8FC8 File Offset: 0x000A71C8
			public long success
			{
				get
				{
					return this._success;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._success = value;
				}
			}

			// Token: 0x17000A9E RID: 2718
			// (get) Token: 0x06002600 RID: 9728 RVA: 0x000A8FE0 File Offset: 0x000A71E0
			public bool HasSuccess
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x06002601 RID: 9729 RVA: 0x000A8FF0 File Offset: 0x000A71F0
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
							this.success = this.deserialize.read_integer();
						}
					}
					else
					{
						this.id = this.deserialize.read_integer();
					}
				}
			}

			// Token: 0x06002602 RID: 9730 RVA: 0x000A9068 File Offset: 0x000A7268
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.id, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_integer(this.success, 1);
				}
				return this.serialize.close();
			}

			// Token: 0x04001CD3 RID: 7379
			private static int max_field_count = 2;

			// Token: 0x04001CD4 RID: 7380
			private long _id;

			// Token: 0x04001CD5 RID: 7381
			private long _success;
		}
	}
}
