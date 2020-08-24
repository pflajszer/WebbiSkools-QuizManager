
$(document).ready(function () {
	hideExcessAnswers();
});

var answerCount = $('.answerCount')[0]?.id;
if (answerCount) {
	answerCount--;
} else {
	answerCount = 2;
}

var minimumAnswerIndex = 2;
var maximumAnswerIndex = 4;

var answerIndex = answerCount;
var newAnswersIndex = 0;

$('#addAnswerBtn').on('click', function () {
	if (answerIndex < maximumAnswerIndex) {
		$('#answer-' + (answerIndex + 1)).slideDown(500);
		answerIndex++;
		if (answerIndex > answerCount+1) {
			newAnswersIndex++;
		}
	} else {
		$('#addAnswerMsgBox')[0].innerText = "The maximum number of answers is 5";
		$('#addAnswerMsgBox').show();
	}
});



function hideExcessAnswers() {
	for (var i = answerCount + 1; i <= maximumAnswerIndex; i++) {
		$('#answer-' + i).hide();
	}
}


function selectOnlyThis(id) {
	var myCheckbox = document.getElementsByClassName("isCorrectCheckbox");
	Array.prototype.forEach.call(myCheckbox, function (el) {
		el.checked = false;
	});
	id.checked = true;
}