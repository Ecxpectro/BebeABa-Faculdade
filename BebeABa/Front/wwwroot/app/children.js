if (typeof (Children) == "undefined") {
	Children = {};
}
Children.URL_Save = `${App.RootLocationURL}/Children/Save`;
Children.URL_SaveTimeLine = `${App.RootLocationURL}/Children/SaveTimeLine`;
Children.URL_GridTimeLine = `${App.RootLocationURL}/Children/GridTimeLine`;

Children.Children = null;
Children.ChildrenId = null;

Children.Init = function () {
	Children.DataPicker();
	Children.SaveChildren();
	
	Children.SaveTimeLine();
	console.log(Children.ChildrenId);
}
Children.InitTimeLine = function(){
	Children.GridChildrenTimeLine();
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

Children.SaveChildren = function () {
	$(document).on("submit", "#formRegister", function (e) {
		e.preventDefault();
		App.ShowLoadingModal();
		var selectedDate = $(".datePicker").datepicker('getDate');
		var day = selectedDate.getDate();
		var month = selectedDate.getMonth() + 1;
		var year = selectedDate.getFullYear();
		var date = moment(`${year}/${month.toString().padStart(2, '0')}/${day}`).format("YYYY-MM-DDTHH:mm:ss");

		const children = {
			ChildrenId: 0,
			UserId: App.User.userId,
			ChildrenName: $("#childrenName").val(),
			ChildrenFatherName: $("#fatherName").val(),
			ChildrenMotherName: $("#motherName").val(),
			BirthDate: date,
			ChildSex: $("#childSex").val()
		}
		console.log(children)
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
					window.location.href = '/Children/ChildrenProfile';
				} else {
					App.ToastError(result.msg);
					App.HideLoadingModal();
				}
				return true;
			}
		});
	});

}

Children.ShowModalChildrenTimeLine = function (children) {

	if (children != null) {
		Children.Children = children;
		$("#weight").val(children.weight);
		$("#height").val(children.height);
		$("#vaccine").val(children.vaccine);
		$("#date-picker").val(children.timeLineDate);
		$("#description").val(children.description);
		$("#treatmentType").val(children.treatmentType);

	}

	$("#modalTimeLine").modal("show");
}

Children.GridChildrenTimeLine = function () {
	console.log(Children.ChildrenId);
	$("#GridChildrenTimeLine").DataTable({
		responsive: true,
		"destroy": true,
		"scrollX": false,
		"searching": false,
		"processing": true,
		"serverSide": true,
		"ajax": {
			"url": Children.URL_GridTimeLine,
			"data": { childrenId: Children.ChildrenId },
			"type": "POST",
			"datatype": "json"
		},
		"columnDefs": [
			{
				"targets": "_all",
				"className": "text-center"
			}
		],
		"columns": [
			{ "data": "childAge", "name": "childAge", "autoWidth": true },
			{ "data": "weight", "name": "weight", "autoWidth": true },
			{ "data": "height", "name": "height", "autoWidth": true },
			{ "data": "vaccine", "name": "vaccine", "autoWidth": true },
			{ "data": "treatmentType", "name": "treatmentType", "autoWidth": true },
			{
				"data": "timeLineDate",
				"name": "timeLineDate",
				"autoWidth": true,
				"render": function (data) {
					var formattedDate = moment(data).format('DD/MM/YYYY');
					return formattedDate;
				}
			},
			{ "data": "description", "name": "description", "autoWidth": true },
			{
				"render": function (data, type, full, meta) {
					var render =
						"<div class='custom-actions'>" +
						"<a class='pointer' onclick='Children.ShowModalChildrenTimeLine(" + JSON.stringify(full).toString() + ");'> <i class='fa fa-edit' style='color: #3AB6FF;' title='Editar'></i> </a>" +
						`<a href='../DownloadFile?fileName=${full.filePath}' class='pointer'> <i class='fa fa-download' style="color: #3AB6FF;" title='Baixar'></i> </a>` +
						"</div>";
					return render;
				}
			}
		],
	});
}



Children.SaveTimeLine = function () {
	$(document).on("submit", "#formRegisterTimeLine", function (e) {
		e.preventDefault();
		App.ShowLoadingModal();
		var selectedDate = $(".datePicker").datepicker('getDate');
		var day = selectedDate.getDate();
		var month = selectedDate.getMonth() + 1;
		var year = selectedDate.getFullYear();
		var date = moment(`${year}/${month.toString().padStart(2, '0')}/${day}`).format("YYYY-MM-DDTHH:mm:ss");

		const childrenTimeLine = {
			ChildrenTimeLineId: Children.Children == null ? 0 : Children.Children.childrenTimeLineId,
			ChildrenId: 7,
			Height: $("#height").val(),
			Weight: $("#weight").val(),
			Vaccine: $("#vaccine").val(),
			TreatmentType: $("#treatmentType").val(),
			Description: $("#description").val(),
			TimeLineDate: date,
			filePath: Children?.Children?.filePath
		}
		var formData = new FormData();
		var childrenTimeLineJson = JSON.stringify(childrenTimeLine);
		var file = $("#file")[0].files[0];

		formData.append("ChildrenTimeLineJson", childrenTimeLineJson);
		formData.append("File", file);

		$.ajax({
			type: "POST",
			url: Children.URL_SaveTimeLine,
			data: formData,
			cache: false,
			contentType: false,
			processData: false,
			success: function (result) {
				if (result.success) {
					console.log(result);
					App.HideLoadingModal();
					$("#modalTimeLine").modal("hide");
				} else {
					App.ToastError(result.msg);
					App.HideLoadingModal();
				}
				return true;
			}
		});
	});

}
