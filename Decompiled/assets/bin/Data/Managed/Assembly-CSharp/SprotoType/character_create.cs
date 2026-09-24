using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000341 RID: 833
	public class character_create
	{
		// Token: 0x02000342 RID: 834
		public class request : SprotoTypeBase
		{
			// Token: 0x06001853 RID: 6227 RVA: 0x0008DB0C File Offset: 0x0008BD0C
			public request() : base(character_create.request.max_field_count)
			{
			}

			// Token: 0x06001854 RID: 6228 RVA: 0x0008DB1C File Offset: 0x0008BD1C
			public request(byte[] buffer) : base(character_create.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000577 RID: 1399
			// (get) Token: 0x06001856 RID: 6230 RVA: 0x0008DB38 File Offset: 0x0008BD38
			// (set) Token: 0x06001857 RID: 6231 RVA: 0x0008DB40 File Offset: 0x0008BD40
			public general character
			{
				get
				{
					return this._character;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._character = value;
				}
			}

			// Token: 0x17000578 RID: 1400
			// (get) Token: 0x06001858 RID: 6232 RVA: 0x0008DB58 File Offset: 0x0008BD58
			public bool HasCharacter
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x06001859 RID: 6233 RVA: 0x0008DB68 File Offset: 0x0008BD68
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
						this.character = this.deserialize.read_obj<general>();
					}
				}
			}

			// Token: 0x0600185A RID: 6234 RVA: 0x0008DBC4 File Offset: 0x0008BDC4
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_obj(this.character, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x0400192E RID: 6446
			private static int max_field_count = 1;

			// Token: 0x0400192F RID: 6447
			private general _character;
		}

		// Token: 0x02000343 RID: 835
		public class response : SprotoTypeBase
		{
			// Token: 0x0600185B RID: 6235 RVA: 0x0008DC0C File Offset: 0x0008BE0C
			public response() : base(character_create.response.max_field_count)
			{
			}

			// Token: 0x0600185C RID: 6236 RVA: 0x0008DC1C File Offset: 0x0008BE1C
			public response(byte[] buffer) : base(character_create.response.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000579 RID: 1401
			// (get) Token: 0x0600185E RID: 6238 RVA: 0x0008DC38 File Offset: 0x0008BE38
			// (set) Token: 0x0600185F RID: 6239 RVA: 0x0008DC40 File Offset: 0x0008BE40
			public character_overview character
			{
				get
				{
					return this._character;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._character = value;
				}
			}

			// Token: 0x1700057A RID: 1402
			// (get) Token: 0x06001860 RID: 6240 RVA: 0x0008DC58 File Offset: 0x0008BE58
			public bool HasCharacter
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x1700057B RID: 1403
			// (get) Token: 0x06001861 RID: 6241 RVA: 0x0008DC68 File Offset: 0x0008BE68
			// (set) Token: 0x06001862 RID: 6242 RVA: 0x0008DC70 File Offset: 0x0008BE70
			public long errno
			{
				get
				{
					return this._errno;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._errno = value;
				}
			}

			// Token: 0x1700057C RID: 1404
			// (get) Token: 0x06001863 RID: 6243 RVA: 0x0008DC88 File Offset: 0x0008BE88
			public bool HasErrno
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x06001864 RID: 6244 RVA: 0x0008DC98 File Offset: 0x0008BE98
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
							this.errno = this.deserialize.read_integer();
						}
					}
					else
					{
						this.character = this.deserialize.read_obj<character_overview>();
					}
				}
			}

			// Token: 0x06001865 RID: 6245 RVA: 0x0008DD10 File Offset: 0x0008BF10
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_obj(this.character, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_integer(this.errno, 1);
				}
				return this.serialize.close();
			}

			// Token: 0x04001930 RID: 6448
			private static int max_field_count = 2;

			// Token: 0x04001931 RID: 6449
			private character_overview _character;

			// Token: 0x04001932 RID: 6450
			private long _errno;
		}
	}
}
