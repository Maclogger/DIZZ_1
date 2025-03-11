let charts = {};

export function generateLabels(length) {
    const weekdays = ["PON", "UTO", "STR", "ŠTV", "PIA", "SOB", "NED"];
    const labels = [];
    for (let i = 1; i <= length; i++) {
        labels.push(`${i}: ${weekdays[(i - 1) % weekdays.length]}`);
    }
    return labels;
}

export function initializeChart(canvasId, config, dataList) {
    const ctx = document.getElementById(canvasId).getContext("2d");
    console.log(dataList);

    console.log(config);

    const data = {
        labels: generateLabels(dataList.length),
        datasets: [{
            label: config.title,
            data: dataList
        }]
    }

    charts[canvasId] = new Chart(ctx, {
        type: "bar",
        data: data,
        options: {
            scales: {
                y: {
                    beginAtZero: true,
                },
            },
        },
    });
}
