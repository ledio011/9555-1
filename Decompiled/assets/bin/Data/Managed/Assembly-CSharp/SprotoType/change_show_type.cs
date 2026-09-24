using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000336 RID: 822
	public class change_show_type
	{
		// Token: 0x02000337 RID: 823
		public class request : SprotoTypeBase
		{
			// Token: 0x0600178D RID: 6029 RVA: 0x0008C010 File Offset: 0x0008A210
			public request() : base(change_show_type.request.max_field_count)
			{
			}

			// Token: 0x0600178E RID: 6030 RVA: 0x0008C020 File Offset: 0x0008A220
			public request(byte[] buffer) : base(change_show_type.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000513 RID: 1299
			// (get) Token: 0x06001790 RID: 6032 RVA: 0x0008C03C File Offset: 0x0008A23C
			// (set) Token: 0x06001791 RID: 6033 RVA: 0x0008C044 File Offset: 0x0008A244
			public long showType
			{
				get
				{
					return this._showType;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._showType = value;
				}
			}

			// Token: 0x17000514 RID: 1300
			// (get) Token: 0x06001792 RID: 6034 RVA: 0x0008C05C File Offset: 0x0008A25C
			public bool HasShowType
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x06001793 RID: 6035 RVA: 0x0008C06C File Offset: 0x0008A26C
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
						this.showType = this.deserialize.read_integer();
					}
				}
			}

			// Token: 0x06001794 RID: 6036 RVA: 0x0008C0C8 File Offset: 0x0008A2C8
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.showType, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x040018EF RID: 6383
			private static int max_field_count = 1;

			// Token: 0x040018F0 RID: 6384
			private long _showType;
		}
	}
}
