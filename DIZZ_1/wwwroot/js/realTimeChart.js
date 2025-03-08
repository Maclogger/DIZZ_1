let charts = {};

export function initializeChart(canvasId, config) {
  const ctx = document.getElementById(canvasId).getContext("2d");
  charts[canvasId] = new Chart(ctx, {
    type: "line",
    data: {
      datasets: [
        {
          label: config.Title,
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
          type: "linear", // alebo "time", ak používate časové hodnoty
          title: {
            display: true,
            text: config.XAxisLabel,
          },
        },
        y: {
          title: {
            display: true,
            text: config.YAxisLabel,
          },
        },
      },
      animation: {
        duration: 0,
      },
      plugins: {
        decimation: {
          enabled: true,
          algorithm: "min-max", // skúste aj "min-max"
          samples: 50, // nastavte počet vzoriek podľa vášho prípadu použitia
          threshold: 10, // spustí sa decimation, keď je viac ako 1000 bodov vo viditeľnej časti
        },
      },
    },
  });
}

export function addDataPoint(canvasId, value) {
  if (!(canvasId in charts)) return;
  const chart = charts[canvasId];
  const newIndex = chart.data.datasets[0].data.length + 1;
  chart.data.datasets[0].data.push({ x: newIndex, y: value });
  chart.update("none");
}
