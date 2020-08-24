$(document).ready(function () {
	$('#updateForm').on('submit', function (evt) {
		evt.preventDefault();
		$.post('?handler=Update', $('#updateForm').serialize(), function (result) {
			var alertBox = $('#alertBox');
			alertBox.show(1000);
			alertBox[0].innerHTML = result.addMessage + '<br/>' + result.removeMessage;
			$("#membershipTable").load('?handler=RefreshMembershipTable&id=' + result.roleId, function () {
				//callback
			});
		});
	});
});