let productCategoryData;

document.addEventListener("DOMContentLoaded", () => {
    const donutChart = document.querySelector("#donutChart");

    const chartOptions = {
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
        new ApexCharts(donutChart, chartOptions).render();
    }
});
