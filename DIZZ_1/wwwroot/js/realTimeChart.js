let charts = {};

export function initializeChart(canvasId, config) {
  console.log(config);
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
      animation: {
        duration: 0,
      },
    /*  plugins: {
        title: {
          display: true,
          text: config.title,
        }
      },*/
    },
  });
}

export function addDataPoint(canvasId, value) {
  if (!(canvasId in charts)) return;
  const chart = charts[canvasId];
  const newIndex = chart.data.datasets[0].data.length;
  chart.data.datasets[0].data.push({ x: newIndex, y: value });
  chart.update("none");
}

export function reset(canvasId) {
  if (!(canvasId in charts)) return;
  const chart = charts[canvasId];
  chart.data.datasets[0].data = [];
  chart.update("none");
}
