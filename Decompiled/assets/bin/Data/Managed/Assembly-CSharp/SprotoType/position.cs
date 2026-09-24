using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000445 RID: 1093
	public class position : SprotoTypeBase
	{
		// Token: 0x06002208 RID: 8712 RVA: 0x000A1C88 File Offset: 0x0009FE88
		public position() : base(position.max_field_count)
		{
		}

		// Token: 0x06002209 RID: 8713 RVA: 0x000A1C98 File Offset: 0x0009FE98
		public position(byte[] buffer) : base(position.max_field_count, buffer)
		{
			this.decode();
		}

		// Token: 0x1700099B RID: 2459
		// (get) Token: 0x0600220B RID: 8715 RVA: 0x000A1CB4 File Offset: 0x0009FEB4
		// (set) Token: 0x0600220C RID: 8716 RVA: 0x000A1CBC File Offset: 0x0009FEBC
		public long x
		{
			get
			{
				return this._x;
			}
			set
			{
				this.has_field.set_field(0, true);
				this._x = value;
			}
		}

		// Token: 0x1700099C RID: 2460
		// (get) Token: 0x0600220D RID: 8717 RVA: 0x000A1CD4 File Offset: 0x0009FED4
		public bool HasX
		{
			get
			{
				return this.has_field.has_field(0);
			}
		}

		// Token: 0x1700099D RID: 2461
		// (get) Token: 0x0600220E RID: 8718 RVA: 0x000A1CE4 File Offset: 0x0009FEE4
		// (set) Token: 0x0600220F RID: 8719 RVA: 0x000A1CEC File Offset: 0x0009FEEC
		public long y
		{
			get
			{
				return this._y;
			}
			set
			{
				this.has_field.set_field(1, true);
				this._y = value;
			}
		}

		// Token: 0x1700099E RID: 2462
		// (get) Token: 0x06002210 RID: 8720 RVA: 0x000A1D04 File Offset: 0x0009FF04
		public bool HasY
		{
			get
			{
				return this.has_field.has_field(1);
			}
		}

		// Token: 0x1700099F RID: 2463
		// (get) Token: 0x06002211 RID: 8721 RVA: 0x000A1D14 File Offset: 0x0009FF14
		// (set) Token: 0x06002212 RID: 8722 RVA: 0x000A1D1C File Offset: 0x0009FF1C
		public long z
		{
			get
			{
				return this._z;
			}
			set
			{
				this.has_field.set_field(2, true);
				this._z = value;
			}
		}

		// Token: 0x170009A0 RID: 2464
		// (get) Token: 0x06002213 RID: 8723 RVA: 0x000A1D34 File Offset: 0x0009FF34
		public bool HasZ
		{
			get
			{
				return this.has_field.has_field(2);
			}
		}

		// Token: 0x170009A1 RID: 2465
		// (get) Token: 0x06002214 RID: 8724 RVA: 0x000A1D44 File Offset: 0x0009FF44
		// (set) Token: 0x06002215 RID: 8725 RVA: 0x000A1D4C File Offset: 0x0009FF4C
		public long o
		{
			get
			{
				return this._o;
			}
			set
			{
				this.has_field.set_field(3, true);
				this._o = value;
			}
		}

		// Token: 0x170009A2 RID: 2466
		// (get) Token: 0x06002216 RID: 8726 RVA: 0x000A1D64 File Offset: 0x0009FF64
		public bool HasO
		{
			get
			{
				return this.has_field.has_field(3);
			}
		}

		// Token: 0x06002217 RID: 8727 RVA: 0x000A1D74 File Offset: 0x0009FF74
		protected override void decode()
		{
			int num;
			while ((num = this.deserialize.read_tag()) != -1)
			{
				switch (num)
				{
				case 0:
					this.x = this.deserialize.read_integer();
					break;
				case 1:
					this.y = this.deserialize.read_integer();
					break;
				case 2:
					this.z = this.deserialize.read_integer();
					break;
				case 3:
					this.o = this.deserialize.read_integer();
					break;
				default:
					this.deserialize.read_unknow_data();
					break;
				}
			}
		}

		// Token: 0x06002218 RID: 8728 RVA: 0x000A1E20 File Offset: 0x000A0020
		public override int encode(SprotoStream stream)
		{
			this.serialize.open(stream);
			if (this.has_field.has_field(0))
			{
				this.serialize.write_integer(this.x, 0);
			}
			if (this.has_field.has_field(1))
			{
				this.serialize.write_integer(this.y, 1);
			}
			if (this.has_field.has_field(2))
			{
				this.serialize.write_integer(this.z, 2);
			}
			if (this.has_field.has_field(3))
			{
				this.serialize.write_integer(this.o, 3);
			}
			return this.serialize.close();
		}

		// Token: 0x04001BE8 RID: 7144
		private static int max_field_count = 4;

		// Token: 0x04001BE9 RID: 7145
		private long _x;

		// Token: 0x04001BEA RID: 7146
		private long _y;

		// Token: 0x04001BEB RID: 7147
		private long _z;

		// Token: 0x04001BEC RID: 7148
		private long _o;
	}
}
