/****************************************
Use: UserAuthentication Demo
*****************************************/
CREATE TABLE USER_MASTER(
user_master_guid uniqueidentifier not null,
user_name nvarchar(50) not null,
user_password nvarchar(512) not null,
user_salt nvarchar(512) not null)

/****************************************
Use: SearchAJAX Demo
*****************************************/
CREATE TABLE State_Master(
state_ansi_code nvarchar(2) not null,
state_name nvarchar(128) not null,
state_capital nvarchar(128) not null,
state_largest_city nvarchar(128) not null,
state_largest_metro nvarchar(128) not null)

INSERT INTO State_Master (state_ansi_code, state_name, state_capital, state_largest_city, state_largest_metro) 
VALUES ('AL', 'Alabama', 'Montgomery', 'Birmingham', 'Greater Birmingham Area')

INSERT INTO State_Master (state_ansi_code, state_name, state_capital, state_largest_city, state_largest_metro) 
VALUES ('AK', 'Alaska', 'Juneau', 'Anchorage', '')

INSERT INTO State_Master (state_ansi_code, state_name, state_capital, state_largest_city, state_largest_metro) 
VALUES ('AZ', 'Arizona', 'Phoenix', 'Phoenix', 'Salt River Valley')

INSERT INTO State_Master (state_ansi_code, state_name, state_capital, state_largest_city, state_largest_metro) 
VALUES ('AR', 'Arkansas', 'Little Rock', 'Little Rock', 'Little Rock-North Little Rock')

INSERT INTO State_Master (state_ansi_code, state_name, state_capital, state_largest_city, state_largest_metro) 
VALUES ('CA', 'California', 'Sacramento', 'Los Angeles', 'Greater Los Angeles Area')

INSERT INTO State_Master (state_ansi_code, state_name, state_capital, state_largest_city, state_largest_metro) 
VALUES ('CO', 'Colorado', 'Denver', 'Denver', 'Denver-Aurora CSA')

INSERT INTO State_Master (state_ansi_code, state_name, state_capital, state_largest_city, state_largest_metro) 
VALUES ('CT', 'Connecticut', 'Hartford', 'Bridgeport', 'Greater Hartford')

INSERT INTO State_Master (state_ansi_code, state_name, state_capital, state_largest_city, state_largest_metro) 
VALUES ('DE', 'Delaware', 'Dover', 'Wilmington', '')

INSERT INTO State_Master (state_ansi_code, state_name, state_capital, state_largest_city, state_largest_metro) 
VALUES ('FL', 'Florida', 'Tallahassee', 'Jacksonville', 'Miami')

INSERT INTO State_Master (state_ansi_code, state_name, state_capital, state_largest_city, state_largest_metro) 
VALUES ('GA', 'Georgia', 'Atlanta', 'Atlanta', 'Atlanta')

INSERT INTO State_Master (state_ansi_code, state_name, state_capital, state_largest_city, state_largest_metro) 
VALUES ('HI', 'Hawaii', 'Honolulu', 'Honolulu', 'Oahu')

INSERT INTO State_Master (state_ansi_code, state_name, state_capital, state_largest_city, state_largest_metro) 
VALUES ('ID', 'Idaho', 'Boise', 'Boise', 'Boise City-Nampa')

INSERT INTO State_Master (state_ansi_code, state_name, state_capital, state_largest_city, state_largest_metro) 
VALUES ('IL', 'Illinois', 'Springfield', 'Chicago', 'Chicago')

INSERT INTO State_Master (state_ansi_code, state_name, state_capital, state_largest_city, state_largest_metro) 
VALUES ('IN', 'Indiana', 'Indianapolis', 'Indianapolis', 'Greater Indianapolis')

INSERT INTO State_Master (state_ansi_code, state_name, state_capital, state_largest_city, state_largest_metro) 
VALUES ('IA', 'Iowa', 'Des Moines', 'Des Moines', 'Des Moines-West Des Moines')

