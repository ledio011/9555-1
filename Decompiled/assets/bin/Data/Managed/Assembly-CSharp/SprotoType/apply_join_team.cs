using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020002FC RID: 764
	public class apply_join_team
	{
		// Token: 0x020002FD RID: 765
		public class request : SprotoTypeBase
		{
			// Token: 0x06001560 RID: 5472 RVA: 0x000877C0 File Offset: 0x000859C0
			public request() : base(apply_join_team.request.max_field_count)
			{
			}

			// Token: 0x06001561 RID: 5473 RVA: 0x000877D0 File Offset: 0x000859D0
			public request(byte[] buffer) : base(apply_join_team.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000421 RID: 1057
			// (get) Token: 0x06001563 RID: 5475 RVA: 0x000877EC File Offset: 0x000859EC
			// (set) Token: 0x06001564 RID: 5476 RVA: 0x000877F4 File Offset: 0x000859F4
			public teammember member
			{
				get
				{
					return this._member;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._member = value;
				}
			}

			// Token: 0x17000422 RID: 1058
			// (get) Token: 0x06001565 RID: 5477 RVA: 0x0008780C File Offset: 0x00085A0C
			public bool HasMember
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x06001566 RID: 5478 RVA: 0x0008781C File Offset: 0x00085A1C
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
						this.member = this.deserialize.read_obj<teammember>();
					}
				}
			}

			// Token: 0x06001567 RID: 5479 RVA: 0x00087878 File Offset: 0x00085A78
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_obj(this.member, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x04001854 RID: 6228
			private static int max_field_count = 1;

			// Token: 0x04001855 RID: 6229
			private teammember _member;
		}
	}
}
