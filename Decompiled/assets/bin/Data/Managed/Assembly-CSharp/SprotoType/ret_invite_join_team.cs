using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000549 RID: 1353
	public class ret_invite_join_team
	{
		// Token: 0x0200054A RID: 1354
		public class request : SprotoTypeBase
		{
			// Token: 0x06002771 RID: 10097 RVA: 0x000ABE38 File Offset: 0x000AA038
			public request() : base(ret_invite_join_team.request.max_field_count)
			{
			}

			// Token: 0x06002772 RID: 10098 RVA: 0x000ABE48 File Offset: 0x000AA048
			public request(byte[] buffer) : base(ret_invite_join_team.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000B21 RID: 2849
			// (get) Token: 0x06002774 RID: 10100 RVA: 0x000ABE64 File Offset: 0x000AA064
			// (set) Token: 0x06002775 RID: 10101 RVA: 0x000ABE6C File Offset: 0x000AA06C
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

			// Token: 0x17000B22 RID: 2850
			// (get) Token: 0x06002776 RID: 10102 RVA: 0x000ABE84 File Offset: 0x000AA084
			public bool HasOk
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x17000B23 RID: 2851
			// (get) Token: 0x06002777 RID: 10103 RVA: 0x000ABE94 File Offset: 0x000AA094
			// (set) Token: 0x06002778 RID: 10104 RVA: 0x000ABE9C File Offset: 0x000AA09C
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

			// Token: 0x17000B24 RID: 2852
			// (get) Token: 0x06002779 RID: 10105 RVA: 0x000ABEB4 File Offset: 0x000AA0B4
			public bool HasId
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x0600277A RID: 10106 RVA: 0x000ABEC4 File Offset: 0x000AA0C4
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

			// Token: 0x0600277B RID: 10107 RVA: 0x000ABF3C File Offset: 0x000AA13C
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

			// Token: 0x04001D3A RID: 7482
			private static int max_field_count = 2;

			// Token: 0x04001D3B RID: 7483
			private long _ok;

			// Token: 0x04001D3C RID: 7484
			private long _id;
		}
	}
}
