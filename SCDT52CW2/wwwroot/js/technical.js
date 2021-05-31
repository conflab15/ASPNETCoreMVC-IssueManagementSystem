var dataTableTech;

$(document).ready(function () {
    loadTableDataTech();
});

function loadTableDataTech() {
    dataTableTech = $('#technical-data').DataTable({
        'ajax': {
            "url": '/Issue/Issue/GetTechnicalIssues'
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
                            <a href="" class="btn btn-success text-white">View Technical Issue</a>
                         </div>
                        `;
                },
                'width': '30%'
            }
        ]
    });
}