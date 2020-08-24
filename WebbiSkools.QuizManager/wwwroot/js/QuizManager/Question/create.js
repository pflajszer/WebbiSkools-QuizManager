

$(document).ready(function () {
	$('#Question_Answers_0__IsCorrect').prop("checked", true);
});

$('#removeAnswerBtn').on('click', function () {
	if (answerIndex > minimumAnswerIndex) {
		$('#Question_Answers_' + answerIndex + '__Text').val('');
		$('#Question_Answers_' + answerIndex + '__IsCorrect').prop("checked", false);
		$('#answer-' + answerIndex).slideUp(500);
		answerIndex--;
	} else {
		$('#addAnswerMsgBox')[0].innerText = "The minimum number of answers is 3";
		$('#addAnswerMsgBox').show();
	}
});