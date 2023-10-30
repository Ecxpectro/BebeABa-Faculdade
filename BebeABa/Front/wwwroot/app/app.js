if (typeof (App) == "undefined") {
    App = {};
}

App.RootURL = "";
App.RootLocationURL = "";
App.User = null;

App.ToastError = function (msg) {
	$.toast({
		heading: "",
		text: msg,
		position: "top-center",
		icon: "error"
	});
}
App.ShowLoadingModal = function () {
	waitingDialog.show(App.LoadingText);
	$(".modal-dialog").css("opacity", "0");
	$(".btn-wsp").css("opacity", "0");
}

App.HideLoadingModal = function () {
	waitingDialog.hide();
	$(".modal-dialog").css("opacity", "1");

	setTimeout(function () {
		$(".btn-wsp").css("opacity", "1");
	}, 100);
}
App.ToastSuccess = function (msg) {
	$.toast({
		heading: "",
		text: msg,
		position: "top-center",
		icon: "success"
	});
};

App.AlertSuccess = function (msg) {
	swal({
		title: "",
		text: msg,
		type: "success",
		showCancelButton: false,
		showConfirmButton: true,
		confirmButtonClass: "btn-sm btn-success",
		confirmButtonText: "OK",
		closeOnConfirm: true
	});
};

App.AlertWarning = function (msg) {
	swal({
		title: "",
		text: msg,
		type: "warning",
		showCancelButton: false,
		showConfirmButton: true,
		confirmButtonClass: "btn-sm btn-warning",
		confirmButtonText: "OK",
		closeOnConfirm: true
	});
};
App.AlertError = function (msg) {
	swal({
		title: "",
		text: msg,
		type: "error",
		showCancelButton: false,
		showConfirmButton: true,
		confirmButtonClass: "btn-sm btn-danger",
		confirmButtonText: "OK",
		closeOnConfirm: true
	});
};