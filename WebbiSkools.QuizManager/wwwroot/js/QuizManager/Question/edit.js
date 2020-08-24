


$('#removeAnswerBtn').on('click', function () {
	if (answerIndex > minimumAnswerIndex) {
		$('#Question_Answers_' + answerIndex + '__Text').val('');
		$('#Question_Answers_' + answerIndex + '__IsCorrect').prop("checked", false);
		if (answerIndex > answerCount) {
			$('#NewAnswers_' + newAnswersIndex + '__Text').val('');
			$('#NewAnswers_' + newAnswersIndex + '__IsCorrect').prop("checked", false);
			if (newAnswersIndex > 0) {
				newAnswersIndex--;
			}
		}
		$('#answer-' + answerIndex).slideUp(500);
		answerIndex--;
	} else {
		$('#addAnswerMsgBox')[0].innerText = "The minimum number of answers is 3";
		$('#addAnswerMsgBox').show();
	}
});