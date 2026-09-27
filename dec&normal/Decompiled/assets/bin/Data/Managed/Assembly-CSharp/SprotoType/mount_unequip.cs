using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x0200041B RID: 1051
	public class mount_unequip
	{
		// Token: 0x0200041C RID: 1052
		public class request : SprotoTypeBase
		{
			// Token: 0x0600209F RID: 8351 RVA: 0x0009EE28 File Offset: 0x0009D028
			public request() : base(mount_unequip.request.max_field_count)
			{
			}

			// Token: 0x060020A0 RID: 8352 RVA: 0x0009EE38 File Offset: 0x0009D038
			public request(byte[] buffer) : base(mount_unequip.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000903 RID: 2307
			// (get) Token: 0x060020A2 RID: 8354 RVA: 0x0009EE54 File Offset: 0x0009D054
			// (set) Token: 0x060020A3 RID: 8355 RVA: 0x0009EE5C File Offset: 0x0009D05C
			public string ID
			{
				get
				{
					return this._ID;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._ID = value;
				}
			}

			// Token: 0x17000904 RID: 2308
			// (get) Token: 0x060020A4 RID: 8356 RVA: 0x0009EE74 File Offset: 0x0009D074
			public bool HasID
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x060020A5 RID: 8357 RVA: 0x0009EE84 File Offset: 0x0009D084
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
						this.ID = this.deserialize.read_string();
					}
				}
			}

			// Token: 0x060020A6 RID: 8358 RVA: 0x0009EEE0 File Offset: 0x0009D0E0
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_string(this.ID, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x04001B85 RID: 7045
			private static int max_field_count = 1;

			// Token: 0x04001B86 RID: 7046
			private string _ID;
		}
	}
}
