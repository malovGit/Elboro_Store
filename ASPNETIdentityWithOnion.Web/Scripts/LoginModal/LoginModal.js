$(document).ready(function () {
    var loginLink = $("a[id='loginLink']");
    loginLink.attr({ "href": "#", "data-toggle": "modal", "data-target": "#ModalLogin" });

    $("#loginform").submit(function (event) {
        if ($("#loginform").valid()) {
            var username = $("#Email").val();
            var password = $("#Password").val();
            var rememberme = $("#RememberMe").val();
            var antiForgeryToken = ASPNETIdentityWithOnion.Web.Views.Common.getAntiForgeryValue();

            ASPNETIdentityWithOnion.Web.Identity.LoginIntoStd(username, password, rememberme, antiForgeryToken,
                ASPNETIdentityWithOnion.Web.Views.LoginModal.loginSuccess, ASPNETIdentityWithOnion.Web.Views.LoginModal.loginFailure);
        }
        return false;
    });

    $("#ModalLogin").on("hidden.bs.modal", function (e) {
        ASPNETIdentityWithOnion.Web.Views.LoginModal.resetLoginForm();
    });

    //TODO alle Referenzen auf Form controls bezogen auf form, um Doppeldeutigkeiten zu vermeiden.
    $("#ModalLogin").on("shown.bs.modal", function (e) {
        $("#Email").focus();
    });

});

var ASPNETIdentityWithOnion = ASPNETIdentityWithOnion || {};
ASPNETIdentityWithOnion.Web = ASPNETIdentityWithOnion.Web || {};
ASPNETIdentityWithOnion.Web.Views = ASPNETIdentityWithOnion.Web.Views || {}

ASPNETIdentityWithOnion.Web.Views.Common = {
    getAntiForgeryValue: function () {
        return $('input[name="__RequestVerificationToken"]').val();
    }
}

ASPNETIdentityWithOnion.Web.Views.LoginModal = {
    resetLoginForm: function () {
        $("#loginform").get(0).reset();
        $("#alertBox").css("display", "none");
    },

    loginFailure: function (message) {
        var alertBox = $("#alertBox");
        alertBox.html(message);
        alertBox.css("display", "block");
    },

    loginSuccess: function () {
        window.location.href = window.location.href;
    }
}


ASPNETIdentityWithOnion.Web.Identity = {
    LoginIntoStd: function (username, password, rememberme, antiForgeryToken, successCallback, failureCallback) {
        var data = { "__RequestVerificationToken": antiForgeryToken, "username": username, "password": password, "rememberme": rememberme };

        $.ajax({
            url: "/Account/LoginJson",
            type: "POST",
            data: data
        })
        .done(function (loginSuccessful) {
            if (loginSuccessful) {
                successCallback();
            }
            else {
                failureCallback("Invalid login attempt.");
            }
        })
        .fail(function (jqxhr, textStatus, errorThrown) {
            failureCallback(errorThrown);
        });
    }
}
