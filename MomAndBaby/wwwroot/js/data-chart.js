let productCategoryData;
var totalYearData = [300, 2350, 4800, 6800, 2950, 1950, 1800, 2800, 3750, 3750, 9700, 4700];
var thisYearData = [3350, 3350, 4800, 4800, 2950, 2950, 1800, 1800, 3750, 3750, 5700, 5700];
var lastYearData = [3200, 3100, 4500, 4600, 2800, 3000, 1900, 1700, 3600, 3700, 5500, 5300];

let apexChartInstance = null;

document.addEventListener("DOMContentLoaded", () => {
    const donutChart = document.querySelector("#donutChart");

    const donutChartOpts = {
        chart: { height: 390, type: "donut" },
        labels: productCategoryData.item1,
        series: productCategoryData.item2,
        colors: ["#fee802", "#3fd0bd", "#826bf8", "#2b9bf4", "#02fe23"],
        stroke: { show: false, curve: "straight" },
        dataLabels: {
            enabled: true,
            formatter: function (value) {
                return parseInt(value, 10) + "%";
            },
        },
        legend: {
            show: true,
            position: "bottom",
            markers: { offsetX: -3 },
            itemMargin: { vertical: 3, horizontal: 10 },
            labels: { colors: "black", useSeriesColors: false },
        },
        plotOptions: {
            pie: {
                donut: {
                    labels: {
                        show: true,
                        name: { fontSize: "2rem", fontFamily: "Public Sans" },
                        value: {
                            fontSize: "1.2rem",
                            color: "black",
                            fontFamily: "Public Sans",
                            formatter: function (value) {
                                return parseInt(value, 10) + "%";
                            },
                        },
                        total: {
                            show: true,
                            fontSize: "1.5rem",
                            color: "black",
                            label: "Operational",
                            formatter: function () {
                                return "42%";
                            },
                        },
                    },
                },
            },
        },
        responsive: [
            {
                breakpoint: 992,
                options: {
                    chart: { height: 380 },
                    legend: {
                        position: "bottom",
                        labels: { colors: "black", useSeriesColors: false }
                    },
                },
            },
            {
                breakpoint: 576,
                options: {
                    chart: { height: 320 },
                    plotOptions: {
                        pie: {
                            donut: {
                                labels: {
                                    show: true,
                                    name: { fontSize: "1.5rem" },
                                    value: { fontSize: "1rem" },
                                    total: { fontSize: "1.5rem" },
                                },
                            },
                        },
                    },
                    legend: {
                        position: "bottom",
                        labels: { colors: "black", useSeriesColors: false },
                    },
                },
            },
            {
                breakpoint: 420,
                options: { chart: { height: 280 }, legend: { show: false } },
            },
            {
                breakpoint: 360,
                options: { chart: { height: 250 }, legend: { show: false } },
            },
        ],
    };

    if (donutChart !== null) {
        new ApexCharts(donutChart, donutChartOpts).render();
    }

    const totalIncomeChart = document.querySelector("#totalIncomeCharts");

    const totalIncomeChartOpts = {
        chart: {
            height: 300,
            type: "area",
            toolbar: !1,
            dropShadow: {
                enabled: !0,
                top: 14,
                left: 2,
                blur: 3,
                color: '#7367F0',
                opacity: 0.15,
            },
        },
        series: [
            {
                data: totalYearData,
            },
        ],
        dataLabels: { enabled: false },
        stroke: { width: 3, curve: "straight" },
        colors: ['#7367F0'],
        fill: {
            type: "gradient",
            gradient: {
                shade: true,
                shadeIntensity: 0.8,
                opacityFrom: 0.7,
                opacityTo: 0.25,
                stops: [0, 95, 100],
            },
        },
        grid: {
            show: !0,
            borderColor: '#f0f0f0',
            padding: { top: -15, bottom: -10, left: 0, right: 0 },
        },
        xaxis: {
            categories: [
                "Jan",
                "Feb",
                "Mar",
                "Apr",
                "May",
                "Jun",
                "Jul",
                "Aug",
                "Sep",
                "Oct",
                "Nov",
                "Dec",
            ],
            labels: { offsetX: 0, style: { colors: "#82868b", fontSize: "13px" } },
            axisBorder: { show: !1 },
            axisTicks: { show: !1 },
            lines: { show: !1 },
        },
        yaxis: {
            labels: {
                offsetX: -15,
                formatter: function (o) {
                    return "$" + parseInt(o / Math.min(...totalYearData)) + "k";
                },
                style: { fontSize: "13px", colors: "#82868b" },
            },
            min: Math.min(...totalYearData),
            max: Math.max(...totalYearData),
            tickAmount: 7,
        },
    };

    if (totalIncomeChart !== null) {
        apexChartInstance = new ApexCharts(totalIncomeChart, totalIncomeChartOpts);
        apexChartInstance.render();
    }

    
});

// Update chart function
window.updateChart = function (period) {
    if (period === 'thisyear') {
        newData = thisYearData;
        document.getElementsByClassName('title-chart')[0].innerHTML = 'This Year';
    } else if (period === 'lastyear') {
        newData = lastYearData;
        document.getElementsByClassName('title-chart')[0].innerHTML = 'Last Year';
    } else {
        newData = totalYearData;
        document.getElementsByClassName('title-chart')[0].innerHTML = 'Total Year';
    }

    const min = Math.min(...newData);
    const max = Math.max(...newData);

    apexChartInstance.updateOptions({
        yaxis: {
            min: min,
            max: max,
            tickAmount: 7,
        }
    });

    apexChartInstance.updateSeries([{ data: newData }]);

}

const updateChartIncome = (listData, year) => {
    document.getElementsByClassName('title-chart')[0].innerHTML = `Year: ${year}`;
    const min = Math.min(...listData);
    const max = Math.max(...listData);
    apexChartInstance.updateOptions({
        yaxis: {
            min: min,
            max: max,
            tickAmount: 7,
        }
    });

    apexChartInstance.updateSeries([{ data: listData }]);
}

