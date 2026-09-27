using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000409 RID: 1033
	public class local_npc_die
	{
		// Token: 0x0200040A RID: 1034
		public class request : SprotoTypeBase
		{
			// Token: 0x06001FF7 RID: 8183 RVA: 0x0009D87C File Offset: 0x0009BA7C
			public request() : base(local_npc_die.request.max_field_count)
			{
			}

			// Token: 0x06001FF8 RID: 8184 RVA: 0x0009D88C File Offset: 0x0009BA8C
			public request(byte[] buffer) : base(local_npc_die.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x170008BB RID: 2235
			// (get) Token: 0x06001FFA RID: 8186 RVA: 0x0009D8A8 File Offset: 0x0009BAA8
			// (set) Token: 0x06001FFB RID: 8187 RVA: 0x0009D8B0 File Offset: 0x0009BAB0
			public string npcid
			{
				get
				{
					return this._npcid;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._npcid = value;
				}
			}

			// Token: 0x170008BC RID: 2236
			// (get) Token: 0x06001FFC RID: 8188 RVA: 0x0009D8C8 File Offset: 0x0009BAC8
			public bool HasNpcid
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x170008BD RID: 2237
			// (get) Token: 0x06001FFD RID: 8189 RVA: 0x0009D8D8 File Offset: 0x0009BAD8
			// (set) Token: 0x06001FFE RID: 8190 RVA: 0x0009D8E0 File Offset: 0x0009BAE0
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

			// Token: 0x170008BE RID: 2238
			// (get) Token: 0x06001FFF RID: 8191 RVA: 0x0009D8F8 File Offset: 0x0009BAF8
			public bool HasX
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x170008BF RID: 2239
			// (get) Token: 0x06002000 RID: 8192 RVA: 0x0009D908 File Offset: 0x0009BB08
			// (set) Token: 0x06002001 RID: 8193 RVA: 0x0009D910 File Offset: 0x0009BB10
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

			// Token: 0x170008C0 RID: 2240
			// (get) Token: 0x06002002 RID: 8194 RVA: 0x0009D928 File Offset: 0x0009BB28
			public bool HasZ
			{
				get
				{
					return this.has_field.has_field(2);
				}
			}

			// Token: 0x170008C1 RID: 2241
			// (get) Token: 0x06002003 RID: 8195 RVA: 0x0009D938 File Offset: 0x0009BB38
			// (set) Token: 0x06002004 RID: 8196 RVA: 0x0009D940 File Offset: 0x0009BB40
			public long type
			{
				get
				{
					return this._type;
				}
				set
				{
					this.has_field.set_field(3, true);
					this._type = value;
				}
			}

			// Token: 0x170008C2 RID: 2242
			// (get) Token: 0x06002005 RID: 8197 RVA: 0x0009D958 File Offset: 0x0009BB58
			public bool HasType
			{
				get
				{
					return this.has_field.has_field(3);
				}
			}

			// Token: 0x06002006 RID: 8198 RVA: 0x0009D968 File Offset: 0x0009BB68
			protected override void decode()
			{
				int num;
				while ((num = this.deserialize.read_tag()) != -1)
				{
					switch (num)
					{
					case 0:
						this.npcid = this.deserialize.read_string();
						break;
					case 1:
						this.x = this.deserialize.read_integer();
						break;
					case 2:
						this.z = this.deserialize.read_integer();
						break;
					case 3:
						this.type = this.deserialize.read_integer();
						break;
					default:
						this.deserialize.read_unknow_data();
						break;
					}
				}
			}

			// Token: 0x06002007 RID: 8199 RVA: 0x0009DA14 File Offset: 0x0009BC14
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_string(this.npcid, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_integer(this.x, 1);
				}
				if (this.has_field.has_field(2))
				{
					this.serialize.write_integer(this.z, 2);
				}
				if (this.has_field.has_field(3))
				{
					this.serialize.write_integer(this.type, 3);
				}
				return this.serialize.close();
			}

			// Token: 0x04001B55 RID: 6997
			private static int max_field_count = 4;

			// Token: 0x04001B56 RID: 6998
			private string _npcid;

			// Token: 0x04001B57 RID: 6999
			private long _x;

			// Token: 0x04001B58 RID: 7000
			private long _z;

			// Token: 0x04001B59 RID: 7001
			private long _type;
		}
	}
}
