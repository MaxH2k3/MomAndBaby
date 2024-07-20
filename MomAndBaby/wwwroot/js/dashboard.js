const getTotalOrderByYear = (year) => {
    $.ajax({
        url: `/api/v1/dashboard/orders/${year}`,
        type: "GET",
        contentType: "application/json",
        dataType: "json",
        success: function (data) {
            console.log(data);
            updateChartIncome(data, year);
        },
        error: function (xhr, status, error) {
            console.error('An error occurred:', error);
        }
    })
}