USE `3010943379_kepler_games`;

-- Genres
INSERT INTO genres (name) VALUES ('4X');
INSERT INTO genres (name) VALUES ('Action');
INSERT INTO genres (name) VALUES ('Adventure');
INSERT INTO genres (name) VALUES ('Arcade');
INSERT INTO genres (name) VALUES ('Fantasy');
INSERT INTO genres (name) VALUES ('RPG');
INSERT INTO genres (name) VALUES ('Sandbox');
INSERT INTO genres (name) VALUES ('Simulation');
INSERT INTO genres (name) VALUES ('Survival');
INSERT INTO genres (name) VALUES ('Stealth');
INSERT INTO genres (name) VALUES ('Strategy');
INSERT INTO genres (name) VALUES ('Sports');


-- Developers
INSERT INTO Developers (dev_id, name, description) VALUES
  (1, 'Kepler Games', 'Kepler Extreme'),
  (2, 'Game Testers', 'Test Games'),
  (4, 'Paxandell', 'We Make Your Wildest Dreams Come True!');

-- Games
INSERT INTO Games (game_id, name, dev_id, path, dateadded, description) VALUES
  (28, 'Bubbles Shooter', 1, '\\\\win3a-20\\Games\\Bubbles Shooter\\bubble_shooter2.swf', '2016-12-03 08:29:11',
   'Dont Cause Trouble Just Blow up the bubble'),
  (31, 'Tetris', 1, '\\\\win3a-20\\Games\\Tetris\\tetris.swf', '2016-12-03 08:34:44', 'Good Old Tetris'),
  (32, 'Solitaire', 1, '\\\\win3a-20\\Games\\Solitaire\\solitaire.swf', '2016-12-03 08:38:52',
   'Want to Relax but you dont have any playing cards? Then this is the game for you!'),
  (33, 'Bowman 2', 2, '\\\\win3a-20\\Games\\Bowman 2\\Bowman 2.swf', '2016-12-03 08:41:09',
   'Practise Your Archery Skills!'),
  (34, 'Ludo', 1, '\\\\win3a-20\\Games\\Ludo\\Ludo.swf', '2016-12-03 08:47:01',
   'Play Alone or Play with friends, its the classic Ludo!'),
  (35, 'Santa Snowboard', 1, '\\\\win3a-20\\Games\\Santa Snowboard\\santasnowboard.swf', '2016-12-03 08:49:16',
   'Make Christmas Come Early By Helping Santa Snowboard To Your Home'),
  (36, 'Motorcycle Mania', 1, '\\\\win3a-20\\Games\\MotorCycleMania\\motorcyclemania.swf', '2016-12-03 08:56:48',
   'Do You Like Motorcycles? if yes just play the game'),
  (39, '123', 1, '1231', '2016-12-03 09:08:58', '1231');

-- Departments
INSERT INTO Departments (dep_id, name) VALUES
  (1, 'StandardUsers'),
  (2, 'Reviewers'),
  (3, 'Programmers'),
  (4, 'Bossman'),
  (5, 'Web Users');

-- Users
INSERT INTO Users (user_id, dep_id, name, email, username, access_level, joined, loggedin, title) VALUES
  (1, 4, 'Ólafur Jón Valgeirsson', 'Olafurjonb@hotmail.com', 'OlaVal', 4, '21.11.2016', 0, 'CEO'),
  (2, 4, 'Valdimar Gunnarsson', 'valdi1212@gmail.com', 'valdi1212', 4, '21.11.2016', 0, 'CEO'),
  (3, 3, 'Andrea Klara Hauksdóttir', 'Andreahar@gmail.com', 'AndHau', 3, '21.11.2016', 0, 'Programmer'),
  (4, 3, 'Guðmundur Steindórsson', 'Gummisteinn@gmail.com', 'GudSte', 3, '21.11.2016', 0, 'Programmer'),
  (5, 2, 'Halldor V. Jónsson', 'HalliVal@hotmail.com', 'HalJon', 2, '21.11.2016', 0, 'Reviewer'),
  (6, 2, 'Harpa Hjaltested', 'Harpa@hotmail.com', 'Harhja', 2, '21.11.2016', 0, 'Reviewer'),
  (7, 1, 'Hrannar Arason', 'Hrannaara@hotmail.com', 'HraAra', 1, '21.11.2016', 0, 'StandardUser'),
  (8, 1, 'Ingibjörg J. Guðlaugsdóttir', 'ingagugga@hotmail.com', 'IngGud', 1, '21.11.2016', 0, 'StandardUser'),
  (10, 3, 'Dean Win', 'Dd@kepler.is', 'Deantest', 3, '2016-11-30 00:00:00', 0, ''),
  (12, 3, 'Deanwin', 'dd@kepler.is', 'deanwon', 3, '2016-11-30 00:00:00', 0, ''),
  (20, 1, 'dang', 'dang', 'dang', 2, '2016-11-30 00:00:00', 0, ''),
  (27, 3, 'Alexander Guðmundsson', 'AleGud@kepler.is', 'AleGud', 3, '2016-11-30 06:09:49', 0, 'Programmer');

-- DeveloperMembers
INSERT INTO DeveloperMembers (devmember_id, user_id, dev_id, dev_title) VALUES
  (1, 2, 1, 'Head Programmer'),
  (2, 1, 1, 'Programmer'),
  (3, 27, 4, 'Owner');

-- Comments
INSERT INTO Comments (comment_id, date, user_id, game_id, comment) VALUES
  (2, '2016-12-01 00:00:00', 1, 28, 'Best game ever!'),
  (3, '2016-12-01 00:00:00', 1, 31, 'ermagherd'),
  (4, '2016-12-01 00:00:00', 1, 32, 'Good Game\r\n'),
  (5, '2016-12-01 00:00:00', 1, 33, 'Good Game\r\n'),
  (6, '2016-12-02 00:00:00', 1, 34, 'hellapður ejnas ');