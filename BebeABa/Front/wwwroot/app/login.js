if (typeof (Login) == "undefined") {
	Login = {};
}

Login.URL_Login = `${App.RootLocationURL}/Login/Login`;

Login.Init = function () {
	Login.Login();
}

Login.Login = function () {
	$(document).on("submit", "#formLogin", function (e) {
		e.preventDefault();
		var user;
		user = {
			UserEmail: $("#username").val(),
			UserPassword: $("#password").val()
		}
		user.checkRememberMe = $("#checkRememberMe").attr("checked") != undefined ? true : false;

		$.post(Login.URL_Login, user, function (result) {
			if (result.success) {
				if (result.user.childrens == null) {
					window.location.href = '/Children/RegisterChild'
				}
				else {
					window.location.href = '/Children/Index'
				}
			
				
			} else {
				if (result.msg != "") {
					App.AlertError(result.msg);
				} else {
					App.AlertWarning("Usuário ou senha incorreto.");
				}
			}
		});
	});
}
