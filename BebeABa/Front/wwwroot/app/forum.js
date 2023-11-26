if (typeof (Forum) == "undefined") {
	Forum = {};
}

Forum.URL_CreatePost = `${App.RootLocationURL}/Forum/CreatePost`;
Forum.URL_CreateAnswer = `${App.RootLocationURL}/Forum/CreateAnswer`;
Forum.URL_DeleteMainForum = `${App.RootLocationURL}/Forum/DeleteMainForum`;
Forum.URL_DeleteAnswer = `${App.RootLocationURL}/Forum/DeleteAnswer`;


Forum.MainTopicId = null;

Forum.Init = function () {
    Forum.CreateNewPost();
    Forum.AnswerOnClick();
    Forum.DeleteMainForum();
    Forum.DeleteAnswer();
}

Forum.CreateNewPost = function () {
	console.log("test")
	$(document).on("submit", "#topic-form", function (e) {
		e.preventDefault();
		var mainForum = {
			MainForumTitle: $("#topic-title").val(),
			MainForumMessage: $("#topic-content").val(),
			MainForumDate: moment(new Date()).format("YYYY-MM-DDTHH:mm:ss"),
			UserId: App.User.userId
		}

		$.post(Forum.URL_CreatePost, mainForum)
			.done(function (result) {
				if (result.success) {
                    App.ToastSuccess(App.SaveSuccessfulMsg);
                    console.log(result.data.mainForumId)
                    Forum.AddManualPost(result.data.mainForumId);
				}
				else { App.ToastError(App.ErrorMsg); }
			});
       
	});
}
Forum.AddManualPost = function (id) {
    var title = $("#topic-title").val();
    var content = $("#topic-content").val();


    var topicContainer = document.getElementById("topics-list");

    var topicDiv = document.createElement("div");
    topicDiv.className = "topic";
    topicDiv.setAttribute("data-id", id);

    var userInfoDiv = document.createElement("div");
    userInfoDiv.className = "user-info";

    var userImageElement = document.createElement("img");
    userImageElement.src = `/img/imgProjetoInt/User/${App.User.userFilePath}`;
    userImageElement.alt = "Imagem do Usuário";

    var userNameElement = document.createElement("span");
    userNameElement.textContent = App.User.userFullName;

    userInfoDiv.appendChild(userImageElement);
    userInfoDiv.appendChild(userNameElement);

    var topicContent = document.createElement("div");
    topicContent.innerHTML = `<h2>${title}</h2><p>${content}</p>`;

    var metadata = document.createElement("div");
    metadata.className = "metadata";
    metadata.textContent = Forum.getCurrentDateTime();

    var actions = document.createElement("div");
    actions.className = "actions";

    var deleteButton = document.createElement("button");
    deleteButton.className = "delete-button";
    deleteButton.textContent = "Excluir";
    deleteButton.setAttribute("data-id", id);
    deleteButton.setAttribute("name", "mainForum");
    deleteButton.onclick = function () {
        Forum.deleteElement(topicDiv);
    };

    var replyButton = document.createElement("button");
    replyButton.className = "reply-button";
    replyButton.textContent = "Responder";
    replyButton.setAttribute("data-id", id);
   

    replyButton.onclick = function () {
        Forum.replyToTopic(topicDiv, false);
    };

    actions.appendChild(deleteButton);
    actions.appendChild(replyButton);

    topicDiv.appendChild(userInfoDiv);
    topicDiv.appendChild(topicContent);
    topicDiv.appendChild(metadata);
    topicDiv.appendChild(actions);

    topicContainer.insertBefore(topicDiv, topicContainer.firstChild);

    document.getElementById("topic-title").value = "";
    document.getElementById("topic-content").value = "";
}
Forum.replyToTopic = function (topicDiv, isResponse, answer, forumAnswerId) {

    console.log("resposta" +answer);

    if (answer !== null) {
        var responseDiv = document.createElement("div");
        responseDiv.className = "response";

        var userInfoDiv = document.createElement("div");
        userInfoDiv.className = "user-info";

        var userImageElement = document.createElement("img");
        userImageElement.src = "/img/imgProjetoInt/User/" + App.User.userFilePath;
        userImageElement.alt = "Imagem do Usuário";

        var userNameElement = document.createElement("span");
        userNameElement.textContent = App.User.userFullName;

        userInfoDiv.appendChild(userImageElement);
        userInfoDiv.appendChild(userNameElement);

        var responseContentElement = document.createElement("div");
        responseContentElement.innerHTML = `<h3>${isResponse ? "Resposta" : "Comentário"}</h3><p>${answer}</p>`;

        var metadata = document.createElement("div");
        metadata.className = "metadata";
        metadata.textContent = Forum.getCurrentDateTime();

        var actions = document.createElement("div");
        actions.className = "actions";

        var deleteButton = document.createElement("button");
        deleteButton.className = "delete-button";
        deleteButton.textContent = "Excluir";
        deleteButton.setAttribute("data-id", forumAnswerId);
        deleteButton.setAttribute("name", "answer");

        actions.appendChild(deleteButton);

        responseDiv.appendChild(userInfoDiv);
        responseDiv.appendChild(responseContentElement);
        responseDiv.appendChild(metadata);
        responseDiv.appendChild(actions);

        topicDiv.appendChild(responseDiv);
    }
}
Forum.deleteElement = function (element) {
    var result = confirm("Deseja realmente excluir este conteúdo?");
    if (result) {
        element.parentNode.removeChild(element);
    }
}
Forum.getCurrentDateTime = function () {
    var currentDate = new Date();
    var options = { weekday: 'long', year: 'numeric', month: 'long', day: 'numeric', hour: 'numeric', minute: 'numeric', second: 'numeric', timeZoneName: 'short' };
    return currentDate.toLocaleDateString('pt-BR', options);
}


