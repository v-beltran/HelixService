/****************************************
Use: Password Cryptography
*****************************************/
CREATE TABLE USER_MASTER(
user_master_guid uniqueidentifier not null,
user_name nvarchar(50) not null,
user_password nvarchar(512) not null,
user_salt nvarchar(512) not null)

