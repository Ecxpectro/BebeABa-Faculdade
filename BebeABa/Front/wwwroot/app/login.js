if (typeof (Login) == "undefined") {
	Login = {};
}

Login.URL_Login = `${App.RootLocationURL}/Login/Login`;

Login.Init = function () {
	Login.Login();
	Login.RememberMe();
	Login.RegisterMe();

	if (App.User != null && App.User != undefined) {
		if (App.User.checkRememberMe) {
			$("#checkRememberMe").attr("checked", "");
			$("#checkRememberMe").prop("checked", true);
			$("#username").val(App.User.userEmail);
			$("#password").val(App.User.userPassword);

			Login.User = {
				UserEmail: App.User.userEmail,
				UserPassword: App.User.userPassword
			}

			$("#formLogin").submit();
		} else {
			$("#checkRememberMe").removeAttr("checked");
			$("#checkRememberMe").prop("checked", false);
		}
	}

}

Login.Login = function () {
	$(document).on("submit", "#formLogin", function (e) {
		e.preventDefault();
		console.log("test")
		var user;
		user = {
			UserEmail: $("#username").val(),
			UserPassword: $("#password").val()
		}
		user.checkRememberMe = $("#checkRememberMe").attr("checked") != undefined ? true : false;

		$.post(Login.URL_Login, user, function (result) {
			if (result.success) {
				console.log(result.user)
				if (result.user.isDoctor) {
					window.location.href = '/Forum/Index'
				}
				else {
					if (result.user.childrens == null) {
						window.location.href = '/Children/RegisterChild'
					}
					else {
						window.location.href = '/Children/ChildrenProfile'
					}
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

Login.RememberMe = function () {
	$(document).on("click", "#checkRememberMe", function () {
		if ($(this).attr("checked") != undefined) {
			$(this).removeAttr("checked");
			$(this).prop("checked", false);
		} else {
			$(this).attr("checked", "");
			$(this).prop("checked", true);
		}
	});
}

Login.RegisterMe = function () {
	
	$(document).on("click", "#registerMeButton", function () {
		console.log("test")
		window.location.href = '/Register/Index'
	});
}