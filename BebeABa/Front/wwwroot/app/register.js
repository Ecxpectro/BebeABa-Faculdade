if (typeof (Register) == "undefined") {
    Register = {};
}

Register.URL_CreateUser = `${App.RootLocationURL}/Register/SaveUser`;

Register.Init = function () {
	Register.ValidadeUserCreate();
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
			//window.location = `${App.RootLocationURL}/Home`;
		}
		else {
			App.ToastError(result.message);
			App.HideLoadingModal();
		}
	});
}
