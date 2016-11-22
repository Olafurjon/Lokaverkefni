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

-- Games

-- Departments
INSERT INTO departments (dep_id, name) VALUES
  (1, 'StandardUsers'),
  (2, 'Reviewers'),
  (3, 'Programmers'),
  (4, 'Bossman');

-- Users
INSERT INTO Users (user_id, dep_id, name, email, username, access_level, joined, loggedin, title) VALUES
  (1, 4, 'Ólafur Jón Valgeirsson', 'Olafurjonb@hotmail.com', 'OlaVal', 4, '21.11.2016', 0, 'CEO'),
  (2, 4, 'Valdimar Gunnarsson', 'valdi1212@gmail.com', 'valdi1212', 4, '21.11.2016', 0, 'CEO'),
  (3, 3, 'Andrea Klara Hauksdóttir', 'Andreahar@gmail.com', 'AndHau', 3, '21.11.2016', 0, 'Programmer'),
  (4, 3, 'Guðmundur Steindórsson', 'Gummisteinn@gmail.com', 'GudSte', 3, '21.11.2016', 0, 'Programmer'),
  (5, 2, 'Halldor V. Jónsson', 'HalliVal@hotmail.com', 'HalJon', 2, '21.11.2016', 0, 'Reviewer'),
  (6, 2, 'Harpa Hjaltested', 'Harpa@hotmail.com', 'Harhja', 2, '21.11.2016', 0, 'Reviewer'),
  (7, 1, 'Hrannar Arason', 'Hrannaara@hotmail.com', 'HraAra', 1, '21.11.2016', 0, 'StandardUser'),
  (8, 1, 'Ingibjörg J. Guðlaugsdóttir', 'ingagugga@hotmail.com', 'IngGud', 1, '21.11.2016', 0, 'StandardUser');
