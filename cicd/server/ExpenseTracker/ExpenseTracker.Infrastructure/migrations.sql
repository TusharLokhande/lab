CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20251123130957_Initial') THEN
    CREATE TABLE "RefreshToken" (
        "Id" uuid NOT NULL,
        "UserId" uuid NOT NULL,
        "Token" text NOT NULL,
        "ExpiresAt" timestamp with time zone NOT NULL,
        "RevokedAt" timestamp with time zone,
        "IsActive" boolean NOT NULL,
        "CreatedBy" text,
        "CreatedAt" timestamp with time zone NOT NULL,
        "ModifiedBy" text,
        "UpdatedAt" timestamp with time zone,
        CONSTRAINT "PK_RefreshToken" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20251123130957_Initial') THEN
    CREATE TABLE "Roles" (
        "Id" uuid NOT NULL,
        "Name" character varying(50) NOT NULL,
        "Code" character varying(50) NOT NULL,
        "IsActive" boolean NOT NULL,
        "CreatedBy" text,
        "CreatedAt" timestamp with time zone NOT NULL,
        "ModifiedBy" text,
        "UpdatedAt" timestamp with time zone,
        CONSTRAINT "PK_Roles" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20251123130957_Initial') THEN
    CREATE TABLE "Users" (
        "Id" uuid NOT NULL,
        "Name" text NOT NULL,
        "Email" text NOT NULL,
        "PhoneNumber" text NOT NULL,
        "IsActive" boolean NOT NULL,
        "CreatedBy" text,
        "CreatedAt" timestamp with time zone NOT NULL DEFAULT (CURRENT_TIMESTAMP),
        "ModifiedBy" text,
        "UpdatedAt" timestamp with time zone DEFAULT (CURRENT_TIMESTAMP),
        CONSTRAINT "PK_Users" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20251123130957_Initial') THEN
    CREATE TABLE "AuthProviders" (
        "Id" uuid NOT NULL,
        "UserId" uuid NOT NULL,
        "Provider" integer NOT NULL,
        "ProviderUserId" character varying(200) NOT NULL,
        "IsActive" boolean NOT NULL,
        "CreatedBy" text,
        "CreatedAt" timestamp with time zone NOT NULL,
        "ModifiedBy" text,
        "UpdatedAt" timestamp with time zone,
        CONSTRAINT "PK_AuthProviders" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_AuthProviders_Users_UserId" FOREIGN KEY ("UserId") REFERENCES "Users" ("Id") ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20251123130957_Initial') THEN
    INSERT INTO "Roles" ("Id", "Code", "CreatedAt", "CreatedBy", "IsActive", "ModifiedBy", "Name", "UpdatedAt")
    VALUES ('11111111-1111-1111-1111-111111111111', 'Admin', TIMESTAMPTZ '2025-01-01T00:00:00Z', NULL, TRUE, NULL, 'Admin', NULL);
    INSERT INTO "Roles" ("Id", "Code", "CreatedAt", "CreatedBy", "IsActive", "ModifiedBy", "Name", "UpdatedAt")
    VALUES ('22222222-2222-2222-2222-222222222222', 'User', TIMESTAMPTZ '2025-01-01T00:00:00Z', NULL, TRUE, NULL, 'User', NULL);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20251123130957_Initial') THEN
    CREATE UNIQUE INDEX "IX_AuthProviders_Provider_ProviderUserId" ON "AuthProviders" ("Provider", "ProviderUserId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20251123130957_Initial') THEN
    CREATE UNIQUE INDEX "IX_AuthProviders_UserId_Provider" ON "AuthProviders" ("UserId", "Provider");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20251123130957_Initial') THEN
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20251123130957_Initial', '9.0.10');
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20251128135950_UserRoleMapping') THEN
    CREATE TABLE "UserRoleMappings" (
        "Id" uuid NOT NULL,
        "UserId" uuid NOT NULL,
        "RoleId" uuid NOT NULL,
        "IsActive" boolean NOT NULL,
        "CreatedBy" text,
        "CreatedAt" timestamp with time zone NOT NULL,
        "ModifiedBy" text,
        "UpdatedAt" timestamp with time zone,
        CONSTRAINT "PK_UserRoleMappings" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_UserRoleMappings_Roles_RoleId" FOREIGN KEY ("RoleId") REFERENCES "Roles" ("Id") ON DELETE CASCADE,
        CONSTRAINT "FK_UserRoleMappings_Users_UserId" FOREIGN KEY ("UserId") REFERENCES "Users" ("Id") ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20251128135950_UserRoleMapping') THEN
    CREATE INDEX "IX_UserRoleMappings_RoleId" ON "UserRoleMappings" ("RoleId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20251128135950_UserRoleMapping') THEN
    CREATE UNIQUE INDEX "IX_UserRoleMappings_UserId_RoleId" ON "UserRoleMappings" ("UserId", "RoleId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20251128135950_UserRoleMapping') THEN
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20251128135950_UserRoleMapping', '9.0.10');
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20251128142035_HandledPhoneNumber') THEN
    ALTER TABLE "Users" ALTER COLUMN "PhoneNumber" DROP NOT NULL;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20251128142035_HandledPhoneNumber') THEN
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20251128142035_HandledPhoneNumber', '9.0.10');
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20251128142944_HandledPhoneNumber2') THEN
    ALTER TABLE "Users" ALTER COLUMN "UpdatedAt" DROP DEFAULT;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20251128142944_HandledPhoneNumber2') THEN
    ALTER TABLE "Users" ALTER COLUMN "CreatedAt" DROP DEFAULT;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20251128142944_HandledPhoneNumber2') THEN
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20251128142944_HandledPhoneNumber2', '9.0.10');
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20251128144740_ChangesInDefault') THEN
    ALTER TABLE "Users" ALTER COLUMN "IsActive" SET DEFAULT TRUE;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20251128144740_ChangesInDefault') THEN
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20251128144740_ChangesInDefault', '9.0.10');
    END IF;
END $EF$;
COMMIT;

