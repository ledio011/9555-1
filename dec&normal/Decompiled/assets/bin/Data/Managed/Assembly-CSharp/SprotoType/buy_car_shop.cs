using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000322 RID: 802
	public class buy_car_shop
	{
		// Token: 0x02000323 RID: 803
		public class request : SprotoTypeBase
		{
			// Token: 0x06001712 RID: 5906 RVA: 0x0008B0FC File Offset: 0x000892FC
			public request() : base(buy_car_shop.request.max_field_count)
			{
			}

			// Token: 0x06001713 RID: 5907 RVA: 0x0008B10C File Offset: 0x0008930C
			public request(byte[] buffer) : base(buy_car_shop.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x170004E9 RID: 1257
			// (get) Token: 0x06001715 RID: 5909 RVA: 0x0008B128 File Offset: 0x00089328
			// (set) Token: 0x06001716 RID: 5910 RVA: 0x0008B130 File Offset: 0x00089330
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

			// Token: 0x170004EA RID: 1258
			// (get) Token: 0x06001717 RID: 5911 RVA: 0x0008B148 File Offset: 0x00089348
			public bool HasMountId
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x06001718 RID: 5912 RVA: 0x0008B158 File Offset: 0x00089358
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
						this.mountId = this.deserialize.read_string();
					}
				}
			}

			// Token: 0x06001719 RID: 5913 RVA: 0x0008B1B4 File Offset: 0x000893B4
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_string(this.mountId, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x040018D0 RID: 6352
			private static int max_field_count = 1;

			// Token: 0x040018D1 RID: 6353
			private string _mountId;
		}
	}
}
