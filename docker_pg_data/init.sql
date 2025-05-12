-- ----------------------------
-- Table structure for TaskAssignments
-- ----------------------------
DROP TABLE IF EXISTS "public"."TaskAssignments";
CREATE TABLE "public"."TaskAssignments" (
                                            "TaskId" uuid NOT NULL,
                                            "UserId" uuid NOT NULL,
                                            "AssignedAt" timestamptz(6) NOT NULL
)
;
ALTER TABLE "public"."TaskAssignments" OWNER TO "user";

-- ----------------------------
-- Records of TaskAssignments
-- ----------------------------
BEGIN;
INSERT INTO "public"."TaskAssignments" ("TaskId", "UserId", "AssignedAt") VALUES ('9d32a5ca-fa04-4fda-bcf2-40e7a1a5687e', '582c1bd5-c2fb-42eb-ba97-300956fae069', '2025-05-05 12:00:00+00');
INSERT INTO "public"."TaskAssignments" ("TaskId", "UserId", "AssignedAt") VALUES ('9d32a5ca-fa04-4fda-bcf2-40e7a1a5687e', 'a86193b7-280c-4fca-b7f4-1956563f8d47', '2025-05-05 12:00:00+00');
INSERT INTO "public"."TaskAssignments" ("TaskId", "UserId", "AssignedAt") VALUES ('f27d145e-662f-4601-ae80-90c83e430676', 'c4c0122a-d3f4-4f51-bec8-046f27e134d1', '2025-05-06 13:00:00+00');
COMMIT;

-- ----------------------------
-- Table structure for TaskStatuses
-- ----------------------------
DROP TABLE IF EXISTS "public"."TaskStatuses";
CREATE TABLE "public"."TaskStatuses" (
                                         "Id" uuid NOT NULL,
                                         "Name" varchar(50) COLLATE "pg_catalog"."default" NOT NULL
)
;
ALTER TABLE "public"."TaskStatuses" OWNER TO "user";

-- ----------------------------
-- Records of TaskStatuses
-- ----------------------------
BEGIN;
INSERT INTO "public"."TaskStatuses" ("Id", "Name") VALUES ('5d288d05-fabc-4c31-a3a5-efc0c65fcd03', 'Pendiente');
INSERT INTO "public"."TaskStatuses" ("Id", "Name") VALUES ('e1f7b815-e9eb-48c1-a487-d90097a5b03f', 'En progreso');
INSERT INTO "public"."TaskStatuses" ("Id", "Name") VALUES ('e969c3bc-2ffd-4315-9da3-ce6ebc3f4599', 'Completada');
COMMIT;

-- ----------------------------
-- Table structure for Tasks
-- ----------------------------
DROP TABLE IF EXISTS "public"."Tasks";
CREATE TABLE "public"."Tasks" (
                                  "Id" uuid NOT NULL,
                                  "Title" varchar(200) COLLATE "pg_catalog"."default" NOT NULL,
                                  "Description" text COLLATE "pg_catalog"."default",
                                  "DueDate" timestamptz(6),
                                  "StatusId" uuid NOT NULL,
                                  "CreatedBy" uuid NOT NULL,
                                  "CreatedAt" timestamptz(6) NOT NULL,
                                  "UpdatedAt" timestamptz(6) NOT NULL
)
;
ALTER TABLE "public"."Tasks" OWNER TO "user";

