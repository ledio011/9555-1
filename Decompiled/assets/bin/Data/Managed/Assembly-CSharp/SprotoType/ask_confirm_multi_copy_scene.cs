using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000306 RID: 774
	public class ask_confirm_multi_copy_scene
	{
		// Token: 0x02000307 RID: 775
		public class request : SprotoTypeBase
		{
			// Token: 0x060015A0 RID: 5536 RVA: 0x00087F98 File Offset: 0x00086198
			public request() : base(ask_confirm_multi_copy_scene.request.max_field_count)
			{
			}

			// Token: 0x060015A1 RID: 5537 RVA: 0x00087FA8 File Offset: 0x000861A8
			public request(byte[] buffer) : base(ask_confirm_multi_copy_scene.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000435 RID: 1077
			// (get) Token: 0x060015A3 RID: 5539 RVA: 0x00087FC4 File Offset: 0x000861C4
			// (set) Token: 0x060015A4 RID: 5540 RVA: 0x00087FCC File Offset: 0x000861CC
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

			// Token: 0x17000436 RID: 1078
			// (get) Token: 0x060015A5 RID: 5541 RVA: 0x00087FE4 File Offset: 0x000861E4
			public bool HasId
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x17000437 RID: 1079
			// (get) Token: 0x060015A6 RID: 5542 RVA: 0x00087FF4 File Offset: 0x000861F4
			// (set) Token: 0x060015A7 RID: 5543 RVA: 0x00087FFC File Offset: 0x000861FC
			public long type1
			{
				get
				{
					return this._type1;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._type1 = value;
				}
			}

			// Token: 0x17000438 RID: 1080
			// (get) Token: 0x060015A8 RID: 5544 RVA: 0x00088014 File Offset: 0x00086214
			public bool HasType1
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x17000439 RID: 1081
			// (get) Token: 0x060015A9 RID: 5545 RVA: 0x00088024 File Offset: 0x00086224
			// (set) Token: 0x060015AA RID: 5546 RVA: 0x0008802C File Offset: 0x0008622C
			public long session
			{
				get
				{
					return this._session;
				}
				set
				{
					this.has_field.set_field(2, true);
					this._session = value;
				}
			}

			// Token: 0x1700043A RID: 1082
			// (get) Token: 0x060015AB RID: 5547 RVA: 0x00088044 File Offset: 0x00086244
			public bool HasSession
			{
				get
				{
					return this.has_field.has_field(2);
				}
			}

			// Token: 0x060015AC RID: 5548 RVA: 0x00088054 File Offset: 0x00086254
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
						this.type1 = this.deserialize.read_integer();
						break;
					case 2:
						this.session = this.deserialize.read_integer();
						break;
					default:
						this.deserialize.read_unknow_data();
						break;
					}
				}
			}

			// Token: 0x060015AD RID: 5549 RVA: 0x000880E8 File Offset: 0x000862E8
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_string(this.id, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_integer(this.type1, 1);
				}
				if (this.has_field.has_field(2))
				{
					this.serialize.write_integer(this.session, 2);
				}
				return this.serialize.close();
			}

			// Token: 0x04001864 RID: 6244
			private static int max_field_count = 3;

			// Token: 0x04001865 RID: 6245
			private string _id;

			// Token: 0x04001866 RID: 6246
			private long _type1;

			// Token: 0x04001867 RID: 6247
			private long _session;
		}
	}
}
