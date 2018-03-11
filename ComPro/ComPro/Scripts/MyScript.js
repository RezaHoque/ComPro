$(document).ready(function () {
    $('#Tridha').click(function () {
        $("p").hide();
    });

    $('#Rubel').click(function () {
        alert('Please wait while form is submitting');
        $("p").show();
    });

    
        $("#message").fadeIn(3000);
    
    
        $("#dialog").dialog({
            autoOpen: false,
            show: {
                effect: "blind",
                duration: 1000
            },
            hide: {
                effect: "explode",
                duration: 1000
            }
        });

        $("#btnpop").on("click", function () {
            $("#dialog").dialog("open");
        });
    
       
       
});
$("#message").fadeIn(3000);
function foo(message) {
    //alert(message);
    $("#message").fadeIn(3000);
}

