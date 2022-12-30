
$(function () {
   //setup tabs
    $('#navigationTabs a').click(function (e) {
        e.preventDefault();
        $(this).tab('show');
    });

    //Клик по меню "выйти"
    $("#signOutMenuitem").click(function () { window.location.replace(window.location.href + "/Account/SignOut"); });

//    $('#adminTab').click(function (e) {
//        e.preventDefault();
//        $(this).tab('show');
//        $('#secureManagerDiv').addClass('active');
//    });

});




