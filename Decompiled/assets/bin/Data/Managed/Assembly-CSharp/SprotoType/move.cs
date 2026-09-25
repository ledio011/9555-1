using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x0200041F RID: 1055
	public class move
	{
		// Token: 0x02000420 RID: 1056
		public class request : SprotoTypeBase
		{
			// Token: 0x060020B4 RID: 8372 RVA: 0x0009F0A8 File Offset: 0x0009D2A8
			public request() : base(move.request.max_field_count)
			{
			}

			// Token: 0x060020B5 RID: 8373 RVA: 0x0009F0B8 File Offset: 0x0009D2B8
			public request(byte[] buffer) : base(move.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000909 RID: 2313
			// (get) Token: 0x060020B7 RID: 8375 RVA: 0x0009F0D4 File Offset: 0x0009D2D4
			// (set) Token: 0x060020B8 RID: 8376 RVA: 0x0009F0DC File Offset: 0x0009D2DC
			public position pos
			{
				get
				{
					return this._pos;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._pos = value;
				}
			}

			// Token: 0x1700090A RID: 2314
			// (get) Token: 0x060020B9 RID: 8377 RVA: 0x0009F0F4 File Offset: 0x0009D2F4
			public bool HasPos
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x1700090B RID: 2315
			// (get) Token: 0x060020BA RID: 8378 RVA: 0x0009F104 File Offset: 0x0009D304
			// (set) Token: 0x060020BB RID: 8379 RVA: 0x0009F10C File Offset: 0x0009D30C
			public bool moving
			{
				get
				{
					return this._moving;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._moving = value;
				}
			}

			// Token: 0x1700090C RID: 2316
			// (get) Token: 0x060020BC RID: 8380 RVA: 0x0009F124 File Offset: 0x0009D324
			public bool HasMoving
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x1700090D RID: 2317
			// (get) Token: 0x060020BD RID: 8381 RVA: 0x0009F134 File Offset: 0x0009D334
			// (set) Token: 0x060020BE RID: 8382 RVA: 0x0009F13C File Offset: 0x0009D33C
			public long index
			{
				get
				{
					return this._index;
				}
				set
				{
					this.has_field.set_field(2, true);
					this._index = value;
				}
			}

			// Token: 0x1700090E RID: 2318
			// (get) Token: 0x060020BF RID: 8383 RVA: 0x0009F154 File Offset: 0x0009D354
			public bool HasIndex
			{
				get
				{
					return this.has_field.has_field(2);
				}
			}

			// Token: 0x1700090F RID: 2319
			// (get) Token: 0x060020C0 RID: 8384 RVA: 0x0009F164 File Offset: 0x0009D364
			// (set) Token: 0x060020C1 RID: 8385 RVA: 0x0009F16C File Offset: 0x0009D36C
			public long parm
			{
				get
				{
					return this._parm;
				}
				set
				{
					this.has_field.set_field(3, true);
					this._parm = value;
				}
			}

			// Token: 0x17000910 RID: 2320
			// (get) Token: 0x060020C2 RID: 8386 RVA: 0x0009F184 File Offset: 0x0009D384
			public bool HasParm
			{
				get
				{
					return this.has_field.has_field(3);
				}
			}

			// Token: 0x060020C3 RID: 8387 RVA: 0x0009F194 File Offset: 0x0009D394
			protected override void decode()
			{
				int num;
				while ((num = this.deserialize.read_tag()) != -1)
				{
					switch (num)
					{
					case 0:
						this.pos = this.deserialize.read_obj<position>();
						break;
					case 1:
						this.moving = this.deserialize.read_boolean();
						break;
					case 2:
						this.index = this.deserialize.read_integer();
						break;
					case 3:
						this.parm = this.deserialize.read_integer();
						break;
					default:
						this.deserialize.read_unknow_data();
						break;
					}
				}
			}

			// Token: 0x060020C4 RID: 8388 RVA: 0x0009F240 File Offset: 0x0009D440
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_obj(this.pos, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_boolean(this.moving, 1);
				}
				if (this.has_field.has_field(2))
				{
					this.serialize.write_integer(this.index, 2);
				}
				if (this.has_field.has_field(3))
				{
					this.serialize.write_integer(this.parm, 3);
				}
				return this.serialize.close();
			}

			// Token: 0x04001B8A RID: 7050
			private static int max_field_count = 4;

			// Token: 0x04001B8B RID: 7051
			private position _pos;

			// Token: 0x04001B8C RID: 7052
			private bool _moving;

			// Token: 0x04001B8D RID: 7053
			private long _index;

			// Token: 0x04001B8E RID: 7054
			private long _parm;
		}

		// Token: 0x02000421 RID: 1057
		public class response : SprotoTypeBase
		{
			// Token: 0x060020C5 RID: 8389 RVA: 0x0009F2F0 File Offset: 0x0009D4F0
			public response() : base(move.response.max_field_count)
			{
			}

			// Token: 0x060020C6 RID: 8390 RVA: 0x0009F300 File Offset: 0x0009D500
			public response(byte[] buffer) : base(move.response.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000911 RID: 2321
			// (get) Token: 0x060020C8 RID: 8392 RVA: 0x0009F31C File Offset: 0x0009D51C
			// (set) Token: 0x060020C9 RID: 8393 RVA: 0x0009F324 File Offset: 0x0009D524
			public position pos
			{
				get
				{
					return this._pos;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._pos = value;
				}
			}

			// Token: 0x17000912 RID: 2322
			// (get) Token: 0x060020CA RID: 8394 RVA: 0x0009F33C File Offset: 0x0009D53C
			public bool HasPos
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x060020CB RID: 8395 RVA: 0x0009F34C File Offset: 0x0009D54C
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
						this.pos = this.deserialize.read_obj<position>();
					}
				}
			}

			// Token: 0x060020CC RID: 8396 RVA: 0x0009F3A8 File Offset: 0x0009D5A8
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_obj(this.pos, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x04001B8F RID: 7055
			private static int max_field_count = 1;

			// Token: 0x04001B90 RID: 7056
			private position _pos;
		}
	}
}
