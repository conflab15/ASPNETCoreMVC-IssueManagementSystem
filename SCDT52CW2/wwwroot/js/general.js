var dataTableGen;

$(document).ready(function () {
    loadTableDataGen();
});

//Much like the asset.js file, this produces a DataTable for the General Issues found within the database which gets returned by an API call from the controller...

function loadTableDataGen() {
    dataTableGen = $('#gen-data').DataTable({
        'ajax': {
            "url": '/Issues/Issue/GetGeneralIssues'
        },
        'columns': [
            { 'data': 'id', 'width': '20%' },
            { 'data': 'desc', 'width': '30%' },
            { 'data': 'date', 'width': '20%' },
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