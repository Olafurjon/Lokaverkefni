USE `3010943379_kepler_games`;

-- CRUD for Users table --

/*
  @name: UsersCreate
  @role: Inserts a new row into Users table
  @parameters:
    dep_id INT
    name VARCHAR(75)
    email VARCHAR(50)
    username VARHCAR(35)
    password VARCHAR(35)
    access_level INT
    title VARCHAR(20)
  @created: 16-11-2016
  @author: Valdimar Gunnarsson
  @description: See @role
 */
DELIMITER $$
DROP PROCEDURE IF EXISTS UsersCreate $$
CREATE PROCEDURE UsersCreate(dep_id   INT, name VARCHAR(75), email VARCHAR(50), username VARCHAR(35),
                             password VARCHAR(35), access_level INT, title VARCHAR(20))
  BEGIN
    INSERT INTO users (dep_id, name, email, username, pass, access_level, joined, title)
    VALUES (dep_id, name, email, username, pass, access_level, curdate(), title);
  END $$
DELIMITER ;

/*
  @name: UsersRead
  @role: Selects a row from the Users table
  @parameters:
    username VARCHAR(35)
  @created: 26-11-2016
  @author: Valdimar Gunnarsson
  @description: See @role
 */
DELIMITER $$
DROP PROCEDURE IF EXISTS UsersRead $$
CREATE PROCEDURE UsersRead(username VARCHAR(35))
  BEGIN
    SELECT
      users.name,
      users.email,
      users.username,
      users.access_level,
      users.joined,
      users.loggedin,
      users.title,
      departments.name
    FROM users
      INNER JOIN departments ON users.dep_id = departments.dep_id
    WHERE username = users.username;
  END $$
DELIMITER ;

/*
  @name: UsersUpdate
  @role: Updates a row from the Users table
  @paramaters:
    dep_id INT
    name VARCHAR(75)
    email VARCHAR(50)
    user_name VARCHAR(35)
    password VARCHAR(35)
    access_level INT
    title VARCHAR(20)
    old_username VARCHAR(35)
  @created: 26-11-2016
  @author: Valdimar Gunnarsson
  @description: See @role
 */
DELIMITER $$
DROP PROCEDURE IF EXISTS UsersUpdate $$
CREATE PROCEDURE UsersUpdate(dep_id   INT, name VARCHAR(75), email VARCHAR(50), user_name VARCHAR(35),
                             password VARCHAR(35), access_level INT, title VARCHAR(20), old_username VARCHAR(35))
  BEGIN
    UPDATE users
    SET dep_id = dep_id, name = name, email = email, pass = password, username = user_name, access_level = access_level, title = title
    WHERE users.username = old_username;
  END $$
DELIMITER ;

/*
  @name: UsersDelete
  @role: Deletes a row from Users
  @paramaters:
    username VARCHAR(35)
  @created: 26-11-2016
  @author: Valdimar Gunnarsson
  @description: See @role
*/
DELIMITER $$
DROP PROCEDURE IF EXISTS UsersDelete $$
CREATE PROCEDURE UsersDelete(username VARCHAR(35))
  BEGIN
    DELETE FROM users
    WHERE users.username = username;
  END $$
DELIMITER ;

-- CRUD for Games table --

/*
  @name: GamesCreate
  @role: Inserts a row into Games
  @parameters:
    dev_id INT
    name VARCHAR(35)
    path VARCHAR(100)
    description VARCHAR(115)
  @created: 3.12.2016
  @author: Valdimar Gunnarsson
  @description: See @role
 */
DELIMITER $$
DROP PROCEDURE IF EXISTS GamesCreate $$
CREATE PROCEDURE GamesCreate(dev_id INT, name VARCHAR(35), path VARCHAR(100), description VARCHAR(115))
  BEGIN
    INSERT INTO games (dev_id, name, path, dateadded, description)
    VALUES (dev_id, name, path, CURDATE(), description);
  END $$
DELIMITER $$

/*
  @name: GamesRead
  @role: Read a row from Games
  @parameters:
   name VARCHAR(35)
  @created: 3.12.2016
  @author: Valdimar Gunnarsson
  @description: See @role
 */
DELIMITER $$
DROP PROCEDURE IF EXISTS GamesRead $$
CREATE PROCEDURE GamesRead(game_name VARCHAR(35))
  BEGIN
    SELECT
      games.name,
      games.dateadded,
      games.description,
      developers.name AS Developer
    FROM games
      INNER JOIN developers ON games.dev_id = developers.dev_id
    WHERE game_name = games.name;
  END $$
DELIMITER ;

/*
  @name: GamesUpdate
  @role: Updates a single row in Games
  @parameters:
    dev_id INT
    name VARCHAR(35)
    path VARCHAR(100)
    description VARCHAR(115)
    old_name VARCHAR(35)
  @created: 3.12.2016
  @author: Valdimar Gunnarsson
  @description: See @role
 */
