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
	var formData = new FormData();
	var userJson = JSON.stringify(user);
	var file = $("#userPhoto")[0].files[0];
	console.log(userJson)
	formData.append("UserJson", userJson);
	formData.append("File", file);

	$.ajax({
		type: "POST",
		url: Register.URL_CreateUser,
		data: formData,
		cache: false,
		contentType: false,
		processData: false,
		success: function (result) {
			if (result.success) {
				App.ToastSuccess("Seja bem vindo!");
				if (user.IsDoctor == true) {
					console.log("test");
					window.location = `${App.RootLocationURL}/Forum/Index`;
				}
				else {
					window.location = `${App.RootLocationURL}/Children/RegisterChild`;
				}
			} else {
				App.ToastError(result.msg);
				App.HideLoadingModal();
			}
			return true;
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