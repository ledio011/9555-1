using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020003F3 RID: 1011
	public class heart_beat
	{
		// Token: 0x020003F4 RID: 1012
		public class request : SprotoTypeBase
		{
			// Token: 0x06001F52 RID: 8018 RVA: 0x0009C3D0 File Offset: 0x0009A5D0
			public request() : base(heart_beat.request.max_field_count)
			{
			}

			// Token: 0x06001F53 RID: 8019 RVA: 0x0009C3E0 File Offset: 0x0009A5E0
			public request(byte[] buffer) : base(heart_beat.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000881 RID: 2177
			// (get) Token: 0x06001F55 RID: 8021 RVA: 0x0009C3FC File Offset: 0x0009A5FC
			// (set) Token: 0x06001F56 RID: 8022 RVA: 0x0009C404 File Offset: 0x0009A604
			public long time
			{
				get
				{
					return this._time;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._time = value;
				}
			}

			// Token: 0x17000882 RID: 2178
			// (get) Token: 0x06001F57 RID: 8023 RVA: 0x0009C41C File Offset: 0x0009A61C
			public bool HasTime
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x17000883 RID: 2179
			// (get) Token: 0x06001F58 RID: 8024 RVA: 0x0009C42C File Offset: 0x0009A62C
			// (set) Token: 0x06001F59 RID: 8025 RVA: 0x0009C434 File Offset: 0x0009A634
			public long time2
			{
				get
				{
					return this._time2;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._time2 = value;
				}
			}

			// Token: 0x17000884 RID: 2180
			// (get) Token: 0x06001F5A RID: 8026 RVA: 0x0009C44C File Offset: 0x0009A64C
			public bool HasTime2
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x06001F5B RID: 8027 RVA: 0x0009C45C File Offset: 0x0009A65C
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
							this.time2 = this.deserialize.read_integer();
						}
					}
					else
					{
						this.time = this.deserialize.read_integer();
					}
				}
			}

			// Token: 0x06001F5C RID: 8028 RVA: 0x0009C4D4 File Offset: 0x0009A6D4
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.time, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_integer(this.time2, 1);
				}
				return this.serialize.close();
			}

			// Token: 0x04001B2A RID: 6954
			private static int max_field_count = 2;

			// Token: 0x04001B2B RID: 6955
			private long _time;

			// Token: 0x04001B2C RID: 6956
			private long _time2;
		}

		// Token: 0x020003F5 RID: 1013
		public class response : SprotoTypeBase
		{
			// Token: 0x06001F5D RID: 8029 RVA: 0x0009C540 File Offset: 0x0009A740
			public response() : base(heart_beat.response.max_field_count)
			{
			}

			// Token: 0x06001F5E RID: 8030 RVA: 0x0009C550 File Offset: 0x0009A750
			public response(byte[] buffer) : base(heart_beat.response.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000885 RID: 2181
			// (get) Token: 0x06001F60 RID: 8032 RVA: 0x0009C56C File Offset: 0x0009A76C
			// (set) Token: 0x06001F61 RID: 8033 RVA: 0x0009C574 File Offset: 0x0009A774
			public long time
			{
				get
				{
					return this._time;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._time = value;
				}
			}

			// Token: 0x17000886 RID: 2182
			// (get) Token: 0x06001F62 RID: 8034 RVA: 0x0009C58C File Offset: 0x0009A78C
			public bool HasTime
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x17000887 RID: 2183
			// (get) Token: 0x06001F63 RID: 8035 RVA: 0x0009C59C File Offset: 0x0009A79C
			// (set) Token: 0x06001F64 RID: 8036 RVA: 0x0009C5A4 File Offset: 0x0009A7A4
			public long serverTime
			{
				get
				{
					return this._serverTime;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._serverTime = value;
				}
			}

			// Token: 0x17000888 RID: 2184
			// (get) Token: 0x06001F65 RID: 8037 RVA: 0x0009C5BC File Offset: 0x0009A7BC
			public bool HasServerTime
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x06001F66 RID: 8038 RVA: 0x0009C5CC File Offset: 0x0009A7CC
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
							this.serverTime = this.deserialize.read_integer();
						}
					}
					else
					{
						this.time = this.deserialize.read_integer();
					}
				}
			}

			// Token: 0x06001F67 RID: 8039 RVA: 0x0009C644 File Offset: 0x0009A844
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.time, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_integer(this.serverTime, 1);
				}
				return this.serialize.close();
			}

			// Token: 0x04001B2D RID: 6957
			private static int max_field_count = 2;

			// Token: 0x04001B2E RID: 6958
			private long _time;

			// Token: 0x04001B2F RID: 6959
			private long _serverTime;
		}
	}
}
