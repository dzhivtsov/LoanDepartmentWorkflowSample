function refreshKendoGrid(gridSelector) {
    var gridData = $(gridSelector).data("kendoGrid");
    if (gridData) {
        gridData.dataSource.read();
        gridData.refresh();
    }
}

function submitButtonClickHandler(e) {
    $(e.event.target).closest("form").submit();
}