-- ----------------------------
-- Records of Tasks
-- ----------------------------
BEGIN;
INSERT INTO "public"."Tasks" ("Id", "Title", "Description", "DueDate", "StatusId", "CreatedBy", "CreatedAt", "UpdatedAt") VALUES ('37439628-c72b-4087-9269-4b4845d24aef', 'Fix bug #42 in TaskController', 'Resolve null-reference error when updating task status via API.', '2025-05-12 00:00:00+00', 'e1f7b815-e9eb-48c1-a487-d90097a5b03f', 'a86193b7-280c-4fca-b7f4-1956563f8d47', '2025-05-07 09:15:00+00', '2025-05-07 09:15:00+00');
INSERT INTO "public"."Tasks" ("Id", "Title", "Description", "DueDate", "StatusId", "CreatedBy", "CreatedAt", "UpdatedAt") VALUES ('434bcb00-de9e-440f-a12d-3ca17bc4c156', 'Write unit tests for TaskService', 'Cover main CRUD operations in TaskService with xUnit tests.', '2025-05-05 00:00:00+00', 'e969c3bc-2ffd-4315-9da3-ce6ebc3f4599', '47809d2b-f27b-46ec-bc1a-1631ccdb6629', '2025-05-03 13:20:00+00', '2025-05-05 13:20:00+00');
INSERT INTO "public"."Tasks" ("Id", "Title", "Description", "DueDate", "StatusId", "CreatedBy", "CreatedAt", "UpdatedAt") VALUES ('4823a311-d377-497e-80a9-ad3c030c6f92', 'Refactor services layer', 'Clean up and reorganize service classes for better maintainability.', '2025-05-08 00:00:00+00', 'e969c3bc-2ffd-4315-9da3-ce6ebc3f4599', 'd7895c16-c97e-4b27-b070-516dd7166d89', '2025-05-04 16:00:00+00', '2025-05-08 16:00:00+00');
INSERT INTO "public"."Tasks" ("Id", "Title", "Description", "DueDate", "StatusId", "CreatedBy", "CreatedAt", "UpdatedAt") VALUES ('9d32a5ca-fa04-4fda-bcf2-40e7a1a5687e', 'Implement user authentication', 'Set up JWT-based auth, configure login & registration endpoints.', '2025-05-15 00:00:00+00', '5d288d05-fabc-4c31-a3a5-efc0c65fcd03', '47809d2b-f27b-46ec-bc1a-1631ccdb6629', '2025-05-05 10:00:00+00', '2025-05-05 10:00:00+00');
INSERT INTO "public"."Tasks" ("Id", "Title", "Description", "DueDate", "StatusId", "CreatedBy", "CreatedAt", "UpdatedAt") VALUES ('a2047ded-82bc-4ab5-a666-e57a027f4099', 'Optimize SQL queries for reports', 'Improve performance of monthly summary reports by adding indexes.', '2025-05-18 00:00:00+00', 'e1f7b815-e9eb-48c1-a487-d90097a5b03f', 'c4c0122a-d3f4-4f51-bec8-046f27e134d1', '2025-05-08 11:45:00+00', '2025-05-08 11:45:00+00');
INSERT INTO "public"."Tasks" ("Id", "Title", "Description", "DueDate", "StatusId", "CreatedBy", "CreatedAt", "UpdatedAt") VALUES ('f27d145e-662f-4601-ae80-90c83e430676', 'Design database schema', 'Create tables and relationships for tasks, users and assignments.', '2025-05-20 00:00:00+00', '5d288d05-fabc-4c31-a3a5-efc0c65fcd03', '582c1bd5-c2fb-42eb-ba97-300956fae069', '2025-05-06 14:30:00+00', '2025-05-06 14:30:00+00');
COMMIT;

-- ----------------------------
-- Table structure for Users
-- ----------------------------
DROP TABLE IF EXISTS "public"."Users";
CREATE TABLE "public"."Users" (
                                  "Id" uuid NOT NULL,
                                  "Name" text COLLATE "pg_catalog"."default" NOT NULL,
                                  "Email" text COLLATE "pg_catalog"."default" NOT NULL,
                                  "PasswordHash" text COLLATE "pg_catalog"."default" NOT NULL,
                                  "ProfileImage" text COLLATE "pg_catalog"."default" NOT NULL
)
;
ALTER TABLE "public"."Users" OWNER TO "user";

-- ----------------------------
-- Records of Users
-- ----------------------------
BEGIN;
INSERT INTO "public"."Users" ("Id", "Name", "Email", "PasswordHash", "ProfileImage") VALUES ('47809d2b-f27b-46ec-bc1a-1631ccdb6629', 'Juan', 'juan@gmail.com', '$2a$11$ISa.OtzV/z749.siWa4eCeYxD4z5ZW64JH8yRo3Sx7v/CpCHd3kRe', 'https://api.dicebear.com/9.x/adventurer/svg?seed=Juan');
INSERT INTO "public"."Users" ("Id", "Name", "Email", "PasswordHash", "ProfileImage") VALUES ('582c1bd5-c2fb-42eb-ba97-300956fae069', 'Pedro', 'pedro@gmail.com', '$2a$11$k1JlK5TBemoH.HogQHpsS.MjPAtJiwZ2fNO6fD3x3AxneCPoQjpce', 'https://api.dicebear.com/9.x/adventurer/svg?seed=Pedro');
INSERT INTO "public"."Users" ("Id", "Name", "Email", "PasswordHash", "ProfileImage") VALUES ('a86193b7-280c-4fca-b7f4-1956563f8d47', 'Maria', 'maria@gmail.com', '$2a$11$k1JlK5TBemoH.HogQHpsS.MjPAtJiwZ2fNO6fD3x3AxneCPoQjpce', 'https://api.dicebear.com/9.x/adventurer/svg?seed=Maria');
INSERT INTO "public"."Users" ("Id", "Name", "Email", "PasswordHash", "ProfileImage") VALUES ('c4c0122a-d3f4-4f51-bec8-046f27e134d1', 'Ana', 'ana@gmail.com', '$2a$11$AvgbZtrqb3QYahLjPgEAReoGhKWJKmU8rshLFQL9VwT.S67Xm0wAy', 'https://api.dicebear.com/9.x/adventurer/svg?seed=Ana');
INSERT INTO "public"."Users" ("Id", "Name", "Email", "PasswordHash", "ProfileImage") VALUES ('d7895c16-c97e-4b27-b070-516dd7166d89', 'Luis', 'luis@gmail.com', '$2a$11$YGDf30Qh0Qodt4qm.m58ZOY/aeSY36YHThpd5Hcne/Z606W0xQdAe', 'https://api.dicebear.com/9.x/adventurer/svg?seed=Luis');
COMMIT;