Forum.AnswerOnClick = function () {
    $(".reply-button").on("click", function () {

        $(".response-input").remove();
        Forum.MainTopicId = null;

        Forum.MainTopicId = $(this).attr("data-id");

        var responseInput = $("<div class='response-input' data-id='" + Forum.MainTopicId + "'><textarea class='text-area-response' id='topic-content' placeholder='Digite sua resposta' ></textarea><button class='reply-button' onClick='Forum.OnSubmitAnswer();'>Enviar</button></div>");

        $(this).closest(".topic").find(".answer-hide-div").after(responseInput);
        $(".send-button").on("click", function () {

            var resposta = $(this).siblings("textarea").val();
            
            $(this).closest(".topic").find(".response-input[data-id='" + Forum.MainTopicId + "']").remove();

        });
    });
}

Forum.OnSubmitAnswer = function () {
    var respostaInput = $(".response-input textarea");

    var answer = respostaInput.val();
    var mainForumId = respostaInput.closest(".response-input").attr("data-id");
    if (answer.length > 0) {
        var forumAnswerModel = {
            ForumAnswerId: 0,
            ForumAnswer1: answer,
            ForumAnswerDate: moment(new Date()).format("YYYY-MM-DDTHH:mm:ss"),
            UserId: App.User.userId,
            MainForumId: mainForumId
    
        }

        $.post(Forum.URL_CreateAnswer, forumAnswerModel)
            .done(function (result) {
                if (result.success) {
                    App.ToastSuccess(App.SaveSuccessfulMsg);
                    var topicDiv = $(".topic[data-id='" + mainForumId + "']")[0];
                    Forum.replyToTopic(topicDiv, true, answer, result.data.forumAnswerId);
                }
                else { App.ToastError(App.ErrorMsg); }
            });
        respostaInput.closest(".response-input").remove();
    } else {
        respostaInput.addClass("error");
        respostaInput.attr("placeholder", "Por favor, digite uma resposta válida");
    }
};

Forum.DeleteMainForum = function () {
    $(document).on("click", "[name='mainForum']", function () {

        const mainfForumId = $(this).attr("data-id");

        $.post(Forum.URL_DeleteMainForum, { id: mainfForumId }, function (result) {
            if (result.success) {
                App.ToastSuccess("Forum Deletado corretamente.");
                window.location = `${App.RootLocationURL}/Forum/Index`;
            } else {
                App.ToastError(App.ErrorMsg);
            }
        })
    });
}

Forum.DeleteAnswer = function () {
    $(document).on("click", "[name='answer']", function () {

        const answerId = $(this).attr("data-id");

        $.post(Forum.URL_DeleteAnswer, { id: answerId }, function (result) {
            if (result.success) {
                App.ToastSuccess("Forum Deletado corretamente.");
                window.location = `${App.RootLocationURL}/Forum/Index`;
            } else {
                App.ToastError(App.ErrorMsg);
            }
        })
    });
}

