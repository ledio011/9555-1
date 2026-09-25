using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x0200038D RID: 909
	public class enter_new_map
	{
		// Token: 0x0200038E RID: 910
		public class request : SprotoTypeBase
		{
			// Token: 0x06001B5A RID: 7002 RVA: 0x00093FD8 File Offset: 0x000921D8
			public request() : base(enter_new_map.request.max_field_count)
			{
			}

			// Token: 0x06001B5B RID: 7003 RVA: 0x00093FE8 File Offset: 0x000921E8
			public request(byte[] buffer) : base(enter_new_map.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x170006C5 RID: 1733
			// (get) Token: 0x06001B5D RID: 7005 RVA: 0x00094004 File Offset: 0x00092204
			// (set) Token: 0x06001B5E RID: 7006 RVA: 0x0009400C File Offset: 0x0009220C
			public string mapInfoId
			{
				get
				{
					return this._mapInfoId;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._mapInfoId = value;
				}
			}

			// Token: 0x170006C6 RID: 1734
			// (get) Token: 0x06001B5F RID: 7007 RVA: 0x00094024 File Offset: 0x00092224
			public bool HasMapInfoId
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x06001B60 RID: 7008 RVA: 0x00094034 File Offset: 0x00092234
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
						this.mapInfoId = this.deserialize.read_string();
					}
				}
			}

			// Token: 0x06001B61 RID: 7009 RVA: 0x00094090 File Offset: 0x00092290
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_string(this.mapInfoId, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x04001A0B RID: 6667
			private static int max_field_count = 1;

			// Token: 0x04001A0C RID: 6668
			private string _mapInfoId;
		}
	}
}
