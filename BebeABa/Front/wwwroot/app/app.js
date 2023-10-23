if (typeof (App) == "undefined") {
    App = {};
}

App.RootURL = "";
App.RootLocationURL = "";

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