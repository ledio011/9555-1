Railway persistent data

The server stores character data, guilds, and event state in JSON files. Railway's deployment filesystem is ephemeral, so attach a volume to the service and mount it at /data. The server uses Railway's RAILWAY_VOLUME_MOUNT_PATH automatically; DATA_DIR can override it.

On the first startup with a volume, existing state files beside 9555.py are copied into the volume only when the corresponding volume file does not exist. Existing volume data is never overwritten. If Railway has already discarded the old deployment filesystem, this migration cannot recover that lost data; restore it from a backup.

Without a volume (or DATA_DIR), the server logs a warning on Railway and continues using its deployment directory, which can still be lost during redeploys.

Encrypted GitHub backups

The server can also upload a Fernet-encrypted snapshot immediately at startup and every 24 hours to ledio011/AutoTheftDatabase (or GITHUB_BACKUP_REPOSITORY). It includes character data, its backup file, guild data, and persisted event state. The snapshot is stored at backups/server-data.zip.fernet; each upload updates that file, leaving prior encrypted versions in Git history. Uploads are refused unless the repository API reports it as private.

Configure these Railway variables:

GITHUB_BACKUP_TOKEN: a fine-grained GitHub token with Contents: Read and write access to the private backup repository.
GITHUB_BACKUP_ENCRYPTION_KEY: a Fernet key generated with python -c "from cryptography.fernet import Fernet; print(Fernet.generate_key().decode())". Keep a copy outside Railway and GitHub; backups cannot be decrypted without it.
GITHUB_BACKUP_REPOSITORY: optional owner/repository override; defaults to ledio011/AutoTheftDatabase.
GITHUB_BACKUP_BRANCH: optional branch override; defaults to the repository's default branch.

The backup worker logs an error and retries the next day if the repository is unavailable or the token lacks access. The current development GitHub connection could not resolve ledio011/AutoTheftDatabase, so verify that the repository exists and that the Railway token can access it before relying on backups.
