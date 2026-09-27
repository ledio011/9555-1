using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020002EA RID: 746
	public class aoi_add
	{
		// Token: 0x020002EB RID: 747
		public class request : SprotoTypeBase
		{
			// Token: 0x06001506 RID: 5382 RVA: 0x00086D28 File Offset: 0x00084F28
			public request() : base(aoi_add.request.max_field_count)
			{
			}

			// Token: 0x06001507 RID: 5383 RVA: 0x00086D38 File Offset: 0x00084F38
			public request(byte[] buffer) : base(aoi_add.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000409 RID: 1033
			// (get) Token: 0x06001509 RID: 5385 RVA: 0x00086D54 File Offset: 0x00084F54
			// (set) Token: 0x0600150A RID: 5386 RVA: 0x00086D5C File Offset: 0x00084F5C
			public character_aoi character
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

			// Token: 0x1700040A RID: 1034
			// (get) Token: 0x0600150B RID: 5387 RVA: 0x00086D74 File Offset: 0x00084F74
			public bool HasCharacter
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x0600150C RID: 5388 RVA: 0x00086D84 File Offset: 0x00084F84
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
						this.character = this.deserialize.read_obj<character_aoi>();
					}
				}
			}

			// Token: 0x0600150D RID: 5389 RVA: 0x00086DE0 File Offset: 0x00084FE0
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_obj(this.character, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x0400183F RID: 6207
			private static int max_field_count = 1;

			// Token: 0x04001840 RID: 6208
			private character_aoi _character;
		}
	}
}
