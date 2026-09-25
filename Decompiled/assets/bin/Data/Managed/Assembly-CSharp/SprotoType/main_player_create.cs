using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000416 RID: 1046
	public class main_player_create
	{
		// Token: 0x02000417 RID: 1047
		public class request : SprotoTypeBase
		{
			// Token: 0x06002078 RID: 8312 RVA: 0x0009E938 File Offset: 0x0009CB38
			public request() : base(main_player_create.request.max_field_count)
			{
			}

			// Token: 0x06002079 RID: 8313 RVA: 0x0009E948 File Offset: 0x0009CB48
			public request(byte[] buffer) : base(main_player_create.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x170008F5 RID: 2293
			// (get) Token: 0x0600207B RID: 8315 RVA: 0x0009E964 File Offset: 0x0009CB64
			// (set) Token: 0x0600207C RID: 8316 RVA: 0x0009E96C File Offset: 0x0009CB6C
			public character character
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

			// Token: 0x170008F6 RID: 2294
			// (get) Token: 0x0600207D RID: 8317 RVA: 0x0009E984 File Offset: 0x0009CB84
			public bool HasCharacter
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x170008F7 RID: 2295
			// (get) Token: 0x0600207E RID: 8318 RVA: 0x0009E994 File Offset: 0x0009CB94
			// (set) Token: 0x0600207F RID: 8319 RVA: 0x0009E99C File Offset: 0x0009CB9C
			public movement movement
			{
				get
				{
					return this._movement;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._movement = value;
				}
			}

			// Token: 0x170008F8 RID: 2296
			// (get) Token: 0x06002080 RID: 8320 RVA: 0x0009E9B4 File Offset: 0x0009CBB4
			public bool HasMovement
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x06002081 RID: 8321 RVA: 0x0009E9C4 File Offset: 0x0009CBC4
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
							this.movement = this.deserialize.read_obj<movement>();
						}
					}
					else
					{
						this.character = this.deserialize.read_obj<character>();
					}
				}
			}

			// Token: 0x06002082 RID: 8322 RVA: 0x0009EA3C File Offset: 0x0009CC3C
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_obj(this.character, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_obj(this.movement, 1);
				}
				return this.serialize.close();
			}

			// Token: 0x04001B7A RID: 7034
			private static int max_field_count = 2;

			// Token: 0x04001B7B RID: 7035
			private character _character;

			// Token: 0x04001B7C RID: 7036
			private movement _movement;
		}
	}
}
