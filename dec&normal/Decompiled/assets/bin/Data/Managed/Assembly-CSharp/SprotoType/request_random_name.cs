using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020004BC RID: 1212
	public class request_random_name
	{
		// Token: 0x020004BD RID: 1213
		public class request : SprotoTypeBase
		{
			// Token: 0x06002431 RID: 9265 RVA: 0x000A5A34 File Offset: 0x000A3C34
			public request() : base(request_random_name.request.max_field_count)
			{
			}

			// Token: 0x06002432 RID: 9266 RVA: 0x000A5A44 File Offset: 0x000A3C44
			public request(byte[] buffer) : base(request_random_name.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000A19 RID: 2585
			// (get) Token: 0x06002434 RID: 9268 RVA: 0x000A5A60 File Offset: 0x000A3C60
			// (set) Token: 0x06002435 RID: 9269 RVA: 0x000A5A68 File Offset: 0x000A3C68
			public long type
			{
				get
				{
					return this._type;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._type = value;
				}
			}

			// Token: 0x17000A1A RID: 2586
			// (get) Token: 0x06002436 RID: 9270 RVA: 0x000A5A80 File Offset: 0x000A3C80
			public bool HasType
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x06002437 RID: 9271 RVA: 0x000A5A90 File Offset: 0x000A3C90
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
						this.type = this.deserialize.read_integer();
					}
				}
			}

			// Token: 0x06002438 RID: 9272 RVA: 0x000A5AEC File Offset: 0x000A3CEC
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.type, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x04001C64 RID: 7268
			private static int max_field_count = 1;

			// Token: 0x04001C65 RID: 7269
			private long _type;
		}

		// Token: 0x020004BE RID: 1214
		public class response : SprotoTypeBase
		{
			// Token: 0x06002439 RID: 9273 RVA: 0x000A5B34 File Offset: 0x000A3D34
			public response() : base(request_random_name.response.max_field_count)
			{
			}

			// Token: 0x0600243A RID: 9274 RVA: 0x000A5B44 File Offset: 0x000A3D44
			public response(byte[] buffer) : base(request_random_name.response.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000A1B RID: 2587
			// (get) Token: 0x0600243C RID: 9276 RVA: 0x000A5B60 File Offset: 0x000A3D60
			// (set) Token: 0x0600243D RID: 9277 RVA: 0x000A5B68 File Offset: 0x000A3D68
			public string name
			{
				get
				{
					return this._name;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._name = value;
				}
			}

			// Token: 0x17000A1C RID: 2588
			// (get) Token: 0x0600243E RID: 9278 RVA: 0x000A5B80 File Offset: 0x000A3D80
			public bool HasName
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x0600243F RID: 9279 RVA: 0x000A5B90 File Offset: 0x000A3D90
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
						this.name = this.deserialize.read_string();
					}
				}
			}

			// Token: 0x06002440 RID: 9280 RVA: 0x000A5BEC File Offset: 0x000A3DEC
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_string(this.name, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x04001C66 RID: 7270
			private static int max_field_count = 1;

			// Token: 0x04001C67 RID: 7271
			private string _name;
		}
	}
}