INSERT INTO State_Master (state_ansi_code, state_name, state_capital, state_largest_city, state_largest_metro) 
VALUES ('KS', 'Kansas', 'Topeka', 'Wichita', 'Kansas portion of Kansas City')

INSERT INTO State_Master (state_ansi_code, state_name, state_capital, state_largest_city, state_largest_metro) 
VALUES ('KY', 'Kentucky', 'Frankfort', 'Louisville', 'Louisville-Jefferson County')

INSERT INTO State_Master (state_ansi_code, state_name, state_capital, state_largest_city, state_largest_metro) 
VALUES ('LA', 'Louisiana', 'Baton Rouge', 'New Orleans', 'Greater New Orleans')

INSERT INTO State_Master (state_ansi_code, state_name, state_capital, state_largest_city, state_largest_metro) 
VALUES ('ME', 'Maine', 'Augusta', 'Portland', 'Portland-South Portland-Biddeford')

INSERT INTO State_Master (state_ansi_code, state_name, state_capital, state_largest_city, state_largest_metro) 
VALUES ('MD', 'Maryland', 'Annapolis', 'Baltimore', 'Baltimore-Washington Metropolitan Area')

INSERT INTO State_Master (state_ansi_code, state_name, state_capital, state_largest_city, state_largest_metro) 
VALUES ('MA', 'Massachusetts', 'Boston', 'Boston', 'Greater Boston')

INSERT INTO State_Master (state_ansi_code, state_name, state_capital, state_largest_city, state_largest_metro) 
VALUES ('MI', 'Michigan', 'Lansing', 'Detroit', 'Detroit')

INSERT INTO State_Master (state_ansi_code, state_name, state_capital, state_largest_city, state_largest_metro) 
VALUES ('MN', 'Minnesota', 'Saint Paul', 'Minneapolis', 'Minneapolis-Saint Paul')

INSERT INTO State_Master (state_ansi_code, state_name, state_capital, state_largest_city, state_largest_metro) 
VALUES ('MS', 'Mississippi', 'Jackson', 'Jackson', '')

INSERT INTO State_Master (state_ansi_code, state_name, state_capital, state_largest_city, state_largest_metro) 
VALUES ('MO', 'Missouri', 'Jefferson City', 'Kansas City', 'Greater St. Louis Area')

INSERT INTO State_Master (state_ansi_code, state_name, state_capital, state_largest_city, state_largest_metro) 
VALUES ('MT', 'Montana', 'Helena', 'Billings', 'Billings')

INSERT INTO State_Master (state_ansi_code, state_name, state_capital, state_largest_city, state_largest_metro) 
VALUES ('NE', 'Nebraska', 'Lincoln', 'Omaha', 'Omaha-Council Bluffs')

INSERT INTO State_Master (state_ansi_code, state_name, state_capital, state_largest_city, state_largest_metro) 
VALUES ('NV', 'Nevada', 'Carson City', 'Las Vegas', 'Las Vegas-Paradise')

INSERT INTO State_Master (state_ansi_code, state_name, state_capital, state_largest_city, state_largest_metro) 
VALUES ('NH', 'New Hampshire', 'Concord', 'Manchester', 'Greater Manchester')

INSERT INTO State_Master (state_ansi_code, state_name, state_capital, state_largest_city, state_largest_metro) 
VALUES ('NJ', 'New Jersey', 'Trenton', 'Newark', 'New York City')

INSERT INTO State_Master (state_ansi_code, state_name, state_capital, state_largest_city, state_largest_metro) 
VALUES ('NM', 'New Mexico', 'Santa Fe', 'Albuquerque', 'Albuquerque')

INSERT INTO State_Master (state_ansi_code, state_name, state_capital, state_largest_city, state_largest_metro) 
VALUES ('NY', 'New York', 'Albany', 'New York City', 'New York City')

INSERT INTO State_Master (state_ansi_code, state_name, state_capital, state_largest_city, state_largest_metro) 
VALUES ('NC', 'North Carolina', 'Raleigh', 'Charlotte', 'Charlotte')

