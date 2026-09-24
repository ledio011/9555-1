using System;
using System.Collections.Generic;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000551 RID: 1361
	public class ret_mount_use_color
	{
		// Token: 0x02000552 RID: 1362
		public class request : SprotoTypeBase
		{
			// Token: 0x0600279E RID: 10142 RVA: 0x000AC3A4 File Offset: 0x000AA5A4
			public request() : base(ret_mount_use_color.request.max_field_count)
			{
			}

			// Token: 0x0600279F RID: 10143 RVA: 0x000AC3B4 File Offset: 0x000AA5B4
			public request(byte[] buffer) : base(ret_mount_use_color.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000B2D RID: 2861
			// (get) Token: 0x060027A1 RID: 10145 RVA: 0x000AC3D0 File Offset: 0x000AA5D0
			// (set) Token: 0x060027A2 RID: 10146 RVA: 0x000AC3D8 File Offset: 0x000AA5D8
			public Dictionary<string, mount> mount_info
			{
				get
				{
					return this._mount_info;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._mount_info = value;
				}
			}

			// Token: 0x17000B2E RID: 2862
			// (get) Token: 0x060027A3 RID: 10147 RVA: 0x000AC3F0 File Offset: 0x000AA5F0
			public bool HasMount_info
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x17000B2F RID: 2863
			// (get) Token: 0x060027A4 RID: 10148 RVA: 0x000AC400 File Offset: 0x000AA600
			// (set) Token: 0x060027A5 RID: 10149 RVA: 0x000AC408 File Offset: 0x000AA608
			public string mountId
			{
				get
				{
					return this._mountId;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._mountId = value;
				}
			}

			// Token: 0x17000B30 RID: 2864
			// (get) Token: 0x060027A6 RID: 10150 RVA: 0x000AC420 File Offset: 0x000AA620
			public bool HasMountId
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x17000B31 RID: 2865
			// (get) Token: 0x060027A7 RID: 10151 RVA: 0x000AC430 File Offset: 0x000AA630
			// (set) Token: 0x060027A8 RID: 10152 RVA: 0x000AC438 File Offset: 0x000AA638
			public string colorId
			{
				get
				{
					return this._colorId;
				}
				set
				{
					this.has_field.set_field(2, true);
					this._colorId = value;
				}
			}

			// Token: 0x17000B32 RID: 2866
			// (get) Token: 0x060027A9 RID: 10153 RVA: 0x000AC450 File Offset: 0x000AA650
			public bool HasColorId
			{
				get
				{
					return this.has_field.has_field(2);
				}
			}

			// Token: 0x060027AA RID: 10154 RVA: 0x000AC460 File Offset: 0x000AA660
			protected override void decode()
			{
				int num;
				while ((num = this.deserialize.read_tag()) != -1)
				{
					switch (num)
					{
					case 0:
						this.mount_info = this.deserialize.read_map<string, mount>((mount v) => v.ID);
						break;
					case 1:
						this.mountId = this.deserialize.read_string();
						break;
					case 2:
						this.colorId = this.deserialize.read_string();
						break;
					default:
						this.deserialize.read_unknow_data();
						break;
					}
				}
			}

			// Token: 0x060027AB RID: 10155 RVA: 0x000AC510 File Offset: 0x000AA710
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_obj<string, mount>(this.mount_info, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_string(this.mountId, 1);
				}
				if (this.has_field.has_field(2))
				{
					this.serialize.write_string(this.colorId, 2);
				}
				return this.serialize.close();
			}

			// Token: 0x04001D47 RID: 7495
			private static int max_field_count = 3;

			// Token: 0x04001D48 RID: 7496
			private Dictionary<string, mount> _mount_info;

			// Token: 0x04001D49 RID: 7497
			private string _mountId;

			// Token: 0x04001D4A RID: 7498
			private string _colorId;
		}
	}
}
