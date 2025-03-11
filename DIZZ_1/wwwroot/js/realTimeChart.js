let charts = {};

export function initializeChart(canvasId, config) {
    const ctx = document.getElementById(canvasId).getContext("2d");
    charts[canvasId] = new Chart(ctx, {
        type: "line",
        data: {
            datasets: [
                {
                    label: config.title,
                    data: [],
                    borderColor: "rgba(75, 192, 192, 1)",
                    backgroundColor: "rgba(75, 192, 192, 0.2)",
                    fill: true,
                    tension: 0.1,
                    parsing: false,
                },
            ],
        },
        options: {
            responsive: false,
            scales: {
                x: {
                    type: "linear",
                    title: {
                        display: true,
                        text: config.xAxisLabel,
                    },
                },
                y: {
                    title: {
                        display: true,
                        text: config.yAxisLabel,
                    },
                },
            },
            /*
                  animation: {
                    duration: 0,
                  },
            */
            /*  plugins: {
                title: {
                  display: true,
                  text: config.title,
                }
              },*/
        },
    });
}

let pendingUpdate = false;

export async function addDataPoint(canvasId, value) {
    try {
        return new Promise((resolve, reject) => {
            if (!(canvasId in charts)) return;
            const chart = charts[canvasId];
            const newIndex = chart.data.datasets[0].data.length;
            chart.data.datasets[0].data.push({x: newIndex, y: value});

            if (!pendingUpdate) {
                pendingUpdate = true;
                requestAnimationFrame(() => {
                    chart.update("none");
                    pendingUpdate = false;
                });
            }
        })
    } catch (error) {}
}

export function reset(canvasId) {
    if (!(canvasId in charts)) return;
    const chart = charts[canvasId];
    chart.data.datasets[0].data = [];
    chart.update("none");
}
