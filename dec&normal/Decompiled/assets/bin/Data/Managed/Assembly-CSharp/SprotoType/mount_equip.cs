using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000419 RID: 1049
	public class mount_equip
	{
		// Token: 0x0200041A RID: 1050
		public class request : SprotoTypeBase
		{
			// Token: 0x06002096 RID: 8342 RVA: 0x0009ED20 File Offset: 0x0009CF20
			public request() : base(mount_equip.request.max_field_count)
			{
			}

			// Token: 0x06002097 RID: 8343 RVA: 0x0009ED30 File Offset: 0x0009CF30
			public request(byte[] buffer) : base(mount_equip.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000901 RID: 2305
			// (get) Token: 0x06002099 RID: 8345 RVA: 0x0009ED4C File Offset: 0x0009CF4C
			// (set) Token: 0x0600209A RID: 8346 RVA: 0x0009ED54 File Offset: 0x0009CF54
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

			// Token: 0x17000902 RID: 2306
			// (get) Token: 0x0600209B RID: 8347 RVA: 0x0009ED6C File Offset: 0x0009CF6C
			public bool HasID
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x0600209C RID: 8348 RVA: 0x0009ED7C File Offset: 0x0009CF7C
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

			// Token: 0x0600209D RID: 8349 RVA: 0x0009EDD8 File Offset: 0x0009CFD8
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_string(this.ID, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x04001B83 RID: 7043
			private static int max_field_count = 1;

			// Token: 0x04001B84 RID: 7044
			private string _ID;
		}
	}
}
