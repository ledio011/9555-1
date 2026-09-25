using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000330 RID: 816
	public class change_mount_state
	{
		// Token: 0x02000331 RID: 817
		public class request : SprotoTypeBase
		{
			// Token: 0x06001772 RID: 6002 RVA: 0x0008BCF8 File Offset: 0x00089EF8
			public request() : base(change_mount_state.request.max_field_count)
			{
			}

			// Token: 0x06001773 RID: 6003 RVA: 0x0008BD08 File Offset: 0x00089F08
			public request(byte[] buffer) : base(change_mount_state.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x1700050D RID: 1293
			// (get) Token: 0x06001775 RID: 6005 RVA: 0x0008BD24 File Offset: 0x00089F24
			// (set) Token: 0x06001776 RID: 6006 RVA: 0x0008BD2C File Offset: 0x00089F2C
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

			// Token: 0x1700050E RID: 1294
			// (get) Token: 0x06001777 RID: 6007 RVA: 0x0008BD44 File Offset: 0x00089F44
			public bool HasID
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x06001778 RID: 6008 RVA: 0x0008BD54 File Offset: 0x00089F54
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

			// Token: 0x06001779 RID: 6009 RVA: 0x0008BDB0 File Offset: 0x00089FB0
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_string(this.ID, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x040018E9 RID: 6377
			private static int max_field_count = 1;

			// Token: 0x040018EA RID: 6378
			private string _ID;
		}
	}
}
