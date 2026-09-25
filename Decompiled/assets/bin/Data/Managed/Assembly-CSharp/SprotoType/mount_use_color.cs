using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x0200041D RID: 1053
	public class mount_use_color
	{
		// Token: 0x0200041E RID: 1054
		public class request : SprotoTypeBase
		{
			// Token: 0x060020A8 RID: 8360 RVA: 0x0009EF30 File Offset: 0x0009D130
			public request() : base(mount_use_color.request.max_field_count)
			{
			}

			// Token: 0x060020A9 RID: 8361 RVA: 0x0009EF40 File Offset: 0x0009D140
			public request(byte[] buffer) : base(mount_use_color.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000905 RID: 2309
			// (get) Token: 0x060020AB RID: 8363 RVA: 0x0009EF5C File Offset: 0x0009D15C
			// (set) Token: 0x060020AC RID: 8364 RVA: 0x0009EF64 File Offset: 0x0009D164
			public string mountId
			{
				get
				{
					return this._mountId;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._mountId = value;
				}
			}

			// Token: 0x17000906 RID: 2310
			// (get) Token: 0x060020AD RID: 8365 RVA: 0x0009EF7C File Offset: 0x0009D17C
			public bool HasMountId
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x17000907 RID: 2311
			// (get) Token: 0x060020AE RID: 8366 RVA: 0x0009EF8C File Offset: 0x0009D18C
			// (set) Token: 0x060020AF RID: 8367 RVA: 0x0009EF94 File Offset: 0x0009D194
			public string colorId
			{
				get
				{
					return this._colorId;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._colorId = value;
				}
			}

			// Token: 0x17000908 RID: 2312
			// (get) Token: 0x060020B0 RID: 8368 RVA: 0x0009EFAC File Offset: 0x0009D1AC
			public bool HasColorId
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x060020B1 RID: 8369 RVA: 0x0009EFBC File Offset: 0x0009D1BC
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
							this.colorId = this.deserialize.read_string();
						}
					}
					else
					{
						this.mountId = this.deserialize.read_string();
					}
				}
			}

			// Token: 0x060020B2 RID: 8370 RVA: 0x0009F034 File Offset: 0x0009D234
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_string(this.mountId, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_string(this.colorId, 1);
				}
				return this.serialize.close();
			}

			// Token: 0x04001B87 RID: 7047
			private static int max_field_count = 2;

			// Token: 0x04001B88 RID: 7048
			private string _mountId;

			// Token: 0x04001B89 RID: 7049
			private string _colorId;
		}
	}
}
