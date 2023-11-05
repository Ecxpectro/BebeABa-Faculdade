// Call the dataTables jQuery plugin
$(document).ready(function () {
    $("#dataTable").DataTable({
        responsive: true,
        language: {
            url: "/functions/datatable/es-cl.json"
        },
        "destroy": true,
        "scrollX": true
    });
});

$(document).ready(function () {
    $('#dataTableActivity').DataTable({
        "order": [[0, 'desc']],
    });
});
