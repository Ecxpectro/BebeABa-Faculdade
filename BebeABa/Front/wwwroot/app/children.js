if (typeof (Children) == "undefined") {
	Children = {};
}
Children.URL_Save = `${App.RootLocationURL}/Children/Save`;
Children.Init = function () {
	Children.DataPicker();
	Children.Save();
}

Children.DataPicker = function () {
	$.datepicker.regional['pt'] = {
		monthNames: ['Janeiro', 'Fevereiro', 'Março', 'Abril', 'Maio', 'Junho', 'Julho', 'Agosto', 'Setembro', 'Outubro', 'Novembro', 'Dezembro'],
		monthNamesShort: ['Jan', 'Fev', 'Mar', 'Abr', 'Mai', 'Jun', 'Jul', 'Ago', 'Set', 'Out', 'Nov', 'Dez'],
		dateFormat: 'dd/mm/yy',
		closeText: "Pronto",
		currentText: "Data de hoje"
	};

	$.datepicker.setDefaults($.datepicker.regional['pt']);

	$(".datePicker").datepicker({
		regional: "pt",
		showButtonPanel: true,
		changeMonth: true,
		changeYear: true,
		dateFormat: 'dd/mm/yy',
		yearRange: (2000) + ':' + new Date().getFullYear(),
		onClose: function (dateText, inst) {
			if ($(this).datepicker('getDate') != null) {
				var selectedDate = $(this).datepicker('getDate');
				var day = selectedDate.getDate();
				var month = selectedDate.getMonth() + 1;
				var year = selectedDate.getFullYear();

				$(this).val((day < 10 ? "0" + day : day) + '/' + (month < 10 ? "0" + month : month) + '/' + year);
			}
		}
	});
	$(".datePicker").focus(function () {

		$("#ui-datepicker-div").position({
			my: "center top",
			at: "center bottom",
			of: $(this)
		});
	});
}

Children.Save = function () {
	$(document).on("submit", "#formRegister", function (e) {
		e.preventDefault();
		App.ShowLoadingModal();
		var selectedDate = $(".datePicker").datepicker('getDate');
		var day = selectedDate.getDate();
		var month = selectedDate.getMonth() + 1;
		var year = selectedDate.getFullYear();
		var date = moment(`${year}/${month.toString().padStart(2, '0')}/${day}`).format("YYYY-MM-DDTHH:mm:ss");

		const children = {
			ChildrenId : 0,
			UserId : App.User.userId,
			ChildrenName : $("#childrenName").val(),
			ChildrenFatherName : $("#fatherName").val(),
			ChildrenMotherName :  $("#motherName").val(),
			BirthDate : date
		}
		var formData = new FormData();
		var childrenJson = JSON.stringify(children);
		var file = $("#childrenPicture")[0].files[0];

		formData.append("ChildrenJson", childrenJson);
		formData.append("File", file);

		$.ajax({
			type: "POST",
			url: Children.URL_Save,
			data: formData,
			cache: false,
			contentType: false,
			processData: false,
			success: function (result) {
				if (result.success) {
					console.log(result);
					App.HideLoadingModal();
				} else {
					App.ToastError(result.msg);
					App.HideLoadingModal();
				}
				return true;
			}
		});
	});

}