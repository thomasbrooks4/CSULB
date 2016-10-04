SELECT * from Software_Teams
SELECT * from Meetings
SELECT * from Conference_Rooms
SELECT Software_Team_Name from Software_Teams

CREATE TABLE Software_Teams
(
	Software_Team_Name varchar(15),
	Team_Leader varchar(15),
	Team_formed_date date,
	Project_Name varchar(55),
    PRIMARY KEY (Software_Team_Name)
)

CREATE TABLE Meetings
(
	Software_Team_Name varchar(15),
	Room_Name varchar(15),
	Date date,
	Purpose_of_Meeting varchar(55),
    PRIMARY KEY (Software_Team_Name, Room_Name, Date),
    FOREIGN KEY (Software_Team_Name) REFERENCES Software_Teams(Software_Team_Name)
)

CREATE TABLE Conference_Rooms
(
	Room_Number int,
	Room_Name varchar(15),
	Building_Name varchar(15),
	Room_Phone int,
	Projector_Type varchar(15),
    PRIMARY KEY (Room_Name)
)

ALTER TABLE Meetings
ADD FOREIGN KEY (Room_Name)
REFERENCES Conference_Rooms(Room_Name)

INSERT INTO Conference_Rooms
(
	Room_Number,
	Room_Name,
	Building_Name,
	Room_Phone,
	Projector_Type
)
VALUES
(101, 'Conference A', 'Executive HQ', 424611, 'VGA'),
(102, 'Conference B', 'Executive HQ', 346187, 'HDMI'),
(103, 'Conference C', 'Executive HQ', 879431, 'Display Port'),
(201, 'Conference D', 'Executive HQ', 845126, 'VGA'),
(202, 'Conference E', 'Executive HQ', 651342, 'VGA')

INSERT INTO Software_Teams
(
	Software_Team_Name,
	Team_leader,
	Team_formed_date,
	Project_Name
)
VALUES
('Team 1', 'Jerry', '20141126', 'Pharm'),
('Team 2', 'Jake', '20141129', 'Webscription'),
('Team 3', 'Daniel', '20141205', 'Webscrip'),
('Team 4', 'Greg', '20141208', 'e-Script'),
('Team 5', 'Tom', '20141211', 'Medi-Web'),
('Team 6', 'John', '20141217', 'Chrome Tools'),
('Team 7', 'Harrison', '20141226', 'FBX plugin'),
('Team 8', 'Chris', '20150106', 'ez-Tax'),
('Team 9', 'Hansons', '20150116', 'Movin’ Music'),
('Team 10', 'Claire', '20150124', 'Health-er'),
('Team 11', 'Abigail', '20150209', 'Scrabble'),
('Team 12', 'Brian', '20150226', 'Slap Chat'),
('Team 13', 'Grace', '20150228', 'Flinch!'),
('Team 14', 'Alex', '20150302', 'Finale'),
('Team 15', 'Jenny', '20150314', 'My Jams'),
('Team 16', 'Patricia', '20150323', 'Beach Radio'),
('Team 17', 'Cathy', '20150327', 'Flashlight'),
('Team 18', 'Mike', '20150407', 'PassProtect'),
('Team 19', 'Adam', '20150419', 'Weatheria'),
('Team 20', 'Robin', '20150423', 'Pocket Physics')