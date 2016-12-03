$(document).ready(function() // Þetta er settá viðburðasíðuna til þess að þurfa ekki að vera með 4 undirsíður til að viðhalda viðburðina
{
    $('.btsida1').click(function(){
      $('.btsida1, .btsida2, .btsida3, .btsida4').removeClass("valid");
      $('.btsida1').addClass("valid");
        $('.sida1').removeClass("hidden");
        $('.sida1').addClass("show");
  		$('.sida2 , .sida3, .sida4').addClass("hidden");
           });

    $('.btsida2').click(function(){
      $('.btsida1, .btsida2, .btsida3, .btsida4').removeClass("valid");
      $('.btsida2').addClass("valid");
    	$('.sida1').removeClass("show");
        $('.sida2').removeClass("hidden");
        $('.sida2').addClass("show");
  		$('.sida1 , .sida3, .sida4').addClass("hidden");
           });
        $('.btsida3').click(function(){
          $('.btsida1, .btsida2, .btsida3, .btsida4').removeClass("valid");
          $('.btsida3').addClass("valid");
        $('.sida2').removeClass("show");
        $('.sida3').removeClass("hidden");
        $('.sida3').addClass("show");
  		$('.sida2 , .sida1, .sida4').addClass("hidden");
           });

        $('.btsida4').click(function(){
          $('.btsida1, .btsida2, .btsida3, .btsida4').removeClass("valid");
          $('.btsida4').addClass("valid");
        $('.sida3').removeClass("show");
        $('.sida1 , .sida3, .sida2').addClass("hidden");
        $('.sida4').removeClass("hidden");
        $('.sida4').addClass("show");
  		
           });

        $('#banner').one('mouseout',function() {
          if ($('.adspace').hasClass("once")) {
          }
          else
          {
          $('.adspace').removeClass('adspace').addClass('adshow , once');
          }



        });

        $('.exitads').click(function() {
        $('.adshow').remove();
        });


        $('#nav').one('mouseout',function() {
          if ($('.adspace2').hasClass("once")) {
          }
          else
          {
          $('.adspace2').removeClass('adspace2').addClass('adshow2 , once');
          $('#one').addClass('bakgrunnur');
          }



        });

        $('.exitads2').click(function() {
        $('.adshow2').remove();
        $('#one').removeClass('bakgrunnur');
        });

             $('.fyrrisida').click(function() { 
        if ($('.btsida4').hasClass('valid')) 
          {
          $('.btsida1, .btsida2, .btsida3, .btsida4').removeClass("valid");
          $('.btsida3').addClass("valid");
          $('.sida2').removeClass("show");
          $('.sida3').removeClass("hidden");
          $('.sida3').addClass("show");
          $('.sida2 , .sida1, .sida4').addClass("hidden");
          }
        else if ($('.btsida3').hasClass('valid')) 
          {
            $('.btsida1, .btsida2, .btsida3, .btsida4').removeClass("valid");
            $('.btsida2').addClass("valid");
            $('.sida1').removeClass("show");
            $('.sida2').removeClass("hidden");
            $('.sida2').addClass("show");
            $('.sida1 , .sida3, .sida4').addClass("hidden");
          }
        else if ($('.btsida2').hasClass('valid')) 
          {
            $('.btsida1, .btsida2, .btsida3, .btsida4').removeClass("valid");
            $('.btsida1').addClass("valid");
            $('.sida1').removeClass("hidden");
            $('.sida2').removeClass("hidden");
            $('.sida1').addClass("show");
            $('.sida2 , .sida3, .sida4').addClass("hidden");  
          };

       
           });  

           $('.seinnisida').click(function() {
            if ($('.btsida1').hasClass('valid'))
             {
              $('.btsida1, .btsida2, .btsida3, .btsida4').removeClass("valid");
              $('.btsida2').addClass("valid");
              $('.sida1').removeClass("show");
              $('.sida2').removeClass("hidden");
              $('.sida2').addClass("show");
              $('.sida1 , .sida3, .sida4').addClass("hidden");
             }
             else if ($('.btsida2').hasClass('valid'))
             {
                $('.btsida1, .btsida2, .btsida3, .btsida4').removeClass("valid");
                $('.btsida3').addClass("valid");
                $('.sida2').removeClass("show");
                $('.sida3').removeClass("hidden");
                $('.sida3').addClass("show");
                $('.sida2 , .sida1, .sida4').addClass("hidden");

             }

             else if($('.btsida3').hasClass('valid'))
             {
              $('.btsida1, .btsida2, .btsida3, .btsida4').removeClass("valid");
              $('.btsida4').addClass("valid");
              $('.sida3').removeClass("show");
              $('.sida1 , .sida3, .sida2').addClass("hidden");
              $('.sida4').removeClass("hidden");
              $('.sida4').addClass("show");

             };


           });

$(".fa-users").hover(function() {
$(".fa-users").addClass("fa-spin");
  });
$(".fa-users").mouseout(function() {
$(".fa-users").removeClass("fa-spin");
  });


      
    
    });
