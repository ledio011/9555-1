using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000659 RID: 1625
	public class use_dance_sound_box
	{
		// Token: 0x0200065A RID: 1626
		public class request : SprotoTypeBase
		{
			// Token: 0x06002EF0 RID: 12016 RVA: 0x000BADB8 File Offset: 0x000B8FB8
			public request() : base(use_dance_sound_box.request.max_field_count)
			{
			}

			// Token: 0x06002EF1 RID: 12017 RVA: 0x000BADC8 File Offset: 0x000B8FC8
			public request(byte[] buffer) : base(use_dance_sound_box.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000DCF RID: 3535
			// (get) Token: 0x06002EF3 RID: 12019 RVA: 0x000BADE4 File Offset: 0x000B8FE4
			// (set) Token: 0x06002EF4 RID: 12020 RVA: 0x000BADEC File Offset: 0x000B8FEC
			public long index
			{
				get
				{
					return this._index;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._index = value;
				}
			}

			// Token: 0x17000DD0 RID: 3536
			// (get) Token: 0x06002EF5 RID: 12021 RVA: 0x000BAE04 File Offset: 0x000B9004
			public bool HasIndex
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x17000DD1 RID: 3537
			// (get) Token: 0x06002EF6 RID: 12022 RVA: 0x000BAE14 File Offset: 0x000B9014
			// (set) Token: 0x06002EF7 RID: 12023 RVA: 0x000BAE1C File Offset: 0x000B901C
			public long x
			{
				get
				{
					return this._x;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._x = value;
				}
			}

			// Token: 0x17000DD2 RID: 3538
			// (get) Token: 0x06002EF8 RID: 12024 RVA: 0x000BAE34 File Offset: 0x000B9034
			public bool HasX
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x17000DD3 RID: 3539
			// (get) Token: 0x06002EF9 RID: 12025 RVA: 0x000BAE44 File Offset: 0x000B9044
			// (set) Token: 0x06002EFA RID: 12026 RVA: 0x000BAE4C File Offset: 0x000B904C
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

			// Token: 0x17000DD4 RID: 3540
			// (get) Token: 0x06002EFB RID: 12027 RVA: 0x000BAE64 File Offset: 0x000B9064
			public bool HasZ
			{
				get
				{
					return this.has_field.has_field(2);
				}
			}

			// Token: 0x06002EFC RID: 12028 RVA: 0x000BAE74 File Offset: 0x000B9074
			protected override void decode()
			{
				int num;
				while ((num = this.deserialize.read_tag()) != -1)
				{
					switch (num)
					{
					case 0:
						this.index = this.deserialize.read_integer();
						break;
					case 1:
						this.x = this.deserialize.read_integer();
						break;
					case 2:
						this.z = this.deserialize.read_integer();
						break;
					default:
						this.deserialize.read_unknow_data();
						break;
					}
				}
			}

			// Token: 0x06002EFD RID: 12029 RVA: 0x000BAF08 File Offset: 0x000B9108
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.index, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_integer(this.x, 1);
				}
				if (this.has_field.has_field(2))
				{
					this.serialize.write_integer(this.z, 2);
				}
				return this.serialize.close();
			}

			// Token: 0x04001F48 RID: 8008
			private static int max_field_count = 3;

			// Token: 0x04001F49 RID: 8009
			private long _index;

			// Token: 0x04001F4A RID: 8010
			private long _x;

			// Token: 0x04001F4B RID: 8011
			private long _z;
		}
	}
}
