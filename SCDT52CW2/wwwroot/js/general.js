var dataTableGen;

$(document).ready(function () {
    loadTableDataGen();
});

function loadTableDataGen() {
    dataTableGen = $('#gen-data').DataTable({
        'ajax': {
            "url": '/Issue/Issue/GetGeneralIssues'
        },
        'columns': [
            { 'data': 'iD', 'width': '20%' },
            { 'data': 'desc', 'width': '30%' },
            { 'data': 'Date', 'width': '20%' },
            {
                'data': 'id',
                'render': function (data) {
                    return `
                        <div class="text-center"> 
                            <a href="" class="btn btn-success text-white">View Issue</a>
                         </div>
                        `;
                },
                'width': '30%'
            }
        ]
    });
}