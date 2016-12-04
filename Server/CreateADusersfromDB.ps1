Add-Type -Path "C:\Users\Administrator\Desktop\MySql.Data.dll"
[System.Reflection.Assembly]::LoadWithPartialName("System.Windows.Forms") 
Import-Module ActiveDirectory  
$Server = "tsuts.tskoli.is";
$database = "3010943379_kepler_games";
$uid = "3010943379";
$tsutspass = "mypassword";
$Connstring = "Server=" + $Server + ";" + "DATABASE=" + $database + ";" + "UID=" + $uid + ";" + "PASSWORD=" + $tsutspass + ";";

 $adusers = @()
 $i = $null  
 $users = Get-ADUser -SearchBase "ou=KeplerUsers,dc=KeplerGames,dc=local" -filter * `  -property description  
 ForEach($user in $users)  
  {
    
    $adusers += $user.SamAccountName

 }  

$dbusers = @()

$connection = New-Object Mysql.Data.MySqlClient.MySqlConnection
$connection.connectionstring = $Connstring 
$connection.Open()
$command = $connection.CreateCommand()
$command.CommandText  = "select username from users";
$reader = $command.ExecuteReader()
$dataset = New-Object System.Data.DataSet
while($reader.Read()){
for ($i = 0; $i -lt $reader.FieldCount; $i++)
{
$dbusers += $reader.GetValue(0).ToString()
}
}
$connection.Close()

$missing = @()
$webusers = @()

foreach($username in $dbusers)
{
    if($adusers -notcontains $username)
    {
        $connection = New-Object Mysql.Data.MySqlClient.MySqlConnection
        $connection.connectionstring = $Connstring 
        $connection.Open()
        $command = $connection.CreateCommand()
        $command.CommandText  = "select * from users where username = '$username'";
        $reader = $command.ExecuteReader()
        while($reader.Read()){
        for($i = 0; $i -lt $reader.FieldCount; $i++)
        {
            $missing += $reader.GetValue($i).ToString()
            }
          
         }
            
            
    }
     $connection.Close()
}
$error.Clear()
 if($missing.Count -eq 0)
    {
    [System.Windows.Forms.MessageBox]::Show("No New Users in Database" , "Status") 

    }
    else {
  
try{

    $0 = 0
    $1 = 1
    $2 = 2
    $3 = 3
    $4 = 4
    $5 = 5
    $6 = 6
    $7 = 7
    $8 = 8
    $9 = 9
    $count = $missing.Count / 10
    [int]$missing[$1] = $missing[$1]
    [int]$missing[$0] = $missing[$0]
    [int]$missing[$6] = $missing[$6]
    [int]$missing[$8] = $missing[$8]
    $department = "StandardUsers";
for($i = 0 ; $i -lt $count ; $i++)
{
   
    
    

    if($missing[$1] -ne 5)
    {
       
        if($missing[$1] -eq 3)
        {
        $department = "Programmers"
        }
        elseif($missing[$1] -eq 2)
        {
        $department = "Reviewers"
        }
        elseif($missing[$1] -eq 1)
        {
        $department = "StandardUsers"
        }
      
        [int]$empid = $missing[$0]
        $splitname = $missing[$2].Split()
        $fullname = $missing[$2]
        $email = $missing[$3]        
        $user = $missing[$4]
        $password = $missing[$5]
        [int]$keplerlevel = $missing[$6]
        $joined = $missing[$7]
        [int]$loggedin = $missing[$8]
        $title = $missing[$9]
        $ppname = $($user + "@KeplerGames.Local")

        $givenname = $splitname[0]
        $surname = $splitname[1]

        New-ADUser -Name $fullname -DisplayName $fullname -GivenName $givenname -Surname $surname -SamAccountName $user -UserPrincipalName $ppname -Path "OU=$department,OU=KeplerUsers,DC=KeplerGames,DC=local" -AccountPassword (ConvertTo-SecureString -AsPlainText $password -Force) -Enabled $true -Title $title -EmployeeID $empid -Department $department -EmailAddress $email
        # New-ADUser -Name $name -DisplayName $name -GivenName $n.firstname -Surname $n.lastname -SamAccountName $n.username -UserPrincipalName $($n.username + "@"+$domainname+".Local") -Path $("OU=" + $department + ",OU=KeplerUsers,DC=$domainname,DC=local") -AccountPassword (ConvertTo-SecureString -AsPlainText "pass.123" -Force) -Enabled $true -Title $n.title -EmployeeID $n.user_id -Department $n.dep_name -EmailAddress $n.email#
        Add-ADGroupMember -Identity $department -Members $user
        Set-AdUser -Identity $user -add @{userJoined = $joined}
        if($loggedin -eq 1)
            {$loggedin = $true}
            else{$loggedin = $false}
        Set-AdUser -Identity $user -add @{userLoggedin = $false}
        Set-AdUser -Identity $user -add @{userKeplerAccessLvL = $keplerlevel}
        "User $user Has been created"

    }
    elseif($missing[$1] -eq 5)
    {
    $webusers += $missing[$i];
    }
    else
    {
    Write-Output "Error"
    }
    $0 += 10
    $1 += 10
    $2 += 10
    $3 += 10
    $4 += 10
    $5 += 10
    $6 += 10
    $7 += 10
    $8 += 10
    $9 += 10
    }
    [System.Windows.Forms.MessageBox]::Show("Users Created: $i" , "Status") 
    
    



}catch{$error}
}
