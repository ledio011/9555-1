using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x0200049A RID: 1178
	public class request_change_pk_mode
	{
		// Token: 0x0200049B RID: 1179
		public class request : SprotoTypeBase
		{
			// Token: 0x060023B9 RID: 9145 RVA: 0x000A4E2C File Offset: 0x000A302C
			public request() : base(request_change_pk_mode.request.max_field_count)
			{
			}

			// Token: 0x060023BA RID: 9146 RVA: 0x000A4E3C File Offset: 0x000A303C
			public request(byte[] buffer) : base(request_change_pk_mode.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000A0D RID: 2573
			// (get) Token: 0x060023BC RID: 9148 RVA: 0x000A4E58 File Offset: 0x000A3058
			// (set) Token: 0x060023BD RID: 9149 RVA: 0x000A4E60 File Offset: 0x000A3060
			public long pk
			{
				get
				{
					return this._pk;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._pk = value;
				}
			}

			// Token: 0x17000A0E RID: 2574
			// (get) Token: 0x060023BE RID: 9150 RVA: 0x000A4E78 File Offset: 0x000A3078
			public bool HasPk
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x060023BF RID: 9151 RVA: 0x000A4E88 File Offset: 0x000A3088
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
						this.pk = this.deserialize.read_integer();
					}
				}
			}

			// Token: 0x060023C0 RID: 9152 RVA: 0x000A4EE4 File Offset: 0x000A30E4
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.pk, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x04001C4D RID: 7245
			private static int max_field_count = 1;

			// Token: 0x04001C4E RID: 7246
			private long _pk;
		}
	}
}
