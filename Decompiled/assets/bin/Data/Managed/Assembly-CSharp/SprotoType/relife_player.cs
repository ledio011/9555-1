using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000468 RID: 1128
	public class relife_player
	{
		// Token: 0x02000469 RID: 1129
		public class request : SprotoTypeBase
		{
			// Token: 0x060022D8 RID: 8920 RVA: 0x000A3548 File Offset: 0x000A1748
			public request() : base(relife_player.request.max_field_count)
			{
			}

			// Token: 0x060022D9 RID: 8921 RVA: 0x000A3558 File Offset: 0x000A1758
			public request(byte[] buffer) : base(relife_player.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x170009DB RID: 2523
			// (get) Token: 0x060022DB RID: 8923 RVA: 0x000A3574 File Offset: 0x000A1774
			// (set) Token: 0x060022DC RID: 8924 RVA: 0x000A357C File Offset: 0x000A177C
			public bool isInplace
			{
				get
				{
					return this._isInplace;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._isInplace = value;
				}
			}

			// Token: 0x170009DC RID: 2524
			// (get) Token: 0x060022DD RID: 8925 RVA: 0x000A3594 File Offset: 0x000A1794
			public bool HasIsInplace
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x060022DE RID: 8926 RVA: 0x000A35A4 File Offset: 0x000A17A4
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
						this.isInplace = this.deserialize.read_boolean();
					}
				}
			}

			// Token: 0x060022DF RID: 8927 RVA: 0x000A3600 File Offset: 0x000A1800
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_boolean(this.isInplace, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x04001C1B RID: 7195
			private static int max_field_count = 1;

			// Token: 0x04001C1C RID: 7196
			private bool _isInplace;
		}
	}
}