DELIMITER $$
DROP PROCEDURE IF EXISTS GamesUpdate $$
CREATE PROCEDURE GamesUpdate(dev_id INT, game_name VARCHAR(35), path VARCHAR(100), description VARCHAR(115), old_name VARCHAR(35))
  BEGIN
    UPDATE games
    SET dev_id = dev_id, name = game_name, path = path, description = description
    WHERE games.name = old_name;
  END $$
DELIMITER ;

/*
  @name: GamesDelete
  @role: Deletes a single row from Games
  @parameters:
    name VARCHAR(35)
  @created: 3.12.2016
  @author: Valdimar Gunnarsson
  @description: See @role
 */
DELIMITER $$
DROP PROCEDURE IF EXISTS GamesDelete $$
CREATE PROCEDURE GamesDelete(game_name VARCHAR(35))
  BEGIN
    DELETE FROM games
    WHERE game_name = games.name;
  END $$
DELIMITER ;

-- CRUD for Developers table --

/*
  @name: DevelopersCreate
  @role: Inserts a new row into Developers
  @parameters:
   name VARCHAR(35)
   description VARCHAR(115)
  @created: 3.12.2016
  @author: Valdimar Gunnarsson
  @description: See @role
 */
DELIMITER $$
DROP PROCEDURE IF EXISTS DevelopersCreate $$
CREATE PROCEDURE DevelopersCreate(name VARCHAR(35), description VARCHAR(115))
  BEGIN
    INSERT INTO developers (name, description)
    VALUES (name, description);
  END $$
DELIMITER ;

/*
  @name: DevelopersRead
  @role: Selects a single row from Developers
  @parameters:
    name VARCHAR(35)
  @created: 3.12.2016
  @author: Valdimar Gunnarsson
  @description: See @role
 */
DELIMITER $$
DROP PROCEDURE IF EXISTS DevelopersRead $$
CREATE PROCEDURE DevelopersRead(name VARCHAR(35))
  BEGIN
    SELECT (name, description)
    FROM developers
    WHERE name = name;
  END $$
DELIMITER ;

/*
  @name: DevelopersUpdate
  @role: Updates a single row from Developers
  @parameters:
   name VARCHAR(35)
   description VARCHAR(115)
  @created: 3.12.2016
  @author: Valdimar Gunnarsson
  @description: See @role
 */
DELIMITER $$
DROP PROCEDURE IF EXISTS DevelopersUpdate $$
CREATE PROCEDURE DevelopersUpdate(dev_name VARCHAR(35), description VARCHAR(115), old_name VARCHAR(35))
  BEGIN
    UPDATE developers
    SET  name = dev_name, description = description
    WHERE name = old_name;
  END $$
DELIMITER ;

/*
  @name: DevelopersDelete
  @role: Deletes a single row from Developers
  @parameters:
    name VARCHAR(35)
  @created: 3.12.2016
  @author: Valdimar Gunnarsson
  @description: See @role
 */
DELIMITER $$
DROP PROCEDURE IF EXISTS DevelopersDelete $$
CREATE PROCEDURE DevelopersDelete(dev_name VARCHAR(35))
  BEGIN
    DELETE FROM developers
    WHERE dev_name = developers.name;
  END $$
DELIMITER ;

-- Miscellaneous Procedures

DELIMITER $$
DROP PROCEDURE IF EXISTS UsersList $$
CREATE PROCEDURE UsersList()
  BEGIN
    SELECT
      users.name,
      users.email,
      users.username,
      users.access_level,
      users.joined,
      users.loggedin,
      users.title,
      departments.name AS Department
    FROM users
      INNER JOIN departments ON users.dep_id = departments.dep_id;
  END $$
DELIMITER ;

DELIMITER $$
DROP PROCEDURE IF EXISTS GamesList $$
CREATE PROCEDURE GamesList()
  BEGIN
    SELECT
      games.name,
      games.dateadded,
      games.description,
      developers.name AS Developer
    FROM games
      INNER JOIN developers ON games.dev_id = developers.dev_id;
  END $$
DELIMITER ;

DELIMITER $$
DROP PROCEDURE IF EXISTS AddGenreToGame $$
CREATE PROCEDURE AddGenreToGame(game_name VARCHAR(35), genre_name VARCHAR(35))
  BEGIN
    DECLARE gameid INT;
    DECLARE genreid INT;

    SELECT game_id
    FROM games
    WHERE games.name = game_name
    INTO gameid;

    SELECT genre_id
    FROM genres
    WHERE genres.name = genre_name
    INTO genreid;

    INSERT INTO gamegenres (game_id, genre_id)
    VALUES (gameid, genreid);
  END $$
DELIMITER ;

DELIMITER $$
DROP PROCEDURE IF EXISTS PostReview $$
CREATE PROCEDURE PostReview(username VARCHAR(35), gamename VARCHAR(35), comment VARCHAR(255))
  BEGIN
    DECLARE gameid INT;
    DECLARE userid INT;

    SELECT game_id
    FROM games
    WHERE games.name = gamename
    INTO gameid;

    SELECT user_id
    FROM users
    WHERE users.username = username
    INTO userid;

    INSERT INTO comments (user_id, game_id, date, comment)
    VALUES (userid, gameid, curdate(), comment);
  END $$
DELIMITER ;
