/****************************************
Use: UserAuthentication Demo
*****************************************/
CREATE TABLE User_Master(
user_master_guid uniqueidentifier not null,
user_name nvarchar(50) not null,
user_password nvarchar(512) not null,
user_salt nvarchar(512) not null);
GO

/****************************************
Use: BinaryHandler Demo
*****************************************/
CREATE TABLE Binary_Master(
binary_id int identity(1,1) not null,
binary_data varbinary(max) not null,
binary_name nvarchar(256) not null,
binary_mime_type nvarchar(128) not null,
binary_size int not null);
GO

/****************************************
Use: SearchAJAX Demo
*****************************************/
CREATE TABLE State_Master(
state_ansi_code nvarchar(2) not null,
state_name nvarchar(128) not null,
state_capital nvarchar(128) not null,
state_largest_city nvarchar(128) not null,
state_largest_metro nvarchar(128) not null);
GO

INSERT INTO State_Master (state_ansi_code, state_name, state_capital, state_largest_city, state_largest_metro) 
VALUES ('AL', 'Alabama', 'Montgomery', 'Birmingham', 'Greater Birmingham Area');
GO

INSERT INTO State_Master (state_ansi_code, state_name, state_capital, state_largest_city, state_largest_metro) 
VALUES ('AK', 'Alaska', 'Juneau', 'Anchorage', '');
GO

INSERT INTO State_Master (state_ansi_code, state_name, state_capital, state_largest_city, state_largest_metro) 
VALUES ('AZ', 'Arizona', 'Phoenix', 'Phoenix', 'Salt River Valley');
GO

INSERT INTO State_Master (state_ansi_code, state_name, state_capital, state_largest_city, state_largest_metro) 
VALUES ('AR', 'Arkansas', 'Little Rock', 'Little Rock', 'Little Rock-North Little Rock');
GO

INSERT INTO State_Master (state_ansi_code, state_name, state_capital, state_largest_city, state_largest_metro) 
VALUES ('CA', 'California', 'Sacramento', 'Los Angeles', 'Greater Los Angeles Area');
GO

INSERT INTO State_Master (state_ansi_code, state_name, state_capital, state_largest_city, state_largest_metro) 
VALUES ('CO', 'Colorado', 'Denver', 'Denver', 'Denver-Aurora CSA');
GO

INSERT INTO State_Master (state_ansi_code, state_name, state_capital, state_largest_city, state_largest_metro) 
VALUES ('CT', 'Connecticut', 'Hartford', 'Bridgeport', 'Greater Hartford');
GO

INSERT INTO State_Master (state_ansi_code, state_name, state_capital, state_largest_city, state_largest_metro) 
VALUES ('DE', 'Delaware', 'Dover', 'Wilmington', '');
GO

INSERT INTO State_Master (state_ansi_code, state_name, state_capital, state_largest_city, state_largest_metro) 
VALUES ('FL', 'Florida', 'Tallahassee', 'Jacksonville', 'Miami');
GO

INSERT INTO State_Master (state_ansi_code, state_name, state_capital, state_largest_city, state_largest_metro) 
VALUES ('GA', 'Georgia', 'Atlanta', 'Atlanta', 'Atlanta');
GO

INSERT INTO State_Master (state_ansi_code, state_name, state_capital, state_largest_city, state_largest_metro) 
VALUES ('HI', 'Hawaii', 'Honolulu', 'Honolulu', 'Oahu');
GO

INSERT INTO State_Master (state_ansi_code, state_name, state_capital, state_largest_city, state_largest_metro) 
VALUES ('ID', 'Idaho', 'Boise', 'Boise', 'Boise City-Nampa');
GO

INSERT INTO State_Master (state_ansi_code, state_name, state_capital, state_largest_city, state_largest_metro) 
VALUES ('IL', 'Illinois', 'Springfield', 'Chicago', 'Chicago');
GO

INSERT INTO State_Master (state_ansi_code, state_name, state_capital, state_largest_city, state_largest_metro) 
VALUES ('IN', 'Indiana', 'Indianapolis', 'Indianapolis', 'Greater Indianapolis');
GO

