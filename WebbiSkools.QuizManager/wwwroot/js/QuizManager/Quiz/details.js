
function toggleAnswers(id) {
	var fullId = '#answers-' + id;
	if ($(fullId + ':visible').length == 0) {
		$(fullId).slideDown(500);
		$('#showHideBtn-' + id)[0].innerText = "Hide Answers";
	} else {
		$(fullId).slideUp(500);
		$('#showHideBtn-' + id)[0].innerText = "View Answers";
	}
}

