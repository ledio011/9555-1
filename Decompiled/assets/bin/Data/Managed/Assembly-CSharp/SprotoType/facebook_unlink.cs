using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020003B0 RID: 944
	public class facebook_unlink
	{
		// Token: 0x020003B1 RID: 945
		public class request : SprotoTypeBase
		{
			// Token: 0x06001C6B RID: 7275 RVA: 0x00096260 File Offset: 0x00094460
			public request() : base(facebook_unlink.request.max_field_count)
			{
			}

			// Token: 0x06001C6C RID: 7276 RVA: 0x00096270 File Offset: 0x00094470
			public request(byte[] buffer) : base(facebook_unlink.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000731 RID: 1841
			// (get) Token: 0x06001C6E RID: 7278 RVA: 0x0009628C File Offset: 0x0009448C
			// (set) Token: 0x06001C6F RID: 7279 RVA: 0x00096294 File Offset: 0x00094494
			public string id
			{
				get
				{
					return this._id;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._id = value;
				}
			}

			// Token: 0x17000732 RID: 1842
			// (get) Token: 0x06001C70 RID: 7280 RVA: 0x000962AC File Offset: 0x000944AC
			public bool HasId
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x17000733 RID: 1843
			// (get) Token: 0x06001C71 RID: 7281 RVA: 0x000962BC File Offset: 0x000944BC
			// (set) Token: 0x06001C72 RID: 7282 RVA: 0x000962C4 File Offset: 0x000944C4
			public string key
			{
				get
				{
					return this._key;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._key = value;
				}
			}

			// Token: 0x17000734 RID: 1844
			// (get) Token: 0x06001C73 RID: 7283 RVA: 0x000962DC File Offset: 0x000944DC
			public bool HasKey
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x17000735 RID: 1845
			// (get) Token: 0x06001C74 RID: 7284 RVA: 0x000962EC File Offset: 0x000944EC
			// (set) Token: 0x06001C75 RID: 7285 RVA: 0x000962F4 File Offset: 0x000944F4
			public string facebook_id
			{
				get
				{
					return this._facebook_id;
				}
				set
				{
					this.has_field.set_field(2, true);
					this._facebook_id = value;
				}
			}

			// Token: 0x17000736 RID: 1846
			// (get) Token: 0x06001C76 RID: 7286 RVA: 0x0009630C File Offset: 0x0009450C
			public bool HasFacebook_id
			{
				get
				{
					return this.has_field.has_field(2);
				}
			}

			// Token: 0x17000737 RID: 1847
			// (get) Token: 0x06001C77 RID: 7287 RVA: 0x0009631C File Offset: 0x0009451C
			// (set) Token: 0x06001C78 RID: 7288 RVA: 0x00096324 File Offset: 0x00094524
			public string facebook_token
			{
				get
				{
					return this._facebook_token;
				}
				set
				{
					this.has_field.set_field(3, true);
					this._facebook_token = value;
				}
			}

			// Token: 0x17000738 RID: 1848
			// (get) Token: 0x06001C79 RID: 7289 RVA: 0x0009633C File Offset: 0x0009453C
			public bool HasFacebook_token
			{
				get
				{
					return this.has_field.has_field(3);
				}
			}

			// Token: 0x17000739 RID: 1849
			// (get) Token: 0x06001C7A RID: 7290 RVA: 0x0009634C File Offset: 0x0009454C
			// (set) Token: 0x06001C7B RID: 7291 RVA: 0x00096354 File Offset: 0x00094554
			public long bindType
			{
				get
				{
					return this._bindType;
				}
				set
				{
					this.has_field.set_field(4, true);
					this._bindType = value;
				}
			}

			// Token: 0x1700073A RID: 1850
			// (get) Token: 0x06001C7C RID: 7292 RVA: 0x0009636C File Offset: 0x0009456C
			public bool HasBindType
			{
				get
				{
					return this.has_field.has_field(4);
				}
			}

			// Token: 0x06001C7D RID: 7293 RVA: 0x0009637C File Offset: 0x0009457C
			protected override void decode()
			{
				int num;
				while ((num = this.deserialize.read_tag()) != -1)
				{
					switch (num)
					{
					case 0:
						this.id = this.deserialize.read_string();
						break;
					case 1:
						this.key = this.deserialize.read_string();
						break;
					case 2:
						this.facebook_id = this.deserialize.read_string();
						break;
					case 3:
						this.facebook_token = this.deserialize.read_string();
						break;
					case 4:
						this.bindType = this.deserialize.read_integer();
						break;
					default:
						this.deserialize.read_unknow_data();
						break;
					}
				}
			}

			// Token: 0x06001C7E RID: 7294 RVA: 0x00096444 File Offset: 0x00094644
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_string(this.id, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_string(this.key, 1);
				}
				if (this.has_field.has_field(2))
				{
					this.serialize.write_string(this.facebook_id, 2);
				}
				if (this.has_field.has_field(3))
				{
					this.serialize.write_string(this.facebook_token, 3);
				}
				if (this.has_field.has_field(4))
				{
					this.serialize.write_integer(this.bindType, 4);
				}
				return this.serialize.close();
			}

			// Token: 0x04001A54 RID: 6740
			private static int max_field_count = 5;

			// Token: 0x04001A55 RID: 6741
			private string _id;

			// Token: 0x04001A56 RID: 6742
			private string _key;

			// Token: 0x04001A57 RID: 6743
			private string _facebook_id;

			// Token: 0x04001A58 RID: 6744
			private string _facebook_token;

			// Token: 0x04001A59 RID: 6745
			private long _bindType;
		}

		// Token: 0x020003B2 RID: 946
		public class response : SprotoTypeBase
		{
			// Token: 0x06001C7F RID: 7295 RVA: 0x00096518 File Offset: 0x00094718
			public response() : base(facebook_unlink.response.max_field_count)
			{
			}

			// Token: 0x06001C80 RID: 7296 RVA: 0x00096528 File Offset: 0x00094728
			public response(byte[] buffer) : base(facebook_unlink.response.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x1700073B RID: 1851
			// (get) Token: 0x06001C82 RID: 7298 RVA: 0x00096544 File Offset: 0x00094744
			// (set) Token: 0x06001C83 RID: 7299 RVA: 0x0009654C File Offset: 0x0009474C
			public long state
			{
				get
				{
					return this._state;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._state = value;
				}
			}

			// Token: 0x1700073C RID: 1852
			// (get) Token: 0x06001C84 RID: 7300 RVA: 0x00096564 File Offset: 0x00094764
			public bool HasState
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x1700073D RID: 1853
			// (get) Token: 0x06001C85 RID: 7301 RVA: 0x00096574 File Offset: 0x00094774
			// (set) Token: 0x06001C86 RID: 7302 RVA: 0x0009657C File Offset: 0x0009477C
			public long bindType
			{
				get
				{
					return this._bindType;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._bindType = value;
				}
			}

			// Token: 0x1700073E RID: 1854
			// (get) Token: 0x06001C87 RID: 7303 RVA: 0x00096594 File Offset: 0x00094794
			public bool HasBindType
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x06001C88 RID: 7304 RVA: 0x000965A4 File Offset: 0x000947A4
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
							this.bindType = this.deserialize.read_integer();
						}
					}
					else
					{
						this.state = this.deserialize.read_integer();
					}
				}
			}

			// Token: 0x06001C89 RID: 7305 RVA: 0x0009661C File Offset: 0x0009481C
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.state, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_integer(this.bindType, 1);
				}
				return this.serialize.close();
			}

			// Token: 0x04001A5A RID: 6746
			private static int max_field_count = 2;

			// Token: 0x04001A5B RID: 6747
			private long _state;

			// Token: 0x04001A5C RID: 6748
			private long _bindType;
		}
	}
}
