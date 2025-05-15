import { Component, Input, ViewChild, ElementRef, AfterViewInit, OnChanges } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Chart, registerables } from 'chart.js';
import 'chartjs-adapter-date-fns';
import { MatCardModule } from '@angular/material/card';

Chart.register(...registerables);

@Component({
  selector: 'app-scatter-chart',
  standalone: true,
  imports: [MatCardModule, CommonModule],
  templateUrl: './scatter-chart.component.html',
  styleUrl: './scatter-chart.component.scss'
})
export class ScatterChartComponent implements AfterViewInit, OnChanges {
  @Input() chartData: any[] = [];
  @ViewChild('chartCanvas', { static: false }) chartCanvas!: ElementRef<HTMLCanvasElement>;

  private chart!: Chart;

  ngAfterViewInit(): void {
    this.createChart();
  }

  ngOnChanges(): void {
    if (this.chart) {
      this.updateChart();
    }
  }

  private createChart(): void {
    const context = this.chartCanvas.nativeElement.getContext('2d');

    if (!context) return;

    this.chart = new Chart(context, {
      type: 'scatter',
      data: {
        datasets: [{
          label: 'Search Rank Over Time',
          data: this.toScatterData(),
          pointRadius: 5,
          backgroundColor: 'blue',
        }]
      },
      options: {
        responsive: true,
        plugins: {
          title: {
            display: false,
            text: 'Search Rank Over Time'
          },
          tooltip: {
            callbacks: {
              label: (ctx) => {
                const x = new Date(ctx.parsed.x).toDateString();
                const y = ctx.parsed.y;
                const raw = ctx.raw as any;
                return `Date: ${x}, Rank: ${y}, Term: ${raw.term}`;
              }
            }
          }
        },
        scales: {
          x: {
            type: 'time',
            title: {
              display: true,
              text: 'Date'
            },
            time: {
              unit: 'day'
            }
          },
          y: {
            reverse: false, 
            title: {
              display: true,
              text: 'Rank'
            },
            beginAtZero: true
          }
        }
      }
    });
  }

  private updateChart(): void {
    this.chart.data.datasets[0].data = this.toScatterData();
    let compDate = getTimestampDaysAgo(7);
    let minDataDate = Math.min(...this.chartData.map(d => new Date(d.entryDate).getTime()));

    (this.chart.options.scales!['x'] as any).min = Math.min(minDataDate, compDate);
    this.chart.update();
  }

  private toScatterData() {
    return this.chartData.map(entry => ({
      x: new Date(entry.entryDate).getTime(),
      y: entry.rank,
      term: entry.searchTerm
    }));
  }

}

function getTimestampDaysAgo(days: number): number {
  const date = new Date();
  date.setDate(date.getDate() - days);
  return date.getTime();
}