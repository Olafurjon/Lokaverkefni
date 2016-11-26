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
  @created: 26-11-2016
  @author: Valdimar Gunnarsson
  @description: See @role
 */
DELIMITER $$
DROP PROCEDURE IF EXISTS UsersUpdate $$
CREATE PROCEDURE UsersUpdate(dep_id   INT, name VARCHAR(75), email VARCHAR(50), user_name VARCHAR(35),
                             password VARCHAR(35), access_level INT, title VARCHAR(20))
  BEGIN
    UPDATE users
    SET dep_id = dep_id, name = name, email = email, pass = password, access_level = access_level, title = title
    WHERE users.username = user_name;
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