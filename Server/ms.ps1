function NetkortogForest #Vilt keyra þetta fyrst, uppsetning á domaininu
{
 param(
        [Parameter(Mandatory=$true, HelpMessage="Hvað á Domainið að heita? (1/5)")]
        [string]$domain,
        [Parameter(Mandatory=$true, HelpMessage="Sláðu inn heitið á netkortinu sem þú vilt nota.(2/5)")] #Yfirleitt Ethernet 1 eða tvö
        [string]$gamlanetkort,
        [Parameter(Mandatory=$true, HelpMessage="Sláðu inn nýtt nafn fyrir það (3/5)")]
        [string]$nyjanetkort,
        [Parameter(Mandatory=$true, HelpMessage="Sláðu inn IP töluna sem þú vilt nota (4/5)")]
        $ipaddress,
        [Parameter(Mandatory=$true, HelpMessage="Sláðu inn prefixið á iptölunni (5/5)")]
        [int]$prefix
        
    )
    $local = ".local"
    $domainlocal = $domain+$local;
   



try {
if($gamlanetkort -notlike "" -or $nyjanetkort -notlike "") 
{
Rename-NetAdapter -Name $gamlanetkort -NewName $nyjanetkort
New-NetIPAddress -InterfaceAlias $nyjanetkort -IPAddress $ipaddress -PrefixLength $prefix #-DefaultGateway 192.168.1.1 notum ekki default gateway
Set-DnsClientServerAddress -InterfaceAlias $nyjanetkort -ServerAddresses 127.0.0.1 
}

Install-WindowsFeature -Name AD-Domain-Services –IncludeManagementTools
Install-ADDSForest –DomainName $domainlocal –InstallDNS -SafeModeAdministratorPassword (ConvertTo-SecureString -AsPlainText "pass.123" -Force) 
}
catch {$error, "Vandamál kom upp" }
if (!$error) {
$wshell = New-Object -ComObject Wscript.Shell
$wshell.Popup("Aðgerð Tókst, Vél mun endurræsa sig",0,"Okei",0x1)

}
}
function SetjauppDHCPscopeogClientADomain #Keyra þetta nr2
{
 param(
        [Parameter(Mandatory=$true, HelpMessage="Hvað á scopeið að heita? (1/7)")]
        [string]$scopename,
        [Parameter(Mandatory=$true, HelpMessage="Á hvaða IP á scopeið að byrja?(2/7)")] #Hversu marga notendur viltu í raun hafa
        $ipstart,
        [Parameter(Mandatory=$true, HelpMessage="Á hvaða Ip á scopeið að enda? (3/7)")]
        $ipend,
        [Parameter(Mandatory=$true, HelpMessage="Hver er subnet maskinn? (4/7)")]
        $subnet,
        [Parameter(Mandatory=$true, HelpMessage="Hvaða IP tala á serverinn að vera? (5/7)")]
        $routerip,
        [Parameter(Mandatory=$true, HelpMessage="Hvað er nafnið á server vélinni? (t.d. WIN3A-15) (6/7)")]
        [string]$servervel,
        [Parameter(Mandatory=$true, HelpMessage="Hvað er nafnið á Client vélinni? (t.d. WIN3A-W81-15) (7/7)")]
        [string]$client
        
    )
    $domain2 = Get-ADDomain
    $local = $domain2.DNSroot

    $domainname = $domain2.name
    $dnsname = $domainname+".local"

    
  
Install-WindowsFeature –Name DHCP –IncludeManagementTools
Add-DhcpServerv4Scope -Name $scopename -StartRange $ipstart -EndRange $ipend -SubnetMask $subnet
Set-DhcpServerv4OptionValue -DnsServer $routerip -Router $routerip
Add-DhcpServerInDC -DnsName $domain2.DNSroot #t.d. $($env:computername + “.” $env:userdnsdomain)


Add-Computer -ComputerName $client -LocalCredential $client\Administrator -DomainName $domainname -Credential $domain2.DNSroot+\Administrator


if (!$error) {
$wshell = New-Object -ComObject Wscript.Shell
$wshell.Popup("Aðgerð Tókst, Vél mun endurræsa sig",0,"Okei",0x1) 
 }
 }
  function BuaTilNotendur #Býr til CSV skrá, er með harðkóðað bara pathið og CSV destination bara til að auðvelda lífð
{
 param(
        [Parameter(Mandatory=$true, HelpMessage="Hvar viltu búa til möppurnar?(C:\)")]
        [string]$path
   
    )
     $path = "C:\KeplerGames"

   $getdomain = Get-ADDomain
   $domainname  = $getdomain.Name

New-ADOrganizationalUnit -Name KeplerUsers -ProtectedFromAccidentalDeletion $false
New-ADGroup -Name KeplerUsersAllir -Path "OU=KeplerUsers,DC=$domainname,DC=local" -GroupScope Global
#Bý til möppuna
new-item $path\SharedFolder -ItemType Directory
 
#sæki núverandi réttindi
$rettindi = Get-Acl -Path $path\SharedFolder 
 
#bý til þau réttindi sem ég ætla að bæta við möppuna
$nyrettindi = New-Object System.Security.AccessControl.FileSystemAccessRule "$domainname\KeplerUsersAllir","Modify","Allow"
#Hver á að fá réttindin, hvaða réttindi á viðkomandi að fá, erum við að leyfa eða banna (allow eða deny)
 
#bæti nýju réttindunum við þau sem ég sótti áðan
$rettindi.AddAccessRule($nyrettindi)
 
#Set réttindin aftur á möppuna
Set-Acl -Path $path\SharedFolder $rettindi
 
#Share-a möppunni
New-SmbShare -Name SharedFolder -Path $path\SharedFolder -FullAccess $domainname\KeplerUsersAllir, administrators 

Add-PrinterDriver -Name "Brother Color Type3 Class Driver"
Add-Printer -Name "SharedFolder Printer2" -Location "SharedFolder" -Shared -PortName LPT1: -Drivername "Brother Color Type3 Class Driver" -Published

$csv = "C:\Users\Administrator\OneDrive\tskoli2016  seinni\Lokaverkefni\Server"
$KeplerUsers = Import-Csv $csv\notendur_csv.csv

foreach($n in $KeplerUsers)


{$getdomain = Get-ADDomain
   $domainname  = $getdomain.Name
    $depname = $n.dep_name

    if((Get-ADOrganizationalUnit -Filter { name -eq $depname }).Name -ne $depname)
    {

        New-ADOrganizationalUnit -Name $n.dep_name -Path "OU=KeplerUsers,DC=$domainname,DC=local" -ProtectedFromAccidentalDeletion $false
        New-ADGroup -Name $depname -Path $("OU=" + $depname + ",OU=KeplerUsers,DC=$domainname,DC=local") -GroupScope Global
        Add-ADGroupMember -Identity KeplerUsersAllir -Members $depname

        #Bý til möppuna
        new-item $path\$depname -ItemType Directory
 
        #sæki núverandi réttindi
        $rettindi = Get-Acl -Path $path\$depname
 
        #bý til þau réttindi sem ég ætla að bæta við möppuna
        $nyrettindi = New-Object System.Security.AccessControl.FileSystemAccessRule $domainname\$depname,"Modify","Allow"
        #Hver á að fá réttindin, hvaða réttindi á viðkomandi að fá, erum við að leyfa eða banna (allow eða deny)
 
        #bæti nýju réttindunum við þau sem ég sótti áðan
        $rettindi.AddAccessRule($nyrettindi)
 
        #Set réttindin aftur á möppuna
        Set-Acl -Path $path\$depname $rettindi
 
        #Share-a möppunni
        New-SmbShare -Name $depname -Path $path\$depname -FullAccess $domainname\$depname, administrators 

        Add-Printer -Name $($depname + " Printer") -Location $depname -Shared -PortName LPT1: -Drivername "Brother Color Type3 Class Driver" -Published

        
    }
    New-ADUser -Name $name -DisplayName $name -GivenName $n.firstname -Surname $n.lastname -SamAccountName $n.username -UserPrincipalName $($n.username + "@"+$domainname+".Local") -Path $("OU=" + $depname + ",OU=KeplerUsers,DC=$domainname,DC=local") -AccountPassword (ConvertTo-SecureString -AsPlainText "pass.123" -Force) -Enabled $true -Title $n.title -EmployeeID $n.user_id -Department $n.dep_name -EmailAddress $n.email
    Add-ADGroupMember -Identity $depname -Members $n.username
    Set-AdUser -Identity $n.username -add @{userJoined = $n.joined}
    if($n.loggedin -eq 1)
        {$n.loggedin = $true}
        else{$n.loggedin = $false}
    Set-AdUser -Identity $n.username -add @{userLoggedin = $n.loggedin}
    Set-AdUser -Identity $n.username -add @{userKeplerAccessLvL = $n.access_level}
}



}