INSERT INTO State_Master (state_ansi_code, state_name, state_capital, state_largest_city, state_largest_metro) 
VALUES ('ND', 'North Dakota', 'Bismarck', 'Fargo', 'Fargo-Moorhead')

INSERT INTO State_Master (state_ansi_code, state_name, state_capital, state_largest_city, state_largest_metro) 
VALUES ('OH', 'Ohio', 'Columbus', 'Columbus', 'Greater Cleveland')

INSERT INTO State_Master (state_ansi_code, state_name, state_capital, state_largest_city, state_largest_metro) 
VALUES ('OK', 'Oklahoma', 'Oklahoma City', 'Oklahoma City', 'Oklahoma City-Shawnee')

INSERT INTO State_Master (state_ansi_code, state_name, state_capital, state_largest_city, state_largest_metro) 
VALUES ('OR', 'Oregon', 'Salem', 'Portland', 'Greater Portland')

INSERT INTO State_Master (state_ansi_code, state_name, state_capital, state_largest_city, state_largest_metro) 
VALUES ('PA', 'Pennsylvania', 'Harrisburg', 'Philadelphia', 'Delaware Valley')

INSERT INTO State_Master (state_ansi_code, state_name, state_capital, state_largest_city, state_largest_metro) 
VALUES ('RI', 'Rhode Island', 'Providence', 'Providence', '')

INSERT INTO State_Master (state_ansi_code, state_name, state_capital, state_largest_city, state_largest_metro) 
VALUES ('SC', 'South Carolina', 'Columbia', 'Columbia', 'Greenville-Anderson-Mauldin')

INSERT INTO State_Master (state_ansi_code, state_name, state_capital, state_largest_city, state_largest_metro) 
VALUES ('SD', 'South Dakota', 'Pierre', 'Sioux Falls', 'Sioux Falls')

INSERT INTO State_Master (state_ansi_code, state_name, state_capital, state_largest_city, state_largest_metro) 
VALUES ('TN', 'Tennessee', 'Nashville', 'Memphis', 'Nashville')

INSERT INTO State_Master (state_ansi_code, state_name, state_capital, state_largest_city, state_largest_metro) 
VALUES ('TX', 'Texas', 'Austin', 'Houstin', 'Dallas-Fort Worth-Arlington')

INSERT INTO State_Master (state_ansi_code, state_name, state_capital, state_largest_city, state_largest_metro) 
VALUES ('UT', 'Utah', 'Salt Lake City', 'Salt Lake City', '')

INSERT INTO State_Master (state_ansi_code, state_name, state_capital, state_largest_city, state_largest_metro) 
VALUES ('VT', 'Vermont', 'Montpelier', 'Burlington', 'Burlington-South Burlington')

INSERT INTO State_Master (state_ansi_code, state_name, state_capital, state_largest_city, state_largest_metro) 
VALUES ('VA', 'Virginia', 'Richmond', 'Virginia Beach', 'Washington–Arlington–Alexandria')

INSERT INTO State_Master (state_ansi_code, state_name, state_capital, state_largest_city, state_largest_metro) 
VALUES ('WA', 'Washington', 'Olympia', 'Seattle', 'Seattle–Tacoma–Bellevue')

INSERT INTO State_Master (state_ansi_code, state_name, state_capital, state_largest_city, state_largest_metro) 
VALUES ('WV', 'West Virginia', 'Charleston', 'Charleston', 'Huntington')

INSERT INTO State_Master (state_ansi_code, state_name, state_capital, state_largest_city, state_largest_metro) 
VALUES ('WI', 'Wisconsin', 'Madison', 'Milwaukee', 'Greater Milwaukee')

INSERT INTO State_Master (state_ansi_code, state_name, state_capital, state_largest_city, state_largest_metro) 
VALUES ('WY', 'Wyoming', 'Cheyenne', 'Cheyenne', 'Cheyenne')