using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000482 RID: 1154
	public class req_invite_team_result
	{
		// Token: 0x02000483 RID: 1155
		public class request : SprotoTypeBase
		{
			// Token: 0x06002356 RID: 9046 RVA: 0x000A437C File Offset: 0x000A257C
			public request() : base(req_invite_team_result.request.max_field_count)
			{
			}

			// Token: 0x06002357 RID: 9047 RVA: 0x000A438C File Offset: 0x000A258C
			public request(byte[] buffer) : base(req_invite_team_result.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x170009FB RID: 2555
			// (get) Token: 0x06002359 RID: 9049 RVA: 0x000A43A8 File Offset: 0x000A25A8
			// (set) Token: 0x0600235A RID: 9050 RVA: 0x000A43B0 File Offset: 0x000A25B0
			public long ok
			{
				get
				{
					return this._ok;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._ok = value;
				}
			}

			// Token: 0x170009FC RID: 2556
			// (get) Token: 0x0600235B RID: 9051 RVA: 0x000A43C8 File Offset: 0x000A25C8
			public bool HasOk
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x170009FD RID: 2557
			// (get) Token: 0x0600235C RID: 9052 RVA: 0x000A43D8 File Offset: 0x000A25D8
			// (set) Token: 0x0600235D RID: 9053 RVA: 0x000A43E0 File Offset: 0x000A25E0
			public long id
			{
				get
				{
					return this._id;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._id = value;
				}
			}

			// Token: 0x170009FE RID: 2558
			// (get) Token: 0x0600235E RID: 9054 RVA: 0x000A43F8 File Offset: 0x000A25F8
			public bool HasId
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x0600235F RID: 9055 RVA: 0x000A4408 File Offset: 0x000A2608
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
							this.id = this.deserialize.read_integer();
						}
					}
					else
					{
						this.ok = this.deserialize.read_integer();
					}
				}
			}

			// Token: 0x06002360 RID: 9056 RVA: 0x000A4480 File Offset: 0x000A2680
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.ok, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_integer(this.id, 1);
				}
				return this.serialize.close();
			}

			// Token: 0x04001C38 RID: 7224
			private static int max_field_count = 2;

			// Token: 0x04001C39 RID: 7225
			private long _ok;

			// Token: 0x04001C3A RID: 7226
			private long _id;
		}
	}
}