-- ----------------------------
-- Table structure for __EFMigrationsHistory
-- ----------------------------
DROP TABLE IF EXISTS "public"."__EFMigrationsHistory";
CREATE TABLE "public"."__EFMigrationsHistory" (
                                                  "MigrationId" varchar(150) COLLATE "pg_catalog"."default" NOT NULL,
                                                  "ProductVersion" varchar(32) COLLATE "pg_catalog"."default" NOT NULL
)
;
ALTER TABLE "public"."__EFMigrationsHistory" OWNER TO "user";

-- ----------------------------
-- Records of __EFMigrationsHistory
-- ----------------------------
BEGIN;
INSERT INTO "public"."__EFMigrationsHistory" ("MigrationId", "ProductVersion") VALUES ('20250512014138_InitDatabase', '9.0.4');
COMMIT;

-- ----------------------------
-- Indexes structure for table TaskAssignments
-- ----------------------------
CREATE INDEX "IX_TaskAssignments_UserId" ON "public"."TaskAssignments" USING btree (
    "UserId" "pg_catalog"."uuid_ops" ASC NULLS LAST
    );

-- ----------------------------
-- Primary Key structure for table TaskAssignments
-- ----------------------------
ALTER TABLE "public"."TaskAssignments" ADD CONSTRAINT "PK_TaskAssignments" PRIMARY KEY ("TaskId", "UserId");

-- ----------------------------
-- Indexes structure for table TaskStatuses
-- ----------------------------
CREATE UNIQUE INDEX "IX_TaskStatuses_Name" ON "public"."TaskStatuses" USING btree (
    "Name" COLLATE "pg_catalog"."default" "pg_catalog"."text_ops" ASC NULLS LAST
    );

-- ----------------------------
-- Primary Key structure for table TaskStatuses
-- ----------------------------
ALTER TABLE "public"."TaskStatuses" ADD CONSTRAINT "PK_TaskStatuses" PRIMARY KEY ("Id");

-- ----------------------------
-- Indexes structure for table Tasks
-- ----------------------------
CREATE INDEX "IX_Tasks_CreatedBy" ON "public"."Tasks" USING btree (
    "CreatedBy" "pg_catalog"."uuid_ops" ASC NULLS LAST
    );
CREATE INDEX "IX_Tasks_StatusId" ON "public"."Tasks" USING btree (
    "StatusId" "pg_catalog"."uuid_ops" ASC NULLS LAST
    );

-- ----------------------------
-- Primary Key structure for table Tasks
-- ----------------------------
ALTER TABLE "public"."Tasks" ADD CONSTRAINT "PK_Tasks" PRIMARY KEY ("Id");

-- ----------------------------
-- Primary Key structure for table Users
-- ----------------------------
ALTER TABLE "public"."Users" ADD CONSTRAINT "PK_Users" PRIMARY KEY ("Id");

-- ----------------------------
-- Primary Key structure for table __EFMigrationsHistory
-- ----------------------------
ALTER TABLE "public"."__EFMigrationsHistory" ADD CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId");

-- ----------------------------
-- Foreign Keys structure for table TaskAssignments
-- ----------------------------
ALTER TABLE "public"."TaskAssignments" ADD CONSTRAINT "FK_TaskAssignments_Tasks_TaskId" FOREIGN KEY ("TaskId") REFERENCES "public"."Tasks" ("Id") ON DELETE CASCADE ON UPDATE NO ACTION;
ALTER TABLE "public"."TaskAssignments" ADD CONSTRAINT "FK_TaskAssignments_Users_UserId" FOREIGN KEY ("UserId") REFERENCES "public"."Users" ("Id") ON DELETE CASCADE ON UPDATE NO ACTION;

-- ----------------------------
-- Foreign Keys structure for table Tasks
-- ----------------------------
ALTER TABLE "public"."Tasks" ADD CONSTRAINT "FK_Tasks_TaskStatuses_StatusId" FOREIGN KEY ("StatusId") REFERENCES "public"."TaskStatuses" ("Id") ON DELETE RESTRICT ON UPDATE NO ACTION;
ALTER TABLE "public"."Tasks" ADD CONSTRAINT "FK_Tasks_Users_CreatedBy" FOREIGN KEY ("CreatedBy") REFERENCES "public"."Users" ("Id") ON DELETE RESTRICT ON UPDATE NO ACTION;
