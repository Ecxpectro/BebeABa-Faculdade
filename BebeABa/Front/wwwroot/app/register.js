if (typeof (Register) == "undefined") {
    Register = {};
}

Register.URL_CreateUser = `${App.RootLocationURL}/Register/SaveUser`;

Register.Init = function () {
	Register.ValidadeUserCreate();
	Register.Switcher();
}

Register.ValidadeUserCreate = function () {
	$(document).on("submit", "#formRegister", function (e) {
		e.preventDefault();

		var password = $("#registerPassword");
		var confirmPassword = $("#registerConfirmPassword");
		if (password.val() != confirmPassword.val()) {
			App.ToastError("As senhas devem ser as mesmas.");
		}
		else {
			const user = {
				UserEmail: $("#email").val(),
				UserFullName: $("#username").val(),
				UserPassword: $("#registerPassword").val(),
				IsDoctor: $("#isDoctor").attr("checked") != undefined
			}

			Register.Register(user);
		}
	});
}

Register.Register = function (user) {
	App.ShowLoadingModal();
	$.post(Register.URL_CreateUser, user).done(function (result) {
		if (result.success) {
			App.ToastSuccess("Seja bem vindo!");
			if (user.IsDoctor == true) {
				console.log("test")
			}
			else {
				window.location = `${App.RootLocationURL}/Children/RegisterChild`;
			}
			
		}
		else {
			App.ToastError(result.message);
			App.HideLoadingModal();
		}
	});
}

Register.Switcher = function () {
	$(document).on("click", "#isDoctor", function () {
		if ($(this).attr("checked") != undefined) {
			$(this).removeAttr("checked");
			$(this).prop("checked", false);
		} else {
			$(this).attr("checked", "");
			$(this).prop("checked", true);
		}
	});
}