INSERT INTO State_Master (state_ansi_code, state_name, state_capital, state_largest_city, state_largest_metro) 
VALUES ('IA', 'Iowa', 'Des Moines', 'Des Moines', 'Des Moines-West Des Moines');
GO

INSERT INTO State_Master (state_ansi_code, state_name, state_capital, state_largest_city, state_largest_metro) 
VALUES ('KS', 'Kansas', 'Topeka', 'Wichita', 'Kansas portion of Kansas City');
GO

INSERT INTO State_Master (state_ansi_code, state_name, state_capital, state_largest_city, state_largest_metro) 
VALUES ('KY', 'Kentucky', 'Frankfort', 'Louisville', 'Louisville-Jefferson County');
GO

INSERT INTO State_Master (state_ansi_code, state_name, state_capital, state_largest_city, state_largest_metro) 
VALUES ('LA', 'Louisiana', 'Baton Rouge', 'New Orleans', 'Greater New Orleans');
GO

INSERT INTO State_Master (state_ansi_code, state_name, state_capital, state_largest_city, state_largest_metro) 
VALUES ('ME', 'Maine', 'Augusta', 'Portland', 'Portland-South Portland-Biddeford');
GO

INSERT INTO State_Master (state_ansi_code, state_name, state_capital, state_largest_city, state_largest_metro) 
VALUES ('MD', 'Maryland', 'Annapolis', 'Baltimore', 'Baltimore-Washington Metropolitan Area');
GO

INSERT INTO State_Master (state_ansi_code, state_name, state_capital, state_largest_city, state_largest_metro) 
VALUES ('MA', 'Massachusetts', 'Boston', 'Boston', 'Greater Boston');
GO

INSERT INTO State_Master (state_ansi_code, state_name, state_capital, state_largest_city, state_largest_metro) 
VALUES ('MI', 'Michigan', 'Lansing', 'Detroit', 'Detroit');
GO

INSERT INTO State_Master (state_ansi_code, state_name, state_capital, state_largest_city, state_largest_metro) 
VALUES ('MN', 'Minnesota', 'Saint Paul', 'Minneapolis', 'Minneapolis-Saint Paul');
GO

INSERT INTO State_Master (state_ansi_code, state_name, state_capital, state_largest_city, state_largest_metro) 
VALUES ('MS', 'Mississippi', 'Jackson', 'Jackson', '');
GO

INSERT INTO State_Master (state_ansi_code, state_name, state_capital, state_largest_city, state_largest_metro) 
VALUES ('MO', 'Missouri', 'Jefferson City', 'Kansas City', 'Greater St. Louis Area');
GO

INSERT INTO State_Master (state_ansi_code, state_name, state_capital, state_largest_city, state_largest_metro) 
VALUES ('MT', 'Montana', 'Helena', 'Billings', 'Billings');
GO

INSERT INTO State_Master (state_ansi_code, state_name, state_capital, state_largest_city, state_largest_metro) 
VALUES ('NE', 'Nebraska', 'Lincoln', 'Omaha', 'Omaha-Council Bluffs');
GO

INSERT INTO State_Master (state_ansi_code, state_name, state_capital, state_largest_city, state_largest_metro) 
VALUES ('NV', 'Nevada', 'Carson City', 'Las Vegas', 'Las Vegas-Paradise');
GO

INSERT INTO State_Master (state_ansi_code, state_name, state_capital, state_largest_city, state_largest_metro) 
VALUES ('NH', 'New Hampshire', 'Concord', 'Manchester', 'Greater Manchester');
GO

INSERT INTO State_Master (state_ansi_code, state_name, state_capital, state_largest_city, state_largest_metro) 
VALUES ('NJ', 'New Jersey', 'Trenton', 'Newark', 'New York City');
GO

INSERT INTO State_Master (state_ansi_code, state_name, state_capital, state_largest_city, state_largest_metro) 
VALUES ('NM', 'New Mexico', 'Santa Fe', 'Albuquerque', 'Albuquerque');
GO

INSERT INTO State_Master (state_ansi_code, state_name, state_capital, state_largest_city, state_largest_metro) 
VALUES ('NY', 'New York', 'Albany', 'New York City', 'New York City');
GO

