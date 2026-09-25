using System;
using System.Collections.Generic;
using Sproto;

namespace SprotoType
{
	// Token: 0x0200054F RID: 1359
	public class ret_mount_info
	{
		// Token: 0x02000550 RID: 1360
		public class request : SprotoTypeBase
		{
			// Token: 0x06002794 RID: 10132 RVA: 0x000AC278 File Offset: 0x000AA478
			public request() : base(ret_mount_info.request.max_field_count)
			{
			}

			// Token: 0x06002795 RID: 10133 RVA: 0x000AC288 File Offset: 0x000AA488
			public request(byte[] buffer) : base(ret_mount_info.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000B2B RID: 2859
			// (get) Token: 0x06002797 RID: 10135 RVA: 0x000AC2A4 File Offset: 0x000AA4A4
			// (set) Token: 0x06002798 RID: 10136 RVA: 0x000AC2AC File Offset: 0x000AA4AC
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

			// Token: 0x17000B2C RID: 2860
			// (get) Token: 0x06002799 RID: 10137 RVA: 0x000AC2C4 File Offset: 0x000AA4C4
			public bool HasMount_info
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x0600279A RID: 10138 RVA: 0x000AC2D4 File Offset: 0x000AA4D4
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
						this.mount_info = this.deserialize.read_map<string, mount>((mount v) => v.ID);
					}
				}
			}

			// Token: 0x0600279B RID: 10139 RVA: 0x000AC34C File Offset: 0x000AA54C
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_obj<string, mount>(this.mount_info, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x04001D44 RID: 7492
			private static int max_field_count = 1;

			// Token: 0x04001D45 RID: 7493
			private Dictionary<string, mount> _mount_info;
		}
	}
}
