-- ==================================*** CREATE DATABASE SECTION ***==================================
DROP DATABASE IF EXISTS `3010943379_kepler_games`;
CREATE DATABASE `3010943379_kepler_games`;
USE `3010943379_kepler_games`;
-- ====================================*** CREATE TABLE SECTION ***===================================
CREATE TABLE Departments
(
  dep_id INT         NOT NULL AUTO_INCREMENT,
  name   VARCHAR(35) NOT NULL,
  CONSTRAINT dep_PK PRIMARY KEY (dep_id)
);

CREATE TABLE Developers
(
  dev_id      INT NOT NULL AUTO_INCREMENT,
  name        VARCHAR(35),
  description VARCHAR(115),
  CONSTRAINT developer_PK PRIMARY KEY (dev_id)
);

CREATE TABLE Genres
(
  genre_id INT NOT NULL AUTO_INCREMENT,
  name     VARCHAR(35),
  CONSTRAINT genre_PK PRIMARY KEY (genre_id)
);

CREATE TABLE Users
(
  user_id      INT         NOT NULL AUTO_INCREMENT,
  dep_id       INT,
  name         VARCHAR(75) NOT NULL,
  email        VARCHAR(50) NOT NULL,
  username     VARCHAR(35) NOT NULL,
  pass         VARCHAR(35) NOT NULL DEFAULT 'pass.123',
  access_level TINYINT(1),
  joined       DATETIME,
  loggedin     BINARY      NOT NULL,
  title        VARCHAR(20),
  CONSTRAINT user_PK PRIMARY KEY (user_id),
  CONSTRAINT user_dep_FK FOREIGN KEY (dep_id) REFERENCES Departments (dep_id)
);

CREATE TABLE DeveloperMembers
(
  devmember_id INT NOT NULL AUTO_INCREMENT,
  user_id      INT NOT NULL,
  dev_id       INT NOT NULL,
  dev_title    VARCHAR(35),
  CONSTRAINT PRIMARY KEY (devmember_id),
  CONSTRAINT FOREIGN KEY (user_id) REFERENCES Users (user_id),
  CONSTRAINT FOREIGN KEY (dev_id) REFERENCES Developers (dev_id)
);

CREATE TABLE Games
(
  game_id     INT         NOT NULL AUTO_INCREMENT,
  name        VARCHAR(35) NOT NULL,
  dev_id      INT,
  path        VARCHAR(100),
  dateadded   DATETIME,
  description VARCHAR(115),
  CONSTRAINT game_PK PRIMARY KEY (game_id),
  CONSTRAINT game_dev_FK FOREIGN KEY (dev_id) REFERENCES Developers (dev_id)
);

CREATE TABLE GameGenres
(
  gamegenre_id INT NOT NULL AUTO_INCREMENT,
  game_id      INT,
  genre_id     INT,
  CONSTRAINT gamegenre_PK PRIMARY KEY (gamegenre_id),
  CONSTRAINT gamegenre_game_FK FOREIGN KEY (game_id) REFERENCES Games (game_id),
  CONSTRAINT gamegenre_genre_FK FOREIGN KEY (genre_id) REFERENCES Genres (genre_id)
);