using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020003AD RID: 941
	public class facebook_link
	{
		// Token: 0x020003AE RID: 942
		public class request : SprotoTypeBase
		{
			// Token: 0x06001C42 RID: 7234 RVA: 0x00095CEC File Offset: 0x00093EEC
			public request() : base(facebook_link.request.max_field_count)
			{
			}

			// Token: 0x06001C43 RID: 7235 RVA: 0x00095CFC File Offset: 0x00093EFC
			public request(byte[] buffer) : base(facebook_link.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x1700071D RID: 1821
			// (get) Token: 0x06001C45 RID: 7237 RVA: 0x00095D18 File Offset: 0x00093F18
			// (set) Token: 0x06001C46 RID: 7238 RVA: 0x00095D20 File Offset: 0x00093F20
			public string facebook_id
			{
				get
				{
					return this._facebook_id;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._facebook_id = value;
				}
			}

			// Token: 0x1700071E RID: 1822
			// (get) Token: 0x06001C47 RID: 7239 RVA: 0x00095D38 File Offset: 0x00093F38
			public bool HasFacebook_id
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x1700071F RID: 1823
			// (get) Token: 0x06001C48 RID: 7240 RVA: 0x00095D48 File Offset: 0x00093F48
			// (set) Token: 0x06001C49 RID: 7241 RVA: 0x00095D50 File Offset: 0x00093F50
			public string facebook_token
			{
				get
				{
					return this._facebook_token;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._facebook_token = value;
				}
			}

			// Token: 0x17000720 RID: 1824
			// (get) Token: 0x06001C4A RID: 7242 RVA: 0x00095D68 File Offset: 0x00093F68
			public bool HasFacebook_token
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x17000721 RID: 1825
			// (get) Token: 0x06001C4B RID: 7243 RVA: 0x00095D78 File Offset: 0x00093F78
			// (set) Token: 0x06001C4C RID: 7244 RVA: 0x00095D80 File Offset: 0x00093F80
			public string id
			{
				get
				{
					return this._id;
				}
				set
				{
					this.has_field.set_field(2, true);
					this._id = value;
				}
			}

			// Token: 0x17000722 RID: 1826
			// (get) Token: 0x06001C4D RID: 7245 RVA: 0x00095D98 File Offset: 0x00093F98
			public bool HasId
			{
				get
				{
					return this.has_field.has_field(2);
				}
			}

			// Token: 0x17000723 RID: 1827
			// (get) Token: 0x06001C4E RID: 7246 RVA: 0x00095DA8 File Offset: 0x00093FA8
			// (set) Token: 0x06001C4F RID: 7247 RVA: 0x00095DB0 File Offset: 0x00093FB0
			public string key
			{
				get
				{
					return this._key;
				}
				set
				{
					this.has_field.set_field(3, true);
					this._key = value;
				}
			}

			// Token: 0x17000724 RID: 1828
			// (get) Token: 0x06001C50 RID: 7248 RVA: 0x00095DC8 File Offset: 0x00093FC8
			public bool HasKey
			{
				get
				{
					return this.has_field.has_field(3);
				}
			}

			// Token: 0x17000725 RID: 1829
			// (get) Token: 0x06001C51 RID: 7249 RVA: 0x00095DD8 File Offset: 0x00093FD8
			// (set) Token: 0x06001C52 RID: 7250 RVA: 0x00095DE0 File Offset: 0x00093FE0
			public long confirm
			{
				get
				{
					return this._confirm;
				}
				set
				{
					this.has_field.set_field(4, true);
					this._confirm = value;
				}
			}

			// Token: 0x17000726 RID: 1830
			// (get) Token: 0x06001C53 RID: 7251 RVA: 0x00095DF8 File Offset: 0x00093FF8
			public bool HasConfirm
			{
				get
				{
					return this.has_field.has_field(4);
				}
			}

			// Token: 0x17000727 RID: 1831
			// (get) Token: 0x06001C54 RID: 7252 RVA: 0x00095E08 File Offset: 0x00094008
			// (set) Token: 0x06001C55 RID: 7253 RVA: 0x00095E10 File Offset: 0x00094010
			public long bindType
			{
				get
				{
					return this._bindType;
				}
				set
				{
					this.has_field.set_field(5, true);
					this._bindType = value;
				}
			}

			// Token: 0x17000728 RID: 1832
			// (get) Token: 0x06001C56 RID: 7254 RVA: 0x00095E28 File Offset: 0x00094028
			public bool HasBindType
			{
				get
				{
					return this.has_field.has_field(5);
				}
			}

			// Token: 0x06001C57 RID: 7255 RVA: 0x00095E38 File Offset: 0x00094038
			protected override void decode()
			{
				int num;
				while ((num = this.deserialize.read_tag()) != -1)
				{
					switch (num)
					{
					case 0:
						this.facebook_id = this.deserialize.read_string();
						break;
					case 1:
						this.facebook_token = this.deserialize.read_string();
						break;
					case 2:
						this.id = this.deserialize.read_string();
						break;
					case 3:
						this.key = this.deserialize.read_string();
						break;
					case 4:
						this.confirm = this.deserialize.read_integer();
						break;
					case 5:
						this.bindType = this.deserialize.read_integer();
						break;
					default:
						this.deserialize.read_unknow_data();
						break;
					}
				}
			}

			// Token: 0x06001C58 RID: 7256 RVA: 0x00095F18 File Offset: 0x00094118
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_string(this.facebook_id, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_string(this.facebook_token, 1);
				}
				if (this.has_field.has_field(2))
				{
					this.serialize.write_string(this.id, 2);
				}
				if (this.has_field.has_field(3))
				{
					this.serialize.write_string(this.key, 3);
				}
				if (this.has_field.has_field(4))
				{
					this.serialize.write_integer(this.confirm, 4);
				}
				if (this.has_field.has_field(5))
				{
					this.serialize.write_integer(this.bindType, 5);
				}
				return this.serialize.close();
			}

			// Token: 0x04001A48 RID: 6728
			private static int max_field_count = 6;

			// Token: 0x04001A49 RID: 6729
			private string _facebook_id;

			// Token: 0x04001A4A RID: 6730
			private string _facebook_token;

			// Token: 0x04001A4B RID: 6731
			private string _id;

			// Token: 0x04001A4C RID: 6732
			private string _key;

			// Token: 0x04001A4D RID: 6733
			private long _confirm;

			// Token: 0x04001A4E RID: 6734
			private long _bindType;
		}

		// Token: 0x020003AF RID: 943
		public class response : SprotoTypeBase
		{
			// Token: 0x06001C59 RID: 7257 RVA: 0x00096010 File Offset: 0x00094210
			public response() : base(facebook_link.response.max_field_count)
			{
			}

			// Token: 0x06001C5A RID: 7258 RVA: 0x00096020 File Offset: 0x00094220
			public response(byte[] buffer) : base(facebook_link.response.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000729 RID: 1833
			// (get) Token: 0x06001C5C RID: 7260 RVA: 0x0009603C File Offset: 0x0009423C
			// (set) Token: 0x06001C5D RID: 7261 RVA: 0x00096044 File Offset: 0x00094244
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

			// Token: 0x1700072A RID: 1834
			// (get) Token: 0x06001C5E RID: 7262 RVA: 0x0009605C File Offset: 0x0009425C
			public bool HasState
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x1700072B RID: 1835
			// (get) Token: 0x06001C5F RID: 7263 RVA: 0x0009606C File Offset: 0x0009426C
			// (set) Token: 0x06001C60 RID: 7264 RVA: 0x00096074 File Offset: 0x00094274
			public string id
			{
				get
				{
					return this._id;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._id = value;
				}
			}

			// Token: 0x1700072C RID: 1836
			// (get) Token: 0x06001C61 RID: 7265 RVA: 0x0009608C File Offset: 0x0009428C
			public bool HasId
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x1700072D RID: 1837
			// (get) Token: 0x06001C62 RID: 7266 RVA: 0x0009609C File Offset: 0x0009429C
			// (set) Token: 0x06001C63 RID: 7267 RVA: 0x000960A4 File Offset: 0x000942A4
			public string key
			{
				get
				{
					return this._key;
				}
				set
				{
					this.has_field.set_field(2, true);
					this._key = value;
				}
			}

			// Token: 0x1700072E RID: 1838
			// (get) Token: 0x06001C64 RID: 7268 RVA: 0x000960BC File Offset: 0x000942BC
			public bool HasKey
			{
				get
				{
					return this.has_field.has_field(2);
				}
			}

			// Token: 0x1700072F RID: 1839
			// (get) Token: 0x06001C65 RID: 7269 RVA: 0x000960CC File Offset: 0x000942CC
			// (set) Token: 0x06001C66 RID: 7270 RVA: 0x000960D4 File Offset: 0x000942D4
			public long bindType
			{
				get
				{
					return this._bindType;
				}
				set
				{
					this.has_field.set_field(3, true);
					this._bindType = value;
				}
			}

			// Token: 0x17000730 RID: 1840
			// (get) Token: 0x06001C67 RID: 7271 RVA: 0x000960EC File Offset: 0x000942EC
			public bool HasBindType
			{
				get
				{
					return this.has_field.has_field(3);
				}
			}

			// Token: 0x06001C68 RID: 7272 RVA: 0x000960FC File Offset: 0x000942FC
			protected override void decode()
			{
				int num;
				while ((num = this.deserialize.read_tag()) != -1)
				{
					switch (num)
					{
					case 0:
						this.state = this.deserialize.read_integer();
						break;
					case 1:
						this.id = this.deserialize.read_string();
						break;
					case 2:
						this.key = this.deserialize.read_string();
						break;
					case 3:
						this.bindType = this.deserialize.read_integer();
						break;
					default:
						this.deserialize.read_unknow_data();
						break;
					}
				}
			}

			// Token: 0x06001C69 RID: 7273 RVA: 0x000961A8 File Offset: 0x000943A8
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.state, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_string(this.id, 1);
				}
				if (this.has_field.has_field(2))
				{
					this.serialize.write_string(this.key, 2);
				}
				if (this.has_field.has_field(3))
				{
					this.serialize.write_integer(this.bindType, 3);
				}
				return this.serialize.close();
			}

			// Token: 0x04001A4F RID: 6735
			private static int max_field_count = 4;

			// Token: 0x04001A50 RID: 6736
			private long _state;

			// Token: 0x04001A51 RID: 6737
			private string _id;

			// Token: 0x04001A52 RID: 6738
			private string _key;

			// Token: 0x04001A53 RID: 6739
			private long _bindType;
		}
	}
}