INSERT INTO State_Master (state_ansi_code, state_name, state_capital, state_largest_city, state_largest_metro) 
VALUES ('NC', 'North Carolina', 'Raleigh', 'Charlotte', 'Charlotte');
GO

INSERT INTO State_Master (state_ansi_code, state_name, state_capital, state_largest_city, state_largest_metro) 
VALUES ('ND', 'North Dakota', 'Bismarck', 'Fargo', 'Fargo-Moorhead');
GO

INSERT INTO State_Master (state_ansi_code, state_name, state_capital, state_largest_city, state_largest_metro) 
VALUES ('OH', 'Ohio', 'Columbus', 'Columbus', 'Greater Cleveland');
GO

INSERT INTO State_Master (state_ansi_code, state_name, state_capital, state_largest_city, state_largest_metro) 
VALUES ('OK', 'Oklahoma', 'Oklahoma City', 'Oklahoma City', 'Oklahoma City-Shawnee');
GO

INSERT INTO State_Master (state_ansi_code, state_name, state_capital, state_largest_city, state_largest_metro) 
VALUES ('OR', 'Oregon', 'Salem', 'Portland', 'Greater Portland');
GO

INSERT INTO State_Master (state_ansi_code, state_name, state_capital, state_largest_city, state_largest_metro) 
VALUES ('PA', 'Pennsylvania', 'Harrisburg', 'Philadelphia', 'Delaware Valley');
GO

INSERT INTO State_Master (state_ansi_code, state_name, state_capital, state_largest_city, state_largest_metro) 
VALUES ('RI', 'Rhode Island', 'Providence', 'Providence', '');
GO

INSERT INTO State_Master (state_ansi_code, state_name, state_capital, state_largest_city, state_largest_metro) 
VALUES ('SC', 'South Carolina', 'Columbia', 'Columbia', 'Greenville-Anderson-Mauldin');
GO

INSERT INTO State_Master (state_ansi_code, state_name, state_capital, state_largest_city, state_largest_metro) 
VALUES ('SD', 'South Dakota', 'Pierre', 'Sioux Falls', 'Sioux Falls');
GO

INSERT INTO State_Master (state_ansi_code, state_name, state_capital, state_largest_city, state_largest_metro) 
VALUES ('TN', 'Tennessee', 'Nashville', 'Memphis', 'Nashville');
GO

INSERT INTO State_Master (state_ansi_code, state_name, state_capital, state_largest_city, state_largest_metro) 
VALUES ('TX', 'Texas', 'Austin', 'Houstin', 'Dallas-Fort Worth-Arlington');
GO

INSERT INTO State_Master (state_ansi_code, state_name, state_capital, state_largest_city, state_largest_metro) 
VALUES ('UT', 'Utah', 'Salt Lake City', 'Salt Lake City', '');
GO

INSERT INTO State_Master (state_ansi_code, state_name, state_capital, state_largest_city, state_largest_metro) 
VALUES ('VT', 'Vermont', 'Montpelier', 'Burlington', 'Burlington-South Burlington');
GO

INSERT INTO State_Master (state_ansi_code, state_name, state_capital, state_largest_city, state_largest_metro) 
VALUES ('VA', 'Virginia', 'Richmond', 'Virginia Beach', 'Washington–Arlington–Alexandria');
GO

INSERT INTO State_Master (state_ansi_code, state_name, state_capital, state_largest_city, state_largest_metro) 
VALUES ('WA', 'Washington', 'Olympia', 'Seattle', 'Seattle–Tacoma–Bellevue');
GO

INSERT INTO State_Master (state_ansi_code, state_name, state_capital, state_largest_city, state_largest_metro) 
VALUES ('WV', 'West Virginia', 'Charleston', 'Charleston', 'Huntington');
GO

INSERT INTO State_Master (state_ansi_code, state_name, state_capital, state_largest_city, state_largest_metro) 
VALUES ('WI', 'Wisconsin', 'Madison', 'Milwaukee', 'Greater Milwaukee');
GO

INSERT INTO State_Master (state_ansi_code, state_name, state_capital, state_largest_city, state_largest_metro) 
VALUES ('WY', 'Wyoming', 'Cheyenne', 'Cheyenne', 'Cheyenne');
GO
