using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000395 RID: 917
	public class enter_teleport_point
	{
		// Token: 0x02000396 RID: 918
		public class request : SprotoTypeBase
		{
			// Token: 0x06001B87 RID: 7047 RVA: 0x00094548 File Offset: 0x00092748
			public request() : base(enter_teleport_point.request.max_field_count)
			{
			}

			// Token: 0x06001B88 RID: 7048 RVA: 0x00094558 File Offset: 0x00092758
			public request(byte[] buffer) : base(enter_teleport_point.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x170006D3 RID: 1747
			// (get) Token: 0x06001B8A RID: 7050 RVA: 0x00094574 File Offset: 0x00092774
			// (set) Token: 0x06001B8B RID: 7051 RVA: 0x0009457C File Offset: 0x0009277C
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

			// Token: 0x170006D4 RID: 1748
			// (get) Token: 0x06001B8C RID: 7052 RVA: 0x00094594 File Offset: 0x00092794
			public bool HasX
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x170006D5 RID: 1749
			// (get) Token: 0x06001B8D RID: 7053 RVA: 0x000945A4 File Offset: 0x000927A4
			// (set) Token: 0x06001B8E RID: 7054 RVA: 0x000945AC File Offset: 0x000927AC
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

			// Token: 0x170006D6 RID: 1750
			// (get) Token: 0x06001B8F RID: 7055 RVA: 0x000945C4 File Offset: 0x000927C4
			public bool HasY
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x170006D7 RID: 1751
			// (get) Token: 0x06001B90 RID: 7056 RVA: 0x000945D4 File Offset: 0x000927D4
			// (set) Token: 0x06001B91 RID: 7057 RVA: 0x000945DC File Offset: 0x000927DC
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

			// Token: 0x170006D8 RID: 1752
			// (get) Token: 0x06001B92 RID: 7058 RVA: 0x000945F4 File Offset: 0x000927F4
			public bool HasZ
			{
				get
				{
					return this.has_field.has_field(2);
				}
			}

			// Token: 0x170006D9 RID: 1753
			// (get) Token: 0x06001B93 RID: 7059 RVA: 0x00094604 File Offset: 0x00092804
			// (set) Token: 0x06001B94 RID: 7060 RVA: 0x0009460C File Offset: 0x0009280C
			public long index
			{
				get
				{
					return this._index;
				}
				set
				{
					this.has_field.set_field(3, true);
					this._index = value;
				}
			}

			// Token: 0x170006DA RID: 1754
			// (get) Token: 0x06001B95 RID: 7061 RVA: 0x00094624 File Offset: 0x00092824
			public bool HasIndex
			{
				get
				{
					return this.has_field.has_field(3);
				}
			}

			// Token: 0x06001B96 RID: 7062 RVA: 0x00094634 File Offset: 0x00092834
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
						this.index = this.deserialize.read_integer();
						break;
					default:
						this.deserialize.read_unknow_data();
						break;
					}
				}
			}

			// Token: 0x06001B97 RID: 7063 RVA: 0x000946E0 File Offset: 0x000928E0
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
					this.serialize.write_integer(this.index, 3);
				}
				return this.serialize.close();
			}

			// Token: 0x04001A16 RID: 6678
			private static int max_field_count = 4;

			// Token: 0x04001A17 RID: 6679
			private long _x;

			// Token: 0x04001A18 RID: 6680
			private long _y;

			// Token: 0x04001A19 RID: 6681
			private long _z;

			// Token: 0x04001A1A RID: 6682
			private long _index;
		}
	}
}
