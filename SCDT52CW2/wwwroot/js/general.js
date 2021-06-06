var dataTableGen;

$(document).ready(function () {
    loadTableDataGen();
});

function loadTableDataGen() {
    dataTableGen = $('#gen-data').DataTable({
        'ajax': {
            "url": '/Issues/Issue/GetGeneralIssues'
        },
        'columns': [
            { 'data': 'id', 'width': '20%' },
            { 'data': 'desc', 'width': '30%' },
            { 'data': 'Date', 'width': '20%' },
            {
                'data': 'id',
                'render': function (data) {
                    return `
                        <div class="text-center"> 
                            <a href="/Issues/Issue/Details/${data}" class="btn btn-success text-white">View Issue</a>
                         </div>
                        `;
                },
                'width': '30%'
            }
        ]
    });
}