<?php
session_start();
include_once 'dbcon.php';
?>
<!DOCTYPE HTML>
<html>
	<head>
		<title>Kepler Games</title>
		<meta charset="utf-8" />
		<meta name="viewport" content="width=device-width, initial-scale=1" />
		<!--[if lte IE 8]><script src="assets/js/ie/html5shiv.js"></script><![endif]-->
		<link rel="stylesheet" href="assets/css/main.css" />
		<!--[if lte IE 8]><link rel="stylesheet" href="assets/css/ie8.css" /><![endif]-->
		<!--[if lte IE 9]><link rel="stylesheet" href="assets/css/ie9.css" /><![endif]-->
		<link rel="shortcut icon" type="image/png" href="images/favicon.png" class="fa-gamepad fa-spin"/>
		
	</head>
	<body class="landing">

		<!-- Header -->
			<header id="header" class="">
				<h1><a href="index.php">Kepler Games</a></h1>
				<a class="fixed" href="#nav">Menu</a>
				<div class="session">
				<?php
				if(isset($_SESSION['user_name']) == true)
				{
				echo '<a href="Profile.php">'.$_SESSION['user_name'].'</a>';
				}

				?>
				</div>
				
			</header>

		<!-- Nav -->
			<nav id="nav">
				<ul class="links">
					<li><a href="index.php"><i class="fa fa-home"></i> Home</a></li>
					<li><a href="games.php"> <i class="fa fa-gamepad"></i> Games</a></li>
					<?php 
						if (isset($_SESSION['user_name']))
						{
							echo '<li><a href="Profile.php"><i class="fa fa-user"></i> Profile</a></li>';
						} else {
							echo '<li><a href="login.php"><i class="fa fa-sign-in"></i> Log In </a></li>'; 
						}
					?>
					

					<li><a href="about.php"><i class="fa  fa-certificate fa-spin"></i> About us </a></li>
					<li><a href="Contact.php"><i class="fa fa-comment "> Contact Us </i> </a></li>
				</ul>
			</nav>

		<!-- Banner -->
			<section id="banner">
				<i class="icon fa-gamepad"></i>
				<h2>Kepler Games</h2>
				<p>We Bring Easy Gaming To You</p>
					
			</section>

		<!-- One -->
			<section id="one" class="wrapper style1">
				<div class="inner">
					<article class="feature left">
						
						<div class="content">
							<h2>What Are We</h2>
							<p>Well if you like to play games, come visit us and we will set you up with a nice gaming enviroment where you can play for endless hours, and if you want to read about games and what other people think then check out our webpage and look at the comments, Do you have something to say? Become a reviewer for Kepler Games</p>
							<ul class="actions">
								<li>
									<a href="comments.php" class="button alt">View</a>
								</li>
							</ul>
						</div>
						<span class="image"><img src="images/kg.png" alt="" /></span>
					</article>

					<article class="feature left">
						<span class="image"><img src="images/coder.jpg" alt="" /></span>
						<div class="content">
							<h2>Are you a programmer?</h2>
							<p>Come Join the amazing society of Kepler Games, Can you create the world's most popular game? Do want to join one of the most finest gaming community of all times? Well if you got what it takes you are hired!</p>
							<ul class="actions">
								<li>
									<a href="Come join us.php" class="button alt">More</a>

								</li>
							</ul>
							
						
						</div>
					</article>

				</div>
			</section>

		<!-- Two -->
			<div class="adspace">

			<div class="auglysing">
			<h5 class="exitads">Close<i class="fa fa-times fa-times fa-2x"></i></h5>
			<img class="auglysingmynd" src="images/ad1.jpg"><a class="adlink" href="tskoli.is">Click here to view more</a>
			</div>
			</div>
			

		<!-- Three -->
			<section id="three" class="wrapper style3 special">
			<form method="post" action="junkmail.php">
				<div class="inner">
					<header class="major narrow	">
					<div class="postlisti">
						<h2>Be the first to test new games!</h2>
						<p>Register your email and you could be the first one to test our amazing games before they are released!</p>
						<input class="skrapost" type="text" name="postlisti" placeholder="username@email.com">
						
					
					<ul class="actions">
						<li><input type="submit" value="Skrá mig á póstlista" class="button big alt"></li>
					</ul>
					

				</div>
				</form>
			</section>

		<!-- Four -->
			<section id="four" class="wrapper style2 special">
				<form method="post" action="email.php" >
				<div class="inner">
					<header class="major narrow">
						<h2>Got Any Questions?</h2>
						<p>Don't hesitate to ask, we will answer you as soon as we press pause</p>
					</header>
					 <!---Gátum ekki fengið til að virka á tskolaservernum en gætum látið virka á öðrum serverum sem eru með SMTP -->
						
						<div class="container 75%">
						
							<div class="row uniform 50%">
								<div class="6u 12u$(xsmall)">
									<input name="name" placeholder="Name" type="text" />
								</div>
								<div class="6u$ 12u$(xsmall)">
									<input name="email" placeholder="Email" type="email" />
								</div>
								<div class="12u$">
									<textarea name="message" placeholder="Message" rows="4"></textarea>
								</div>
							</div>
							
						</div>
						<ul>
							 <input type="Submit" id="submit" name="submit" value="Send Email">
							<input type="reset" class="alt" value="Reset" />
						</ul>
						
				</div>
				</form>
			</section>

		<!-- Footer -->
			<footer id="footer">
				<div class="inner">
					<ul class="icons">
						<li><a href="" class="icon fa-facebook">
							<span class="label">Facebook</span>
						</a></li>
						<li><a href="" class="icon fa-twitter">
							<span class="label">Twitter</span>
						</a></li>
						<li><a href="" class="icon fa-instagram">
							<span class="label">Instagram</span>
						</a></li>
						<li><a href="" class="icon fa-linkedin">
							<span class="label">LinkedIn</span>
						</a></li>
					</ul>
					<ul class="copyright">
						<li>&copy; Kepler Games.</li>
						<li>Images: <a href="http://google.com">Google</a>.</li>
						<li>Design: Kepler Games</a>.</li>
					</ul>
				</div>
			</footer>

		

		<!-- Scripts -->
			<script src="assets/js/jquery.min.js"></script>
			<script src="assets/js/skel.min.js"></script>
			<script src="assets/js/util.js"></script>
			<!--[if lte IE 8]><script src="assets/js/ie/respond.min.js"></script><![endif]-->
			<script src="assets/js/newjquery.js"></script>
			<script src="assets/js/main.js"></script>

	</body>
</html>