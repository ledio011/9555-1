using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000328 RID: 808
	public class cancel_apply_join_team
	{
		// Token: 0x02000329 RID: 809
		public class request : SprotoTypeBase
		{
			// Token: 0x06001733 RID: 5939 RVA: 0x0008B4F4 File Offset: 0x000896F4
			public request() : base(cancel_apply_join_team.request.max_field_count)
			{
			}

			// Token: 0x06001734 RID: 5940 RVA: 0x0008B504 File Offset: 0x00089704
			public request(byte[] buffer) : base(cancel_apply_join_team.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x170004F3 RID: 1267
			// (get) Token: 0x06001736 RID: 5942 RVA: 0x0008B520 File Offset: 0x00089720
			// (set) Token: 0x06001737 RID: 5943 RVA: 0x0008B528 File Offset: 0x00089728
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

			// Token: 0x170004F4 RID: 1268
			// (get) Token: 0x06001738 RID: 5944 RVA: 0x0008B540 File Offset: 0x00089740
			public bool HasId
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x06001739 RID: 5945 RVA: 0x0008B550 File Offset: 0x00089750
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
						this.id = this.deserialize.read_integer();
					}
				}
			}

			// Token: 0x0600173A RID: 5946 RVA: 0x0008B5AC File Offset: 0x000897AC
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.id, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x040018D8 RID: 6360
			private static int max_field_count = 1;

			// Token: 0x040018D9 RID: 6361
			private long _id;
		}
	}
}
