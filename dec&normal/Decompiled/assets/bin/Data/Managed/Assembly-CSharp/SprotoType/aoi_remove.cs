using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020002EE RID: 750
	public class aoi_remove
	{
		// Token: 0x020002EF RID: 751
		public class request : SprotoTypeBase
		{
			// Token: 0x06001518 RID: 5400 RVA: 0x00086F38 File Offset: 0x00085138
			public request() : base(aoi_remove.request.max_field_count)
			{
			}

			// Token: 0x06001519 RID: 5401 RVA: 0x00086F48 File Offset: 0x00085148
			public request(byte[] buffer) : base(aoi_remove.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x1700040D RID: 1037
			// (get) Token: 0x0600151B RID: 5403 RVA: 0x00086F64 File Offset: 0x00085164
			// (set) Token: 0x0600151C RID: 5404 RVA: 0x00086F6C File Offset: 0x0008516C
			public long character
			{
				get
				{
					return this._character;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._character = value;
				}
			}

			// Token: 0x1700040E RID: 1038
			// (get) Token: 0x0600151D RID: 5405 RVA: 0x00086F84 File Offset: 0x00085184
			public bool HasCharacter
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x0600151E RID: 5406 RVA: 0x00086F94 File Offset: 0x00085194
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
						this.character = this.deserialize.read_integer();
					}
				}
			}

			// Token: 0x0600151F RID: 5407 RVA: 0x00086FF0 File Offset: 0x000851F0
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.character, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x04001843 RID: 6211
			private static int max_field_count = 1;

			// Token: 0x04001844 RID: 6212
			private long _character;
		}
	}
}
