$(document).ready(function () {

	$('#createRole').click(function () {
		$('#createCard').slideToggle(500);
	})

	$('#createForm').on('submit', function (evt) {
		evt.preventDefault();
		var alertBox = $('#alertBox');
		$.post('?handler=Create', $('#createForm').serialize(), function (result) {
			alertBox.show(1000);
			alertBox[0].innerText = result.message;
			if (result.success) {
				$('#createCard').slideUp(500);
				alertBox[0].innerText = result.message;
				$("#rolesTable").load('?handler=RefreshRoleTable', function () {
					setTimeout(() => alertBox.hide(1000), 5000);
				});
			}
		}).done(function () {
			// done callback
		}).fail(function () {
			// fail callback
		}).always(function () {
			// finally callback
		});
	});
});