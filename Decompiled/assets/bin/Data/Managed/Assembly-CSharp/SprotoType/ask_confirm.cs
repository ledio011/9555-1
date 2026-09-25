using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000303 RID: 771
	public class ask_confirm
	{
		// Token: 0x02000304 RID: 772
		public class request : SprotoTypeBase
		{
			// Token: 0x06001586 RID: 5510 RVA: 0x00087C48 File Offset: 0x00085E48
			public request() : base(ask_confirm.request.max_field_count)
			{
			}

			// Token: 0x06001587 RID: 5511 RVA: 0x00087C58 File Offset: 0x00085E58
			public request(byte[] buffer) : base(ask_confirm.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x1700042B RID: 1067
			// (get) Token: 0x06001589 RID: 5513 RVA: 0x00087C74 File Offset: 0x00085E74
			// (set) Token: 0x0600158A RID: 5514 RVA: 0x00087C7C File Offset: 0x00085E7C
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

			// Token: 0x1700042C RID: 1068
			// (get) Token: 0x0600158B RID: 5515 RVA: 0x00087C94 File Offset: 0x00085E94
			public bool HasType
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x1700042D RID: 1069
			// (get) Token: 0x0600158C RID: 5516 RVA: 0x00087CA4 File Offset: 0x00085EA4
			// (set) Token: 0x0600158D RID: 5517 RVA: 0x00087CAC File Offset: 0x00085EAC
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

			// Token: 0x1700042E RID: 1070
			// (get) Token: 0x0600158E RID: 5518 RVA: 0x00087CC4 File Offset: 0x00085EC4
			public bool HasId
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x1700042F RID: 1071
			// (get) Token: 0x0600158F RID: 5519 RVA: 0x00087CD4 File Offset: 0x00085ED4
			// (set) Token: 0x06001590 RID: 5520 RVA: 0x00087CDC File Offset: 0x00085EDC
			public long parm1
			{
				get
				{
					return this._parm1;
				}
				set
				{
					this.has_field.set_field(2, true);
					this._parm1 = value;
				}
			}

			// Token: 0x17000430 RID: 1072
			// (get) Token: 0x06001591 RID: 5521 RVA: 0x00087CF4 File Offset: 0x00085EF4
			public bool HasParm1
			{
				get
				{
					return this.has_field.has_field(2);
				}
			}

			// Token: 0x17000431 RID: 1073
			// (get) Token: 0x06001592 RID: 5522 RVA: 0x00087D04 File Offset: 0x00085F04
			// (set) Token: 0x06001593 RID: 5523 RVA: 0x00087D0C File Offset: 0x00085F0C
			public long param2
			{
				get
				{
					return this._param2;
				}
				set
				{
					this.has_field.set_field(3, true);
					this._param2 = value;
				}
			}

			// Token: 0x17000432 RID: 1074
			// (get) Token: 0x06001594 RID: 5524 RVA: 0x00087D24 File Offset: 0x00085F24
			public bool HasParam2
			{
				get
				{
					return this.has_field.has_field(3);
				}
			}

			// Token: 0x06001595 RID: 5525 RVA: 0x00087D34 File Offset: 0x00085F34
			protected override void decode()
			{
				int num;
				while ((num = this.deserialize.read_tag()) != -1)
				{
					switch (num)
					{
					case 0:
						this.type = this.deserialize.read_integer();
						break;
					case 1:
						this.id = this.deserialize.read_string();
						break;
					case 2:
						this.parm1 = this.deserialize.read_integer();
						break;
					case 3:
						this.param2 = this.deserialize.read_integer();
						break;
					default:
						this.deserialize.read_unknow_data();
						break;
					}
				}
			}

			// Token: 0x06001596 RID: 5526 RVA: 0x00087DE0 File Offset: 0x00085FE0
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.type, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_string(this.id, 1);
				}
				if (this.has_field.has_field(2))
				{
					this.serialize.write_integer(this.parm1, 2);
				}
				if (this.has_field.has_field(3))
				{
					this.serialize.write_integer(this.param2, 3);
				}
				return this.serialize.close();
			}

			// Token: 0x0400185D RID: 6237
			private static int max_field_count = 4;

			// Token: 0x0400185E RID: 6238
			private long _type;

			// Token: 0x0400185F RID: 6239
			private string _id;

			// Token: 0x04001860 RID: 6240
			private long _parm1;

			// Token: 0x04001861 RID: 6241
			private long _param2;
		}

		// Token: 0x02000305 RID: 773
		public class response : SprotoTypeBase
		{
			// Token: 0x06001597 RID: 5527 RVA: 0x00087E90 File Offset: 0x00086090
			public response() : base(ask_confirm.response.max_field_count)
			{
			}

			// Token: 0x06001598 RID: 5528 RVA: 0x00087EA0 File Offset: 0x000860A0
			public response(byte[] buffer) : base(ask_confirm.response.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000433 RID: 1075
			// (get) Token: 0x0600159A RID: 5530 RVA: 0x00087EBC File Offset: 0x000860BC
			// (set) Token: 0x0600159B RID: 5531 RVA: 0x00087EC4 File Offset: 0x000860C4
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

			// Token: 0x17000434 RID: 1076
			// (get) Token: 0x0600159C RID: 5532 RVA: 0x00087EDC File Offset: 0x000860DC
			public bool HasState
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x0600159D RID: 5533 RVA: 0x00087EEC File Offset: 0x000860EC
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
						this.state = this.deserialize.read_integer();
					}
				}
			}

			// Token: 0x0600159E RID: 5534 RVA: 0x00087F48 File Offset: 0x00086148
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.state, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x04001862 RID: 6242
			private static int max_field_count = 1;

			// Token: 0x04001863 RID: 6243
			private long _state;
		}
	}
}
