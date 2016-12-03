<?php

$servername = 'tsuts.tskoli.is';
$user = '3010943379';
$password = 'mypassword';
$dbname = "3010943379_kepler_games";
try {
$connection = new PDO("mysql:host=$servername;dbname=$dbname", $user, $password);
$connection -> setAttribute (PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);
$connection -> exec('SET NAMES "utf8"');
// $dbh->exec("set names utf8");
}
 catch (PDOException $e)
{
echo 'Connection Failed: ' . $e->getMessage();
}
?>