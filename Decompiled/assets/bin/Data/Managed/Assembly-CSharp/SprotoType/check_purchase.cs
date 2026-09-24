using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x0200034F RID: 847
	public class check_purchase
	{
		// Token: 0x02000350 RID: 848
		public class request : SprotoTypeBase
		{
			// Token: 0x0600191F RID: 6431 RVA: 0x0008F66C File Offset: 0x0008D86C
			public request() : base(check_purchase.request.max_field_count)
			{
			}

			// Token: 0x06001920 RID: 6432 RVA: 0x0008F67C File Offset: 0x0008D87C
			public request(byte[] buffer) : base(check_purchase.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x170005D7 RID: 1495
			// (get) Token: 0x06001922 RID: 6434 RVA: 0x0008F698 File Offset: 0x0008D898
			// (set) Token: 0x06001923 RID: 6435 RVA: 0x0008F6A0 File Offset: 0x0008D8A0
			public string productId
			{
				get
				{
					return this._productId;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._productId = value;
				}
			}

			// Token: 0x170005D8 RID: 1496
			// (get) Token: 0x06001924 RID: 6436 RVA: 0x0008F6B8 File Offset: 0x0008D8B8
			public bool HasProductId
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x170005D9 RID: 1497
			// (get) Token: 0x06001925 RID: 6437 RVA: 0x0008F6C8 File Offset: 0x0008D8C8
			// (set) Token: 0x06001926 RID: 6438 RVA: 0x0008F6D0 File Offset: 0x0008D8D0
			public string token
			{
				get
				{
					return this._token;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._token = value;
				}
			}

			// Token: 0x170005DA RID: 1498
			// (get) Token: 0x06001927 RID: 6439 RVA: 0x0008F6E8 File Offset: 0x0008D8E8
			public bool HasToken
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x170005DB RID: 1499
			// (get) Token: 0x06001928 RID: 6440 RVA: 0x0008F6F8 File Offset: 0x0008D8F8
			// (set) Token: 0x06001929 RID: 6441 RVA: 0x0008F700 File Offset: 0x0008D900
			public string payload
			{
				get
				{
					return this._payload;
				}
				set
				{
					this.has_field.set_field(2, true);
					this._payload = value;
				}
			}

			// Token: 0x170005DC RID: 1500
			// (get) Token: 0x0600192A RID: 6442 RVA: 0x0008F718 File Offset: 0x0008D918
			public bool HasPayload
			{
				get
				{
					return this.has_field.has_field(2);
				}
			}

			// Token: 0x170005DD RID: 1501
			// (get) Token: 0x0600192B RID: 6443 RVA: 0x0008F728 File Offset: 0x0008D928
			// (set) Token: 0x0600192C RID: 6444 RVA: 0x0008F730 File Offset: 0x0008D930
			public string packageName
			{
				get
				{
					return this._packageName;
				}
				set
				{
					this.has_field.set_field(3, true);
					this._packageName = value;
				}
			}

			// Token: 0x170005DE RID: 1502
			// (get) Token: 0x0600192D RID: 6445 RVA: 0x0008F748 File Offset: 0x0008D948
			public bool HasPackageName
			{
				get
				{
					return this.has_field.has_field(3);
				}
			}

			// Token: 0x0600192E RID: 6446 RVA: 0x0008F758 File Offset: 0x0008D958
			protected override void decode()
			{
				int num;
				while ((num = this.deserialize.read_tag()) != -1)
				{
					switch (num)
					{
					case 0:
						this.productId = this.deserialize.read_string();
						break;
					case 1:
						this.token = this.deserialize.read_string();
						break;
					case 2:
						this.payload = this.deserialize.read_string();
						break;
					case 3:
						this.packageName = this.deserialize.read_string();
						break;
					default:
						this.deserialize.read_unknow_data();
						break;
					}
				}
			}

			// Token: 0x0600192F RID: 6447 RVA: 0x0008F804 File Offset: 0x0008DA04
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_string(this.productId, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_string(this.token, 1);
				}
				if (this.has_field.has_field(2))
				{
					this.serialize.write_string(this.payload, 2);
				}
				if (this.has_field.has_field(3))
				{
					this.serialize.write_string(this.packageName, 3);
				}
				return this.serialize.close();
			}

			// Token: 0x0400196E RID: 6510
			private static int max_field_count = 4;

			// Token: 0x0400196F RID: 6511
			private string _productId;

			// Token: 0x04001970 RID: 6512
			private string _token;

			// Token: 0x04001971 RID: 6513
			private string _payload;

			// Token: 0x04001972 RID: 6514
			private string _packageName;
		}
	}
}