Add-Type -Path "C:\Users\Administrator\Desktop\MySql.Data.dll"

$Server = "tsuts.tskoli.is";
$database = "3010943379_kepler_games";
$uid = "3010943379";
$tsutspass = "mypassword";
$Connstring = "Server=" + $Server + ";" + "DATABASE=" + $database + ";" + "UID=" + $uid + ";" + "PASSWORD=" + $tsutspass + ";";

Try {
$connection = New-Object Mysql.Data.MySqlClient.MySqlConnection
$connection.connectionstring = $Connstring 
$connection.Open()

}
catch {
     Write-Host "ERROR : Unable to run query : $query 'n$Error[0]"
     }

     Finally{
     $connection.close()
     }
run-MySQLquery -ConnectionString $connstring -Query "SELECT * FROM Users"
Function Run-MySQLQuery { ## Function til að auðvelda MySql lífið

	Param(
        [Parameter(
            Mandatory = $true,
            ParameterSetName = '',
            ValueFromPipeline = $true)]
            [string]$query,   
		[Parameter(
            Mandatory = $true,
            ParameterSetName = '',
            ValueFromPipeline = $true)]
            [string]$connectionString
        )
	Begin {
		Write-Verbose "Starting Begin Section"		
    }
	Process {
		Write-Verbose "Starting Process Section"
		try {
			# load MySQL driver and create connection
			Write-Verbose "Create Database Connection"
			# You could also could use a direct Link to the DLL File
			# $mySQLDataDLL = "C:\scripts\mysql\MySQL.Data.dll"
			# [void][system.reflection.Assembly]::LoadFrom($mySQLDataDLL)
			[void][System.Reflection.Assembly]::LoadWithPartialName("MySql.Data")
			$connection = New-Object MySql.Data.MySqlClient.MySqlConnection
			$connection.ConnectionString = $ConnectionString
			Write-Verbose "Open Database Connection"
			$connection.Open()
			
			# Run MySQL Querys
			Write-Verbose "Run MySQL Querys"
			$command = New-Object MySql.Data.MySqlClient.MySqlCommand($query, $connection)
			$dataAdapter = New-Object MySql.Data.MySqlClient.MySqlDataAdapter($command)
			$dataSet = New-Object System.Data.DataSet
			$recordCount = $dataAdapter.Fill($dataSet, "data")
			$dataSet.Tables["data"] | Format-Table
		}		
		catch {
			Write-Host "Could not run MySQL Query" $Error[0]	
		}	
		Finally {
			Write-Verbose "Close Connection"
			$connection.Close()
		}
    }
	End {
		Write-Verbose "Starting End Section"
	}
}

function bidjaumnotenda{
        param(
        [Parameter(Mandatory=$true, HelpMessage="What table do you want to search? (1/3)")]
        [string]$table,
        [Parameter(Mandatory=$true, HelpMessage="After what column do you want to search? (2/3)")]
        [string]$column,
        [Parameter(Mandatory=$true, HelpMessage="Like?(3/3)")] 
        $like
        )

        Run-MySQLQuery -connectionString $connstring -query "SELECT * FROM $table WHERE $column LIKE '%$like%'"
}
Get-ADUser samaccountname -

$Error.clear()
try
{

foreach($n in $KeplerUsers)
{
$username = $n.username
Run-MySQLQuery -connectionString $connstring -query "Update Users set joined = '2016-11-21 16:00:00' WHERE Username = '$username'" 
}
}catch{$Error}