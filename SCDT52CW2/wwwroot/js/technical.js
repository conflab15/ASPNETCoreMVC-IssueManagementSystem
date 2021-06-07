var dataTableTech;

$(document).ready(function () {
    loadTableDataTech();
});

function loadTableDataTech() {
    dataTableTech = $('#technical-data').DataTable({
        'ajax': {
            "url": '/Issues/Issue/GetTechnicalIssues'
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
                            <a href="/Issues/Issue/Details/${data}" class="btn btn-success text-white">View Technical Issue</a>
                         </div>
                        `;
                },
                'width': '30%'
            }
        ]
    });
}